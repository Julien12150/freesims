using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Julien12150.FreeSims.Game.Activity;

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
        Cursor cursor;

        bool pressedButton = false;

        public Game(int height, int width, Control control, Cursor cursor, SpriteBatch spriteBatch, Sprite sprites)
        {
            humanList.Add(new Human(width / 2, height / 2, 2, 50, control, cursor, sprites, spriteBatch));
            humanList.Add(new Human(width / 2, height / 2, 6, 75, control, cursor, sprites, spriteBatch));
            humanList.Add(new Human(width / 2 + 80, height / 2 + 60, 0, 25, control, cursor, sprites, spriteBatch));
            this.control = control;
            this.height = height;
            this.spriteBatch = spriteBatch;
            this.sprites = sprites;
            this.cursor = cursor;
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

                    if(cursor.posX < humanList[i].posX + (sprites.humanSprite.Width / 2) && cursor.posX > humanList[i].posX - (sprites.humanSprite.Width / 2) &&
                        cursor.posY < humanList[i].posY && cursor.posY > humanList[i].posY - sprites.humanSprite.Height)
                    {
                        if(control.isControllerMode && control.B)
                        {
                            humanList[i].activity = new Talk(humanList[selectedHuman], humanList[i]);
                            humanList[selectedHuman].activity = new Talk(humanList[i], humanList[selectedHuman]);
                        }
                        else if (!control.isControllerMode && control.RightMouseClick)
                        {
                            humanList[i].activity = new Talk(humanList[selectedHuman], humanList[i]);
                            humanList[selectedHuman].activity = new Talk(humanList[i], humanList[selectedHuman]);
                        }
                    }
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
                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5), new Rectangle(0, 0, sprites.statBar.Width, sprites.statBar.Height / 2), Color.White);
                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5), new Rectangle(0, sprites.statBar.Height / 2, 2 + humanList[i].Social * 2, sprites.statBar.Height / 2), Color.White);
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
