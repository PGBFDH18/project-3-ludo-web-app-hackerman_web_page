using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Color { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Name { get; set; }
        public Piece[] Piece { get; set; }
    }
}
