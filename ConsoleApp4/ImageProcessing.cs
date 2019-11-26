using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannonGame
{
    public static class ImageProcessing
    {
        public static Bitmap ARGBFilter(this Image sourceImage, ColorARGB color)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                       {
                        new float[]{color.r, 0, 0, 0, 0},
                        new float[]{0, color.g, 0, 0, 0},
                        new float[]{0, 0, color.b, 0, 0},
                        new float[]{0, 0, 0, color.a, 0},
                        new float[]{0, 0, 0, 0, 1}
                       });

            return ApplyColorMatrix(sourceImage, colorMatrix);
        }

        private static Bitmap ApplyColorMatrix(Image sourceImage, ColorMatrix colorMatrix)
        {
            var bmp32BppSource = GetArgbCopy(sourceImage);
            var bmp32BppDest = new Bitmap(bmp32BppSource.Width, bmp32BppSource.Height, PixelFormat.Format32bppArgb);

            using (var graphics = Graphics.FromImage(bmp32BppDest))
            {
                var bmpAttributes = new ImageAttributes();
                bmpAttributes.SetColorMatrix(colorMatrix);

                graphics.DrawImage(bmp32BppSource, new Rectangle(0, 0, bmp32BppSource.Width, bmp32BppSource.Height),
                                    0, 0, bmp32BppSource.Width, bmp32BppSource.Height, GraphicsUnit.Pixel, bmpAttributes);
            }
            bmp32BppSource.Dispose();

            return bmp32BppDest;
        }

        private static Bitmap GetArgbCopy(Image sourceImage)
        {
            var bmpNew = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);

            using (var graphics = Graphics.FromImage(bmpNew))
            {
                graphics.DrawImage(sourceImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), GraphicsUnit.Pixel);
                graphics.Flush();
            }
            return bmpNew;
        }

    }
}
