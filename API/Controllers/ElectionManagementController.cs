using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class ElectionManagementController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ElectionManagementController(IUnitOfWork uow, IMapper mapper,UserManager<AppUser> userManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        [Authorize("VoterAndAdminRole")]
        [HttpGet("voters")]
        public async Task<ActionResult<List<VoterDto>>> GetVoters()
        {
            var users = await _uow.ElectionManagementRepository.GetVotersList();
            var voters = _mapper.Map<List<VoterDto>>(users);
            
            return Ok(voters);            
        }   
        [Authorize("VoterAndAdminRole")]
        [HttpPost("voters/cast-vote")]
        public async Task<ActionResult<List<UserDto>>> CastVote(CastVoteDto vote)
        {   
            var userId = User.GetUserId();

            var user = await _uow.ElectionManagementRepository.GetVoter(userId);
            if(user is null) return BadRequest("Voter does not exist");            

             if(!user.IsApproved || user.HasVoted) return BadRequest("The Voter is either not approved or already voted or admin");

            var partyVote = _mapper.Map<PartyVotes>(vote);
            partyVote.AppUserId = userId;
            
            await _uow.ElectionManagementRepository.CastVote(partyVote);

            if(await _uow.Complete())
            {                    
                user.HasVoted = true;
                await _userManager.UpdateAsync(user);
                return Ok(); 
            }
           
            return BadRequest("Failed to cast vote");            
        }    
        
        [Authorize(Policy = "RequiredAdminRole")]
        [HttpPost("voters/approve/{id}")]
        public async Task<ActionResult<List<UserDto>>> ApproveVoter(int id)
        {            
            var user = await _uow.ElectionManagementRepository.GetVoter(id);
            if(user is null) return BadRequest("Voter does not exist");

            user.IsApproved = true;
            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded) return Ok();
           
            return BadRequest(result.Errors);            
        }   

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpPost("register-party")]
        public async Task<ActionResult<List<UserDto>>> RegisterParty(PartyDto partyDto)
        {
            var parties = await _uow.ElectionManagementRepository.GetParties();

            if(parties.Count(s => s.SymbolId == partyDto.SymbolId) > 0) return BadRequest("Symbol Already taken");

            var party = _mapper.Map<Party>(partyDto); 

            await _uow.ElectionManagementRepository.RegisterParty(party);
           
            if(await _uow.Complete()) return CreatedAtAction(nameof(GetParties), party);  

            return BadRequest("Error occure in party creation");          
        }    

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpGet("parties")]
        public async Task<ActionResult<List<UserDto>>> GetParties()
        {
           var parties = await _uow.ElectionManagementRepository.GetParties();

           var partyDto = _mapper.Map<List<PartyDto>>(parties);

            return Ok(partyDto);            
        }    

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpPost("register-candidate")]
        public async Task<ActionResult<List<UserDto>>> RegisterCandidate(RegisterCandidateDTo candidateDto)
        {
            if(!await UserExists(candidateDto.VoterId)) return BadRequest("Please register as User and Come back here to register as candidate");
            
            var candidate = _uow.ElectionManagementRepository.GetCandidate(candidateDto.AppUserId);

            if(candidate is not null) return BadRequest("Already registered as a Candidate");

            var newCandidate = _mapper.Map<Candidate>(candidateDto);
             
            await _uow.ElectionManagementRepository.RegisterCandidate(newCandidate);
            
            if(await _uow.Complete()) return CreatedAtAction(nameof(GetCandidates), candidateDto);

            return BadRequest("Error occured while registering the Candidate");             
        }   

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpGet("mp-seats")]
        public async Task<ActionResult<List<UserDto>>> GetMPSeats()
        {             
            var mpSeats = await _uow.ElectionManagementRepository.GetMPSeats();           
            return Ok(mpSeats);             
        }  

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpPut("update/mp-seat")]
        public async Task<ActionResult> UpdateMPSeats(MPSeatDto mpSeatDto)
        {    
            var mpSeat = await _uow.ElectionManagementRepository.GetMPSeat(mpSeatDto.Id);
            
            if(mpSeat is null) return BadRequest("MPSeat not found");

            mpSeat.NumberOfSeats = mpSeatDto.NumberOfSeats;

            _uow.ElectionManagementRepository.Update(mpSeat);

            if(await _uow.Complete()) return NoContent();

            return BadRequest("Failed to update MP Seat");            
        }

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpGet("candidates")]
        public async Task<ActionResult<List<UserDto>>> GetCandidates()
        {             
            var candidates = await _uow.ElectionManagementRepository.GetCandidates();           
            return Ok(candidates);             
        }  

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpGet("result")]
        public async Task<ActionResult<List<ElectionResultDTO>>> Getresult()
        {             
            var result = await _uow.ElectionManagementRepository.GetElectionResults();           
            return Ok(result);             
        }  

        private async Task<bool> UserExists(string voterId)
        {
            return await _userManager.Users.AnyAsync(x => x.VoterId == voterId);
        }
    }
}