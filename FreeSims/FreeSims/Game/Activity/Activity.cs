using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Technochips.FreeSims.Game.Entity;
using Technochips.FreeSims.Game.Entity.Item;

namespace Technochips.FreeSims.Game.Activity
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
		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Sprite sprites, Vector2 camera)
        {

        }
        public virtual void Start(GameTime gameTime)
        {

        }
    }
}
