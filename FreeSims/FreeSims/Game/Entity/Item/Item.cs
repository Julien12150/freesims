using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Julien12150.FreeSims.Game.Entity.Item
{
    public class Item : Entity
    {

        public Texture2D Sprite;

        public List<Human> humanList;

        public string type;

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);
		}
    }
}
