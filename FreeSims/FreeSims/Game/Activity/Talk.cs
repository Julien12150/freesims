using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.Activity
{
    public class Talk : Activity
    {
        const float TIMER = 5;
        float timer = TIMER;

        public Talk(Human human, Human target)
        {
            this.target = target;
            this.human = human;
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if(timer < 0)
            {
                timer = TIMER;
                human.Social++;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Sprite sprites)
        {
            spriteBatch.Draw(sprites.talkBuble, new Vector2(human.posX, human.posY - sprites.humanSprites.mNoColor.Height - 5), Color.White);

            base.Draw(gameTime, spriteBatch, sprites);
        }

        public override void Start(GameTime gameTime)
        {
            Random r = new Random();
            int rx = r.Next(2);
            if (rx == 0)
            {
                int ry = r.Next(2);
                human.finalPosX = (int)target.posX - 30;
                if (ry == 0)
                {
                    human.finalPosY = (int)target.posY - 30;
                    target.angle = 5;
                }
                else if (ry == 1)
                {
                    human.finalPosY = (int)target.posY;
                    target.angle = 6;
                }
                else if (ry == 2)
                {
                    human.finalPosY = (int)target.posY + 30;
                    target.angle = 7;
                }
            }
            else if (rx == 1)
            {
                int ry = r.Next(1);
                human.finalPosX = (int)target.posX;
                if (ry == 0)
                {
                    human.finalPosY = (int)target.posY - 30;
                    target.angle = 4;
                }
                else if (ry == 1)
                {
                    human.finalPosY = (int)target.posY + 30;
                    target.angle = 0;
                }
            }
            else if (rx == 3)
            {
                int ry = r.Next(2);
                human.finalPosX = (int)target.posX + 30;
                if (ry == 0)
                {
                    human.finalPosY = (int)target.posY - 30;
                    target.angle = 5;
                }
                else if (ry == 1)
                {
                    human.finalPosY = (int)target.posY;
                    target.angle = 6;
                }
                else if (ry == 2)
                {
                    human.finalPosY = (int)target.posY + 30;
                    target.angle = 7;
                }
            }

            base.Start(gameTime);
        }
    }
}
