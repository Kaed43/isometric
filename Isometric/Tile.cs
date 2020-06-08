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
        int Xposition;
        int Yposition;
        TileType type;

        public Tile(int x,int y,TileType tiletype) {
            Xposition = x;
            Yposition = y;
            this.type = tiletype;
            hp = tiletype.getMaxHP();
        }
        public Vector2 getScreenPosition()
        {
            // I like all these constants just floating around
            int x = Xposition * 64 - Yposition *64;
            int y = Xposition * 32 + Yposition * 32;
            return new Vector2(x,y);
        }
        public TileType getType()
        {
            return type;
        }
    }
}
