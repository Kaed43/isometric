using Microsoft.Xna.Framework.Graphics;
using NCSV;
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

        public TileType(Csv data, int rowHandle)
        {
            name = data.GetValue(rowHandle, "TileName");
            displayName = data.GetValue(rowHandle, "DisplayName");
            desc = data.GetValue(rowHandle, "Description");
            sprite = ContentLoader.AllTextures[data.GetValue(rowHandle, "SpriteName")];
            maxHP = int.Parse(data.GetValue(rowHandle, "MaxHp"));
            deathTile = data.GetValue(rowHandle, "deathTile");
            losCost = int.Parse(data.GetValue(rowHandle, "LosCost"));
            defMultiplier = float.Parse(data.GetValue(rowHandle, "DefMultiplier"));
            atkMultiplier = float.Parse(data.GetValue(rowHandle, "AttackMultiplier"));
            losBonus = int.Parse(data.GetValue(rowHandle, "LosBonus"));
            MoveCosts = new Dictionary<EMoveType, int>();

            // Fuck. Don't worry about that line, it sucks.
            foreach(EMoveType moveType in Enum.GetValues(typeof(EMoveType)))
            {
                var name = Enum.GetName(typeof(EMoveType), moveType);
                MoveCosts.Add(moveType, int.Parse(data.GetValue(rowHandle, name + "Cost")));
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
