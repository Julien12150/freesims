using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Technochips.FreeSims
{
    public class Sprite
    {
        public Texture2D cursorSprite;
        public Texture2D humanSelectSprite;
        public Texture2D statBar;
        public Texture2D talkBuble;
        public Texture2D colorBar;
        public Texture2D tabTop;
        public Texture2D arrows;
        public Texture2D colorCursor;
        public Texture2D genderButton;
        public Texture2D clothColorButton;
        public Texture2D addButton;
        public Texture2D removeButton;
        public Texture2D randomButton;
        public Texture2D resetButton;
        public Texture2D statIcon;

        public Texture2D noSprite;
        
        public SpriteFont mainFont;

        public HumanSprite humanSprites;

        public Sprite(ContentManager Content)
        {
            cursorSprite = Content.Load<Texture2D>("Gui/cursor");
            humanSelectSprite = Content.Load<Texture2D>("Gui/SelectedHuman");
            statBar = Content.Load<Texture2D>("Gui/StatBar");
            talkBuble = Content.Load<Texture2D>("Gui/TalkBuble");
            colorBar = Content.Load<Texture2D>("Gui/HumanMaker/ColorBar");
            tabTop = Content.Load<Texture2D>("Gui/Human/TabTop");
            arrows = Content.Load<Texture2D>("Gui/HumanMaker/Arrows");
            colorCursor = Content.Load<Texture2D>("Gui/HumanMaker/ColorCursor");
            genderButton = Content.Load<Texture2D>("Gui/HumanMaker/GenderButton");
            clothColorButton = Content.Load<Texture2D>("Gui/HumanMaker/ClothButton");
            addButton = Content.Load<Texture2D>("Gui/HumanMaker/AddButton");
            removeButton = Content.Load<Texture2D>("Gui/HumanMaker/RemoveButton");
            resetButton = Content.Load<Texture2D>("Gui/HumanMaker/ResetButton");
            randomButton = Content.Load<Texture2D>("Gui/HumanMaker/RandomButton");
            statIcon = Content.Load<Texture2D>("Gui/StatIcon");
            noSprite = Content.Load<Texture2D>("NoSprite");

            mainFont = Content.Load<SpriteFont>("font");

            humanSprites = new HumanSprite(Content);
        }
    }
}
