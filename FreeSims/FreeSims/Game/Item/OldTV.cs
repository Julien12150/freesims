using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.Item
{
    public class OldTV : Item
    {
        public override float posX { get {return base.posX; } set {base.posX = value;} }
        public override float posY { get { return base.posY; } set { base.posY = value; } }
        public override int angle { get { return base.angle; } set { base.angle = value; } }

        public OldTV(ItemSprite itemSprite, float posX, float posY, int angle)
        {
            Sprite = itemSprite.oldTv;
            this.posX = posX;
            this.posY = posY;
            this.angle = angle;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Vector2(posX, posY), new Rectangle(Sprite.Width * angle / 8, Sprite.Height / 2, Sprite.Width / 8, Sprite.Height / 2), Color.White);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
