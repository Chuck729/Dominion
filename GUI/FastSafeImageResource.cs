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
                var img = (Image)Resources.ResourceManager.GetObject(imageName);
                RegisteredImages.Add(imageName, img ?? Resources.grass);

                if (img != null)
                {
                    RegisteredImages.Add(imageName + "-dim", AlterBrightness(img, -0.7f));
                    RegisteredImages.Add(imageName + "-bright", AlterBrightness(img, 0.3f));
                    RegisteredImages.Add(imageName + "-superbright", AlterBrightness(img, 0.7f));
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
        /// Manually add an image resource to the RegisteredImages dictionary.
        /// </summary>
        /// <param name="imageName">The <see cref="string"/> that the image should be looked up with.</param>
        /// <param name="image">The <see cref="Image"/> that you want to be returned when the <paramref name="imageName"/> is looked up.</param>
        public static void RegisterImage(string imageName, Image image)
        {
            RegisteredImages.Add(imageName, image);
        }

        /// <summary>
        /// Alters the brightness of the passed in <see cref="Image"/>.
        /// </summary>
        /// <param name="original">The original <see cref="Image"/> that you want to adjust the brightness of.</param>
        /// <param name="adjustmnetAmount">-1 makes the image black, 1 makes it white, 0 doesn't change the original <see cref="Image"/></param>
        /// <returns>A copy of the original <see cref="Image"/> with is brightness adjusted.</returns>
        private static Image AlterBrightness(Image original, float adjustmnetAmount)
        {
            if (adjustmnetAmount < -1 || adjustmnetAmount > 1)
                throw new ArgumentException("Adjustment amount must be between -1 and 1.");

            var oldBitmap = new Bitmap(original);
            var newBitmap = new Bitmap(original);

            for (var x = 0; x < original.Width; x++)
            {
                for (var y = 0; y < original.Height; y++)
                {
                    var oldColor = oldBitmap.GetPixel(x, y);

                    if (oldColor.A == 0)
                    {
                        newBitmap.SetPixel(x, y, Color.Lime);
                        continue;
                    }

                    var newColorR = 0;
                    var newColorG = 0;
                    var newColorB = 0;

                    if (adjustmnetAmount < 0)
                    {
                        newColorR = (int) (oldColor.R*++adjustmnetAmount);
                        newColorG = (int) (oldColor.G*adjustmnetAmount);
                        newColorB = (int) (oldColor.B*adjustmnetAmount--);
                    }
                    else
                    {
                        adjustmnetAmount = 1 - adjustmnetAmount;
                        newColorR += 255-(int) ((255 - oldColor.R)*adjustmnetAmount);
                        newColorG += 255-(int) ((255 - oldColor.G)*adjustmnetAmount);
                        newColorB += 255-(int) ((255 - oldColor.B)*adjustmnetAmount);
                        adjustmnetAmount = 1 - adjustmnetAmount;
                    }

                    if (newColorR < 0) newColorR = 0;
                    if (newColorG < 0) newColorG = 0;
                    if (newColorB < 0) newColorB = 0;

                    if (newColorR > 255) newColorR = 255;
                    if (newColorG > 255) newColorG = 255;
                    if (newColorB > 255) newColorB = 255;

                    var newColor = Color.FromArgb(newColorR, newColorG, newColorB);

                    newBitmap.SetPixel(x, y, newColor);
                }
            }

            newBitmap.MakeTransparent(Color.Lime);
            return newBitmap;
        }
    }
}
