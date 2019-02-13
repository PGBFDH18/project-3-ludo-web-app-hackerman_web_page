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

        public string Name { get; set; }
        public Piece[] Piece { get; set; }

        public int StartPosition { get; set; }

    }
}
