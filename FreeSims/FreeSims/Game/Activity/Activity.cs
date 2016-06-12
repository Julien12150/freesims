using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.Activity
{
    public class Activity
    {
        public string type;
        public Human human;
        public Human targetH;
        public Item.Item targetI;

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Sprite sprites)
        {

        }
        public virtual void Start(GameTime gameTime)
        {

        }
    }
}
