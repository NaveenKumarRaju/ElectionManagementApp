using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IElectionManagementRepository
    {
        Task<List<MPSeat>> GetMPSeats(); 
        Task<MPSeat> GetMPSeat(int id); 
        void Update(MPSeat seat);
        Task RegisterParty(Party party);
        Task<List<Party>> GetParties();  
        Task RegisterCandidate(Candidate candidate);
        Task<Candidate> GetCandidate(int userId);
        Task<List<CandidateDTO>> GetCandidates();
        Task CastVote(PartyVotes votes);
        Task<List<ElectionResultDTO>> GetElectionResults();
        Task<List<AppUser>> GetVotersList();
        Task<AppUser> GetVoter(int id);
    }

}