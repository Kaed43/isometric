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
        public string name { get; }
        string displayName;
        string desc;
        Texture2D sprite;
        int maxHP;
        string deathTile;
        int losCost;
        float defMultiplier;
        float atkMultiplier;
        int losBonus;

        Dictionary<EMoveType, int> MoveCosts;

        public TileType(string[] fileData)
        {
            name = fileData[0];
            displayName = fileData[1];
            desc = fileData[2];
            sprite = ContentLoader.AllTextures[fileData[3]];
            maxHP = int.Parse(fileData[4]);
            deathTile = fileData[5];
            losCost = int.Parse(fileData[6]);
            defMultiplier = float.Parse(fileData[7]);
            atkMultiplier = float.Parse(fileData[8]);
            losBonus = int.Parse(fileData[9]);
            MoveCosts = new Dictionary<EMoveType, int>();

            // Fuck. Don't worry about that line, it sucks.
            foreach(EMoveType moveType in Enum.GetValues(typeof(EMoveType)))
            {
                // Y'know what, don't worry about this one either.
                MoveCosts.Add(moveType, int.Parse(fileData[10 + (int)moveType]));
            }
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
        public int getMoveCostFromTypeString(EMoveType type)
        {
            return MoveCosts[type];
        }
    }
}
