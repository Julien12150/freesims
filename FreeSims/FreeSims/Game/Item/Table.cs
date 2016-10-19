using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.Item
{
    public class Table : Item
    {
        public Table(ItemSprite itemSprite, float posX, float posY, int angle)
        {
            Sprite = itemSprite.table;
            this.posX = posX;
            this.posY = posY;
            this.angle = angle;

            type = "Table";
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Vector2(posX, posY - Sprite.Height), new Rectangle(Sprite.Width * angle / 8, 0, Sprite.Width / 8, Sprite.Height), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
