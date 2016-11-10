using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.Entity.Item
{
    public class TV : Item
    {
        public bool on = false;

        Texture2D shadow;
		Shadow shadowClass;

		public TV(ItemSprite itemSprite, float posX, float posY, float posZ, int angle, List<Human> humanList, GraphicsDevice gd)
        {
            Sprite = itemSprite.oldTv;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            this.angle = angle;

            this.humanList = humanList;

            type = "TV";

			this.shadowClass = new Shadow(gd);

			shadow = shadowClass.GenerateShadow(Sprite, 7, 1);
        }
        public override void Update(GameTime gameTime)
        {
            if (humanList.ToArray().Length > 0)
                on = true;
            else
                on = false;

            /*if (humanList != null)
            {
                try
                {
                    foreach (Human h in humanList)
                    {
                        if (h.activity == null)
                        {
                            humanList.Remove(h);
                        }
                        else if (h.activity.type != "TV")
                        {
                            humanList.Remove(h);
                        }
                    }
                }
                catch (Exception) { }
            }*/
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
			if (shadow != null)
            	spriteBatch.Draw(shadow, new Vector2(posX - 3, posY - (Sprite.Height / 4) + 1), Color.White * 0.5f);
            if (on)
                spriteBatch.Draw(Sprite, new Vector2(posX, (posY - posZ) - (Sprite.Height / 2)), new Rectangle(Sprite.Width * angle / 8, Sprite.Height / 2, Sprite.Width / 8, Sprite.Height / 2), Color.White);
            else
                spriteBatch.Draw(Sprite, new Vector2(posX, (posY - posZ) - (Sprite.Height / 2)), new Rectangle(Sprite.Width * angle / 8, 0, Sprite.Width / 8, Sprite.Height / 2), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
        public void Remove(Human h)
        {
            humanList.Remove(h);
        }
    }
}
