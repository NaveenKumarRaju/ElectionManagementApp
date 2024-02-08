using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterCandidateDTo
    {
        [Required] public string VoterId { get; set; }  
        [Required] public int AppUserId { get; set; }   
        [Required] public int PartyId { get; set; }      
        [Required] public string State { get; set; }  
    }
}