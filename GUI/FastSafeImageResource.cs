using System;
using System.Collections.Generic;
using System.Drawing;
using GUI.Properties;

namespace GUI
{
    public static class FastSafeImageResource
    {
        private static readonly Dictionary<string, Image> RegisteredImages = new Dictionary<string, Image>();

        /// <summary>
        /// This method first looks in a dictionary to try and quickly find the image.
        /// If the image is not in the dictionary if safely tries to get the image by
        /// name in a try catch block, and if the image doesn't exist it will load up
        /// a default image instead.
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static Image GetTileImageFromName(string imageName)
        {
            if (RegisteredImages.ContainsKey(imageName)) return RegisteredImages[imageName];
            imageName = imageName.Split('-')[0];
            if (RegisteredImages.ContainsKey(imageName)) return RegisteredImages[imageName];

            try
            {
                var img = (Image) Resources.ResourceManager.GetObject(imageName);
                RegisteredImages.Add(imageName, img ?? Resources.grass);

                if (img != null)
                {
                    RegisteredImages.Add(imageName + "-dim", AlterBrightness(img, -0.7f));
                    RegisteredImages.Add(imageName + "-bright", AlterBrightness(img, 0.3f));
                    RegisteredImages.Add(imageName + "-superbright", AlterBrightness(img, 0.7f));
                    RegisteredImages.Add(imageName + "-red", AlterBrightness(img, 0.35f, -0.35f, -0.35f));
                }
            }
            catch (Exception)
            {
                if (!RegisteredImages.ContainsKey(imageName))
                    RegisteredImages.Add(imageName, Resources.grass);
            }

            return RegisteredImages[imageName];
        }

        /// <summary>
        /// Alters the brightness of the passed in <see cref="Image"/>.
        /// </summary>
        /// <param name="original">The original <see cref="Image"/> that you want to adjust the brightness of.</param>
        /// <param name="adjR">-1 makes the image black, 1 makes it red, 0 doesn't change the original <see cref="Image"/></param>
        /// <param name="adjG">-1 makes the image black, 1 makes it green, 0 doesn't change the original <see cref="Image"/></param>
        /// <param name="adjB">-1 makes the image black, 1 makes it blue, 0 doesn't change the original <see cref="Image"/></param>
        /// <returns>A copy of the original <see cref="Image"/> with is brightness adjusted.</returns>
        private static Image AlterBrightness(Image original, float adjR, float adjG, float adjB)
        {
            if (adjR < -1 || adjR > 1)
                throw new ArgumentException("Adjustment amount must be between -1 and 1.");

            var oldBitmap = new Bitmap(original);
            var newBitmap = new Bitmap(original);

            for (var x = 0; x < original.Width; x++)
            {
                for (var y = 0; y < original.Height; y++)
                {
                    var oldColor = oldBitmap.GetPixel(x, y);

                    var newColorR = AlterBrightnessHelper(oldColor.R, adjR);
                    var newColorG = AlterBrightnessHelper(oldColor.G, adjG);
                    var newColorB = AlterBrightnessHelper(oldColor.B, adjB);

                    var newColor = Color.FromArgb(newColorR, newColorG, newColorB);

                    newBitmap.SetPixel(x, y, oldColor.A == 0 ? Color.Lime : newColor);
                }
            }

            newBitmap.MakeTransparent(Color.Lime);
            return newBitmap;
        }

        private static int AlterBrightnessHelper(int color, float adj)
        {
            int adjustedBrightness;
            if (adj < 0)
                adjustedBrightness = (int) (color * (adj + 1));
            else
                adjustedBrightness = 255 - (int)((255 - color) * (1 - adj));

            if (adjustedBrightness < 0) adjustedBrightness = 0;
            if (adjustedBrightness > 255) adjustedBrightness = 255;
            return adjustedBrightness;
        }

        /// <summary>
        /// Alters the brightness of the passed in <see cref="Image"/>.
        /// </summary>
        /// <param name="original">The original <see cref="Image"/> that you want to adjust the brightness of.</param>
        /// <param name="adj">-1 makes the image black, 1 makes it white, 0 doesn't change the original <see cref="Image"/></param>
        /// <returns>A copy of the original <see cref="Image"/> with is brightness adjusted.</returns>
        private static Image AlterBrightness(Image original, float adj)
        {
            return AlterBrightness(original, adj, adj, adj);
        }
    }
}
