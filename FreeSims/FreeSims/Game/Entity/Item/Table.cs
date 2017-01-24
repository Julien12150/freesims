using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Technochips.FreeSims.Game.Entity.Item
{
    public class Table : Item
    {
        Texture2D shadow;
		Shadow shadowClass;
		public Table(ItemSprite itemSprite, float posX, float posY, float posZ, int angle, GraphicsDevice gd)
        {
            Sprite = itemSprite.table;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            this.angle = angle;

			this.shadowClass = new Shadow(gd);

			shadow = shadowClass.GenerateShadow(Sprite, 7, 1);

            type = "Table";
        }
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 camera)
        {
			if (shadow != null)
				spriteBatch.Draw(shadow, new Vector2((posX - 4) - (int)camera.X, (posY - ((Sprite.Height / 4) * 3)) - (int)camera.Y), Color.White * 0.5f);
			spriteBatch.Draw(Sprite, new Vector2((posX) - (int)camera.X, ((posY - posZ) - Sprite.Height) -(int) camera.Y), new Rectangle(Sprite.Width * angle / 8, 0, Sprite.Width / 8, Sprite.Height), Color.White);
			base.Draw(gameTime, spriteBatch, camera);
        }
    }
}
