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
        public UnitType Type { get; }
        public Point Position { get; set; }
        public int Player { get; }
        public int Moves { get; set; }
        public int HP { get; }
        public bool InCombat { get; }

        public Unit(UnitType type, Point position, int player)
        {
            Type = type;
            Position = position;
            Player = player;
            Moves = type.getMaxMoves();
            HP = type.getMaxHP();
            InCombat = false;
        }
    }
}
