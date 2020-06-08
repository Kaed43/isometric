using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric
{
    class Weapon
    {
        int strength;
        string atkType;
        int maxSupply;
        int range;
        int minRange;
        //int[] targetLayers;
        public Weapon(int s, string t, int ms, int r, int mr)
        {
            this.strength = s;
            this.atkType = t;
            this.maxSupply = ms;
            this.range = r;
            this.minRange = mr;
            //this.targetLayers = tgtl;
        }
    }
}
