﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NCSV;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Isometric
{
    public class Game1 : Game
    {
        readonly NKeyboard NKeys;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int worldWidth;
        int worldHeight;
        Dictionary<string, TileType> allTileTypes;
        Tile[,] world;
        UnitType aggressorUT;
        UnitType ueFactoryUT;
        List<Unit> units;
        Vector2 cameraOffset;
        Point selectedTile;
        Point lockedTile = new Point(-1,-1);
        int selectedUnitIndex = -1;
        int turn;
        int currentPlayer;

        readonly Dictionary<Keys, Point> MovementDirections;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            NKeys = new NKeyboard();

            MovementDirections = new Dictionary<Keys, Point>()
            {
                { Keys.Down, new Point(0, 1) },
                { Keys.Up, new Point(0, -1) },
                { Keys.Left, new Point(-1, 0) },
                { Keys.Right, new Point(1, 0) },
            };
        }

        protected override void Initialize()
        {
            worldWidth = 16;
            worldHeight = 16;
            cameraOffset = new Vector2(0, 0);
            world = new Tile[worldWidth, worldHeight];
            units = new List<Unit>();
            turn = 1;
            currentPlayer = 1;
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.Initialize(Content);
            //HACK: Make my image loading use your magic
            Texture2D tempAggressorTint = Content.Load<Texture2D>("UDT_Aggressor_tint");
            Texture2D UDT_Factory = Content.Load<Texture2D>("UDT_Factory");
            Texture2D UDT_Factory_tint = Content.Load<Texture2D>("UDT_Factory_tint");
            aggressorUT = new UnitType("Aggressor", "UE", "The “Aggressor” light tank sacrifices firepower for superior armor, granting it the ‘medium’ armor classification- though it is still without most traits of a main battle tank. While this unit’s rapid fire main cannon is effective against soft targets, it lacks armor penetration and is at a disadvantage against equally armored foes.", "Light Tank", 1, 2200, 0, 0, 338, 200, "Medium", new Weapon[] { new Weapon(30, "Pierce", 8, 2, 0) }, 8, EMoveType.Tread, 8, ContentLoader.UDT_Aggressor, tempAggressorTint, 1, 1);
            ueFactoryUT = new UnitType("Factory", "UE", "A sprawling factory complex which completes all necessary operations to process raw metal into any form. Used to produce land vehicles and equip infantry teams. Factories can be assisted by engineering units to complete projects more quickly.", "Factory", 1, 8800, 0, 0, 450, 350,"Structure",new Weapon[0],0,EMoveType.Structure,8,UDT_Factory,UDT_Factory_tint,3,3);


            allTileTypes = new Dictionary<string, TileType>();

            using (var reader = new StreamReader(new FileStream("Content\\TileInfo.csv", FileMode.Open)))
            {
                Csv parsedFile = new Csv(reader);
                for(var rowHandle = 0; rowHandle < parsedFile.Data.Count; rowHandle ++)
                {
                    var tileType = new TileType(parsedFile, rowHandle);
                    allTileTypes.Add(tileType.name, tileType);
                }
            }


            for (int i = 0; i < worldHeight; i++)
            {
                for (int p = 0; p < worldWidth; p++)
                {
                    world[p, i] = new Tile(p, i, allTileTypes["plains"]);
                }
            }
            units.Add(new Unit(ueFactoryUT, new Point(0, 0), 1));
            units.Add(new Unit(ueFactoryUT, new Point(13, 13), 2));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            NKeys.Update();
            if (NKeys.IsKeyPressed(Keys.Tab))
            {
                if (currentPlayer == 1)
                {
                    currentPlayer = 2;
                }
                else
                {
                    currentPlayer = 1;
                }
            }
            if (NKeys.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            MouseState state = Mouse.GetState();
            if (NKeys.IsKeyDown(Keys.W))
            {
                cameraOffset.Y+=5;
            }
            if (NKeys.IsKeyDown(Keys.S))
            {
                cameraOffset.Y -= 5;
            }
            if (NKeys.IsKeyDown(Keys.A))
            {
                cameraOffset.X += 5;
            }
            if (NKeys.IsKeyDown(Keys.D))
            {
                cameraOffset.X -= 5;
            }

            if ( selectedUnitIndex == -1)
            {
                if (NKeys.IsKeyPressed(Keys.Space))
                {
                    turn++;
                    foreach (Unit unit in units)
                    {
                        unit.Moves = unit.Type.maxMoves;
                    }
                }
                if (NKeys.IsKeyPressed(Keys.Enter))
                {
                    bool clear = true;
                    for(var x = 0; x < units.Count; x++)
                    {
                        if(units[x].Position == selectedTile)
                        {
                            if (units[x].Player == currentPlayer)
                            {
                                selectedUnitIndex = x;
                                lockedTile = selectedTile;
                            }
                            clear = false;
                            break;
                        }
                    }
                    if (clear == true)
                    {
                        units.Add(new Unit(aggressorUT, selectedTile, currentPlayer));
                        selectedUnitIndex = -1;
                        lockedTile.X = -1;
                        lockedTile.Y = -1;
                    }
                }

                foreach(var kvp in MovementDirections)
                {
                    if (NKeys.IsKeyPressed(kvp.Key))
                        selectedTile += kvp.Value;
                }

                if (state.LeftButton == ButtonState.Pressed)
                {
                    selectedTile = pixelToWorld(new Vector2(state.X, state.Y), cameraOffset);
                }
            }
            else
            {
                var movType = units[selectedUnitIndex].Type.moveType;

                foreach(var kvp in MovementDirections)
                {
                    if (NKeys.IsKeyPressed(kvp.Key))
                    {
                        var selectedUnit = units[selectedUnitIndex];
                        var tgtCoords = selectedTile + kvp.Value;
                        bool clear = true;
                        for (var x = 0; x < units.Count; x++)
                        {
                            if (units[x].Position == tgtCoords)
                            {
                                clear = false;
                                break;
                            }
                        }
                        if (tgtCoords.X < 0 || tgtCoords.Y < 0 || tgtCoords.X >= worldWidth || tgtCoords.Y >= worldHeight)
                            continue;
                        var targetTile = world[selectedTile.X + kvp.Value.X, selectedTile.Y + kvp.Value.Y];
                        if(targetTile.type.getMoveCostFromTypeString(movType) <= selectedUnit.Moves && clear == true)
                        {
                            selectedUnit.Moves = selectedUnit.Moves - targetTile.type.getMoveCostFromTypeString(movType);
                            selectedUnit.Position = selectedUnit.Position + kvp.Value;
                            selectedTile += kvp.Value;
                        }
                    }
                }
                
                if (NKeys.IsKeyPressed(Keys.Enter))
                {
                    selectedUnitIndex = -1;
                    lockedTile.X = -1;
                    lockedTile.Y = -1;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(cameraOffset.X,cameraOffset.Y,0), blendState: BlendState.AlphaBlend);
            for (int i = 0; i < worldHeight; i++)
            {
                for (int p = 0; p < worldWidth; p++)
                {
                    spriteBatch.Draw(world[p,i].type.sprite, worldToScreenBounds(new Point(world[p,i].Xposition,world[p,i].Yposition),world[p,i].type.pxHeight), Color.White);
                }
            }
            spriteBatch.Draw(ContentLoader.selector, worldToScreenBounds(selectedTile, Constants.TileHeight), Color.CornflowerBlue);
            if ( lockedTile.X!=-1 && lockedTile.Y != -1)
            {
                spriteBatch.Draw(ContentLoader.selector, worldToScreenBounds(lockedTile, Constants.TileHeight), Color.Red);
            }
            foreach (Unit unit in units)
            {
                //HACK: Make tinting not look like shit?
                if (unit.Player == 2)
                {
                    spriteBatch.Draw(unit.Type.sprite, worldToScreenBounds(unit.Position, Constants.TileHeight), unit.Moves == 0 ? Color.Gray : Color.White);
                    spriteBatch.Draw(unit.Type.tintSprite, worldToScreenBounds(unit.Position, Constants.TileHeight), unit.Moves == 0 ? Color.DarkRed : Color.Red);
                }
                else
                {
                    spriteBatch.Draw(unit.Type.sprite, worldToScreenBounds(unit.Position, Constants.TileHeight), unit.Moves == 0 ? Color.Gray : Color.White);
                    spriteBatch.Draw(unit.Type.tintSprite, worldToScreenBounds(unit.Position,Constants.TileHeight), unit.Moves == 0 ? Color.DarkBlue : Color.Blue);
                }
            }
            if (selectedUnitIndex != -1)
            {
                spriteBatch.DrawString(ContentLoader.Arial, "Remaining Movement: "+units[selectedUnitIndex].Moves.ToString(), new Vector2(10, 30)-cameraOffset, Color.Red);
            }
            spriteBatch.DrawString(ContentLoader.Arial, "Turn " + turn +", Player "+currentPlayer, new Vector2(10, 10) - cameraOffset, Color.Red);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        public Point pixelToWorld(Vector2 pixel, Vector2 cameraOffset)
        {
            int mouseX = (int)pixel.X - (int)cameraOffset.X;
            int mouseY = (int)pixel.Y - (int)cameraOffset.Y;
            int posX = (mouseX / Constants.HalfTileWidth + mouseY / Constants.HalfTileHeight) / 2;
            int posY = (-mouseX / Constants.HalfTileWidth + mouseY / Constants.HalfTileHeight) / 2;
            return new Point(posX, posY);
        }

        public Rectangle worldToScreenBounds(Point p,int height)
        {
            return new Rectangle((p.X - p.Y) * Constants.HalfTileWidth, (p.X + p.Y) * Constants.HalfTileHeight-(height-64), Constants.TileWidth, height);
        }
    }
}