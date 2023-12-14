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

        Rectangle airplaneRect, circleRect, groundRect,
            lowRect, targettingCrossRect, outRect, ufoRect;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
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

            cityList = new List<City>();

            cityList.Add(new City(new Rectangle(200, 700, 75, 50)));
            cityList.Add(new City(new Rectangle(310, 705, 75, 50)));
            cityList.Add(new City(new Rectangle(430, 710, 75, 50)));
            cityList.Add(new City(new Rectangle(655, 700, 75, 50)));
            cityList.Add(new City(new Rectangle(740, 695, 75, 50)));
            cityList.Add(new City(new Rectangle(825, 685, 75, 50)));

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

            // TODO: use this.Content to load your game content here
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
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //Background
            spriteBatch.Draw(ground1Text, groundRect, Color.White);
            //Silos
            spriteBatch.Draw(ground2Text, oneSilo.rect, null, Color.White, 0, new Vector2(ground2Text.Width / 2, ground2Text.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(ground2Text, twoSilo.rect, null, Color.White, 0, new Vector2(ground2Text.Width / 2, ground2Text.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(ground2Text, threeSilo.rect, null, Color.White, 0, new Vector2(ground2Text.Width / 2, ground2Text.Height / 2), SpriteEffects.None, 0);
            //ammo
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
            for (int x = 0; x < cityList.Count; x++)
            {
                if (!cityList[x].isHit)
                    spriteBatch.Draw(cityText, cityList[x].rect, null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool isColliding(Rectangle rec1, Rectangle rec2) //probably change
        {
            if (rec2.X + rec2.Width >= rec1.X && !(rec2.X > rec1.X + rec1.Width))
            {
                if (rec2.Y + rec2.Height >= rec1.Y && !(rec2.Y > rec1.Y + rec1.Height))
                    return true;
            }
            return false;
        }

    }
    public enum GameState { startScreen, playing, betweenRounds, lost }
}
