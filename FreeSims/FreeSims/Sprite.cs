using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims
{
    public class Sprite
    {
        public Texture2D cursorSprite;
        public Texture2D humanSelectSprite;
        public Texture2D statBar;
        public Texture2D talkBuble;
        public SpriteFont mainFont;

        public HumanSprite humanSprites;
        public Sprite(ContentManager Content)
        {
            cursorSprite = Content.Load<Texture2D>("Gui/cursor");
            humanSelectSprite = Content.Load<Texture2D>("Gui/SelectedHuman");
            statBar = Content.Load<Texture2D>("Gui/StatBar");
            talkBuble = Content.Load<Texture2D>("Gui/TalkBuble");

            mainFont = Content.Load<SpriteFont>("font");

            humanSprites = new HumanSprite(Content);
        }
    }
}
