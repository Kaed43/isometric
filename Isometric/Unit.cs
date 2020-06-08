using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric
{
    class Unit
    {
        UnitType type;
        Vector2 position;
        int player;
        int moves;
        int HP;
        bool inCombat;

        public Unit(UnitType type, Vector2 position, int player)
        {
            this.type = type;
            this.position = position;
            this.player = player;
            moves = type.getMaxMoves();
            HP = type.getMaxHP();
            inCombat = false;
        }
        public UnitType getUnitType()
        {
            return type;
        }
        public Vector2 getPosition()
        {
            return position;
        }
        public void setPosition(Vector2 x)
        {
            this.position = x;
        }
        public int getMoves()
        {
            return moves;
        }
        public void setMoves(int num)
        {
            this.moves = num;
        }
    }
}
