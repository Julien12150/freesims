using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.Item
{
    public class Table : Item
    {
        Texture2D shadow;
        public Table(ItemSprite itemSprite, float posX, float posY, float posZ, int angle, GraphicsDevice gd)
        {
            Sprite = itemSprite.table;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            this.angle = angle;

            shadow = Shadow.GenerateShadow(Sprite, 7, 1, gd);

            type = "Table";
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
			if (shadow != null)
            	spriteBatch.Draw(shadow, new Vector2(posX - 4, posY - ((Sprite.Height / 4) * 3)), Color.White * 0.5f);
            spriteBatch.Draw(Sprite, new Vector2(posX, (posY - posZ) - Sprite.Height), new Rectangle(Sprite.Width * angle / 8, 0, Sprite.Width / 8, Sprite.Height), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
