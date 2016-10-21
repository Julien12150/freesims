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

        public Texture2D tabEyes;
        public Texture2D tabHair;
        public Texture2D tabHairNoColor;
        public Texture2D tabNoColor;
        public Texture2D tabShirt;
        public Texture2D tabSkin;

        public Texture2D H1;
        public Texture2D H1_NoColor;
        public Texture2D H2;
        public Texture2D H2_NoColor;

        public int haircutNumber;

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

            tabEyes = Content.Load<Texture2D>("Gui/Human/Tab_Eyes");
            tabHair = Content.Load<Texture2D>("Gui/Human/Tab_Hair");
            tabHairNoColor = Content.Load<Texture2D>("Gui/Human/Tab_HairNoColor");
            tabNoColor = Content.Load<Texture2D>("Gui/Human/Tab_NoColor");
            tabShirt = Content.Load<Texture2D>("Gui/Human/Tab_Shirt");
            tabSkin = Content.Load<Texture2D>("Gui/Human/Tab_Skin");

            H1 = Content.Load<Texture2D>("Human/Haircut/1");
            H1_NoColor = Content.Load<Texture2D>("Human/Haircut/1_NoColor");
            H2 = Content.Load<Texture2D>("Human/Haircut/2");
            H2_NoColor = Content.Load<Texture2D>("Human/Haircut/2_NoColor");

            haircutNumber = 2;
        }

        public Texture2D GetHair(int i)
        {
            if (i == 1)
                return H1;
            else if (i == 2)
                return H2;
            else
                return null;
        }
        public Texture2D GetHairNoColor(int i)
        {
            if (i == 1)
                return H1_NoColor;
            else if (i == 2)
                return H2_NoColor;
            else
                return null;
        }
    }
}
