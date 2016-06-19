using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Julien12150.FreeSims.Game.Item
{
    public class ItemSprite
    {
        public Texture2D oldTv;
        public Texture2D Chair;
		public Texture2D Table;
		
        public ItemSprite(ContentManager Content)
        {
            oldTv = Content.Load<Texture2D>("Item/OldTV");
			Chair = Content.Load<Texture2D>("Item/Chair");
			Table = Content.Load<Texture2D>("Item/Table");
        }
    }
}
