using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ElectionManagementRepository : IElectionManagementRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ElectionManagementRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CastVote(PartyVotes votes)
        {
           await _context.PartyVotes.AddAsync(votes);
        }

        public async Task<Candidate> GetCandidate(int userId)
        {
            return await _context.Candidates.FirstOrDefaultAsync(x => x.AppUserId == userId);
        }

        public async Task<List<CandidateDTO>> GetCandidates()
        {
            var candidates = from c in _context.Candidates
                    join p in _context.Parties on c.PartyId equals p.Id
                    join s in _context.Symbols on p.SymbolId equals s.Id
                    join u in _context.Users on c.AppUserId equals u.Id
                    select new CandidateDTO
                    {
                        Id = c.Id,
                        UserName = u.UserName,
                        PartySymbol = s.Name,
                        VoterId = u.VoterId,
                        AppUserId = u.Id,
                        PartyId = p.Id,
                        State = c.State
                    };

            return await candidates.ToListAsync();
        }

        public async Task<List<ElectionResultDTO>> GetElectionResults()
        {
            var query =  from p in _context.Parties
                    join pv in _context.PartyVotes on p.Id equals pv.PartyId into partyVotes                     
                    select  new ElectionResultDTO
                    {
                        PartyId = p.Id,
                        PartyName = p.Name,
                        VoteCount = partyVotes.Count(),
                    };  

            var result = await query.ToListAsync();
            var maxCount = result.GroupBy(x => x.PartyId).Max(g => g.Count());

            result.ForEach(x => x.IsMajority = x.VoteCount == maxCount);

            return result;
        }

        public async Task<MPSeat> GetMPSeat(int id)
        {
            return await _context.MPSeats.FindAsync(id);
        }

        public async Task<List<MPSeat>> GetMPSeats()
        {
           return await _context.MPSeats.ToListAsync();
        }

        public async Task<List<Party>> GetParties()       
        {
            return await _context.Parties.ToListAsync();
        }

        public async Task<AppUser> GetVoter(int id)
        {
            return await _context.Users
                    .Where(x => x.Id == id)
                    .SingleOrDefaultAsync();
        }

        public async Task<List<AppUser>> GetVotersList()
        {
            var users = _context.Users.AsQueryable();
            var roles = _context.UserRoles.AsQueryable();

            roles = roles.Where(role => role.RoleId != 2);
            users = roles.Select(role => role.User);  

            return await users.ToListAsync();
        }

        public async Task RegisterCandidate(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
        }

        public async Task RegisterParty(Party party)
        {
            await _context.Parties.AddAsync(party);
        }

        public  void Update(MPSeat seat)
        {
             _context.Update(seat);
        }        
    }
}