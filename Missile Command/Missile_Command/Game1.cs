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
        GGraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Missile> enemyMissileList;
        List<Missile> ourMissileList;

        Silo one;
        Silo two;
        Silo three;


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

        Rectangle airplaneRect, circleRect, cityRect, groundRect,
            silo1Rect, silo2Rect, silo3Rect, lowRect, targettingCrossRect, outRect, ufoRect;

        Rectangle[] cityRectArray;


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
            silo1Rect = new Rectangle(100, 700, 100, 150);
            silo2Rect = new Rectangle(530, 715, 100, 150);
            silo3Rect = new Rectangle(900, 710, 100, 150);

            cityRectArray = new Rectangle[] { new Rectangle(200, 700, 75, 50), new Rectangle(310, 705, 75, 50), new Rectangle(430, 710, 75, 50),
                new Rectangle(655, 700, 75, 50), new Rectangle(740, 695, 75, 50), new Rectangle(825, 685, 75, 50), };

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            missile.Update(gameTime);
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
            //Silos
            spriteBatch.Draw(ground2Text, silo1Rect, null, Color.White, 0, new Vector2(ground2Text.Width / 2, ground2Text.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(ground2Text, silo2Rect, null, Color.White, 0, new Vector2(ground2Text.Width / 2, ground2Text.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(ground2Text, silo3Rect, null, Color.White, 0, new Vector2(ground2Text.Width / 2, ground2Text.Height / 2), SpriteEffects.None, 0);
            //Cities
            spriteBatch.Draw(cityText, cityRectArray[0], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(cityText, cityRectArray[1], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(cityText, cityRectArray[2], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(cityText, cityRectArray[3], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(cityText, cityRectArray[4], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(cityText, cityRectArray[5], null, Color.White, 0, new Vector2(cityText.Width / 2, cityText.Height / 2), SpriteEffects.None, 0);


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
    }
}
