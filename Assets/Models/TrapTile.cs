using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TrapTile : Tile
    {
        public bool IsVulnerable = false;

        public override bool IsPassable()
        {
            return true;
        }
    }
}
