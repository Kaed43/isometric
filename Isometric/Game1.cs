using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Isometric
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int worldWidth;
        int worldHeight;
        Texture2D selector;
        Texture2D altSelector;
        Texture2D plains;
        TileType plainsTT;
        Texture2D snow;
        TileType snowTT;
        Tile[,] world;
        UnitType aggressorUT;
        Texture2D aggressor;
        List<Unit> units;
        Vector2 cameraOffset;
        Vector2 selectedTile;
        Vector2 lockedTile = new Vector2(-1,-1);
        int selectedUnitIndex = -1;
        SpriteFont font;
        bool spacer;
        int turn;
        public KeyboardState oldState = Keyboard.GetState();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            plains = Content.Load<Texture2D>("plains");
            snow = Content.Load<Texture2D>("snow");
            aggressor = Content.Load<Texture2D>("UDT_Aggressor");
            selector = Content.Load<Texture2D>("selector");
            altSelector = Content.Load<Texture2D>("altSelector");

            aggressorUT = new UnitType("Aggressor", "UE", "The “Aggressor” light tank sacrifices firepower for superior armor, granting it the ‘medium’ armor classification- though it is still without most traits of a main battle tank. While this unit’s rapid fire main cannon is effective against soft targets, it lacks armor penetration and is at a disadvantage against equally armored foes.", "Light Tank", 1, 2200, 0, 0, 338, 200, "Medium", new Weapon[1] { new Weapon(30, "Pierce", 8, 2, 0) }, 8, "tread", 8, aggressor);
            plainsTT = new TileType("plains", "Plains", "An area of primarily open and flat plains.", plains, 500, "plains_damage_1", 1, 1, 1, 0, 2, 1, 2, 2, 2, 2, 1, 1, 0, 0, 0, 0);
            snowTT = new TileType("snow", "Snow", "An area of permafrost covered permanently by deep snow.", snow, 2700, "tundra_battlefield", 1, 0.75f, 0.73f, 0, 5, 6, 6, 4, 6, 2, 1, 1, 0, 0, 0, 0);

            font = Content.Load<SpriteFont>("Arial");

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
            spacer = false;
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            MouseState state = Mouse.GetState();
            if (newState.IsKeyDown(Keys.W))
            {
                cameraOffset.Y+=5;
            }
            if (newState.IsKeyDown(Keys.S))
            {
                cameraOffset.Y -= 5;
            }
            if (newState.IsKeyDown(Keys.A))
            {
                cameraOffset.X += 5;
            }
            if (newState.IsKeyDown(Keys.D))
            {
                cameraOffset.X -= 5;
            }

            if ( selectedUnitIndex == -1)
            {
                if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                {
                    turn++;
                    foreach (Unit unit in units)
                    {
                        unit.setMoves(unit.getUnitType().getMaxMoves());
                    }
                }
                if (newState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    bool clear = true;
                    int x = 0;
                    foreach (Unit unit in units)
                    {
                        x++;
                        if (unit.getPosition() == selectedTile)
                        {
                            System.Console.WriteLine("X: " + x);
                            clear = false;
                            if (selectedUnitIndex == x - 1)
                            {
                                selectedUnitIndex = -1;
                                lockedTile.X = -1;
                                lockedTile.Y = -1;
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

                if (newState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                {
                    selectedTile.Y++;
                }
                if (newState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                {
                    selectedTile.Y--;
                }
                if (newState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
                {
                    selectedTile.X++;
                }
                if (newState.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
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
                if (newState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                {
                    if (world[(int)lockedTile.X, (int)lockedTile.Y+1].getType().getMoveCostFromTypeString(movType)<= units[selectedUnitIndex].getMoves())
                    {
                        units[selectedUnitIndex].setMoves(units[selectedUnitIndex].getMoves() - world[(int)lockedTile.X, (int)lockedTile.Y + 1].getType().getMoveCostFromTypeString(movType));
                        units[selectedUnitIndex].setPosition(new Vector2(units[selectedUnitIndex].getPosition().X, units[selectedUnitIndex].getPosition().Y+1));
                        lockedTile.Y++;
                    }
                }
                if (newState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                {
                    if (world[(int)lockedTile.X, (int)lockedTile.Y - 1].getType().getMoveCostFromTypeString(movType) <= units[selectedUnitIndex].getMoves())
                    {
                        units[selectedUnitIndex].setMoves(units[selectedUnitIndex].getMoves() - world[(int)lockedTile.X, (int)lockedTile.Y - 1].getType().getMoveCostFromTypeString(movType));
                        units[selectedUnitIndex].setPosition(new Vector2(units[selectedUnitIndex].getPosition().X, units[selectedUnitIndex].getPosition().Y - 1));
                        lockedTile.Y--;
                    }
                }
                if (newState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
                {
                    if (world[(int)lockedTile.X+1, (int)lockedTile.Y].getType().getMoveCostFromTypeString(movType) <= units[selectedUnitIndex].getMoves())
                    {
                        units[selectedUnitIndex].setMoves(units[selectedUnitIndex].getMoves() - world[(int)lockedTile.X+1, (int)lockedTile.Y].getType().getMoveCostFromTypeString(movType));
                        units[selectedUnitIndex].setPosition(new Vector2(units[selectedUnitIndex].getPosition().X+1, units[selectedUnitIndex].getPosition().Y));
                        lockedTile.X++;
                    }
                }
                if (newState.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
                {
                    if (world[(int)lockedTile.X - 1, (int)lockedTile.Y].getType().getMoveCostFromTypeString(movType) <= units[selectedUnitIndex].getMoves())
                    {
                        units[selectedUnitIndex].setMoves(units[selectedUnitIndex].getMoves() - world[(int)lockedTile.X - 1, (int)lockedTile.Y].getType().getMoveCostFromTypeString(movType));
                        units[selectedUnitIndex].setPosition(new Vector2(units[selectedUnitIndex].getPosition().X - 1, units[selectedUnitIndex].getPosition().Y));
                        lockedTile.X--;
                    }
                }
                if (newState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter) && spacer==false)
                {
                    selectedUnitIndex = -1;
                    lockedTile.X = -1;
                    lockedTile.Y = -1;
                }
            }
            oldState = newState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(cameraOffset.X,cameraOffset.Y,0));
                for (int i = 0; i < worldHeight; i++)
                {
                    for (int p = 0; p < worldWidth; p++)
                    {
                        spriteBatch.Draw(world[p,i].getType().getSprite(), world[p,i].getScreenPosition());
                    }
                }
                spriteBatch.Draw(selector, new Vector2((selectedTile.X * 64 - selectedTile.Y * 64)-4, (selectedTile.X * 32 + selectedTile.Y * 32)-4));
                if ( lockedTile.X!=-1 && lockedTile.Y != -1)
                {
                    spriteBatch.Draw(altSelector, new Vector2((lockedTile.X * 64 - lockedTile.Y * 64) - 4, (lockedTile.X * 32 + lockedTile.Y * 32) - 4));
                }
            foreach (Unit unit in units)
            {
                spriteBatch.Draw(unit.getUnitType().getSprite(), new Vector2(unit.getPosition().X*64 - unit.getPosition().Y*64, unit.getPosition().X*32 + unit.getPosition().Y*32));
                if (unit.getMoves() == 0)
                {
                    spriteBatch.DrawString(font, "z", new Vector2((unit.getPosition().X * 64 - unit.getPosition().Y * 64)+32, unit.getPosition().X * 32 + unit.getPosition().Y * 32),Color.Black);
                }
            }
            if (selectedUnitIndex != -1)
            {
                spriteBatch.DrawString(font, "Remaining Moves: "+units[selectedUnitIndex].getMoves().ToString(), new Vector2(10, 30)-cameraOffset, Color.Red);
            }
            spriteBatch.DrawString(font, "Turn " + turn, new Vector2(10, 10) - cameraOffset, Color.Red);
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