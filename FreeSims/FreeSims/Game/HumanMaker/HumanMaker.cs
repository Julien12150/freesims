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

        int humanSelection = 0;
        int menuSelected = 0;
        bool pressedButton = false;

        List<string> names = new List<string>();

        List<bool> female = new List<bool>();
        List<Color> eyes = new List<Color>();
        List<Color> hair = new List<Color>();
        List<Color> pants = new List<Color>();
        List<Color> shirt = new List<Color>();
        List<Color> shoes = new List<Color>();
        List<Color> skin = new List<Color>();
        public HumanMaker(SpriteBatch spriteBatch, Sprite sprites, Control control)
        {
            this.spriteBatch = spriteBatch;
            this.sprites = sprites;
            this.control = control;

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
            Color color = shirt[humanSelection];

            spriteBatch.Draw(sprites.colorBar, new Vector2(10, 10), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(0xFF, color.G, color.B));
            spriteBatch.Draw(sprites.colorBar, new Vector2(10, 10), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(0x00, color.G, color.B));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(14 + color.R - 3, 6), Color.White);

            spriteBatch.Draw(sprites.colorBar, new Vector2(10, 10 + (sprites.colorBar.Height / 2)), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, 0xFF, color.B));
            spriteBatch.Draw(sprites.colorBar, new Vector2(10, 10 + (sprites.colorBar.Height / 2)), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, 0x00, color.B));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(14 + color.G - 3, 6 + (sprites.colorBar.Height / 2)), Color.White);

            spriteBatch.Draw(sprites.colorBar, new Vector2(10, 10 + sprites.colorBar.Height), new Rectangle(0, 0, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, color.G, 0xFF));
            spriteBatch.Draw(sprites.colorBar, new Vector2(10, 10 + sprites.colorBar.Height), new Rectangle(0, sprites.colorBar.Height / 2, sprites.colorBar.Width, sprites.colorBar.Height / 2), new Color(color.R, color.G, 0x00));
            spriteBatch.Draw(sprites.colorCursor, new Vector2(14 + color.B - 3, 6 + sprites.colorBar.Height), Color.White);
        }
        public void Update(GameTime gameTime)
        {
            if (control.LB && !pressedButton)
            {
                if (humanSelection == names.ToArray().Length - 1)
                    humanSelection = 0;
                else
                    humanSelection++;
                pressedButton = true;
            }
            else if (control.RB && !pressedButton)
            {
                if (humanSelection == 0)
                    humanSelection = names.ToArray().Length - 1;
                else
                    humanSelection--;
                pressedButton = true;
            }
            else if (!control.LB && !control.RB)
                pressedButton = false;
        }
    }
}
