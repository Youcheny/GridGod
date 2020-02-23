using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ObstacleTile : Tile
    {
        public bool IsPassable()
        {
            return false;
        }
        
    }
}
