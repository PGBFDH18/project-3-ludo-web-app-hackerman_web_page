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
        [RegularExpression(@"^[a-zA-ZäöüßÄÖÜ]+$", ErrorMessage ="Name cant contain any numbers")]
        [Required(ErrorMessage ="Please enter a name.")]
        [StringLength(15)]
        public string Name { get; set; }
        public Piece[] Piece { get; set; }

        public int StartPosition { get; set; }

    }
}
