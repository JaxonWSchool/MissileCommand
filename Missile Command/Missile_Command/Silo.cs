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
    class Silo
    {
        public Rectangle rect;
        public Missile missile;
        public List<Rectangle> loadedAmmo;
        public Silo(Rectangle rect)
        {
            this.rect = rect;
            missile = null;
            loadedAmmo = new List<Rectangle>();

            int width = 10;
            int height = 15;
            //draw ammo in a pyramid
            loadedAmmo.Add(new Rectangle(rect.X, rect.Y, width, height));

            loadedAmmo.Add(new Rectangle(rect.X - width / 2, rect.Y + height, width, height));
            loadedAmmo.Add(new Rectangle(rect.X + width / 2, rect.Y + height, width, height));

            loadedAmmo.Add(new Rectangle(rect.X - width, rect.Y + height * 2, width, height));
            loadedAmmo.Add(new Rectangle(rect.X, rect.Y + height * 2, width, height));
            loadedAmmo.Add(new Rectangle(rect.X + width, rect.Y + height * 2, width, height));

            loadedAmmo.Add(new Rectangle(rect.X - width * 3 / 2, rect.Y + height * 3, width, height));
            loadedAmmo.Add(new Rectangle(rect.X - width * 1 / 2, rect.Y + height * 3, width, height));
            loadedAmmo.Add(new Rectangle(rect.X + width * 1 / 2, rect.Y + height * 3, width, height));
            loadedAmmo.Add(new Rectangle(rect.X + width * 3 / 2, rect.Y + height * 3, width, height));
        }

    }
}
