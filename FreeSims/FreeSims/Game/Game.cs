using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Julien12150.FreeSims.Game
{
    public class Game
    {
        List<Human> humanList = new List<Human>();

        int selectedHuman = 0;

        int height;

        Control control;
        SpriteBatch spriteBatch;
        Sprite sprites;

        bool pressedButton = false;

        public Game(int height, int width, Control control, Cursor cursor, SpriteBatch spriteBatch, Sprite sprites)
        {
            humanList.Add(new Human(width / 2, height / 2, 2, control, cursor, sprites.humanSprite, spriteBatch));
            humanList.Add(new Human(width / 2, height / 2, 6, control, cursor, sprites.humanSprite, spriteBatch));
            humanList.Add(new Human(width / 2 + 80, height / 2 + 60, 0, control, cursor, sprites.humanSprite, spriteBatch));
            this.control = control;
            this.height = height;
            this.spriteBatch = spriteBatch;
            this.sprites = sprites;
        }

        public void Update(GameTime gameTime)
        {
            for(int i = 0; i < humanList.ToArray().Length; i++)
            {
                humanList[i].Update(gameTime);
                if(selectedHuman == i)
                {
                    humanList[i].selected = true;
                }
                else
                {
                    humanList[i].selected = false;
                }
            }

            if(control.isControllerMode)
            {
                ChangeSelect(control.LB, control.RB);
            }
            else
            {
                ChangeSelect(Keyboard.GetState().IsKeyDown(Keys.Left),Keyboard.GetState().IsKeyDown(Keys.Right));
            }
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < humanList.ToArray().Length; i++)
            {
                humanList[i].Draw(gameTime, height);
                if (selectedHuman == i)
                {
                    spriteBatch.Draw(sprites.humanSelectSprite, new Vector2(humanList[i].posX - 9, humanList[i].posY - sprites.humanSprite.Height - 8), Color.White);
                }
            }
        }

        void ChangeSelect(bool left, bool right)
        {
            if (right && !pressedButton)
            {
                if (selectedHuman == humanList.ToArray().Length - 1) selectedHuman = 0;
                else selectedHuman++;

                pressedButton = true;
            }
            if (left && !pressedButton)
            {
                if (selectedHuman == 0) selectedHuman = humanList.ToArray().Length - 1;
                else selectedHuman--;

                pressedButton = true;
            }

            if (!right && !left) pressedButton = false;
        }
    }
}
