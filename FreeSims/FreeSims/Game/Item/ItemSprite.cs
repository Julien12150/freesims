using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Julien12150.FreeSims.Game.Item
{
    public class ItemSprite
    {
        public Texture2D oldTv;

        public ItemSprite(ContentManager Content)
        {
            oldTv = Content.Load<Texture2D>("Item/OldTV");
        }
    }
}
