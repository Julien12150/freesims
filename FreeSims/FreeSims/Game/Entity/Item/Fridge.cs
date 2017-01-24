using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Technochips.FreeSims.Game.Entity.Item
{
    public class Fridge : Item
    {
        public bool on = false;

        Texture2D shadow;

		Shadow shadowClass;

		public Fridge(ItemSprite itemSprite, float posX, float posY, float posZ, int angle, List<Human> humanList, GraphicsDevice gd)
        {
            Sprite = itemSprite.fridge;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            this.angle = angle;

            this.humanList = humanList;

			this.shadowClass = new Shadow(gd);

			shadow = shadowClass.GenerateShadow(Sprite, 7, 2);

            type = "Fridge";
        }
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 camera)
        {
			if (shadow != null)
				spriteBatch.Draw(shadow, new Vector2((posX - 6) - (int)camera.X, posY - ((Sprite.Height / 4) + 1) - (int)camera.Y), Color.White * 0.5f);
			spriteBatch.Draw(Sprite, new Vector2(posX - (int)camera.X, ((posY - posZ) - Sprite.Height) - (int)camera.Y), new Rectangle(Sprite.Width * angle / 8, 0, Sprite.Width / 8, Sprite.Height), Color.White);
            base.Draw(gameTime, spriteBatch, camera);
        }
        public override void Update(GameTime gameTime, Vector2 camera)
        {
			base.Update(gameTime, camera);
        }
        public void Remove(Human h)
        {
            humanList.Remove(h);
        }
    }
}
