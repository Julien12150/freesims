using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Julien12150.FreeSims.Game.Item
{
    public class ItemSprite
    {
        public Texture2D oldTv;
        public Texture2D chair;
		public Texture2D table;
        public Texture2D fridge;

        public Texture2D noSprite;
		
        public ItemSprite(ContentManager Content)
        {
            oldTv = Content.Load<Texture2D>("Item/OldTV");
			chair = Content.Load<Texture2D>("Item/Chair");
			table = Content.Load<Texture2D>("Item/Table");
            fridge = Content.Load<Texture2D>("Item/Fridge");

            noSprite = Content.Load<Texture2D>("Item/NoSprite");
        }
    }
}
