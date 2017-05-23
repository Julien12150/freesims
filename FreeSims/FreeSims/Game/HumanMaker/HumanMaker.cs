using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Technochips.FreeSims.Game;

namespace Technochips.FreeSims.Game.HumanMaker
{
    public class HumanMaker
    {
        SpriteBatch spriteBatch;
        Sprite sprites;
        Control control;
        FreeSims game1;

        Random rnd;

        int height, width;

        int humanSelection = 0;
        int buttonSelected = 0;
        int maxButton = 9;
        int colorSelected = 3;
        bool pressedHumanButton = false;
        bool pressedButtonButton = false;
        bool buttonPressed = false;
        static double keyboardTimePressedMax = 0.25;
        double keyboardTimePressed = 0;

        Keys lastKey;
		
		List<HumanStyle> style = new List<HumanStyle>();
		
        public HumanMaker(SpriteBatch spriteBatch, Sprite sprites, Control control, FreeSims game1, int width, int height)
        {
            this.spriteBatch = spriteBatch;
            this.sprites = sprites;
            this.control = control;
            this.game1 = game1;

            this.height = height;
            this.width = width;

			HumanStyle[] style;
			
			if (HMNFileManager.Read(out style))
            {
                foreach(HumanStyle i in style)
                    this.style.Add(i);
            }
            else
            {
                style = new HumanStyle[]{
            		new HumanStyle("Julian", false, new Color(0,31,255), new Color(66,33,0),1,new Color(0, 0, 56), new Color(255, 255, 255), new Color(96,96,96), new Color(255, 179, 160),2f),
            		new HumanStyle("Joe", false, new Color(22,22,22), new Color(255,155,43),1,new Color(68, 21, 0), new Color(183, 43, 0), new Color(150,150,150), new Color(183, 154, 95),1.5f),
            		new HumanStyle("Tom", false, new Color(193,193,193), new Color(40,9,0),1,new Color(0, 47, 0), new Color(22, 22, 22), new Color(56,20,0), new Color(255, 202, 191),2.5f),
            		new HumanStyle("Christine", true, new Color(255,173,255), new Color(66,33,0),1,new Color(0, 0, 56), new Color(255, 255, 255), new Color(200,0,0), new Color(255, 179, 160),2f),};

                foreach(HumanStyle i in style)
                    this.style.Add(i);

                Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}");
                Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}");

				HMNFileManager.Write(style);
            }
            rnd = new Random();
        }
        public void Draw(GameTime gameTime)
        {
			Color color = Color.White;
			if (colorSelected == 0)
				color = style[humanSelection].eyes;
			else if (colorSelected == 1)
				color = style[humanSelection].hair;
			else if (colorSelected == 2)
				color = style[humanSelection].pants;
			else if (colorSelected == 3)
				color = style[humanSelection].shirt;
			else if (colorSelected == 4)
				color = style[humanSelection].shoes;
			else if (colorSelected == 5)
				color = style[humanSelection].skin;
			else
				color = style[humanSelection].shirt;

            spriteBatch.DrawString(sprites.mainFont, style[humanSelection].name, new Vector2(50, 50), Color.Black);

            for (int i = 0; i < style.Count; i++)
            {
                int tabColor;
                if (humanSelection == i)
                    tabColor = sprites.humanSprites.tabNoColor.Height / 2;
                else
                    tabColor = 0;
				if (style.Count == 1)
					spriteBatch.Draw(sprites.tabTop, new Vector2(52 + ((sprites.tabTop.Width / 4) -1) * i, 0), new Rectangle(59 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
				else
				{
                	if (i == 0)
                	    spriteBatch.Draw(sprites.tabTop, new Vector2(52 + ((sprites.tabTop.Width / 4) -1) * i, 0), new Rectangle(1 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
                	else if (i == style.Count - 1)
                	    spriteBatch.Draw(sprites.tabTop, new Vector2(52 + ((sprites.tabTop.Width / 4) -1) * i, 0), new Rectangle(39 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
                	else
                	    spriteBatch.Draw(sprites.tabTop, new Vector2(52 + ((sprites.tabTop.Width / 4) -1) * i, 0), new Rectangle(20 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
				}
                spriteBatch.Draw(sprites.humanSprites.tabEyes, new Vector2(50 + ((sprites.tabTop.Width / 4) -1) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), style[i].eyes);
                spriteBatch.Draw(sprites.humanSprites.tabShirt, new Vector2(50 + ((sprites.tabTop.Width / 4) -1) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), style[i].shirt);
                spriteBatch.Draw(sprites.humanSprites.tabSkin, new Vector2(50 + ((sprites.tabTop.Width / 4) -1) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), style[i].skin);
                spriteBatch.Draw(sprites.humanSprites.tabNoColor, new Vector2(50 + ((sprites.tabTop.Width / 4) -1) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), Color.White);

                if (style[i].hairStyle != 0)
                {
                    spriteBatch.Draw(sprites.humanSprites.tabHair, new Vector2(50 + ((sprites.tabTop.Width / 4) -1) * i, sprites.tabTop.Height), new Rectangle((sprites.humanSprites.tabHair.Width / 2) * style[i].hairStyle - (sprites.humanSprites.tabHair.Width / 2), tabColor, sprites.humanSprites.tabHair.Width / 2, sprites.humanSprites.tabNoColor.Height / 2), style[i].hair);
                    spriteBatch.Draw(sprites.humanSprites.tabHairNoColor, new Vector2(50 + ((sprites.tabTop.Width / 4) -1) * i, sprites.tabTop.Height), new Rectangle((sprites.humanSprites.tabHair.Width / 2) * style[i].hairStyle - (sprites.humanSprites.tabHair.Width / 2), tabColor, sprites.humanSprites.tabHair.Width / 2, sprites.humanSprites.tabNoColor.Height / 2), Color.White);
                }
            }

            if (!style[humanSelection].female)
            {
                spriteBatch.Draw(sprites.humanSprites.mEyes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style[humanSelection].eyes);
                spriteBatch.Draw(sprites.humanSprites.mPants, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style[humanSelection].pants);
                spriteBatch.Draw(sprites.humanSprites.mShirt, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style[humanSelection].shirt);
                spriteBatch.Draw(sprites.humanSprites.mShoes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style[humanSelection].shoes);
                spriteBatch.Draw(sprites.humanSprites.mSkin, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style[humanSelection].skin);
                spriteBatch.Draw(sprites.humanSprites.mNoColor, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White);

                if (style[humanSelection].hairStyle != 0)
                {
                    spriteBatch.Draw(sprites.humanSprites.GetHair(style[humanSelection].hairStyle), new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style[humanSelection].hair);
                    spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(style[humanSelection].hairStyle), new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(sprites.humanSprites.fEyes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style[humanSelection].eyes);
                spriteBatch.Draw(sprites.humanSprites.fPants, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style[humanSelection].pants);
                spriteBatch.Draw(sprites.humanSprites.fShirt, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style[humanSelection].shirt);
                spriteBatch.Draw(sprites.humanSprites.fShoes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style[humanSelection].shoes);
                spriteBatch.Draw(sprites.humanSprites.fSkin, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style[humanSelection].skin);
                spriteBatch.Draw(sprites.humanSprites.fNoColor, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White);

                if (style[humanSelection].hairStyle != 0)
                {
                    spriteBatch.Draw(sprites.humanSprites.GetHair(style[humanSelection].hairStyle), new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style[humanSelection].hair);
                    spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(style[humanSelection].hairStyle), new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White);
                }
            }

            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 150), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(0xFF, color.G, color.B));
            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 150), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(0x00, color.G, color.B));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(24 + color.R - 3, 146), Color.White);

            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 150 + sprites.colorBar.Height), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, color.G, 0xFF));
            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 150 + sprites.colorBar.Height), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, color.G, 0x00));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(24 + color.B - 3, 146 + sprites.colorBar.Height), Color.White);

            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 150 + (sprites.colorBar.Height / 2)), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, 0xFF, color.B));
            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 150 + (sprites.colorBar.Height / 2)), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, 0x00, color.B));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(24 + color.G - 3, 146 + (sprites.colorBar.Height / 2)), Color.White);
            if (buttonSelected == 2)
            {
                if (colorSelected == 0)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle(0, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle(0, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 1)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle(sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle(sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 2)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 2, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 2, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 3)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 3, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 3, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 4)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 4, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 4, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 5)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 5, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 5, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
            }
            else
            {
                if (colorSelected == 0)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle(0, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle(0, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 1)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle(sprites.clothColorButton.Width / 6, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle(sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 2)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 2, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 2, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 3)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 3, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 3, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 4)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 4, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 4, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 5)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 5, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 100), new Rectangle((sprites.clothColorButton.Width / 6) * 5, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
            }

            if (buttonSelected == 0)
            {
                if (!style[humanSelection].female)
                {
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 10), new Rectangle(0, (sprites.genderButton.Height / 4) * 3, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 50), new Rectangle(sprites.genderButton.Width / 2, 0, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                }
                else
                {
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 10), new Rectangle(0, sprites.genderButton.Height / 4, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 50), new Rectangle(sprites.genderButton.Width / 2, (sprites.genderButton.Height / 4) * 2, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                }
            }
            else if (buttonSelected == 1)
            {
                if (!style[humanSelection].female)
                {
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 10), new Rectangle(0, (sprites.genderButton.Height / 4) * 2, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 50), new Rectangle(sprites.genderButton.Width / 2, sprites.genderButton.Height / 4, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                }
                else
                {
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 10), new Rectangle(0, 0, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 50), new Rectangle(sprites.genderButton.Width / 2, (sprites.genderButton.Height / 4) * 3, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                }
            }
            else
            {
                if (!style[humanSelection].female)
                {
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 10), new Rectangle(0, (sprites.genderButton.Height / 4) * 2, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 50), new Rectangle(sprites.genderButton.Width / 2, 0, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                }
                else
                {
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 10), new Rectangle(0, 0, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.genderButton, new Vector2(10, 50), new Rectangle(sprites.genderButton.Width / 2, (sprites.genderButton.Height / 4) * 2, sprites.genderButton.Width / 2, sprites.genderButton.Height / 4), Color.White);
                }
            }

            if (buttonSelected == 6)
            {
                spriteBatch.Draw(sprites.addButton, new Vector2(5, 200), new Rectangle(0, sprites.addButton.Height / 2, sprites.addButton.Width, sprites.addButton.Height / 2), Color.White);
            }
            else
            {
                spriteBatch.Draw(sprites.addButton, new Vector2(5, 200), new Rectangle(0, 0, sprites.addButton.Width, sprites.addButton.Height / 2), Color.White);
            }

            if (buttonSelected == 7)
            {
                spriteBatch.Draw(sprites.removeButton, new Vector2(5, 250), new Rectangle(0, sprites.addButton.Height / 2, sprites.addButton.Width, sprites.addButton.Height / 2), Color.White);
            }
            else
            {
                spriteBatch.Draw(sprites.removeButton, new Vector2(5, 250), new Rectangle(0, 0, sprites.addButton.Width, sprites.addButton.Height / 2), Color.White);
            }

            if (buttonSelected == 8)
            {
                spriteBatch.Draw(sprites.resetButton, new Vector2(5, 300), new Rectangle(0, sprites.addButton.Height / 2, sprites.addButton.Width, sprites.addButton.Height / 2), Color.White);
            }
            else
            {
                spriteBatch.Draw(sprites.resetButton, new Vector2(5, 300), new Rectangle(0, 0, sprites.addButton.Width, sprites.addButton.Height / 2), Color.White);
            }
            if (buttonSelected == 9)
            {
                spriteBatch.Draw(sprites.randomButton, new Vector2(5, 350), new Rectangle(0, sprites.addButton.Height / 2, sprites.addButton.Width, sprites.addButton.Height / 2), Color.White);
            }
            else
            {
                spriteBatch.Draw(sprites.randomButton, new Vector2(5, 350), new Rectangle(0, 0, sprites.addButton.Width, sprites.addButton.Height / 2), Color.White);
            }

            if (buttonSelected == 3)
            {
                spriteBatch.Draw(sprites.arrows, new Vector2(5, 150), new Rectangle((sprites.arrows.Width / 4) * 3, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
                spriteBatch.Draw(sprites.arrows, new Vector2(sprites.colorBar.Width + 15, 150), new Rectangle((sprites.arrows.Width / 4) * 1, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
            }
            else if (buttonSelected == 4)
            {
                spriteBatch.Draw(sprites.arrows, new Vector2(5, 150 + (sprites.colorBar.Height / 2)), new Rectangle((sprites.arrows.Width / 4) * 3, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
                spriteBatch.Draw(sprites.arrows, new Vector2(sprites.colorBar.Width + 15, 150 + (sprites.colorBar.Height / 2)), new Rectangle((sprites.arrows.Width / 4) * 1, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
            }
            else if (buttonSelected == 5)
            {
                spriteBatch.Draw(sprites.arrows, new Vector2(5, 150 + sprites.colorBar.Height), new Rectangle((sprites.arrows.Width / 4) * 3, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
                spriteBatch.Draw(sprites.arrows, new Vector2(sprites.colorBar.Width + 15, 150 + sprites.colorBar.Height), new Rectangle((sprites.arrows.Width / 4) * 1, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
            }
        }
        public void Update(GameTime gameTime)
        {
            if (control.OtherLeft && !pressedHumanButton)
            {
                if (humanSelection == 0)
                    humanSelection = style.Count - 1;
                else
                    humanSelection--;
                pressedHumanButton = true;
            }
            else if (control.OtherRight && !pressedHumanButton)
            {
                if (humanSelection == style.Count - 1)
                    humanSelection = 0;
                else
                    humanSelection++;
                pressedHumanButton = true;
            }
            else if (!control.OtherLeft && !control.OtherRight)
                pressedHumanButton = false;

            if (control.Up && !pressedButtonButton)
            {
                if (buttonSelected == 0)
                    buttonSelected = maxButton;
                else
                    buttonSelected--;
                pressedButtonButton = true;
            }
            else if (control.Down && !pressedButtonButton)
            {
                if (buttonSelected == maxButton)
                    buttonSelected = 0;
                else
                    buttonSelected++;
                pressedButtonButton = true;
            }
            else if (!control.Down && !control.Up)
                pressedButtonButton = false;

            if (control.GoBack)
            {
				HMNFileManager.Write(style.ToArray());

                game1.ChangeState(GameState.Menu);
                game1.humanMaker = null;
            }

            if(colorSelected == 0)
            {
                if(buttonSelected == 3)
                {
                    if(control.Right && style[humanSelection].eyes.R <= 255)
                    {
                        style[humanSelection].eyes = new Color(style[humanSelection].eyes.R + 1, style[humanSelection].eyes.G, style[humanSelection].eyes.B);
                    }
                    else if(control.Left && style[humanSelection].eyes.R >= 0)
                    {
                        style[humanSelection].eyes = new Color(style[humanSelection].eyes.R - 1, style[humanSelection].eyes.G, style[humanSelection].eyes.B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && style[humanSelection].eyes.G <= 255)
                    {
                        style[humanSelection].eyes = new Color(style[humanSelection].eyes.R, style[humanSelection].eyes.G + 1, style[humanSelection].eyes.B);
                    }
                    else if (control.Left && style[humanSelection].eyes.G >= 0)
                    {
                        style[humanSelection].eyes = new Color(style[humanSelection].eyes.R, style[humanSelection].eyes.G - 1, style[humanSelection].eyes.B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && style[humanSelection].eyes.B <= 255)
                    {
                        style[humanSelection].eyes = new Color(style[humanSelection].eyes.R, style[humanSelection].eyes.G, style[humanSelection].eyes.B + 1);
                    }
                    else if (control.Left && style[humanSelection].eyes.B >= 0)
                    {
                        style[humanSelection].eyes = new Color(style[humanSelection].eyes.R, style[humanSelection].eyes.G, style[humanSelection].eyes.B - 1);
                    }
                }
            }
            else if (colorSelected == 1)
            {
                if (buttonSelected == 3)
                {
                    if (control.Right && style[humanSelection].hair.R <= 255)
                    {
                        style[humanSelection].hair = new Color(style[humanSelection].hair.R + 1, style[humanSelection].hair.G, style[humanSelection].hair.B);
                    }
                    else if (control.Left && style[humanSelection].hair.R >= 0)
                    {
                        style[humanSelection].hair = new Color(style[humanSelection].hair.R - 1, style[humanSelection].hair.G, style[humanSelection].hair.B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && style[humanSelection].hair.G <= 255)
                    {
                        style[humanSelection].hair = new Color(style[humanSelection].hair.R, style[humanSelection].hair.G + 1, style[humanSelection].hair.B);
                    }
                    else if (control.Left && style[humanSelection].hair.G >= 0)
                    {
                        style[humanSelection].hair = new Color(style[humanSelection].hair.R, style[humanSelection].hair.G - 1, style[humanSelection].hair.B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && style[humanSelection].hair.B <= 255)
                    {
                        style[humanSelection].hair = new Color(style[humanSelection].hair.R, style[humanSelection].hair.G, style[humanSelection].hair.B + 1);
                    }
                    else if (control.Left && style[humanSelection].hair.B >= 0)
                    {
                        style[humanSelection].hair = new Color(style[humanSelection].hair.R, style[humanSelection].hair.G, style[humanSelection].hair.B - 1);
                    }
                }
            }
            else if (colorSelected == 2)
            {
                if (buttonSelected == 3)
                {
                    if (control.Right && style[humanSelection].pants.R <= 255)
                    {
                        style[humanSelection].pants = new Color(style[humanSelection].pants.R + 1, style[humanSelection].pants.G, style[humanSelection].pants.B);
                    }
                    else if (control.Left && style[humanSelection].pants.R >= 0)
                    {
                        style[humanSelection].pants = new Color(style[humanSelection].pants.R - 1, style[humanSelection].pants.G, style[humanSelection].pants.B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && style[humanSelection].pants.G <= 255)
                    {
                        style[humanSelection].pants = new Color(style[humanSelection].pants.R, style[humanSelection].pants.G + 1, style[humanSelection].pants.B);
                    }
                    else if (control.Left && style[humanSelection].pants.G >= 0)
                    {
                        style[humanSelection].pants = new Color(style[humanSelection].pants.R, style[humanSelection].pants.G - 1, style[humanSelection].pants.B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && style[humanSelection].pants.B <= 255)
                    {
                        style[humanSelection].pants = new Color(style[humanSelection].pants.R, style[humanSelection].pants.G, style[humanSelection].pants.B + 1);
                    }
                    else if (control.Left && style[humanSelection].pants.B >= 0)
                    {
                        style[humanSelection].pants = new Color(style[humanSelection].pants.R, style[humanSelection].pants.G, style[humanSelection].pants.B - 1);
                    }
                }
            }
            else if (colorSelected == 3)
            {
                if (buttonSelected == 3)
                {
                    if (control.Right && style[humanSelection].shirt.R <= 255)
                    {
                        style[humanSelection].shirt = new Color(style[humanSelection].shirt.R + 1, style[humanSelection].shirt.G, style[humanSelection].shirt.B);
                    }
                    else if (control.Left && style[humanSelection].shirt.R >= 0)
                    {
                        style[humanSelection].shirt = new Color(style[humanSelection].shirt.R - 1, style[humanSelection].shirt.G, style[humanSelection].shirt.B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && style[humanSelection].shirt.G <= 255)
                    {
                        style[humanSelection].shirt = new Color(style[humanSelection].shirt.R, style[humanSelection].shirt.G + 1, style[humanSelection].shirt.B);
                    }
                    else if (control.Left && style[humanSelection].shirt.G >= 0)
                    {
                        style[humanSelection].shirt = new Color(style[humanSelection].shirt.R, style[humanSelection].shirt.G - 1, style[humanSelection].shirt.B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && style[humanSelection].shirt.B <= 255)
                    {
                        style[humanSelection].shirt = new Color(style[humanSelection].shirt.R, style[humanSelection].shirt.G, style[humanSelection].shirt.B + 1);
                    }
                    else if (control.Left && style[humanSelection].shirt.B >= 0)
                    {
                        style[humanSelection].shirt = new Color(style[humanSelection].shirt.R, style[humanSelection].shirt.G, style[humanSelection].shirt.B - 1);
                    }
                }
            }
            else if (colorSelected == 4)
            {
                if (buttonSelected == 3)
                {
                    if (control.Right && style[humanSelection].shoes.R <= 255)
                    {
                        style[humanSelection].shoes = new Color(style[humanSelection].shoes.R + 1, style[humanSelection].shoes.G, style[humanSelection].shoes.B);
                    }
                    else if (control.Left && style[humanSelection].shoes.R >= 0)
                    {
                        style[humanSelection].shoes = new Color(style[humanSelection].shoes.R - 1, style[humanSelection].shoes.G, style[humanSelection].shoes.B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && style[humanSelection].shoes.G <= 255)
                    {
                        style[humanSelection].shoes = new Color(style[humanSelection].shoes.R, style[humanSelection].shoes.G + 1, style[humanSelection].shoes.B);
                    }
                    else if (control.Left && style[humanSelection].shoes.G >= 0)
                    {
                        style[humanSelection].shoes = new Color(style[humanSelection].shoes.R, style[humanSelection].shoes.G - 1, style[humanSelection].shoes.B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && style[humanSelection].shoes.B <= 255)
                    {
                        style[humanSelection].shoes = new Color(style[humanSelection].shoes.R, style[humanSelection].shoes.G, style[humanSelection].shoes.B + 1);
                    }
                    else if (control.Left && style[humanSelection].shoes.B >= 0)
                    {
                        style[humanSelection].shoes = new Color(style[humanSelection].shoes.R, style[humanSelection].shoes.G, style[humanSelection].shoes.B - 1);
                    }
                }
            }
            else if (colorSelected == 5)
            {
                if (buttonSelected == 3)
                {
                    if (control.Right && style[humanSelection].skin.R <= 255)
                    {
                        style[humanSelection].skin = new Color(style[humanSelection].skin.R + 1, style[humanSelection].skin.G, style[humanSelection].skin.B);
                    }
                    else if (control.Left && style[humanSelection].skin.R >= 0)
                    {
                        style[humanSelection].skin = new Color(style[humanSelection].skin.R - 1, style[humanSelection].skin.G, style[humanSelection].skin.B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && style[humanSelection].skin.G <= 255)
                    {
                        style[humanSelection].skin = new Color(style[humanSelection].skin.R, style[humanSelection].skin.G + 1, style[humanSelection].skin.B);
                    }
                    else if (control.Left && style[humanSelection].skin.G >= 0)
                    {
                        style[humanSelection].skin = new Color(style[humanSelection].skin.R, style[humanSelection].skin.G - 1, style[humanSelection].skin.B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && style[humanSelection].skin.B <= 255)
                    {
                        style[humanSelection].skin = new Color(style[humanSelection].skin.R, style[humanSelection].skin.G, style[humanSelection].skin.B + 1);
                    }
                    else if (control.Left && style[humanSelection].skin.B >= 0)
                    {
                        style[humanSelection].skin = new Color(style[humanSelection].skin.R, style[humanSelection].skin.G, style[humanSelection].skin.B - 1);
                    }
                }
            }

            if(buttonSelected == 2)
            {
                if (control.Enter && !buttonPressed)
                {
                    if (colorSelected == 5)
                        colorSelected = 0;
                    else
                        colorSelected++;
                    buttonPressed = true;
                }
                else if (!control.Enter)
                    buttonPressed = false;
            }

            if(buttonSelected == 0)
            {
                if (control.Enter && !buttonPressed)
                {
                    style[humanSelection].female = false;
                    style[humanSelection].hairStyle = 1;
                    buttonPressed = true;
                }
                else if (!control.Enter)
                    buttonPressed = false;
            }
            else if (buttonSelected == 1)
            {
                if (control.Enter && !buttonPressed)
                {
                    style[humanSelection].female = true;
                    style[humanSelection].hairStyle = 2;
                    buttonPressed = true;
                }
                else if (!control.Enter)
                    buttonPressed = false;
            }

            if(buttonSelected == 6)
            {
                if (control.Enter && !buttonPressed)
                {
                    int newNum = style.Count + 1;
                    style.Add(new HumanStyle("Human " + newNum, false, new Color(0, 31, 255), new Color(66, 33, 0), 1, new Color(0, 0, 56), Color.White, new Color(96, 96, 96), new Color(255, 179, 160), 2));
                    humanSelection = style.Count - 1;
                    buttonPressed = true;
                }
                else if (!control.Enter)
                    buttonPressed = false;
            }
            else if(buttonSelected == 7)
            {
                if (control.Enter && !buttonPressed)
                {
					if (humanSelection > 0)
					    if (style.Count > 0)
					    {
						    style.RemoveAt(humanSelection);
				    		humanSelection--;
					    }
                    buttonPressed = true;
                }
                else if (!control.Enter)
                    buttonPressed = false;
            }
            else if(buttonSelected == 8)
            {
                if (control.Enter && !buttonPressed)
                {
                    HumanStyle[] style = new HumanStyle[]{
            		new HumanStyle("Julian", false, new Color(0,31,255), new Color(66,33,0),1,new Color(0, 0, 56), new Color(255, 255, 255), new Color(96,96,96), new Color(255, 179, 160),2f),
            		new HumanStyle("Joe", false, new Color(22,22,22), new Color(255,155,43),1,new Color(68, 21, 0), new Color(183, 43, 0), new Color(150,150,150), new Color(183, 154, 95),1.5f),
            		new HumanStyle("Tom", false, new Color(193,193,193), new Color(40,9,0),1,new Color(0, 47, 0), new Color(22, 22, 22), new Color(56,20,0), new Color(255, 202, 191),2.5f),
            		new HumanStyle("Christine", true, new Color(255,173,255), new Color(66,33,0),1,new Color(0, 0, 56), new Color(255, 255, 255), new Color(200,0,0), new Color(255, 179, 160),2f),};
                    this.style = new List<HumanStyle>();

                    for (int i = 0; i < style.Length; i++)
                    {
                        this.style.Add(style[i]);
                    }
                    buttonPressed = true;
                }
                else if (!control.Enter)
                    buttonPressed = false;
            }
            else if(buttonSelected == 9)
            {
                if (control.Enter && !buttonPressed)
                {
                    int ifemale = rnd.Next(0, 2);
                    bool female = false;
                    if (ifemale == 0)
                        female = false;
                    else
                        female = true;

                    Color pants = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                    Color hair = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                    int hairStyle = rnd.Next(0, sprites.humanSprites.haircutNumber + 1);
                    Color eyes = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                    Color shirt = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                    Color shoes = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                    Color skin = new Color(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
					float walkSpeed = Convert.ToSingle(rnd.NextDouble()) + 1.5f;

                    this.style[humanSelection].female = female;
                    this.style[humanSelection].pants = pants;
                    this.style[humanSelection].hair = hair;
                    this.style[humanSelection].hairStyle = hairStyle;
                    this.style[humanSelection].eyes = eyes;
                    this.style[humanSelection].shirt = shirt;
                    this.style[humanSelection].shoes = shoes;
                    this.style[humanSelection].skin = skin;
					this.style[humanSelection].walkSpeed = walkSpeed;
                    buttonPressed = true;
                }
                else if (!control.Enter)
                    buttonPressed = false;
            }

            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                if (Keyboard.GetState().GetPressedKeys()[Keyboard.GetState().GetPressedKeys().Length - 1] != lastKey)
                    keyboardTimePressed = 0;
                keyboardTimePressed -= gameTime.ElapsedGameTime.TotalSeconds;
                if (keyboardTimePressed < 0)
                {
                    keyboardTimePressed = keyboardTimePressedMax;
                    if (Keyboard.GetState().IsKeyDown(Keys.Back))
                    {
                        if(style[humanSelection].name.Length > 0)
                            style[humanSelection].name = style[humanSelection].name.Remove(style[humanSelection].name.Length - 1);
                    }
                    else if(!Keyboard.GetState().IsKeyDown(Keys.LeftShift) || !Keyboard.GetState().IsKeyDown(Keys.RightShift))
                    {
                        char? c = Extra.GetChar(Keyboard.GetState().GetPressedKeys()[Keyboard.GetState().GetPressedKeys().Length - 1]);
                        if (c != null)
                            style[humanSelection].name += c;
                    }
                    else if(Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                    {
                        char? c = Extra.GetChar(Keyboard.GetState().GetPressedKeys()[Keyboard.GetState().GetPressedKeys().Length - 1]);
                        if (c != null)
                            style[humanSelection].name += c;
                    }
                }
                lastKey = Keyboard.GetState().GetPressedKeys()[Keyboard.GetState().GetPressedKeys().Length - 1];
            }
            else if(Keyboard.GetState().GetPressedKeys().Length == 0)
                keyboardTimePressed = 0;
        }
    }
}
