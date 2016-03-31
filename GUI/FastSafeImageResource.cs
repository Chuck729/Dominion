using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            try
            {
                var img = (Image)Resources.ResourceManager.GetObject(imageName);
                RegisteredImages.Add(imageName, img ?? Resources.error);
            }
            catch (Exception)
            {
                RegisteredImages.Add(imageName, Resources.error);
            }

            return RegisteredImages[imageName];
        }

        public static void RegisterImage(string imageName, Image image)
        {
            RegisteredImages.Add(imageName, image);
        }
    }
}
