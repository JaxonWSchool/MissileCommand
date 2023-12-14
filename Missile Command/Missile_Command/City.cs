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
    class City
    {   
        public Rectangle rect;
        public bool isHit;

        public City(Rectangle rect)
        {
            this.rect = rect;
            isHit = false;
        }

    }
}
