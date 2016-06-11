using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game
{
    public class Human
    {
        public bool selected;

        public float posX, posY;
        int finalPosX, finalPosY;

        int angle;

        Control control;
        Cursor cursor;
        SpriteBatch spriteBatch;

        Texture2D sprite;

        public Human(float posX, float posY, int angle, Control control, Cursor cursor, Texture2D sprite, SpriteBatch spriteBatch)
        {
            this.posX = posX;
            this.posY = posY;
            finalPosX = (int)posX;
            finalPosY = (int)posY;

            this.angle = angle;
            this.sprite = sprite;

            this.control = control;
            this.cursor = cursor;
            this.spriteBatch = spriteBatch;
        }

        public void Update(GameTime gameTime)
        {
            if(selected)
            {
                if(control.A && control.isControllerMode)
                {
                    finalPosX = (int)cursor.posX;
                    finalPosY = (int)cursor.posY;
                }
                else if(control.LeftMouseClick && !control.isControllerMode)
                {
                    finalPosX = (int)cursor.posX;
                    finalPosY = (int)cursor.posY;
                }
            }

            if(posX > finalPosX)
            {
                if(posY > finalPosY)
                {
                    posX--;
                    posY--;
                    angle = 5;
                }
                else if (posY == finalPosY)
                {
                    posX--;
                    angle = 6;
                }
                else if (posY < finalPosY)
                {
                    posX--;
                    posY++;
                    angle = 7;
                }
            }
            else if(posX < finalPosX)
            {
                if (posY > finalPosY)
                {
                    posX++;
                    posY--;
                    angle = 3;
                }
                else if (posY == finalPosY)
                {
                    posX++;
                    angle = 2;
                }
                else if (posY < finalPosY)
                {
                    posX++;
                    posY++;
                    angle = 1;
                }
            }
            else if (posX == finalPosX)
            {
                if (posY > finalPosY)
                {
                    posY--;
                    angle = 4;
                }
                else if (posY < finalPosY)
                {
                    posY++;
                    angle = 0;
                }
            }
        }

        public void Draw(GameTime gameTime, float height)
        {
            spriteBatch.Draw(sprite, new Rectangle((int)posX - ((sprite.Width / 8) / 2), (int)posY - sprite.Height + 16, sprite.Width / 8, sprite.Height), new Rectangle((sprite.Width / 8 ) * angle, 0, sprite.Width / 8, sprite.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, posY / height);
        }
    }
}
