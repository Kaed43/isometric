using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric
{
    class UnitType
    {
        string name;
        string faction;
        string desc;
        string tooltip;
        int tier;
        int mCost;
        int rCost;
        int xCost;
        int eCost;
        int totalCost;
        int maxHP;
        string defType;
        Weapon[] weapons;
        int maxMoves;
        string moveType;
        int los;
        Texture2D sprite;

        public UnitType(string name, string faction, string desc, string tooltip, int tier, int mCost, int rCost, int xCost, int eCost, int maxHP, string defType, Weapon[] weapons, int maxMoves, string moveType, int los, Texture2D sprite)
        {
            this.name = name;
            this.faction = faction;
            this.desc = desc;
            this.tooltip = tooltip;
            this.tier = tier;
            this.mCost = mCost;
            this.rCost = rCost;
            this.xCost = xCost;
            this.eCost = eCost;
            totalCost = mCost + (9 * rCost) + (36 * xCost);
            this.maxHP = maxHP;
            this.defType = defType;
            this.weapons = weapons;
            this.maxMoves = maxMoves;
            this.moveType = moveType;
            this.los = los;
            this.sprite = sprite;
        }
        public int getMaxMoves()
        {
            return maxMoves;
        }
        public int getMaxHP()
        {
            return maxHP;
        }
        public Texture2D getSprite()
        {
            return sprite;
        }
        public string getMovType()
        {
            return moveType;
        }
    }
}
