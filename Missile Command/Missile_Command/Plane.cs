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
    class Plane
    {

        public Rectangle rect;
        public const int velocity = 2;

        public Plane(int screenWidth, int screenHeight) // makes a random planeS
        {
            Random rng = new Random();
            rect = new Rectangle(screenWidth + 50, rng.Next(screenHeight/4)+50, 100, 100);
        }

        public void update()
        {
            rect.X -= velocity;
        }
    }
}
