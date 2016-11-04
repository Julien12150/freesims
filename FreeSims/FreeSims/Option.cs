using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Julien12150.FreeSims
{
    public class Option
    {
        SpriteBatch spriteBatch;
        FreeSims game1;
        Language language;
        SpriteFont font;
        Control control;
        int langSelection = 0;
        int langSelected;
        bool hasPressedButton;
        string[] lang;
        public Option(SpriteBatch spriteBatch, FreeSims game1, Language language, Sprite sprites, Control control)
        {
            this.spriteBatch = spriteBatch;
            this.game1 = game1;
            this.control = control;
            this.language = language;
            font = sprites.mainFont;
            lang = Directory.GetFiles("Language");
            for (int i = 0; i < lang.Length; i++)
            {
                lang[i] = lang[i].Split('.')[0].Split(Path.DirectorySeparatorChar)[1];
                if (language.language == lang[i])
                    langSelected = i;
            }
        }
        public void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(font, language.option_chooselang + "\n" + language.option_restartnote, new Vector2(0, 10), Color.Black);
            for (int i = 0; i < lang.Length; i++)
            {
                string langEnglish = language.GetEnglishLang(lang[i]);
                if (langEnglish == null)
                {
                    if (i == langSelection)
                        spriteBatch.DrawString(font, language.GetLang(lang[i]), new Vector2(0, 80 + (i * 20)), Color.Gray);
                    else
                        spriteBatch.DrawString(font, language.GetLang(lang[i]), new Vector2(0, 80 + (i * 20)), Color.Black);
                }
                else
                {
                    if (i == langSelection)
                        spriteBatch.DrawString(font, $"{language.GetLang(lang[i])} ({langEnglish})", new Vector2(0, 80 + (i * 20)), Color.Gray);
                    else
                        spriteBatch.DrawString(font, $"{language.GetLang(lang[i])} ({langEnglish})", new Vector2(0, 80 + (i * 20)), Color.Black);
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            if (control.isControllerMode)
            {
                ChangeMenu(control.DPadUp, control.DPadDown, control.GoBack);
            }
            else
            {
                ChangeMenu(Keyboard.GetState().IsKeyDown(Keys.Up), Keyboard.GetState().IsKeyDown(Keys.Down), Keyboard.GetState().IsKeyDown(Keys.Escape));
            }
        }
        void ChangeMenu(bool up, bool down, bool select)
        {
            if (down && !hasPressedButton)
            {
                if (langSelection == lang.Length - 1) langSelection = 0;
                else langSelection++;

                hasPressedButton = true;
            }
            if (up && !hasPressedButton)
            {
                if (langSelection == 0) langSelection = lang.Length - 1;
                else langSelection--;

                hasPressedButton = true;
            }

            if (!up && !down) hasPressedButton = false;


            if (select)
            {
                string[] file = File.ReadAllLines($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Julien12150{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}config.txt");
                for (int i = 0; i < file.Length; i++)
                {
                    if (file[i].Split('=')[0] == "lang")
                    {
                        file[i] = $"lang={lang[langSelection]}";
                        break;
                    }
                }
                StreamWriter fw = new StreamWriter($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData + Path.DirectorySeparatorChar)}Julien12150{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}config.txt");
                foreach (string s in file)
                    fw.WriteLine(s);
                fw.Close();
                language.ChangeLanguage(lang[langSelection]);
                game1.ChangeState(Game.GameState.Menu);
            }
        }
    }
}
