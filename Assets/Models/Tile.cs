using System;
using UnityEngine;

namespace Models
{
    public class Tile
    {
        public GameObject tile;
        public string type;

        public Tile()
        {

        }

        virtual public bool IsPassale()
        {
            return true;
        }
    }
}

