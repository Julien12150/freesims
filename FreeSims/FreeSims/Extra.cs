using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Julien12150.FreeSims
{
    public class Extra
    {
        public static char? GetChar(Keys key)
        {
            int keyInt;
            if (key >= Keys.A && key <= Keys.Z)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))keyInt = (int)key;
                else keyInt = (int)key + 32;
                return (char)keyInt;
            }
            else if(key >= Keys.D0 && key <= Keys.D9)
            {
                return (char)key;
            }
            else if(key >= Keys.NumPad0 && key <= Keys.NumPad9)
            {
                keyInt = (int)key - 48;
                return (char)keyInt;
            }
            else if(key == Keys.Space)
            {
                return (char)key;
            }
            else return null;
        }
    }
}
