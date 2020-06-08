using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Isometric
{
    public class Game1 : Game
    {
        readonly NKeyboard NKeys;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int worldWidth;
        int worldHeight;
        TileType plainsTT;
        TileType snowTT;
        Tile[,] world;
        UnitType aggressorUT;
        List<Unit> units;
        Vector2 cameraOffset;
        Vector2 selectedTile;
        Vector2 lockedTile = new Vector2(-1,-1);
        int selectedUnitIndex = -1;
        bool spacer;
        int turn;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            NKeys = new NKeyboard();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            worldWidth = 16;
            worldHeight = 16;
            cameraOffset = new Vector2(0, 0);
            world = new Tile[worldWidth, worldHeight];
            units = new List<Unit>();
            turn = 1;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.Initialize(Content);

            aggressorUT = new UnitType("Aggressor", "UE", "The “Aggressor” light tank sacrifices firepower for superior armor, granting it the ‘medium’ armor classification- though it is still without most traits of a main battle tank. While this unit’s rapid fire main cannon is effective against soft targets, it lacks armor penetration and is at a disadvantage against equally armored foes.", "Light Tank", 1, 2200, 0, 0, 338, 200, "Medium", new Weapon[1] { new Weapon(30, "Pierce", 8, 2, 0) }, 8, "tread", 8, ContentLoader.UDT_Aggressor);
            plainsTT = new TileType("plains", "Plains", "An area of primarily open and flat plains.", ContentLoader.plains, 500, "plains_damage_1", 1, 1, 1, 0, 2, 1, 2, 2, 2, 2, 1, 1, 0, 0, 0, 0);
            snowTT = new TileType("snow", "Snow", "An area of permafrost covered permanently by deep snow.", ContentLoader.snow, 2700, "tundra_battlefield", 1, 0.75f, 0.73f, 0, 5, 6, 6, 4, 6, 2, 1, 1, 0, 0, 0, 0);


            for (int i = 0; i < worldHeight; i++)
            {
                for (int p = 0; p < worldWidth; p++)
                {
                    world[p, i] = new Tile(p, i, plainsTT);
                }
            }

            units.Add(new Unit(aggressorUT, new Vector2(0,0), 1));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            NKeys.Update();
            spacer = false;
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
                        unit.setMoves(unit.getUnitType().getMaxMoves());
                    }
                }
                if (NKeys.IsKeyPressed(Keys.Enter))
                {
                    bool clear = true;
                    // Ok so x is 0...
                    int x = 0;
                    foreach (Unit unit in units)
                    {
                        // and now it's not zero
                        x++;
                        if (unit.getPosition() == selectedTile)
                        {
                            System.Console.WriteLine("X: " + x);
                            clear = false;
                            // and if the selected unit... ... which is -1.... is the previous unit...? WHAT IS THIS?
                            if (selectedUnitIndex == x - 1)
                            {
                                selectedUnitIndex = -1;
                                lockedTile.Y = -1;
                                lockedTile.X = -1;
                            }
                            else
                            {
                                selectedUnitIndex = x - 1;
                                lockedTile = selectedTile;
                                spacer = true;
                            }
                        }
                    }
                    if (clear == true)
                    {
                        units.Add(new Unit(aggressorUT, selectedTile, 1));
                        selectedUnitIndex = -1;
                        lockedTile.X = -1;
                        lockedTile.Y = -1;
                    }

                }

                if (NKeys.IsKeyPressed(Keys.Down))
                {
                    selectedTile.Y++;
                }
                if (NKeys.IsKeyPressed(Keys.Up))
                {
                    selectedTile.Y--;
                }
                if (NKeys.IsKeyPressed(Keys.Right))
                {
                    selectedTile.X++;
                }
                if (NKeys.IsKeyPressed(Keys.Left))
                {
                    selectedTile.X--;
                }

                if (state.LeftButton == ButtonState.Pressed)
                {
                    selectedTile = pixelToWorld(new Vector2(state.X, state.Y), cameraOffset);
                }
            }

            if ( selectedUnitIndex != -1)
            {
                string movType = units[selectedUnitIndex].getUnitType().getMovType();
                if (NKeys.IsKeyPressed(Keys.Down))
                {
                    if (world[(int)lockedTile.X, (int)lockedTile.Y+1].getType().getMoveCostFromTypeString(movType)<= units[selectedUnitIndex].getMoves())
                    {
                        units[selectedUnitIndex].setMoves(units[selectedUnitIndex].getMoves() - world[(int)lockedTile.X, (int)lockedTile.Y + 1].getType().getMoveCostFromTypeString(movType));
                        units[selectedUnitIndex].setPosition(new Vector2(units[selectedUnitIndex].getPosition().X, units[selectedUnitIndex].getPosition().Y+1));
                        lockedTile.Y++;
                    }
                }
                if (NKeys.IsKeyPressed(Keys.Up))
                {
                    if (world[(int)lockedTile.X, (int)lockedTile.Y - 1].getType().getMoveCostFromTypeString(movType) <= units[selectedUnitIndex].getMoves())
                    {
                        units[selectedUnitIndex].setMoves(units[selectedUnitIndex].getMoves() - world[(int)lockedTile.X, (int)lockedTile.Y - 1].getType().getMoveCostFromTypeString(movType));
                        units[selectedUnitIndex].setPosition(new Vector2(units[selectedUnitIndex].getPosition().X, units[selectedUnitIndex].getPosition().Y - 1));
                        lockedTile.Y--;
                    }
                }
                if (NKeys.IsKeyPressed(Keys.Right))
                {
                    if (world[(int)lockedTile.X+1, (int)lockedTile.Y].getType().getMoveCostFromTypeString(movType) <= units[selectedUnitIndex].getMoves())
                    {
                        units[selectedUnitIndex].setMoves(units[selectedUnitIndex].getMoves() - world[(int)lockedTile.X+1, (int)lockedTile.Y].getType().getMoveCostFromTypeString(movType));
                        units[selectedUnitIndex].setPosition(new Vector2(units[selectedUnitIndex].getPosition().X+1, units[selectedUnitIndex].getPosition().Y));
                        lockedTile.X++;
                    }
                }
                if (NKeys.IsKeyPressed(Keys.Left))
                {
                    if (world[(int)lockedTile.X - 1, (int)lockedTile.Y].getType().getMoveCostFromTypeString(movType) <= units[selectedUnitIndex].getMoves())
                    {
                        units[selectedUnitIndex].setMoves(units[selectedUnitIndex].getMoves() - world[(int)lockedTile.X - 1, (int)lockedTile.Y].getType().getMoveCostFromTypeString(movType));
                        units[selectedUnitIndex].setPosition(new Vector2(units[selectedUnitIndex].getPosition().X - 1, units[selectedUnitIndex].getPosition().Y));
                        lockedTile.X--;
                    }
                }
                if (NKeys.IsKeyPressed(Keys.Enter) && spacer==false)
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
            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(cameraOffset.X,cameraOffset.Y,0));
            // WHY IS THIS GUY NOT LINED UP, WHATS WRONG WITH HIM, EH?
                for (int i = 0; i < worldHeight; i++)
                {
                    for (int p = 0; p < worldWidth; p++)
                    {
                        spriteBatch.Draw(world[p,i].getType().getSprite(), world[p,i].getScreenPosition());
                    }
                }
                spriteBatch.Draw(ContentLoader.selector, new Vector2((selectedTile.X * 64 - selectedTile.Y * 64)-4, (selectedTile.X * 32 + selectedTile.Y * 32)-4));
                if ( lockedTile.X!=-1 && lockedTile.Y != -1)
                {
                    spriteBatch.Draw(ContentLoader.altSelector, new Vector2((lockedTile.X * 64 - lockedTile.Y * 64) - 4, (lockedTile.X * 32 + lockedTile.Y * 32) - 4));
                }
            foreach (Unit unit in units)
            {
                spriteBatch.Draw(unit.getUnitType().getSprite(), new Vector2(unit.getPosition().X*64 - unit.getPosition().Y*64, unit.getPosition().X*32 + unit.getPosition().Y*32));
                if (unit.getMoves() == 0)
                {
                    spriteBatch.DrawString(ContentLoader.Arial, "z", new Vector2((unit.getPosition().X * 64 - unit.getPosition().Y * 64)+32, unit.getPosition().X * 32 + unit.getPosition().Y * 32),Color.Black);
                }
            }
            if (selectedUnitIndex != -1)
            {
                spriteBatch.DrawString(ContentLoader.Arial, "Remaining Moves: "+units[selectedUnitIndex].getMoves().ToString(), new Vector2(10, 30)-cameraOffset, Color.Red);
            }
            spriteBatch.DrawString(ContentLoader.Arial, "Turn " + turn, new Vector2(10, 10) - cameraOffset, Color.Red);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        public Vector2 pixelToWorld(Vector2 pixel, Vector2 cameraOffset)
        {
            int mouseX = (int)pixel.X - (int)cameraOffset.X;
            int mouseY = (int)pixel.Y - (int)cameraOffset.Y;
            int posX = (int)System.Math.Floor((mouseX / 64 + mouseY / 32) * 0.5);
            int posY = (int)System.Math.Floor((-mouseX / 64 + mouseY / 32) * 0.5f);
            return new Vector2(posX, posY);
        }
    }
}