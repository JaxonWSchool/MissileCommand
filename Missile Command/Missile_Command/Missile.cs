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
    class Missile
    {
        //Rect is the missile's backing rectangle, and the double x and y variables are to provide more accurate movement towards targets
        public Rectangle rect;
        double doubleRectX, doubleRectY;

        public double rotation;
        public Texture2D texture;
        public Vector2 target;
        public double timeToHit;

        //Rise and run are the amount x and amount y that the missile is incremented each update to hit its target
        double rise, run;
        

        public Missile(Rectangle r, Texture2D tex, int time, Vector2 targetLocation)
        {
            rect = r;
            target = targetLocation;
            target.X = target.X + rect.Width / 2;
            target.Y = target.Y + rect.Height / 2;
            //Find the x and y distances from the starting rectangle point to the target
            double distanceX = (int)target.X - rect.X;
            double distanceY = (int)target.Y - rect.Y;

            texture = tex;
            timeToHit = time * 60;
            doubleRectX = rect.X;
            doubleRectY = rect.Y;

            /*Uses the x and y distance with arctan to find the theta value that the missile needs to be rotated past PI to be aimed
            *at the target
            */
            rotation = Math.PI + (-1 * Math.Atan2(distanceX, distanceY));
            rise = Math.Abs(distanceY / timeToHit);
            run = distanceX / timeToHit;
        }

        public void Update(GameTime gameTime)
        {
            doubleRectX += run;
            doubleRectY += rise;
            rect.X = (int)doubleRectX;
            rect.Y = (int)doubleRectY;
        }
    }
}
