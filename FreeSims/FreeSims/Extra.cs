using Microsoft.Xna.Framework.Input;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Technochips.FreeSims
{
    public static class Extra
    {
        public static char? GetChar(Keys key)
        {
            int keyInt;
            if (key >= Keys.A && key <= Keys.Z)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))keyInt = (int)key;
                else keyInt = (int)key + 32;
                return (char)keyInt;
            }
            else if(key >= Keys.D0 && key <= Keys.D9)
            {
                return (char)key;
            }
            /*else if(key >= Keys.NumPad0 && key <= Keys.NumPad9)
            {
                keyInt = (int)key - 48;
                return (char)keyInt;
            }*/
            else if(key == Keys.Space)
            {
                return (char)key;
            }
            else return null;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height) //by mpen i think
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
