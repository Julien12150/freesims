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
        bool selected = true;

        float posX, posY;
        int finalPosX, finalPosY;

        int angle = 0;

        Control control;
        Cursor cursor;
        SpriteBatch spriteBatch;

        public Human(float posX, float posY, Control control, Cursor cursor, SpriteBatch spriteBatch)
        {
            this.posX = posX;
            this.posY = posY;
            finalPosX = (int)posX;
            finalPosY = (int)posY;

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

        public void Draw(GameTime gameTime, Texture2D sprite)
        {
            spriteBatch.Draw(sprite, new Vector2(posX - ((sprite.Width / 8 ) / 2), posY - sprite.Height + 16), new Rectangle((sprite.Width / 8 ) * angle, 0, sprite.Width / 8, sprite.Height), Color.White);
        }
    }
}
