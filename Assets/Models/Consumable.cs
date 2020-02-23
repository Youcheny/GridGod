using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Models
{
    public class Consumable
    {
        public GameObject consumable;
        public string type;

        public Consumable()
        {
            
        }

        virtual public void OnCollect()
        {
            
        }
        public virtual void OnConsume()
        {

        }
    }
}
