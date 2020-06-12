using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Isometric
{
    class Tile
    {
        int hp;
        public int Xposition { get; }
        public int Yposition { get; }
        int remainingValue;
        public bool isInLos { get; }
        public TileType type { get; }

        public Tile(int x,int y,TileType tiletype) {
            Xposition = x;
            Yposition = y;
            this.type = tiletype;
            hp = tiletype.maxHP;
            isInLos = false;
            remainingValue = tiletype.reclaimValue;
        }
    }
}
