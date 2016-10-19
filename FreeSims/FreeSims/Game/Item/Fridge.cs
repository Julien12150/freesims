using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.Item
{
    public class Fridge : Item
    {
        public bool on = false;

        public Fridge(ItemSprite itemSprite, float posX, float posY, int angle, List<Human> humanList)
        {
            Sprite = itemSprite.fridge;
            this.posX = posX;
            this.posY = posY;
            this.angle = angle;

            this.humanList = humanList;

            type = "Fridge";
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Vector2(posX, posY - Sprite.Height), new Rectangle(Sprite.Width * angle / 8, 0, Sprite.Width / 8, Sprite.Height), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public void Remove(Human h)
        {
            humanList.Remove(h);
        }
    }
}
