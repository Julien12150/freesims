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
            spriteBatch.Draw(sprites.talkBuble, new Vector2(human.posX, human.posY - sprites.humanSprite.Height - 5), Color.White);

            base.Draw(gameTime, spriteBatch, sprites);
        }
    }
}
