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
        public Rectangle getScreenPosition()
        {
            // I like all these constants just floating around
            int x = Xposition * Constants.HalfTileWidth - Yposition * Constants.HalfTileWidth;
            int y = Xposition * Constants.HalfTileHeight + Yposition * Constants.HalfTileHeight;
            return new Rectangle(x, y, Constants.TileWidth, Constants.TileHeight);
        }
        public TileType getType()
        {
            return type;
        }
    }
}
