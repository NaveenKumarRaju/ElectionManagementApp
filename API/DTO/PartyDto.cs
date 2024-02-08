using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class PartyDto
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int SymbolId { get; set; }
    }
}