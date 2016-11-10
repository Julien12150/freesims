using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Julien12150.FreeSims.Game.Entity;
using Julien12150.FreeSims.Game.Entity.Item;

namespace Julien12150.FreeSims.Game.Activity
{
    public class Activity
    {
        public string type;
        public Human human;
        public Human targetH;
        public Item targetI;

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
