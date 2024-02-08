using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class SeedData
    {
        public List<AppUser> Users { get; set; }
        public List<Party> Parties { get; set; }
        public List<MPSeat> MPSeats { get; set; }
    }
}