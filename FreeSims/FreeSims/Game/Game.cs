using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Julien12150.FreeSims.Game.Activity;
using Julien12150.FreeSims.Game.Item;
using Julien12150.FreeSims.Game.HumanMaker;
using System;

namespace Julien12150.FreeSims.Game
{
    public class Game
    {
        List<Human> humanList = new List<Human>();
        List<Item.Item> itemList = new List<Item.Item>();

        int selectedHuman = 0;

        int height;

        Control control;
        SpriteBatch spriteBatch;
        Sprite sprites;
        ItemSprite itemSprites;
        Cursor cursor;

        bool pressedButton = false;

        public Game(int width, int height, Control control, Cursor cursor, SpriteBatch spriteBatch, Sprite sprites, ItemSprite itemSprites)
        {
            this.control = control;
            this.height = height;
            this.spriteBatch = spriteBatch;
            this.sprites = sprites;
            this.cursor = cursor;
            this.itemSprites = itemSprites;

            string[] names;
            bool[] female;
            Color[] eyes;
            Color[] hair;
            Color[] pants;
            Color[] shirt;
            Color[] shoes;
            Color[] skin;
            if (HMNFileManager.Read(out names, out female, out eyes, out hair, out pants, out shirt, out shoes, out skin))
            {
                for(int i = 0; i < names.Length; i++)
                {
                    humanList.Add(new Human(width / 2, height / 2, 0, 50, 50, control, cursor, sprites, spriteBatch, female[i], pants[i], hair[i], eyes[i], shirt[i], shoes[i], skin[i]));
                }
            }
            else
            {
                names = new string[] { "Julian", "Joe", "Tom", "Christine" };
                female = new bool[] { false, false, false, true };
                pants = new Color[] { new Color(0, 31, 255), new Color(22, 22, 22), new Color(193, 193, 193), new Color(255, 173, 255)};
                hair = new Color[] { new Color(66, 33, 0), new Color(255, 155, 43), new Color(40, 9, 0), new Color(66, 33, 0) };
                eyes = new Color[] { new Color(0, 0, 56), new Color(68, 21, 0), new Color(0, 47, 0), new Color(0, 0, 56) };
                shirt = new Color[] { new Color(255, 255, 255), new Color(183, 43, 0), new Color(22, 22, 22), new Color(255, 255, 255) };
                shoes = new Color[] { new Color(96, 96, 96), new Color(150, 150, 150), new Color(56, 20, 0), new Color(200, 0, 0) };
                skin = new Color[] { new Color(255, 179, 160), new Color(183, 124, 95), new Color(255, 202, 191), new Color(255, 179, 160) };

                Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Julien12150/");
                Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Julien12150/FreeSims/");

                HMNFileManager.Write(names, female, eyes, hair, pants, shirt, shoes, skin);

                for (int i = 0; i < names.Length; i++)
                {
                    humanList.Add(new Human(width / 2, height / 2, 0, 50, 50, control, cursor, sprites, spriteBatch, female[i], pants[i], hair[i], eyes[i], shirt[i], shoes[i], skin[i]));
                }
            }
            itemList.Add(new TV(itemSprites, 60, 80, 1, new List<Human>()));
            itemList.Add(new Chair(itemSprites, 130, 80, 1));
            itemList.Add(new Chair(itemSprites, 180, 80, 7));
            itemList.Add(new Chair(itemSprites, 155, 120, 4));
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

                    if(cursor.posX < humanList[i].posX + ((sprites.humanSprites.mNoColor.Width / 8) / 2) &&
                        cursor.posX > humanList[i].posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) &&
                        cursor.posY < humanList[i].posY &&
                        cursor.posY > humanList[i].posY - sprites.humanSprites.mNoColor.Height)
                    {
                        if(control.isControllerMode && control.B)
                        {
                            humanList[i].activity = new Talk(humanList[selectedHuman], humanList[i]);
                            humanList[selectedHuman].activity = new Talk(humanList[i], humanList[selectedHuman]);
                            humanList[i].activity.Start(gameTime);
                        }
                        else if (!control.isControllerMode && control.RightMouseClick)
                        {
                            humanList[i].activity = new Talk(humanList[selectedHuman], humanList[i]);
                            humanList[selectedHuman].activity = new Talk(humanList[i], humanList[selectedHuman]);
                            humanList[i].activity.Start(gameTime);
                        }
                    }
                }
            }
            for(int i = 0; i < itemList.ToArray().Length; i++)
            {
                itemList[i].Update(gameTime);

                if (cursor.posX < itemList[i].posX + (itemList[i].Sprite.Width / 8) &&
                        cursor.posX > itemList[i].posX &&
                        cursor.posY < itemList[i].posY + (itemList[i].Sprite.Height / 2) &&
                        cursor.posY > itemList[i].posY)
                {
                    if (control.isControllerMode && control.B)
                    {
                        if (itemList[i].type == "TV")
                        {
                            humanList[selectedHuman].activity = new TVWatch(humanList[selectedHuman], (TV)itemList[i]);
                            itemList[i].humanList.Add(humanList[selectedHuman]);
                            humanList[selectedHuman].activity.Start(gameTime);
                        }
                        else if(itemList[i].type == "Chair")
                        {
                            humanList[selectedHuman].activity = new SitChair(humanList[selectedHuman], (Chair)itemList[i]);
                            itemList[i].humanList.Add(humanList[selectedHuman]);
                            humanList[selectedHuman].activity.Start(gameTime);
                        }
                    }
                    else if (!control.isControllerMode && control.RightMouseClick)
                    {
                        if (itemList[i].type == "TV")
                        {
                            humanList[selectedHuman].activity = new TVWatch(humanList[selectedHuman], (TV)itemList[i]);
                            itemList[i].humanList.Add(humanList[selectedHuman]);
                            humanList[selectedHuman].activity.Start(gameTime);
                        }
                        else if (itemList[i].type == "Chair")
                        {
                            humanList[selectedHuman].activity = new SitChair(humanList[selectedHuman], (Chair)itemList[i]);
                            itemList[i].humanList.Add(humanList[selectedHuman]);
                            humanList[selectedHuman].activity.Start(gameTime);
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
            for (int i = 0; i < itemList.ToArray().Length; i++)
            {
                itemList[i].Draw(gameTime, spriteBatch);
            }
            for (int i = 0; i < humanList.ToArray().Length; i++)
            {
                int tabColor;

                humanList[i].Draw(gameTime, height);

                if (i == 0)
                    spriteBatch.Draw(sprites.tabTop, new Vector2((sprites.statBar.Width + 30) + (sprites.tabTop.Width / 3) * i, 0), new Rectangle(1 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
                else if(i == humanList.ToArray().Length - 1)
                    spriteBatch.Draw(sprites.tabTop, new Vector2((sprites.statBar.Width + 30) + (sprites.tabTop.Width / 3) * i, 0), new Rectangle(39 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
                else
                    spriteBatch.Draw(sprites.tabTop, new Vector2((sprites.statBar.Width + 30) + (sprites.tabTop.Width / 3) * i, 0), new Rectangle(20 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
                if (selectedHuman == i)
                {
                    spriteBatch.Draw(sprites.humanSelectSprite, new Vector2(humanList[i].posX - 9, humanList[i].posY - sprites.humanSprites.mNoColor.Height - 8), Color.White);
                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5), new Rectangle(0, 0, sprites.statBar.Width, sprites.statBar.Height / 2), Color.White); //Social
                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5), new Rectangle(0, sprites.statBar.Height / 2, 2 + humanList[i].Social * 2, sprites.statBar.Height / 2), Color.White);

                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5 + sprites.statBar.Height / 2), new Rectangle(0, 0, sprites.statBar.Width, sprites.statBar.Height / 2), Color.White); //Fun
                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5 + sprites.statBar.Height / 2), new Rectangle(0, sprites.statBar.Height / 2, 2 + humanList[i].Fun * 2, sprites.statBar.Height / 2), Color.White);

                    tabColor = sprites.humanSprites.tabMNoColor.Height / 2;
                }
                else
                {
                    tabColor = 0;
                }
                if (!humanList[i].female)
                {
                    spriteBatch.Draw(sprites.humanSprites.tabMEyes, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabMNoColor.Width, sprites.humanSprites.tabMNoColor.Height / 2), humanList[i].eyes);
                    spriteBatch.Draw(sprites.humanSprites.tabMHair, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabMNoColor.Width, sprites.humanSprites.tabMNoColor.Height / 2), humanList[i].hair);
                    spriteBatch.Draw(sprites.humanSprites.tabMShirt, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabMNoColor.Width, sprites.humanSprites.tabMNoColor.Height / 2), humanList[i].shirt);
                    spriteBatch.Draw(sprites.humanSprites.tabMSkin, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabMNoColor.Width, sprites.humanSprites.tabMNoColor.Height / 2), humanList[i].skin);
                    spriteBatch.Draw(sprites.humanSprites.tabMNoColor, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabMNoColor.Width, sprites.humanSprites.tabMNoColor.Height / 2), Color.White);
                }
                else
                {
                    spriteBatch.Draw(sprites.humanSprites.tabFEyes, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabMNoColor.Width, sprites.humanSprites.tabMNoColor.Height / 2), humanList[i].eyes);
                    spriteBatch.Draw(sprites.humanSprites.tabFHair, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabMNoColor.Width, sprites.humanSprites.tabMNoColor.Height / 2), humanList[i].hair);
                    spriteBatch.Draw(sprites.humanSprites.tabFShirt, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabMNoColor.Width, sprites.humanSprites.tabMNoColor.Height / 2), humanList[i].shirt);
                    spriteBatch.Draw(sprites.humanSprites.tabFSkin, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabMNoColor.Width, sprites.humanSprites.tabMNoColor.Height / 2), humanList[i].skin);
                    spriteBatch.Draw(sprites.humanSprites.tabFNoColor, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabMNoColor.Width, sprites.humanSprites.tabMNoColor.Height / 2), Color.White);
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
