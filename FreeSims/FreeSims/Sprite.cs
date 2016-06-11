using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Julien12150.FreeSims
{
    public class Sprite
    {
        public Texture2D cursorSprite;
        public Texture2D humanSprite;
        public Texture2D humanSelectSprite;
        public SpriteFont mainFont;
        public Sprite(ContentManager Content)
        {
            cursorSprite = Content.Load<Texture2D>("Gui/cursor");
            humanSprite = Content.Load<Texture2D>("Human/M");
            mainFont = Content.Load<SpriteFont>("font");
            humanSelectSprite = Content.Load<Texture2D>("Gui/SelectedHuman");
        }
    }
}
