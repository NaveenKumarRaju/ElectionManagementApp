using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class CandidateDTO
    {
        public int Id { get; set; }
        [Required] public string VoterId { get; set; }   
        [Required] public int PartyId { get; set; }      
        [Required] public string State { get; set; }   
        public string UserName { get; set; }        
        public string PartySymbol { get; set; }
        public int AppUserId { get; set; }
    }
}