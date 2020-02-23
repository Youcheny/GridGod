using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NormalTile : Tile
    {
        public override bool IsPassable()
        {
            return true;
        }
    }
}
