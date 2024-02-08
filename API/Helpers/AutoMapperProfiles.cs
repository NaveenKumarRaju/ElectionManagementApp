using API.DTO;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
             CreateMap<RegisterDto, AppUser>().ReverseMap();
             CreateMap<PartyDto, Party>().ReverseMap();
             CreateMap<CandidateDTO, Candidate>().ReverseMap();
             CreateMap<RegisterCandidateDTo, Candidate>();
             CreateMap<AppUser, VoterDto>().ReverseMap(); 
            CreateMap<CastVoteDto, PartyVotes>().ReverseMap(); 
        }
    }
}