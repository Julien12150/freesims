using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Technochips.FreeSims
{
	public class Cursor //fun fact: this is the very first thing i have put in the game, even before the menus 
    {
        int width, height;
        public float posX, posY;

        int mX, mY;

        Texture2D sprite;

        Control control;

        public Cursor(int width, int height, Sprite sprites, Control control)
        {
            sprite = sprites.cursorSprite;
            this.control = control;

            this.width = width;
            this.height = height;

            posX = width / 2;
            posY = height / 2;
        }

		public void Update(GameTime gameTime, Vector2 camera)
        {
            MouseState mouseState = Mouse.GetState();

            mX = mouseState.X;
            mY = mouseState.Y;

            if(!control.isControllerMode)
				GoTo(mX + (int)camera.X,mY + (int)camera.Y);
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

				if (posX > width + camera.X)
                    posX = width + camera.X;
				else if (posX < camera.X)
                    posX = camera.X;
				if (posY > height + camera.Y)
                    posY = height + camera.Y;
				else if (posY < camera.Y)
                    posY = camera.Y;
            }
        }

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 camera)
        {
			spriteBatch.Draw(sprite, new Vector2((posX - (sprite.Width / 2)) - camera.X, (posY - (sprite.Height / 2)) - camera.Y), Color.White);
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
