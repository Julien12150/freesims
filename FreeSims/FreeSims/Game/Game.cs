using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Technochips.FreeSims.Game.Activity;
using Technochips.FreeSims.Game.Entity;
using Technochips.FreeSims.Game.Entity.Item;
using Technochips.FreeSims.Game.HumanMaker;
using System;
using System.Linq;

namespace Technochips.FreeSims.Game
{
    public class Game
    {
        List<Human> humanList = new List<Human>();
        List<Item> itemList = new List<Item>();

		Vector2 size;

        int selectedHuman = 0;

        int height;

        string lastLog = "";

        Control control;
        SpriteBatch spriteBatch;
        Sprite sprites;
        ItemSprite itemSprites;
        Cursor cursor;
        Language language;

        bool pressedButton = false;

        FreeSims mainClass;

		Vector2 camera = Vector2.Zero;

        public Game(int width, int height, Control control, Cursor cursor, SpriteBatch spriteBatch, Sprite sprites, ItemSprite itemSprites, Language language, FreeSims mainClass)
        {
            this.control = control;
            this.height = height;
            this.spriteBatch = spriteBatch;
            this.sprites = sprites;
            this.cursor = cursor;
            this.itemSprites = itemSprites;
            this.language = language;
            this.mainClass = mainClass;

			size = new Vector2(width, height);
			
			HumanStyle[] style;

			itemList.Add(new TV(itemSprites, 100, 150, 0, 1, new List<Human>(), mainClass.GraphicsDevice));
			itemList.Add(new Chair(itemSprites, 200, 160, 0, 1, mainClass.GraphicsDevice));
			itemList.Add(new Chair(itemSprites, 240, 160, 0, 7, mainClass.GraphicsDevice));
			itemList.Add(new Table(itemSprites, 200, 180, 0, 1, mainClass.GraphicsDevice));
			itemList.Add(new Fridge(itemSprites, 400, 180, 0, 7, new List<Human>(), mainClass.GraphicsDevice));
			
			if (HMNFileManager.Read(out style))
            {
                for(int i = 0; i < style.Length; i++)
                {
					humanList.Add(new Human(width / 2, height / 2, 0, 0, 50, 50, 25, false, control, cursor, sprites, spriteBatch, style[i], i, itemList.ToArray(), mainClass.GraphicsDevice));
                }
            }
            else
            {
            	style = new HumanStyle[]{
            		new HumanStyle("Julian", false, new Color(0,31,255), new Color(66,33,0),1,new Color(0, 0, 56), new Color(255, 255, 255), new Color(96,96,96), new Color(255, 179, 160),2f),
            		new HumanStyle("Joe", false, new Color(22,22,22), new Color(255,155,43),1,new Color(68, 21, 0), new Color(183, 43, 0), new Color(150,150,150), new Color(183, 154, 95),1.5f),
            		new HumanStyle("Tom", false, new Color(193,193,193), new Color(40,9,0),1,new Color(0, 47, 0), new Color(22, 22, 22), new Color(56,20,0), new Color(255, 202, 191),2.5f),
            		new HumanStyle("Christine", true, new Color(255,173,255), new Color(66,33,0),1,new Color(0, 0, 56), new Color(255, 255, 255), new Color(200,0,0), new Color(255, 179, 160),2f),};

				Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}");
				Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}");

				HMNFileManager.Write(style);

                for (int i = 0; i < style.Length; i++)
                {
					humanList.Add(new Human(width / 2, height / 2, 0, 0, 50, 50, 50, false, control, cursor, sprites, spriteBatch, style[i], i, itemList.ToArray(), mainClass.GraphicsDevice));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
			cursor.Update(gameTime, camera);

            bool isEmpty = true;
            foreach(Human h in humanList)
            {
                isEmpty = false;
                break;
            }

            if (isEmpty)
            {
                mainClass.state = GameState.Menu;
                mainClass.game = null;
                return;
            }

            for (int i = 0; i < humanList.ToArray().Length; i++)
            {
				humanList[i].Update(gameTime, camera);

				if (humanList[i].Hunger < 75)
				{
					
				}

                if (selectedHuman == i)
                {
                    humanList[i].selected = true;
                }
                else
                {
                    humanList[i].selected = false;

					if (cursor.posX < humanList[i].posX + ((sprites.humanSprites.mNoColor.Width / 8) / 2) &&
					     cursor.posX > humanList[i].posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) &&
					     cursor.posY < humanList[i].posY &&
					     cursor.posY > humanList[i].posY - (sprites.humanSprites.mNoColor.Height / 2))
                    {
                        if (control.isControllerMode && control.B)
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

                if(humanList[i].Hunger <= 0)
                {
                    if (!humanList[i].cannotDie)
                    {
                        lastLog = Language.GetNewString(language.log_humandied, new Dictionary<char, string>() { { 'n', humanList[i].style.name } });
                        humanList.Remove(humanList[i]);
                        if (selectedHuman == i && i == humanList.ToArray().Length - 1)
                            selectedHuman--;
                    }
                }
            }
            for(int i = 0; i < itemList.ToArray().Length; i++)
            {
				itemList[i].Update(gameTime, camera);

				if (cursor.posX < itemList[i].posX + (itemList[i].Sprite.Width / 8) &&
					cursor.posX > itemList[i].posX &&
				    cursor.posY < (itemList[i].posY - itemList[i].Sprite.Height) + (itemList[i].Sprite.Height / 2) &&
				    cursor.posY > (itemList[i].posY - itemList[i].Sprite.Height))
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
                        else if(itemList[i].type == "Fridge")
                        {
                            humanList[selectedHuman].activity = new Eat(humanList[selectedHuman], (Fridge)itemList[i]);
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
                        else if (itemList[i].type == "Fridge")
                        {
                            humanList[selectedHuman].activity = new Eat(humanList[selectedHuman], (Fridge)itemList[i]);
							if (itemList[i].humanList == null)
								itemList[i].humanList = new List<Human>();
                            itemList[i].humanList.Add(humanList[selectedHuman]);
                            humanList[selectedHuman].activity.Start(gameTime);
                        }
                    }
                }
            }

			if (control.isControllerMode)
			{
				camera.X += control.RightStickX * 4;
				camera.Y -= control.RightStickY * 4;
			}
			else
			{
				if (Keyboard.GetState().IsKeyDown(Keys.D))
					camera.X += 4;
				if (Keyboard.GetState().IsKeyDown(Keys.S))
					camera.Y += 4;
				if (Keyboard.GetState().IsKeyDown(Keys.A))
					camera.X -= 4;
				if (Keyboard.GetState().IsKeyDown(Keys.W))
					camera.Y -= 4;
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

        public void Draw(GameTime gameTime, GraphicsDevice gd)
        {
            /*for (int i = 0; i < itemList.ToArray().Length; i++)
            {
                itemList[i].Draw(gameTime, spriteBatch);
            }*/

			List<Entity.Entity> e = new List<Entity.Entity>();

			e.AddRange(humanList);
			e.AddRange(itemList);

			var es = e.OrderBy(human => human.posY);

			foreach (Entity.Entity ee in es)
			{
				ee.Draw(gameTime, spriteBatch, camera);
			}

            for (int i = 0; i < humanList.ToArray().Length; i++)
            {
                int tabColor;

                if (i == 0)
                    spriteBatch.Draw(sprites.tabTop, new Vector2((sprites.statBar.Width + 30) + (sprites.tabTop.Width / 3) * i, 0), new Rectangle(1 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
                else if (i == humanList.ToArray().Length - 1)
                    spriteBatch.Draw(sprites.tabTop, new Vector2((sprites.statBar.Width + 30) + (sprites.tabTop.Width / 3) * i, 0), new Rectangle(39 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
                else
                    spriteBatch.Draw(sprites.tabTop, new Vector2((sprites.statBar.Width + 30) + (sprites.tabTop.Width / 3) * i, 0), new Rectangle(20 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
                if (selectedHuman == i)
                {
					spriteBatch.Draw(sprites.humanSelectSprite, new Vector2((humanList[i].posX - 9) - camera.X, humanList[i].posY - ((sprites.humanSprites.mNoColor.Height / 2) - 8) - camera.Y), Color.White);
                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5), new Rectangle(0, 0, sprites.statBar.Width, sprites.statBar.Height / 2), Color.White); //Social
                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5), new Rectangle(0, sprites.statBar.Height / 2, 2 + humanList[i].Social * 2, sprites.statBar.Height / 2), Color.White);

                    if (humanList[i].Social > 75)
                        spriteBatch.Draw(sprites.statIcon, new Vector2(212, 5), new Rectangle(0, 0, sprites.statIcon.Width / 3, sprites.statIcon.Height / 3), Color.White);
                    else if (humanList[i].Social <= 75 && humanList[i].Social > 25)
                        spriteBatch.Draw(sprites.statIcon, new Vector2(212, 5), new Rectangle(0, sprites.statIcon.Height / 3, sprites.statIcon.Width / 3, sprites.statIcon.Height / 3), Color.White);
                    else
                        spriteBatch.Draw(sprites.statIcon, new Vector2(212, 5), new Rectangle(0, (sprites.statIcon.Height / 3) + (sprites.statIcon.Height / 3), sprites.statIcon.Width / 3, sprites.statIcon.Height / 3), Color.White);

                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5 + sprites.statBar.Height / 2), new Rectangle(0, 0, sprites.statBar.Width, sprites.statBar.Height / 2), Color.White); //Fun
                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5 + sprites.statBar.Height / 2), new Rectangle(0, sprites.statBar.Height / 2, 2 + humanList[i].Fun * 2, sprites.statBar.Height / 2), Color.White);


                    if (humanList[i].Fun > 75)
                        spriteBatch.Draw(sprites.statIcon, new Vector2(212, 5 + sprites.statBar.Height / 2), new Rectangle(sprites.statIcon.Width / 3, 0, sprites.statIcon.Width / 3, sprites.statIcon.Height / 3), Color.White);
                    else if (humanList[i].Fun <= 75 && humanList[i].Fun > 25)
                        spriteBatch.Draw(sprites.statIcon, new Vector2(212, 5 + sprites.statBar.Height / 2), new Rectangle(sprites.statIcon.Width / 3, sprites.statIcon.Height / 3, sprites.statIcon.Width / 3, sprites.statIcon.Height / 3), Color.White);
                    else
                        spriteBatch.Draw(sprites.statIcon, new Vector2(212, 5 + sprites.statBar.Height / 2), new Rectangle(sprites.statIcon.Width / 3, (sprites.statIcon.Height / 3) + (sprites.statIcon.Height / 3), sprites.statIcon.Width / 3, sprites.statIcon.Height / 3), Color.White);

                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5 + sprites.statBar.Height), new Rectangle(0, 0, sprites.statBar.Width, sprites.statBar.Height / 2), Color.White); //Hunger
                    spriteBatch.Draw(sprites.statBar, new Vector2(5, 5 + sprites.statBar.Height), new Rectangle(0, sprites.statBar.Height / 2, 2 + humanList[i].Hunger * 2, sprites.statBar.Height / 2), Color.White);

                    if (humanList[i].Hunger > 75)
                        spriteBatch.Draw(sprites.statIcon, new Vector2(212, 5 + sprites.statBar.Height), new Rectangle((sprites.statIcon.Width / 3) + (sprites.statIcon.Width / 3), 0, sprites.statIcon.Width / 3, sprites.statIcon.Height / 3), Color.White);
                    else if (humanList[i].Hunger <= 75 && humanList[i].Hunger > 25)
                        spriteBatch.Draw(sprites.statIcon, new Vector2(212, 5 + sprites.statBar.Height), new Rectangle((sprites.statIcon.Width / 3) + (sprites.statIcon.Width / 3), sprites.statIcon.Height / 3, sprites.statIcon.Width / 3, sprites.statIcon.Height / 3), Color.White);
                    else
                        spriteBatch.Draw(sprites.statIcon, new Vector2(212, 5 + sprites.statBar.Height), new Rectangle((sprites.statIcon.Width / 3) + (sprites.statIcon.Width / 3), (sprites.statIcon.Height / 3) + (sprites.statIcon.Height / 3), sprites.statIcon.Width / 3, sprites.statIcon.Height / 3), Color.White);

                    tabColor = sprites.humanSprites.tabNoColor.Height / 2;
                }
                else
                {
                    tabColor = 0;
                }
				spriteBatch.Draw(sprites.humanSprites.tabEyes, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), humanList[i].style.eyes);
				spriteBatch.Draw(sprites.humanSprites.tabShirt, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), humanList[i].style.shirt);
				spriteBatch.Draw(sprites.humanSprites.tabSkin, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), humanList[i].style.skin);
				spriteBatch.Draw(sprites.humanSprites.tabNoColor, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), Color.White);

				if (humanList[i].style.hairStyle != 0)
				{
					spriteBatch.Draw(sprites.humanSprites.tabHair, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle((sprites.humanSprites.tabHair.Width / 2) * humanList[i].style.hairStyle - (sprites.humanSprites.tabHair.Width / 2), tabColor, sprites.humanSprites.tabHair.Width / 2, sprites.humanSprites.tabNoColor.Height / 2), humanList[i].style.hair);
					spriteBatch.Draw(sprites.humanSprites.tabHairNoColor, new Vector2((sprites.statBar.Width + 28) + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle((sprites.humanSprites.tabHairNoColor.Width / 2) * humanList[i].style.hairStyle - (sprites.humanSprites.tabHairNoColor.Width / 2), tabColor, sprites.humanSprites.tabHairNoColor.Width / 2, sprites.humanSprites.tabNoColor.Height / 2), Color.White);
				}
            }

            spriteBatch.DrawString(sprites.mainFont, lastLog, new Vector2(0, height - 50), Color.Black, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
			if (control.isControllerMode)
				cursor.Draw(gameTime, spriteBatch, camera);
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
