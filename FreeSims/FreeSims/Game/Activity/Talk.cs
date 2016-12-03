using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Julien12150.FreeSims.Game.Entity.Item;
using Julien12150.FreeSims.Game.Entity;

namespace Julien12150.FreeSims.Game.Activity
{
    public class Talk : Activity
    {
        const float TIMER = 5;
        float timer = TIMER;

		public Talk(Human human, Human targetH)
        {
            this.targetH = targetH;
            this.human = human;
            type = "Talk";
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Sprite sprites, Vector2 camera)
        {
			spriteBatch.Draw(sprites.talkBuble, new Vector2(human.posX - camera.X, (human.posY - (sprites.humanSprites.mNoColor.Height / 2) - 5) - camera.Y), Color.White);

			base.Draw(gameTime, spriteBatch, sprites, camera);
        }

        public override void Start(GameTime gameTime)
        {
            Random r = new Random();
            int rx = r.Next(0, 4);
            if (rx == 1)
            {
                int ry = r.Next(1, 4);
                human.finalPosX = (int)targetH.posX - 30;
                if (ry == 1)
                {
                    human.finalPosY = (int)targetH.posY - 30;
                    targetH.angle = 5;
                }
                else if (ry == 2)
                {
                    human.finalPosY = (int)targetH.posY;
                    targetH.angle = 6;
                }
                else if (ry == 3)
                {
                    human.finalPosY = (int)targetH.posY + 30;
                    targetH.angle = 7;
                }
            }
            else if (rx == 2)
            {
                int ry = r.Next(1, 3);
                human.finalPosX = (int)targetH.posX;
                if (ry == 1)
                {
                    human.finalPosY = (int)targetH.posY - 30;
                    targetH.angle = 4;
                }
                else if (ry == 2)
                {
                    human.finalPosY = (int)targetH.posY + 30;
                    targetH.angle = 0;
                }
            }
            else if (rx == 3)
            {
                int ry = r.Next(1, 4);
                human.finalPosX = (int)targetH.posX + 30;
                if (ry == 1)
                {
                    human.finalPosY = (int)targetH.posY - 30;
                    targetH.angle = 3;
                }
                else if (ry == 2)
                {
                    human.finalPosY = (int)targetH.posY;
                    targetH.angle = 2;
                }
                else if (ry == 3)
                {
                    human.finalPosY = (int)targetH.posY + 30;
                    targetH.angle = 1;
                }
            }

            base.Start(gameTime);
        }
    }
}
