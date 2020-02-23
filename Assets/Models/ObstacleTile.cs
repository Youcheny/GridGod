using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Models
{
    public class ObstacleTile : Tile
    {
        public override bool IsPassable()
        {
            MonoBehaviour.print ("called obstacletile");
            return false;
        }
        
    }
}
