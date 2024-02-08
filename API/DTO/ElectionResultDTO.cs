using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class ElectionResultDTO
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public int VoteCount { get; set; }
        public bool IsMajority { get; set; }
    }
}