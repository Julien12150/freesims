using Microsoft.Xna.Framework;
using Julien12150.FreeSims.Game.Item;

namespace Julien12150.FreeSims.Game.Activity
{
    public class SitChair : Activity
    {
        public SitChair(Human human, Chair chair)
        {
            this.human = human;
            targetI = chair;
            type = "SitChair";
        }

        public override void Update(GameTime gameTime)
        { 
            if(human.finalPosX == (int)targetI.posX + (targetI.Sprite.Width / 8 / 2) && human.finalPosY == (int)targetI.posY + 75)
            {
                human.angle = targetI.angle;
            }
            base.Update(gameTime);
        }

        public override void Start(GameTime gameTime)
        {
            human.finalPosX = (int)targetI.posX + (targetI.Sprite.Width / 8 / 2);
            human.finalPosY = (int)targetI.posY + 75;
            base.Start(gameTime);
        }
    }
}
