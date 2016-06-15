using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims.Game.HumanMaker
{
    public class HumanMaker
    {
        SpriteBatch spriteBatch;
        Sprite sprites;
        Control control;
        Game1 game1;

        int humanSelection = 0;
        int buttonSelected = 0;
        int maxButton = 2;
        int colorSelected = 3;
        bool pressedHumanButton = false;
        bool pressedButtonButton = false;

        List<string> names = new List<string>();

        List<bool> female = new List<bool>();
        List<Color> eyes = new List<Color>();
        List<Color> hair = new List<Color>();
        List<Color> pants = new List<Color>();
        List<Color> shirt = new List<Color>();
        List<Color> shoes = new List<Color>();
        List<Color> skin = new List<Color>();
        public HumanMaker(SpriteBatch spriteBatch, Sprite sprites, Control control, Game1 game1)
        {
            this.spriteBatch = spriteBatch;
            this.sprites = sprites;
            this.control = control;
            this.game1 = game1;

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

            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 10), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(0xFF, color.G, color.B));
            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 10), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(0x00, color.G, color.B));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(24 + color.R - 3, 6), Color.White);
            if(buttonSelected == 0)
            {
                spriteBatch.Draw(sprites.arrows, new Vector2(5, 10), new Rectangle((sprites.arrows.Width / 4) * 3, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
                spriteBatch.Draw(sprites.arrows, new Vector2(sprites.colorBar.Width + 15, 10), new Rectangle((sprites.arrows.Width / 4) * 1, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
            }

            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 10 + (sprites.colorBar.Height / 2)), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, 0xFF, color.B));
            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 10 + (sprites.colorBar.Height / 2)), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, 0x00, color.B));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(24 + color.G - 3, 6 + (sprites.colorBar.Height / 2)), Color.White);
            if (buttonSelected == 1)
            {
                spriteBatch.Draw(sprites.arrows, new Vector2(5, 10 + (sprites.colorBar.Height / 2)), new Rectangle((sprites.arrows.Width / 4) * 3, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
                spriteBatch.Draw(sprites.arrows, new Vector2(sprites.colorBar.Width + 15, 10 + (sprites.colorBar.Height / 2)), new Rectangle((sprites.arrows.Width / 4) * 1, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
            }

            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 10 + sprites.colorBar.Height), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, color.G, 0xFF));
            spriteBatch.Draw(sprites.colorBar, new Vector2(20, 10 + sprites.colorBar.Height), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, color.G, 0x00));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(24 + color.B - 3, 6 + sprites.colorBar.Height), Color.White);
            if (buttonSelected == 2)
            {
                spriteBatch.Draw(sprites.arrows, new Vector2(5, 10 + sprites.colorBar.Height), new Rectangle((sprites.arrows.Width / 4) * 3, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
                spriteBatch.Draw(sprites.arrows, new Vector2(sprites.colorBar.Width + 15, 10 + sprites.colorBar.Height), new Rectangle((sprites.arrows.Width / 4) * 1, 0, sprites.arrows.Width / 4, sprites.arrows.Height), Color.White);
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
                game1.ChangeState(GameState.Menu);
                game1.humanMaker = null;
            }

            if(colorSelected == 0)
            {
                if(buttonSelected == 0)
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
                else if (buttonSelected == 1)
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
                else if (buttonSelected == 2)
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
                if (buttonSelected == 0)
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
                else if (buttonSelected == 1)
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
                else if (buttonSelected == 2)
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
                if (buttonSelected == 0)
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
                else if (buttonSelected == 1)
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
                else if (buttonSelected == 2)
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
                if (buttonSelected == 0)
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
                else if (buttonSelected == 1)
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
                else if (buttonSelected == 2)
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
                if (buttonSelected == 0)
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
                else if (buttonSelected == 1)
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
                else if (buttonSelected == 2)
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
                if (buttonSelected == 0)
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
                else if (buttonSelected == 1)
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
                else if (buttonSelected == 2)
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
        }
    }
}
