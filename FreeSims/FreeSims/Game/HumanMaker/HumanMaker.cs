using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Julien12150.FreeSims.Game.HumanMaker
{
    public class HumanMaker
    {
        SpriteBatch spriteBatch;
        Sprite sprites;
        Control control;
        Game1 game1;

        int height, width;

        int humanSelection = 0;
        int buttonSelected = 0;
        int maxButton = 7;
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
        List<Color> pants = new List<Color>();
        List<Color> shirt = new List<Color>();
        List<Color> shoes = new List<Color>();
        List<Color> skin = new List<Color>();
        public HumanMaker(SpriteBatch spriteBatch, Sprite sprites, Control control, Game1 game1, int width, int height)
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
            Color[] pants;
            Color[] shirt;
            Color[] shoes;
            Color[] skin;
            if(HMNFileManager.Read(out names, out female, out eyes, out hair, out pants, out shirt, out shoes, out skin))
            {
                for(int i = 0; i < names.Length; i++)
                {
                    this.names.Add(names[i]);
                    this.female.Add(female[i]);
                    this.eyes.Add(eyes[i]);
                    this.hair.Add(hair[i]);
                    this.pants.Add(pants[i]);
                    this.shirt.Add(shirt[i]);
                    this.shoes.Add(shoes[i]);
                    this.skin.Add(skin[i]);
                }
            }
        }
        public void Draw(GameTime gameTime)
        {
            Color color;
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

            spriteBatch.DrawString(sprites.mainFont, names[humanSelection], new Vector2(80, 20), Color.Black);

            if (!female[humanSelection])
            {
                spriteBatch.Draw(sprites.humanSprites.mEyes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), eyes[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mHair, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), hair[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mPants, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), pants[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mShirt, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), shirt[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mShoes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), shoes[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mSkin, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), skin[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.mNoColor, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * 7, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), Color.White);
            }
            else
            {
                spriteBatch.Draw(sprites.humanSprites.fEyes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * 7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height), eyes[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fHair, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) *7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height), hair[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fPants, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) *7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height), pants[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fShirt, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) *7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height), shirt[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fShoes, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) *7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height), shoes[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fSkin, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) *7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height), skin[humanSelection]);
                spriteBatch.Draw(sprites.humanSprites.fNoColor, new Vector2(width / 2, height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) *7, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height), Color.White);
            }

            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 50), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(0xFF, color.G, color.B));
            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 50), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(0x00, color.G, color.B));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(24 + color.R - 3, 46), Color.White);

            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 50 + sprites.colorBar.Height), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, color.G, 0xFF));
            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 50 + sprites.colorBar.Height), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, color.G, 0x00));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(24 + color.B - 3, 46 + sprites.colorBar.Height), Color.White);

            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 50 + (sprites.colorBar.Height / 2)), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, 0xFF, color.B));
            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 50 + (sprites.colorBar.Height / 2)), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, 0x00, color.B));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(24 + color.G - 3, 46 + (sprites.colorBar.Height / 2)), Color.White);
            if(buttonSelected == 2)
            {
                if (colorSelected == 0)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle(0, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle(0, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 1)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle(sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle(sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 2)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 2, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 2, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 3)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 3, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 3, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 4)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 4, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 4, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 5)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 5, sprites.clothColorButton.Height / 4, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 5, sprites.clothColorButton.Height / 4 * 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
            }
            else
            {
                if (colorSelected == 0)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle(0, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle(0, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 1)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle(sprites.clothColorButton.Width / 6, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle(sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 2)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 2, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 2, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 3)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 3, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 3, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 4)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 4, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 4, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
                else if (colorSelected == 5)
                {
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 5, 0, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), Color.White);
                    spriteBatch.Draw(sprites.clothColorButton, new Vector2(10, 10), new Rectangle((sprites.clothColorButton.Width / 6) * 5, sprites.clothColorButton.Height / 2, sprites.clothColorButton.Width / 6, sprites.clothColorButton.Height / 4), color);
                }
            }
            if (buttonSelected == 3)
            {
                spriteBatch.Draw(sprites.arrows, new Vector2(5, 50), new Rectangle((sprites.arrows.Width / 4) * 3, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
                spriteBatch.Draw(sprites.arrows, new Vector2(sprites.colorBar.Width + 15, 50), new Rectangle((sprites.arrows.Width / 4) * 1, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
            }
            else if (buttonSelected == 4)
            {
                spriteBatch.Draw(sprites.arrows, new Vector2(5, 50 + (sprites.colorBar.Height / 2)), new Rectangle((sprites.arrows.Width / 4) * 3, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
                spriteBatch.Draw(sprites.arrows, new Vector2(sprites.colorBar.Width + 15, 50 + (sprites.colorBar.Height / 2)), new Rectangle((sprites.arrows.Width / 4) * 1, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
            }
            else if (buttonSelected == 5)
            {
                spriteBatch.Draw(sprites.arrows, new Vector2(5, 50 + sprites.colorBar.Height), new Rectangle((sprites.arrows.Width / 4) * 3, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
                spriteBatch.Draw(sprites.arrows, new Vector2(sprites.colorBar.Width + 15, 50 + sprites.colorBar.Height), new Rectangle((sprites.arrows.Width / 4) * 1, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
            }
        }
        public void Update(GameTime gameTime)
        {
            if (control.LB && !pressedHumanButton)
            {
                if (humanSelection == 0)
                    humanSelection = names.ToArray().Length - 1;
                else
                    humanSelection--;
                pressedHumanButton = true;
            }
            else if (control.RB && !pressedHumanButton)
            {
                if (humanSelection == names.ToArray().Length - 1)
                    humanSelection = 0;
                else
                    humanSelection++;
                pressedHumanButton = true;
            }
            else if (!control.LB && !control.RB)
                pressedHumanButton = false;

            if (control.DPadUp && !pressedButtonButton)
            {
                if (buttonSelected == 0)
                    buttonSelected = maxButton;
                else
                    buttonSelected--;
                pressedButtonButton = true;
            }
            else if (control.DPadDown && !pressedButtonButton)
            {
                if (buttonSelected == maxButton)
                    buttonSelected = 0;
                else
                    buttonSelected++;
                pressedButtonButton = true;
            }
            else if (!control.DPadDown && !control.DPadUp)
                pressedButtonButton = false;

            if (control.Start)
            {
                HMNFileManager.Write(names.ToArray(), female.ToArray(), eyes.ToArray(), hair.ToArray(), pants.ToArray(), shirt.ToArray(), shoes.ToArray(), skin.ToArray());

                game1.ChangeState(GameState.Menu);
                game1.humanMaker = null;
            }

            if(colorSelected == 0)
            {
                if(buttonSelected == 3)
                {
                    if(control.DPadRight && eyes[humanSelection].R <= 255)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R + 1, eyes[humanSelection].G, eyes[humanSelection].B);
                    }
                    else if(control.DPadLeft && eyes[humanSelection].R >= 0)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R - 1, eyes[humanSelection].G, eyes[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.DPadRight && eyes[humanSelection].G <= 255)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R, eyes[humanSelection].G + 1, eyes[humanSelection].B);
                    }
                    else if (control.DPadLeft && eyes[humanSelection].G >= 0)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R, eyes[humanSelection].G - 1, eyes[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.DPadRight && eyes[humanSelection].B <= 255)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R, eyes[humanSelection].G, eyes[humanSelection].B + 1);
                    }
                    else if (control.DPadLeft && eyes[humanSelection].B >= 0)
                    {
                        eyes[humanSelection] = new Color(eyes[humanSelection].R, eyes[humanSelection].G, eyes[humanSelection].B - 1);
                    }
                }
            }
            else if (colorSelected == 1)
            {
                if (buttonSelected == 3)
                {
                    if (control.DPadRight && hair[humanSelection].R <= 255)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R + 1, hair[humanSelection].G, hair[humanSelection].B);
                    }
                    else if (control.DPadLeft && hair[humanSelection].R >= 0)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R - 1, hair[humanSelection].G, hair[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.DPadRight && hair[humanSelection].G <= 255)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R, hair[humanSelection].G + 1, hair[humanSelection].B);
                    }
                    else if (control.DPadLeft && hair[humanSelection].G >= 0)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R, hair[humanSelection].G - 1, hair[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.DPadRight && hair[humanSelection].B <= 255)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R, hair[humanSelection].G, hair[humanSelection].B + 1);
                    }
                    else if (control.DPadLeft && hair[humanSelection].B >= 0)
                    {
                        hair[humanSelection] = new Color(hair[humanSelection].R, hair[humanSelection].G, hair[humanSelection].B - 1);
                    }
                }
            }
            else if (colorSelected == 2)
            {
                if (buttonSelected == 3)
                {
                    if (control.DPadRight && pants[humanSelection].R <= 255)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R + 1, pants[humanSelection].G, pants[humanSelection].B);
                    }
                    else if (control.DPadLeft && pants[humanSelection].R >= 0)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R - 1, pants[humanSelection].G, pants[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.DPadRight && pants[humanSelection].G <= 255)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R, pants[humanSelection].G + 1, pants[humanSelection].B);
                    }
                    else if (control.DPadLeft && pants[humanSelection].G >= 0)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R, pants[humanSelection].G - 1, pants[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.DPadRight && pants[humanSelection].B <= 255)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R, pants[humanSelection].G, pants[humanSelection].B + 1);
                    }
                    else if (control.DPadLeft && pants[humanSelection].B >= 0)
                    {
                        pants[humanSelection] = new Color(pants[humanSelection].R, pants[humanSelection].G, pants[humanSelection].B - 1);
                    }
                }
            }
            else if (colorSelected == 3)
            {
                if (buttonSelected == 3)
                {
                    if (control.DPadRight && shirt[humanSelection].R <= 255)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R + 1, shirt[humanSelection].G, shirt[humanSelection].B);
                    }
                    else if (control.DPadLeft && shirt[humanSelection].R >= 0)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R - 1, shirt[humanSelection].G, shirt[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.DPadRight && shirt[humanSelection].G <= 255)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R, shirt[humanSelection].G + 1, shirt[humanSelection].B);
                    }
                    else if (control.DPadLeft && shirt[humanSelection].G >= 0)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R, shirt[humanSelection].G - 1, shirt[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.DPadRight && shirt[humanSelection].B <= 255)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R, shirt[humanSelection].G, shirt[humanSelection].B + 1);
                    }
                    else if (control.DPadLeft && shirt[humanSelection].B >= 0)
                    {
                        shirt[humanSelection] = new Color(shirt[humanSelection].R, shirt[humanSelection].G, shirt[humanSelection].B - 1);
                    }
                }
            }
            else if (colorSelected == 4)
            {
                if (buttonSelected == 3)
                {
                    if (control.DPadRight && shoes[humanSelection].R <= 255)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R + 1, shoes[humanSelection].G, shoes[humanSelection].B);
                    }
                    else if (control.DPadLeft && shoes[humanSelection].R >= 0)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R - 1, shoes[humanSelection].G, shoes[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.DPadRight && shoes[humanSelection].G <= 255)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R, shoes[humanSelection].G + 1, shoes[humanSelection].B);
                    }
                    else if (control.DPadLeft && shoes[humanSelection].G >= 0)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R, shoes[humanSelection].G - 1, shoes[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.DPadRight && shoes[humanSelection].B <= 255)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R, shoes[humanSelection].G, shoes[humanSelection].B + 1);
                    }
                    else if (control.DPadLeft && shoes[humanSelection].B >= 0)
                    {
                        shoes[humanSelection] = new Color(shoes[humanSelection].R, shoes[humanSelection].G, shoes[humanSelection].B - 1);
                    }
                }
            }
            else if (colorSelected == 5)
            {
                if (buttonSelected == 3)
                {
                    if (control.DPadRight && skin[humanSelection].R <= 255)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R + 1, skin[humanSelection].G, skin[humanSelection].B);
                    }
                    else if (control.DPadLeft && skin[humanSelection].R >= 0)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R - 1, skin[humanSelection].G, skin[humanSelection].B);
                    }
                }
                else if (buttonSelected == 4)
                {
                    if (control.DPadRight && skin[humanSelection].G <= 255)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R, skin[humanSelection].G + 1, skin[humanSelection].B);
                    }
                    else if (control.DPadLeft && skin[humanSelection].G >= 0)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R, skin[humanSelection].G - 1, skin[humanSelection].B);
                    }
                }
                else if (buttonSelected == 5)
                {
                    if (control.DPadRight && skin[humanSelection].B <= 255)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R, skin[humanSelection].G, skin[humanSelection].B + 1);
                    }
                    else if (control.DPadLeft && skin[humanSelection].B >= 0)
                    {
                        skin[humanSelection] = new Color(skin[humanSelection].R, skin[humanSelection].G, skin[humanSelection].B - 1);
                    }
                }
            }

            if(buttonSelected == 2)
            {
                if (control.A && !buttonPressed)
                {
                    if (colorSelected == 5)
                        colorSelected = 0;
                    else
                        colorSelected++;
                    buttonPressed = true;
                }
                else if (!control.A)
                    buttonPressed = false;
            }

            if(buttonSelected == 0)
            {
                if (control.A && !buttonPressed)
                {
                    female[humanSelection] = false;
                    buttonPressed = true;
                }
                else if (!control.A)
                    buttonPressed = false;
            }
            else if (buttonSelected == 1)
            {
                if (control.A && !buttonPressed)
                {
                    female[humanSelection] = true;
                    buttonPressed = true;
                }
                else if (!control.A)
                    buttonPressed = false;
            }

            if(buttonSelected == 6)
            {
                if (control.A && !buttonPressed)
                {
                    int newNum = names.ToArray().Length + 1;
                    names.Add("Human " + newNum);
                    female.Add(false);
                    eyes.Add(new Color(0, 0, 56));
                    hair.Add(new Color(66, 33, 0));
                    pants.Add(new Color(0, 31, 255));
                    shirt.Add(Color.White);
                    shoes.Add(new Color(96, 96, 96));
                    skin.Add(new Color(255, 179, 160));
                    humanSelection = names.ToArray().Length - 1;
                    buttonPressed = true;
                }
                else if (!control.A)
                    buttonPressed = false;
            }
            else if(buttonSelected == 7)
            {
                if (control.A && !buttonPressed)
                {
                    if (names.ToArray().Length > 0)
                    {
                        names.RemoveAt(humanSelection);
                        female.RemoveAt(humanSelection);
                        eyes.RemoveAt(humanSelection);
                        hair.RemoveAt(humanSelection);
                        pants.RemoveAt(humanSelection);
                        shirt.RemoveAt(humanSelection);
                        shoes.RemoveAt(humanSelection);
                        skin.RemoveAt(humanSelection);
                        humanSelection--;
                    }
                    buttonPressed = true;
                }
                else if (!control.A)
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
