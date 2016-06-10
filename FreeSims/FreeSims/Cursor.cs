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
        public float posX, posY;

        int mX, mY;

        Texture2D sprite;

        Control control;

        public Cursor(int width, int height, Texture2D sprite, Control control)
        {
            this.sprite = sprite;
            this.control = control;

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

            if(!control.isControllerMode)
                GoTo(mX, mY);
            else
            {
                /*if (control.DPadLeft)
                    posX--;
                if (control.DPadUp)
                    posY--;
                if (control.DPadRight)
                    posX++;
                if (control.DPadDown)
                    posY++;*/
                Move(control.LeftStickX, control.LeftStickY * -1);

                if (posX > width)
                    posX = width;
                else if (posX < 0)
                    posX = 0;
                if (posY > height)
                    posY = height;
                else if (posY < 0)
                    posY = 0;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(posX - (sprite.Width / 2), posY - (sprite.Height / 2)), Color.White);
        }

        public void GoTo(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public void Move(float x, float y)
        {
            posX = posX + x * 4;
            posY = posY + y * 4;
        }
    }
}
