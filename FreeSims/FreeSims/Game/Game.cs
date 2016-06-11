using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Julien12150.FreeSims.Game
{
    public class Game
    {
        List<Human> humanList = new List<Human>();

        Texture2D humanSprite;

        public Game(int height, int width, Control control, Cursor cursor, SpriteBatch spriteBatch, Texture2D humanSprite)
        {
            humanList.Add(new Human(height / 2, width / 2, control, cursor, spriteBatch));
            this.humanSprite = humanSprite;
        }

        public void Update(GameTime gameTime)
        {
            foreach(Human h in humanList)
            {
                h.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach(Human h in humanList)
            {
                h.Draw(gameTime, humanSprite);
            }
        }
    }
}
