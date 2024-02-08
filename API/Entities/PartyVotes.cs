using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class PartyVotes
    {
        [Key]
        public int Id { get; set; }
        public int PartyId { get; set; }
        public Party Party { get; set; } 
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } 
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}