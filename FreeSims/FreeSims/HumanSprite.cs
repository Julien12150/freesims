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
        public HumanSprite(ContentManager Content)
        {
            mEyes = Content.Load<Texture2D>("Human/M_Eyes");
            mHair = Content.Load<Texture2D>("Human/M_Hair");
            mNoColor = Content.Load<Texture2D>("Human/M_NoColor");
            mPants = Content.Load<Texture2D>("Human/M_Pants");
            mShirt = Content.Load<Texture2D>("Human/M_Shirt");
            mShoes = Content.Load<Texture2D>("Human/M_Shoes");
            mSkin = Content.Load<Texture2D>("Human/M_Skin");
        }
    }
}
