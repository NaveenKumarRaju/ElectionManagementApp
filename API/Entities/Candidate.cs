using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Candidate
    {       
        [Key] 
        public int Id { get; set; } 
        public string State { get; set; } 
        public int PartyId { get; set; } 
        public Party Party { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public PartyVotes PartyVotes { get; set; }
    }
}