using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.Entity.Item
{
    public class Chair : Item
    {
        Texture2D shadow;
		Shadow shadowClass;

		public Chair(ItemSprite itemSprite, float posX, float posY, float posZ, int angle, GraphicsDevice gd)
        {
            Sprite = itemSprite.chair;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            this.angle = angle;

            humanList = new List<Human>();

			this.shadowClass = new Shadow(gd);

			shadow = shadowClass.GenerateShadow(Sprite, 7, 1);

            type = "Chair";
        }

        public override void Update(GameTime gameTime, Vector2 camera)
        {
            if(humanList.ToArray().Length > 1)
            {
                humanList.Remove(humanList[0]);
            }
			base.Update(gameTime, camera);
        }
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 camera)
        {
			if (shadow != null)
				spriteBatch.Draw(shadow, new Vector2((posX - 4) - (int)camera.X, (posY - (Sprite.Height / 4) + 1) - (int)camera.Y), Color.White * 0.5f);
			spriteBatch.Draw(Sprite, new Vector2(posX - (int)camera.X, ((posY - posZ) - Sprite.Height) - (int)camera.Y), new Rectangle(Sprite.Width * angle / 8, 0, Sprite.Width / 8, Sprite.Height), Color.White);
            base.Draw(gameTime, spriteBatch, camera);
        }
    }
}
