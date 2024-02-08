using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class CastVoteDto
    {
        public int PartyId { get; set; }
        public int CandidateId { get; set; }
    }
}