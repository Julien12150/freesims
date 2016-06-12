using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Julien12150.FreeSims.Game
{
    public class Menu
    {
        SpriteBatch spriteBatch;
        Control control;
        SpriteFont font;
        Game1 game;

        int menuSelection = 0;
        string title = "FreeSims";
        string[] menu = new string[] { "Play", "Option", "Quit" };

        bool hasPressedButton = false;

        public Menu(SpriteBatch spriteBatch, Control control, Sprite sprites, Game1 game)
        {
            this.spriteBatch = spriteBatch;
            this.control = control;
            font = sprites.mainFont;
            this.game = game;
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(font, title, new Vector2(0, 10), Color.Black);
            for(int i = 0; i < menu.Length; i++)
            {
                if (i == menuSelection)
                    spriteBatch.DrawString(font, menu[i], new Vector2(0, 50 + (i * 20)), Color.Gray);
                else
                    spriteBatch.DrawString(font, menu[i], new Vector2(0, 50 + (i * 20)), Color.Black);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (control.isControllerMode)
            {
                ChangeMenu(control.DPadUp, control.DPadDown, control.A);
            }
            else
            {
                ChangeMenu(Keyboard.GetState().IsKeyDown(Keys.Up), Keyboard.GetState().IsKeyDown(Keys.Down), Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Enter));
            }
        }

        void ChangeMenu(bool up, bool down, bool select)
        {
            if (down && !hasPressedButton)
            {
                if (menuSelection == menu.Length - 1) menuSelection = 0;
                else menuSelection++;

                hasPressedButton = true;
            }
            if (up && !hasPressedButton)
            {
                if (menuSelection == 0) menuSelection = menu.Length - 1;
                else menuSelection--;

                hasPressedButton = true;
            }

            if (!up && !down) hasPressedButton = false;


            if (select)
            {
                if (menuSelection == 0)
                {
                    game.ChangeState(GameState.Game);
                }
                else if (menuSelection == 2)
                {
                    game.Exit();
                }
            }
        }
    }
}