using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Missile_Command
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Missile> enemyMissileList;
        List<Missile> ourMissileList;
        List<City> cityList; 

        Silo oneSilo;
        Silo twoSilo;
        Silo threeSilo;

	int siloIndex;
        int timeToHit;

        Texture2D airPlaneText;
        Texture2D ammoText;
        Texture2D circleText;
        Texture2D cityText;
        Texture2D ground1Text;
        Texture2D ground2Text;
        Texture2D lowText;
        Texture2D outText;
        Texture2D targettingCrossText;
        Texture2D ufoText;
        Texture2D startScreen, endScreen;

        Rectangle airplaneRect, circleRect, cityRect, groundRect,
            lowRect, targettingCrossRect, outRect, ufoRect;

	List<Rectangle> targetingXs;

        Plane plane;

        Rectangle[] cityRectArray;

        GameState gameState;

        Round round;

        KeyboardState kb, oldKb;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            IsMouseVisible = true;
            //one = new Silo(Rectangle);
            //one.ammo = List<Rectangle>
            groundRect = new Rectangle(-4, 650, graphics.PreferredBackBufferWidth + 8, 150);
            oneSilo = new Silo(new Rectangle(100, 700, 100, 150));
            twoSilo = new Silo(new Rectangle(530, 715, 100, 150));
            threeSilo = new Silo(new Rectangle(900, 710, 100, 150));

            cityRectArray = new Rectangle[] { new Rectangle(200, 700, 75, 50), new Rectangle(310, 705, 75, 50), new Rectangle(430, 710, 75, 50),
                new Rectangle(655, 700, 75, 50), new Rectangle(740, 695, 75, 50), new Rectangle(825, 685, 75, 50), };

	    oneSilo = new Silo(silo1Rect);
            twoSilo = new Silo(silo2Rect);
            threeSilo = new Silo(silo3Rect);

            timeToHit = 1;

            gameState = GameState.startScreen;

            cityList = new List<City>();

            cityList.Add(new City(new Rectangle(200, 700, 75, 50)));
            cityList.Add(new City(new Rectangle(310, 705, 75, 50)));
            cityList.Add(new City(new Rectangle(430, 710, 75, 50)));
            cityList.Add(new City(new Rectangle(655, 700, 75, 50)));
            cityList.Add(new City(new Rectangle(740, 695, 75, 50)));
            cityList.Add(new City(new Rectangle(825, 685, 75, 50)));

            plane = null; // use method at bottom to add plane

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            airPlaneText = Content.Load<Texture2D>("airplane");
            ammoText = Content.Load<Texture2D>("ammo");
            circleText = Content.Load<Texture2D>("circle");
            cityText = Content.Load<Texture2D>("city");
            ground1Text = Content.Load<Texture2D>("groundTexture1");
            ground2Text = Content.Load<Texture2D>("groundTexture2");
            lowText = Content.Load<Texture2D>("Low");
            targettingCrossText = Content.Load<Texture2D>("newTargettingCross");
            outText = Content.Load<Texture2D>("out");
            ufoText = Content.Load<Texture2D>("ufo");
            startScreen = Content.Load<Texture2D>("startScreen");
            endScreen = Content.Load<Texture2D>("lostScreen");
            missileText = Content.Load<Texture2D>("white");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            kb = Keyboard.GetState();
            mouse = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            addPlane();
            if(gameState == GameState.startScreen && kb.IsKeyDown(Keys.Space))
            {
                gameState = GameState.playing;
            }
            if (state == GameState.playing)
            {
                if (kb.IsKeyDown(Keys.D1))
                {
                    siloIndex = 1;
                }
                if (kb.IsKeyDown(Keys.D2))
                {
                    siloIndex = 2;
                }
                if (kb.IsKeyDown(Keys.D3))
                {
                    siloIndex = 3;
                }
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    Vector2 target = new Vector2(mouse.X, mouse.Y);
                    if (target.Y > 600)
                    {
                        target.Y = 600;
                    }
                    switch (siloIndex)
                    {
                        case 1:
                            if (oneSilo.missile == null && oneSilo.loadedAmmo.Count > 0)
                            {
                                oneSilo.missile = new Missile(new Rectangle(oneSilo.rect.X, oneSilo.rect.Y, 5, 5), missileText, timeToHit, target, false);
                                oneSilo.loadedAmmo.RemoveAt(oneSilo.loadedAmmo.Count - 1);
                            }
                            break;
                        case 2:
                            if (twoSilo.missile == null && twoSilo.loadedAmmo.Count > 0)
                            {
                                twoSilo.missile = new Missile(new Rectangle(twoSilo.rect.X, twoSilo.rect.Y, 5, 5), missileText, timeToHit, target, false);
                                twoSilo.loadedAmmo.RemoveAt(twoSilo.loadedAmmo.Count - 1);
                            }
                            break;
                        case 3:
                            if (threeSilo.missile == null && threeSilo.loadedAmmo.Count > 0)
                            {
                                threeSilo.missile = new Missile(new Rectangle(threeSilo.rect.X, threeSilo.rect.Y, 5, 5), missileText, timeToHit, target, false);
                                threeSilo.loadedAmmo.RemoveAt(threeSilo.loadedAmmo.Count - 1);
                            }
                            break;
                    }
                }
                if (oneSilo.missile != null)
                {
                    oneSilo.missile.Update(gameTime);
                    oneSilo.missile.ageInFrames++;
                    if (oneSilo.missile.ageInFrames % 60 >= timeToHit)
                    {
                        oneSilo.missile.Explode();
                    }
                }
                if (twoSilo.missile != null)
                {
                    twoSilo.missile.Update(gameTime);
                    twoSilo.missile.ageInFrames++;
                    if (twoSilo.missile.ageInFrames % 60 >= timeToHit)
                    {
                        twoSilo.missile.Explode();
                    }
                }
                if (threeSilo.missile != null)
                {
                    threeSilo.missile.Update(gameTime);
                    threeSilo.missile.ageInFrames++;
                    if (threeSilo.missile.ageInFrames % 60 >= timeToHit)
                    {
                        threeSilo.missile.Explode();
                    }
                }
                //Cities
                for (int x = 0; x < cityList.Count; x++)
                {
                    if (!cityList[x].isHit)
                        spriteBatch.Draw(cityText, cityList[x].rect, null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
                }
                // if plane is off screen delete
                if (plane.rect.X + plane.rect.Width / 2 < 0)
                    plane = null;

                if(plane != null)
                    plane.update();
            }
            //Add check for round being over to change game state

            oldKb = kb;
            oldMouse = mouse;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //Background
            spriteBatch.Draw(ground1Text, groundRect, Color.White);

            if(gameState == GameState.startScreen)
            {
                spriteBatch.Draw(startScreen, new Rectangle(0, 0, 1000, 800), Color.White);
            }
            if(gameState == GameState.playing)
            {
                //Silos
                spriteBatch.Draw(ground2Text, oneSilo.rect, null, Color.White, 0, new Vector2(ground2Text.Width / 2, ground2Text.Height / 2), SpriteEffects.None, 0);
                spriteBatch.Draw(ground2Text, twoSilo.rect, null, Color.White, 0, new Vector2(ground2Text.Width / 2, ground2Text.Height / 2), SpriteEffects.None, 0);
                spriteBatch.Draw(ground2Text, threeSilo.rect, null, Color.White, 0, new Vector2(ground2Text.Width / 2, ground2Text.Height / 2), SpriteEffects.None, 0);
                //Ammo
                for (int i = 0; i < oneSilo.ammo.Count; i++)
                {
                    spriteBatch.Draw(ammoText, oneSilo.ammo[i], null, Color.White, 0, new Vector2(ammoText.Width / 2, ammoText.Height / 2), SpriteEffects.None, 0);
                }
                for (int i = 0; i < twoSilo.ammo.Count; i++)
                {
                    spriteBatch.Draw(ammoText, twoSilo.ammo[i], null, Color.White, 0, new Vector2(ammoText.Width / 2, ammoText.Height / 2), SpriteEffects.None, 0);
                }
                for (int i = 0; i < threeSilo.ammo.Count; i++)
                {
                    spriteBatch.Draw(ammoText, threeSilo.ammo[i], null, Color.White, 0, new Vector2(ammoText.Width / 2, ammoText.Height / 2), SpriteEffects.None, 0);
                }
                //Cities
                spriteBatch.Draw(cityText, cityRectArray[0], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
                spriteBatch.Draw(cityText, cityRectArray[1], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
                spriteBatch.Draw(cityText, cityRectArray[2], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
                spriteBatch.Draw(cityText, cityRectArray[3], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
                spriteBatch.Draw(cityText, cityRectArray[4], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
                spriteBatch.Draw(cityText, cityRectArray[5], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
                //Ammo
                for (int i = 0; i < oneSilo.loadedAmmo.Count; i++)
                {
                    spriteBatch.Draw(ammoText, oneSilo.loadedAmmo[i], null, Color.White, 0, new Vector2(ammoText.Width / 2, ammoText.Height / 2), SpriteEffects.None, 0);
                }
                for (int i = 0; i < twoSilo.loadedAmmo.Count; i++)
                {
                    spriteBatch.Draw(ammoText, twoSilo.loadedAmmo[i], null, Color.White, 0, new Vector2(ammoText.Width / 2, ammoText.Height / 2), SpriteEffects.None, 0);
                }
                for (int i = 0; i < threeSilo.loadedAmmo.Count; i++)
                {
                    spriteBatch.Draw(ammoText, threeSilo.loadedAmmo[i], null, Color.White, 0, new Vector2(ammoText.Width / 2, ammoText.Height / 2), SpriteEffects.None, 0);
                }
                if (oneSilo.missile != null)
                {
                    spriteBatch.Draw(missileText, oneSilo.missile.rect, Color.White);
                }
                if (twoSilo.missile != null)
                {
                    spriteBatch.Draw(missileText, twoSilo.missile.rect, Color.White);
                }
                if (threeSilo.missile != null)
                {
                    spriteBatch.Draw(missileText, threeSilo.missile.rect, Color.White);
                }   
                //plane
                if (plane != null)
                    spriteBatch.Draw(airPlaneText, plane.rect, null, Color.White, 0, new Vector2(airPlaneText.Width/2, airPlaneText.Height/2), SpriteEffects.None, 0);    
            }
            if(gameState == GameState.lost)
            {
                spriteBatch.Draw(endScreen, new Rectangle(0, 0, 1000, 800), Color.White);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
         public Boolean isColliding(Rectangle rec1, Rectangle rec2) //probably change
        {
            if (rec2.X + rec2.Width >= rec1.X && !(rec2.X > rec1.X + rec1.Width))
            {
                if (rec2.Y + rec2.Height >= rec1.Y && !(rec2.Y > rec1.Y + rec1.Height))
                    return true;
            }
            return false;
        }

        public void addPlane()
        {
            if(plane == null)
                plane = new Plane(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        }

        public enum GameState
        {
            startScreen,
            playing,
            betweenRounds,
            lost
        }
    }
}
