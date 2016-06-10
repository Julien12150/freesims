using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FreeSims
{
    public class Cursor
    {
        int width, height;
        public int posX, posY;

        int mX, mY;

        Texture2D sprite;

        public Cursor(int width, int height, Texture2D sprite)
        {
            this.sprite = sprite;

            this.width = width;
            this.height = height;

            posX = width / 2;
            posY = height / 2;
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            mX = mouseState.X;
            mY = mouseState.Y;

            Move(mX, mY);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(posX - (sprite.Width / 2), posY - (sprite.Height / 2)), Color.White);
        }

        public void Move(int x, int y)
        {
            posX = x;
            posY = y;
        }
    }
}
