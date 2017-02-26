using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Technochips.FreeSims.Game.Entity;
using Technochips.FreeSims.Game.Entity.Item;
using Microsoft.Xna.Framework;

namespace Technochips.FreeSims.Game
{
    public class Menu
    {
        SpriteBatch spriteBatch;
        Control control;
        SpriteFont font;
        Cursor cursor;
        Sprite sprites;
        ItemSprite itemSprites;
        FreeSims game1;
        Language language;
        int width, height;

        int menuSelection = 0;
        string title = "FreeSims";
        string[] menu;

        bool hasPressedButton = false;

        public Menu(int width, int height, SpriteBatch spriteBatch, Control control, Sprite sprites, FreeSims game1, Cursor cursor, ItemSprite itemSprites, Language language)
        {
            menu = new string[] { language.menu_play, language.menu_option, language.menu_humanmaker, language.menu_quit };
            this.spriteBatch = spriteBatch;
            this.control = control;
            this.cursor = cursor;
            this.sprites = sprites;
            this.itemSprites = itemSprites;
            this.width = width;
            this.height = height;
            font = sprites.mainFont;
            this.game1 = game1;
            this.language = language;
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
                    game1.ChangeState(GameState.Game);
                    if(game1.game == null)
                    {
                        game1.game = new Game(width, height, control, cursor, spriteBatch, sprites, itemSprites, language, game1);
                    }
                }
                else if(menuSelection == 1)
                {
                    game1.ChangeState(GameState.Option);
                    game1.option = new Option(spriteBatch, game1, language, sprites, control);
                }
                else if(menuSelection == 2)
                {
					game1.game = null;
                    game1.ChangeState(GameState.HumanMaking);
                    game1.humanMaker = new HumanMaker.HumanMaker(spriteBatch, sprites, control, game1, width, height);
                }
                else if (menuSelection == 3)
                {
                    game1.Exit();
                }
            }
        }
    }
}