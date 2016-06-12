using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Julien12150.FreeSims.Game.Item
{
    public class Item
    {
        public float posX;
        public float posY;
        public int angle;

        public Texture2D Sprite;

        public List<Human> humanList;

        public string type;

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }
    }
}
