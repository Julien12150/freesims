using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Technochips.FreeSims.Game.Entity.Item
{
    public class Item : Entity
    {

        public Texture2D Sprite;

        public List<Human> humanList;

        public string type;

		public override void Update(GameTime gameTime, Vector2 camera)
		{
			base.Update(gameTime, camera);
		}
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 camera)
		{
			base.Draw(gameTime, spriteBatch, camera);
		}
    }
}
