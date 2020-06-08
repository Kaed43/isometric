using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isometric
{
    class TileType
    {
        string name;
        string displayName;
        string desc;
        Texture2D sprite;
        int maxHP;
        string deathTile;
        int losCost;
        float defMultiplier;
        float atkMultiplier;
        int losBonus;

        int footCost;
        int tireCost;
        int treadCost;
        int crawlerCost;
        int amphCost;
        int hoverCost;
        int structureCost;
        int airCost;
        int svCost;
        int lvCost;
        int landerCost;
        int subCost;
        public TileType(string name, string displayName, string desc, Texture2D sprite, int maxHP, string deathTile, int losCost, float defMultiplier, float atkMultiplier, int losBonus, int footCost, int tireCost, int treadCost, int crawlerCost, int amphCost, int hoverCost, int structureCost, int airCost, int svCost, int lvCost, int landerCost, int subCost)
        {
            this.name = name;
            this.displayName = displayName;
            this.desc = desc;
            this.sprite = sprite;
            this.maxHP = maxHP;
            this.deathTile = deathTile;
            this.losCost = losCost;
            this.defMultiplier = defMultiplier;
            this.atkMultiplier = atkMultiplier;
            this.losBonus = losBonus;
            this.footCost = footCost;
            this.tireCost = tireCost;
            this.treadCost = treadCost;
            this.crawlerCost = crawlerCost;
            this.amphCost = amphCost;
            this.structureCost = structureCost;
            this.airCost = airCost;
            this.svCost = svCost;
            this.lvCost = lvCost;
            this.landerCost = landerCost;
            this.subCost = subCost;
        }

        // Boy... all these getter functions...
        // I gotta show you how auto-properties work
        public int getMaxHP()
        {
            return maxHP;
        }
        public Texture2D getSprite()
        {
            return sprite;
        }
        public int getMoveCostFromTypeString(string type)
        {
            if (type == "foot")
            {
                return footCost;
            }
            else if (type == "tire")
            {
                return tireCost;
            }
            else if (type == "tread")
            {
                return treadCost;
            }
            else if (type == "crawler")
            {
                return crawlerCost;
            }
            else if (type == "amph")
            {
                return amphCost;
            }
            else if (type == "structure")
            {
                return structureCost;
            }
            else if (type == "sv")
            {
                return svCost;
            }
            else if (type == "lv")
            {
                return lvCost;
            }
            else if (type == "lander")
            {
                return landerCost;
            }
            else if (type == "sub")
            {
                return subCost;
            }
            else
            {
                return airCost;
            }
        }
    }
}
