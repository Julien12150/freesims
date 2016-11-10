using System;
using Microsoft.Xna.Framework;
using Julien12150.FreeSims.Game.Entity.Item;
using Julien12150.FreeSims.Game.Entity;

namespace Julien12150.FreeSims.Game.Activity
{
    public class Eat : Activity
    {
        const float TIMER = 5;
        float timer = TIMER;

        public Eat(Human human, Fridge target)
        {
            this.human = human;
            targetI = target;
            type = "Eat";
        }
        private bool FindHuman(Human obj)
        {
            if (obj == human)
                return true;
            return false;
        }
        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                timer = TIMER;
                human.Hunger = 100;
                human.activity = null;
                targetI.humanList = null;
                targetI = null;
            }
            
            Predicate<Human> ph = FindHuman;

            if (targetI != null)
            {
                if (targetI.humanList == null)
                {
                    human.activity.targetI.humanList.RemoveAll(ph);
                    human.activity = null;
                    human = null;
                    targetI = null;
                }
            }
            else
            {
                human.activity.targetI.humanList.RemoveAll(ph);
                human.activity = null;
                human = null;
                targetI = null;
            }
            base.Update(gameTime);
        }
        public override void Start(GameTime gameTime)
        {
            if (targetI.angle == 0)
            {
                human.finalPosX = (int)targetI.posX;
                human.finalPosY = (int)targetI.posY + 20;
            }
            if (targetI.angle == 1)
            {
                human.finalPosX = (int)targetI.posX + 20;
                human.finalPosY = (int)targetI.posY + 20;
            }
            if (targetI.angle == 2)
            {
                human.finalPosX = (int)targetI.posX + 20;
                human.finalPosY = (int)targetI.posY;
            }
            if (targetI.angle == 3)
            {
                human.finalPosX = (int)targetI.posX + 20;
                human.finalPosY = (int)targetI.posY - 20;
            }
            if (targetI.angle == 4)
            {
                human.finalPosX = (int)targetI.posX;
                human.finalPosY = (int)targetI.posY - 20;
            }
            if (targetI.angle == 5)
            {
                human.finalPosX = (int)targetI.posX - 20;
                human.finalPosY = (int)targetI.posY - 20;
            }
            if (targetI.angle == 6)
            {
                human.finalPosX = (int)targetI.posX - 20;
                human.finalPosY = (int)targetI.posY;
            }
            if (targetI.angle == 7)
            {
                human.finalPosX = (int)targetI.posX - 20;
                human.finalPosY = (int)targetI.posY + 20;
            }
            base.Start(gameTime);
        }
    }
}
