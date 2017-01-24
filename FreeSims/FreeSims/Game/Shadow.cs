using System;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using Technochips.FreeSims;

namespace Technochips.FreeSims.Game
{
    public class Shadow
    {
		GraphicsDevice gd;
		public Shadow(GraphicsDevice gd)
		{
			this.gd = gd;
		}
        public Texture2D GenerateShadow(Texture2D sprite, int row, int column)
        {
            return GenerateShadow(sprite.Width / row, (int)(((double)(sprite.Height * ((sprite.Width / row) / 40)) / column) / 1.5));
        }
        public Texture2D GenerateShadow(int width, int height)
        {
			if (!Environment.OSVersion.VersionString.StartsWith("Unix"))
			{
				Bitmap b = new Bitmap(width / 2, (height / 2) / 2);
				Graphics g = Graphics.FromImage(b);
				Brush br = new SolidBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x00));

				g.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, width, height / 2);
				g.FillEllipse(br, 1, 1, b.Width - 2, b.Height - 2);

				g.Dispose();

				b = Extra.ResizeImage(b, b.Width * 2, b.Height * 2);

				Microsoft.Xna.Framework.Color[] p = new Microsoft.Xna.Framework.Color[b.Width * b.Height];

				for (int y = 0; y < b.Height; y++)
				{
					for (int x = 0; x < b.Width; x++)
					{
						Color c = b.GetPixel(x, y);
						p[(y * b.Width) + x] = new Microsoft.Xna.Framework.Color(c.R, c.G, c.B, c.A);
					}
				}

				Texture2D t = new Texture2D(gd, b.Width, b.Height, false, SurfaceFormat.Color);

				t.SetData(p);

				return t;
			}
			return null;
        }
    }
}
