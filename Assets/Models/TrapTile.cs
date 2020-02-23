using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TrapTile : Tile
    {
        public bool Vulnerable = false;

        public bool IsPassable()
        {
            if(Vulnerable)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
