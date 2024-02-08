using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Party
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int SymbolId { get; set; }
        public Symbol Symbol { get; set; } 
        public ICollection<PartyVotes> PartyVotes { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
    }
}