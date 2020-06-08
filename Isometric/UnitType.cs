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
        public int maxHP { get; }
        string defType;
        Weapon[] weapons;
        public int maxMoves { get; }
        public EMoveType moveType { get; }
        int los;
        public Texture2D sprite { get; }
        public Texture2D tintSprite { get; }

        public UnitType(string name, string faction, string desc, string tooltip, int tier, int mCost, int rCost, int xCost, int eCost, int maxHP, string defType, Weapon[] weapons, int maxMoves, EMoveType moveType, int los, Texture2D sprite, Texture2D tintSprite)
        {
            // TODO: You could CSV-ify this stuff too but if I embark on that right now I'll die
            // Also maybe just wait until we have a better CSV reader too
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
            this.tintSprite = tintSprite;
        }
    }
}
