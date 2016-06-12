using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Julien12150.FreeSims.Game.Item
{
    public class Item
    {
        public virtual float posX { get; set; }
        public virtual float posY { get; set; }
        public virtual int angle { get; set; }

        public Texture2D Sprite { get; set; }

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }
    }
}
