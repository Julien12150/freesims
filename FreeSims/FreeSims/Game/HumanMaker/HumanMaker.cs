using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        List<string> names = new List<string>();

        List<bool> female = new List<bool>();
        List<Color> eyes = new List<Color>();
        List<Color> hair = new List<Color>();
        List<int> hairStyle = new List<int>();
        List<Color> pants = new List<Color>();
        List<Color> shirt = new List<Color>();
        List<Color> shoes = new List<Color>();
        List<Color> skin = new List<Color>();
		List<float> walkSpeed = new List<float>();
        public HumanMaker(SpriteBatch spriteBatch, Sprite sprites, Control control, FreeSims game1, int width, int height)
        {
            this.spriteBatch = spriteBatch;
            this.sprites = sprites;
            this.control = control;
            this.game1 = game1;

            this.height = height;
            this.width = width;

            string[] names;
            bool[] female;
            Color[] eyes;
            Color[] hair;
            int[] hairStyle;
            Color[] pants;
            Color[] shirt;
            Color[] shoes;
            Color[] skin;
			float[] walkSpeed;
			if (HMNFileManager.Read(out names, out female, out eyes, out hair, out hairStyle, out pants, out shirt, out shoes, out skin, out walkSpeed))
            {
                for (int i = 0; i < names.Length; i++)
                {
                    this.names.Add(names[i]);
                    this.female.Add(female[i]);
                    this.eyes.Add(eyes[i]);
                    this.hair.Add(hair[i]);
                    this.hairStyle.Add(hairStyle[i]);
                    this.pants.Add(pants[i]);
                    this.shirt.Add(shirt[i]);
                    this.shoes.Add(shoes[i]);
                    this.skin.Add(skin[i]);
					this.walkSpeed.Add(walkSpeed[i]);
                }
            }
            else
            {
                names = new string[] { "Julian", "Joe", "Tom", "Christine" };
                female = new bool[] { false, false, false, true };
                pants = new Color[] { new Color(0, 31, 255), new Color(22, 22, 22), new Color(193, 193, 193), new Color(255, 173, 255) };
                hair = new Color[] { new Color(66, 33, 0), new Color(255, 155, 43), new Color(40, 9, 0), new Color(66, 33, 0) };
                hairStyle = new int[] { 1, 1, 1, 2 };
                eyes = new Color[] { new Color(0, 0, 56), new Color(68, 21, 0), new Color(0, 47, 0), new Color(0, 0, 56) };
                shirt = new Color[] { new Color(255, 255, 255), new Color(183, 43, 0), new Color(22, 22, 22), new Color(255, 255, 255) };
                shoes = new Color[] { new Color(96, 96, 96), new Color(150, 150, 150), new Color(56, 20, 0), new Color(200, 0, 0) };
                skin = new Color[] { new Color(255, 179, 160), new Color(183, 124, 95), new Color(255, 202, 191), new Color(255, 179, 160) };
				walkSpeed = new float[] { 2, 1.5f, 2.5f, 2 };

                for (int i = 0; i < names.Length; i++)
                {
                    this.names.Add(names[i]);
                    this.female.Add(female[i]);
                    this.pants.Add(pants[i]);
                    this.hair.Add(hair[i]);
                    this.eyes.Add(eyes[i]);
                    this.shirt.Add(shirt[i]);
                    this.shoes.Add(shoes[i]);
                    this.skin.Add(skin[i]);
					this.walkSpeed.Add(walkSpeed[i]);
                }

                Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}");
                Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}");

				HMNFileManager.Write(names, female, eyes, hair, hairStyle, pants, shirt, shoes, skin, walkSpeed);
            }
            rnd = new Random();
        }
        public void Draw(GameTime gameTime)
        {
			Color color = Color.White;
			if (colorSelected == 0)
				color = eyes[humanSelection];
			else if (colorSelected == 1)
				color = hair[humanSelection];
			else if (colorSelected == 2)
				color = pants[humanSelection];
			else if (colorSelected == 3)
				color = shirt[humanSelection];
			else if (colorSelected == 4)
				color = shoes[humanSelection];
			else if (colorSelected == 5)
				color = skin[humanSelection];
			else
				color = shirt[humanSelection];

            spriteBatch.DrawString(sprites.mainFont, names[humanSelection], new Vector2(50, 50), Color.Black);

            for (int i = 0; i < names.ToArray().Length; i++)
            {
                int tabColor;
                if (humanSelection == i)
                    tabColor = sprites.humanSprites.tabNoColor.Height / 2;
                else
                    tabColor = 0;

                if (i == 0)
                    spriteBatch.Draw(sprites.tabTop, new Vector2(52 + (sprites.tabTop.Width / 3) * i, 0), new Rectangle(1 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
                else if (i == names.ToArray().Length - 1)
                    spriteBatch.Draw(sprites.tabTop, new Vector2(52 + (sprites.tabTop.Width / 3) * i, 0), new Rectangle(39 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);
                else
                    spriteBatch.Draw(sprites.tabTop, new Vector2(52 + (sprites.tabTop.Width / 3) * i, 0), new Rectangle(20 * 2, 0, 19 * 2, sprites.tabTop.Height), Color.White);

                spriteBatch.Draw(sprites.humanSprites.tabEyes, new Vector2(50 + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), eyes[i]);
                spriteBatch.Draw(sprites.humanSprites.tabShirt, new Vector2(50 + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), shirt[i]);
                spriteBatch.Draw(sprites.humanSprites.tabSkin, new Vector2(50 + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), skin[i]);
                spriteBatch.Draw(sprites.humanSprites.tabNoColor, new Vector2(50 + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle(0, tabColor, sprites.humanSprites.tabNoColor.Width, sprites.humanSprites.tabNoColor.Height / 2), Color.White);

                if (hairStyle[i] != 0)
                {
                    spriteBatch.Draw(sprites.humanSprites.tabHair, new Vector2(50 + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle((sprites.humanSprites.tabHair.Width / 2) * hairStyle[i] - (sprites.humanSprites.tabHair.Width / 2), tabColor, sprites.humanSprites.tabHair.Width / 2, sprites.humanSprites.tabNoColor.Height / 2), hair[i]);
                    spriteBatch.Draw(sprites.humanSprites.tabHairNoColor, new Vector2(50 + (sprites.tabTop.Width / 3) * i, sprites.tabTop.Height), new Rectangle((sprites.humanSprites.tabHair.Width / 2) * hairStyle[i] - (sprites.humanSprites.tabHair.Width / 2), tabColor, sprites.humanSprites.tabHair.Width / 2, sprites.humanSprites.tabNoColor.Height / 2), Color.White);
                }
            }

            if (!female[humanSelection])
            {
                spriteBatch.Draw(sprites.humanSprites.mEyes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), eyes[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mPants, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), pants[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mShirt, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), shirt[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mShoes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), shoes[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mSkin, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), skin[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mNoColor, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White);

                if (hairStyle[humanSelection] != 0)
                {
                    spriteBatch.Draw(sprites.humanSprites.GetHair(hairStyle[humanSelection]), new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), hair[humanSelection]);
                    spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(hairStyle[humanSelection]), new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(sprites.humanSprites.fEyes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), eyes[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fPants, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), pants[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fShirt, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), shirt[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fShoes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), shoes[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fSkin, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), skin[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fNoColor, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White);

                if (hairStyle[humanSelection] != 0)
                {
                    spriteBatch.Draw(sprites.humanSprites.GetHair(hairStyle[humanSelection]), new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), hair[humanSelection]);
                    spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(hairStyle[humanSelection]), new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White);
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
                if (!female[humanSelection])
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
                if (!female[humanSelection])
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
                if (!female[humanSelection])
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
                    humanSelection = names.ToArray().Length - 1;
                else
                    humanSelection--;
                pressedHumanButton = true;
            }
            else if (control.OtherRight && !pressedHumanButton)
            {
                if (humanSelection == names.ToArray().Length - 1)
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
				HMNFileManager.Write(names.ToArray(), female.ToArray(), eyes.ToArray(), hair.ToArray(), hairStyle.ToArray(), pants.ToArray(), shirt.ToArray(), shoes.ToArray(), skin.ToArray(), walkSpeed.ToArray());

                game1.ChangeState(GameState.Menu);
                game1.humanMaker = null;
            }

            if(colorSelected == 0)
            {
                if(buttonSelected == 3)
                {
                    if(control.Right && eyes[humanSelection].R <= 255)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R + 1, eyes[humanSelection].G, eyes[humanSelection].B);
                    }
                    else if(control.Left && eyes[humanSelection].R >= 0)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R - 1, eyes[humanSelection].G, eyes[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && eyes[humanSelection].G <= 255)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R, eyes[humanSelection].G + 1, eyes[humanSelection].B);
                    }
                    else if (control.Left && eyes[humanSelection].G >= 0)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R, eyes[humanSelection].G - 1, eyes[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && eyes[humanSelection].B <= 255)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R, eyes[humanSelection].G, eyes[humanSelection].B + 1);
                    }
                    else if (control.Left && eyes[humanSelection].B >= 0)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R, eyes[humanSelection].G, eyes[humanSelection].B - 1);
                    }
                }
            }
            else if (colorSelected == 1)
            {
                if (buttonSelected == 3)
                {
                    if (control.Right && hair[humanSelection].R <= 255)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R + 1, hair[humanSelection].G, hair[humanSelection].B);
                    }
                    else if (control.Left && hair[humanSelection].R >= 0)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R - 1, hair[humanSelection].G, hair[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && hair[humanSelection].G <= 255)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R, hair[humanSelection].G + 1, hair[humanSelection].B);
                    }
                    else if (control.Left && hair[humanSelection].G >= 0)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R, hair[humanSelection].G - 1, hair[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && hair[humanSelection].B <= 255)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R, hair[humanSelection].G, hair[humanSelection].B + 1);
                    }
                    else if (control.Left && hair[humanSelection].B >= 0)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R, hair[humanSelection].G, hair[humanSelection].B - 1);
                    }
                }
            }
            else if (colorSelected == 2)
            {
                if (buttonSelected == 3)
                {
                    if (control.Right && pants[humanSelection].R <= 255)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R + 1, pants[humanSelection].G, pants[humanSelection].B);
                    }
                    else if (control.Left && pants[humanSelection].R >= 0)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R - 1, pants[humanSelection].G, pants[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && pants[humanSelection].G <= 255)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R, pants[humanSelection].G + 1, pants[humanSelection].B);
                    }
                    else if (control.Left && pants[humanSelection].G >= 0)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R, pants[humanSelection].G - 1, pants[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && pants[humanSelection].B <= 255)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R, pants[humanSelection].G, pants[humanSelection].B + 1);
                    }
                    else if (control.Left && pants[humanSelection].B >= 0)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R, pants[humanSelection].G, pants[humanSelection].B - 1);
                    }
                }
            }
            else if (colorSelected == 3)
            {
                if (buttonSelected == 3)
                {
                    if (control.Right && shirt[humanSelection].R <= 255)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R + 1, shirt[humanSelection].G, shirt[humanSelection].B);
                    }
                    else if (control.Left && shirt[humanSelection].R >= 0)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R - 1, shirt[humanSelection].G, shirt[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && shirt[humanSelection].G <= 255)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R, shirt[humanSelection].G + 1, shirt[humanSelection].B);
                    }
                    else if (control.Left && shirt[humanSelection].G >= 0)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R, shirt[humanSelection].G - 1, shirt[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && shirt[humanSelection].B <= 255)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R, shirt[humanSelection].G, shirt[humanSelection].B + 1);
                    }
                    else if (control.Left && shirt[humanSelection].B >= 0)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R, shirt[humanSelection].G, shirt[humanSelection].B - 1);
                    }
                }
            }
            else if (colorSelected == 4)
            {
                if (buttonSelected == 3)
                {
                    if (control.Right && shoes[humanSelection].R <= 255)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R + 1, shoes[humanSelection].G, shoes[humanSelection].B);
                    }
                    else if (control.Left && shoes[humanSelection].R >= 0)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R - 1, shoes[humanSelection].G, shoes[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && shoes[humanSelection].G <= 255)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R, shoes[humanSelection].G + 1, shoes[humanSelection].B);
                    }
                    else if (control.Left && shoes[humanSelection].G >= 0)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R, shoes[humanSelection].G - 1, shoes[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && shoes[humanSelection].B <= 255)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R, shoes[humanSelection].G, shoes[humanSelection].B + 1);
                    }
                    else if (control.Left && shoes[humanSelection].B >= 0)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R, shoes[humanSelection].G, shoes[humanSelection].B - 1);
                    }
                }
            }
            else if (colorSelected == 5)
            {
                if (buttonSelected == 3)
                {
                    if (control.Right && skin[humanSelection].R <= 255)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R + 1, skin[humanSelection].G, skin[humanSelection].B);
                    }
                    else if (control.Left && skin[humanSelection].R >= 0)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R - 1, skin[humanSelection].G, skin[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.Right && skin[humanSelection].G <= 255)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R, skin[humanSelection].G + 1, skin[humanSelection].B);
                    }
                    else if (control.Left && skin[humanSelection].G >= 0)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R, skin[humanSelection].G - 1, skin[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.Right && skin[humanSelection].B <= 255)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R, skin[humanSelection].G, skin[humanSelection].B + 1);
                    }
                    else if (control.Left && skin[humanSelection].B >= 0)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R, skin[humanSelection].G, skin[humanSelection].B - 1);
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
                    female[humanSelection] = false;
                    hairStyle[humanSelection] = 1;
                    buttonPressed = true;
                }
                else if (!control.Enter)
                    buttonPressed = false;
            }
            else if (buttonSelected == 1)
            {
                if (control.Enter && !buttonPressed)
                {
                    female[humanSelection] = true;
                    hairStyle[humanSelection] = 2;
                    buttonPressed = true;
                }
                else if (!control.Enter)
                    buttonPressed = false;
            }

            if(buttonSelected == 6)
            {
                if (control.Enter && !buttonPressed)
                {
                    int newNum = names.ToArray().Length + 1;
                    names.Add("Human " + newNum);
                    female.Add(false);
                    eyes.Add(new Color(0, 0, 56));
                    hair.Add(new Color(66, 33, 0));
                    hairStyle.Add(1);
                    pants.Add(new Color(0, 31, 255));
                    shirt.Add(Color.White);
                    shoes.Add(new Color(96, 96, 96));
                    skin.Add(new Color(255, 179, 160));
					walkSpeed.Add(2);
                    humanSelection = names.ToArray().Length - 1;
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
					    if (names.ToArray().Length > 0)
					    {
						    names.RemoveAt(humanSelection);
						    female.RemoveAt(humanSelection);
						    eyes.RemoveAt(humanSelection);
					    	hair.RemoveAt(humanSelection);
		    				hairStyle.RemoveAt(humanSelection);
			    			pants.RemoveAt(humanSelection);
			    			shirt.RemoveAt(humanSelection);
				    		shoes.RemoveAt(humanSelection);
				    		skin.RemoveAt(humanSelection);
				    		walkSpeed.RemoveAt(humanSelection);
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
                    string[] names = new string[] { "Julian", "Joe", "Tom", "Christine" };
                    bool[] female = new bool[] { false, false, false, true };
                    Color[] pants = new Color[] { new Color(0, 31, 255), new Color(22, 22, 22), new Color(193, 193, 193), new Color(255, 173, 255) };
                    Color[] hair = new Color[] { new Color(66, 33, 0), new Color(255, 155, 43), new Color(40, 9, 0), new Color(66, 33, 0) };
                    int[] hairStyle = new int[] { 1, 1, 1, 2};
                    Color[] eyes = new Color[] { new Color(0, 0, 56), new Color(68, 21, 0), new Color(0, 47, 0), new Color(0, 0, 56) };
                    Color[] shirt = new Color[] { new Color(255, 255, 255), new Color(183, 43, 0), new Color(22, 22, 22), new Color(255, 255, 255) };
                    Color[] shoes = new Color[] { new Color(96, 96, 96), new Color(150, 150, 150), new Color(56, 20, 0), new Color(200, 0, 0) };
                    Color[] skin = new Color[] { new Color(255, 179, 160), new Color(183, 124, 95), new Color(255, 202, 191), new Color(255, 179, 160) };
					float[] walkSpeed = new float[] { 2, 1.5f, 2.5f, 2 };

                    this.names = new List<string>();
                    this.female = new List<bool>();
                    this.pants = new List<Color>();
                    this.hair = new List<Color>();
                    this.hairStyle = new List<int>();
                    this.eyes = new List<Color>();
                    this.shirt = new List<Color>();
                    this.shoes = new List<Color>();
                    this.skin = new List<Color>();

                    for (int i = 0; i < names.Length; i++)
                    {
                        this.names.Add(names[i]);
                        this.female.Add(female[i]);
                        this.pants.Add(pants[i]);
                        this.hair.Add(hair[i]);
                        this.hairStyle.Add(hairStyle[i]);
                        this.eyes.Add(eyes[i]);
                        this.shirt.Add(shirt[i]);
                        this.shoes.Add(shoes[i]);
                        this.skin.Add(skin[i]);
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

                    this.female[humanSelection] = female;
                    this.pants[humanSelection] = pants;
                    this.hair[humanSelection] = hair;
                    this.hairStyle[humanSelection] = hairStyle;
                    this.eyes[humanSelection] = eyes;
                    this.shirt[humanSelection] = shirt;
                    this.shoes[humanSelection] = shoes;
                    this.skin[humanSelection] = skin;
					this.walkSpeed[humanSelection] = walkSpeed;
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
                        if(names[humanSelection].Length > 0)
                            names[humanSelection] = names[humanSelection].Remove(names[humanSelection].Length - 1);
                    }
                    else if(!Keyboard.GetState().IsKeyDown(Keys.LeftShift) || !Keyboard.GetState().IsKeyDown(Keys.RightShift))
                    {
                        char? c = Extra.GetChar(Keyboard.GetState().GetPressedKeys()[Keyboard.GetState().GetPressedKeys().Length - 1]);
                        if (c != null)
                            names[humanSelection] += c;
                    }
                    else if(Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                    {
                        char? c = Extra.GetChar(Keyboard.GetState().GetPressedKeys()[Keyboard.GetState().GetPressedKeys().Length - 1]);
                        if (c != null)
                            names[humanSelection] += c;
                    }
                }
                lastKey = Keyboard.GetState().GetPressedKeys()[Keyboard.GetState().GetPressedKeys().Length - 1];
            }
            else if(Keyboard.GetState().GetPressedKeys().Length == 0)
                keyboardTimePressed = 0;
        }
    }
}
