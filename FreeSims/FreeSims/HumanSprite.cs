using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Julien12150.FreeSims
{
    public class HumanSprite
    {
        public Texture2D mEyes;
        public Texture2D mHair;
        public Texture2D mNoColor;
        public Texture2D mPants;
        public Texture2D mShirt;
        public Texture2D mShoes;
        public Texture2D mSkin;

        public Texture2D fEyes;
        public Texture2D fHair;
        public Texture2D fNoColor;
        public Texture2D fPants;
        public Texture2D fShirt;
        public Texture2D fShoes;
        public Texture2D fSkin;

        public Texture2D tabMEyes;
        public Texture2D tabMHair;
        public Texture2D tabMNoColor;
        public Texture2D tabMShirt;
        public Texture2D tabMSkin;

        public Texture2D tabFEyes;
        public Texture2D tabFHair;
        public Texture2D tabFNoColor;
        public Texture2D tabFShirt;
        public Texture2D tabFSkin;
        public HumanSprite(ContentManager Content)
        {
            mEyes = Content.Load<Texture2D>("Human/M_Eyes");
            mHair = Content.Load<Texture2D>("Human/M_Hair");
            mNoColor = Content.Load<Texture2D>("Human/M_NoColor");
            mPants = Content.Load<Texture2D>("Human/M_Pants");
            mShirt = Content.Load<Texture2D>("Human/M_Shirt");
            mShoes = Content.Load<Texture2D>("Human/M_Shoes");
            mSkin = Content.Load<Texture2D>("Human/M_Skin");

            fEyes = Content.Load<Texture2D>("Human/F_Eyes");
            fHair = Content.Load<Texture2D>("Human/F_Hair");
            fNoColor = Content.Load<Texture2D>("Human/F_NoColor");
            fPants = Content.Load<Texture2D>("Human/F_Pants");
            fShirt = Content.Load<Texture2D>("Human/F_Shirt");
            fShoes = Content.Load<Texture2D>("Human/F_Shoes");
            fSkin = Content.Load<Texture2D>("Human/F_Skin");

            tabMEyes = Content.Load<Texture2D>("Gui/Human/TabM_Eyes");
            tabMHair = Content.Load<Texture2D>("Gui/Human/TabM_Hair");
            tabMNoColor = Content.Load<Texture2D>("Gui/Human/TabM_NoColor");
            tabMShirt = Content.Load<Texture2D>("Gui/Human/TabM_Shirt");
            tabMSkin = Content.Load<Texture2D>("Gui/Human/TabM_Skin");

            tabFEyes = Content.Load<Texture2D>("Gui/Human/TabF_Eyes");
            tabFHair = Content.Load<Texture2D>("Gui/Human/TabF_Hair");
            tabFNoColor = Content.Load<Texture2D>("Gui/Human/TabF_NoColor");
            tabFShirt = Content.Load<Texture2D>("Gui/Human/TabF_Shirt");
            tabFSkin = Content.Load<Texture2D>("Gui/Human/TabF_Skin");
        }
    }
}
