using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class AppUser : IdentityUser<int>
{ 
    public string FullName { get; set; }
    public string VoterId { get; set; }
    public string Address { get; set; }
    public byte[] Photo { get; set; } 
    public bool IsApproved { get; set; } = false;
    public bool HasVoted { get; set; } = false;
    public ICollection<AppUserRole> UserRoles { get; set; }
    public Candidate Candidate { get; set; }
    public PartyVotes PartyVotes { get; set; }
}