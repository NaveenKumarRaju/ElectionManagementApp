using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class VoterDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string VoterId { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; } 
        public bool IsApproved { get; set; }
        public bool HasVoted { get; set; }
    }
}