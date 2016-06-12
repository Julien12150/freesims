using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

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
