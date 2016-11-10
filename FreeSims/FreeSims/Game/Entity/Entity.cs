using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.Entity
{
	public class Entity
	{
		public float posX, posY;
		public float posZ;
		public int angle;

		public Entity()
		{
			
		}
		public virtual void Update(GameTime gameTime)
		{

		}
		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{

		}
	}
}
