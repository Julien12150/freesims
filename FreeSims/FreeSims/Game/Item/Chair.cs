using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.Item
{
    public class Chair : Item
    {
        public Chair(ItemSprite itemSprite, float posX, float posY, int angle)
        {
            Sprite = itemSprite.Chair;
            this.posX = posX;
            this.posY = posY;
            this.angle = angle;

            humanList = new List<Human>();

            type = "Chair";
        }

        public override void Update(GameTime gameTime)
        {
            if(humanList.ToArray().Length > 1)
            {
                humanList.Remove(humanList[0]);
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Vector2(posX, posY), new Rectangle(Sprite.Width * angle / 8, 0, Sprite.Width / 8, Sprite.Height), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
