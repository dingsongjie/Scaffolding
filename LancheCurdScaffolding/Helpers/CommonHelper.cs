using Microsoft.AspNet.Scaffolding;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LancheCurdScaffolding.Helpers
{
   internal class CommonHelper
    {
        /// <summary>
        /// iocn to imageSource
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static ImageSource ToImageSource(Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }
        public static TService GetService<TService>(CodeGenerationContext context) where TService : class
        {
            return (TService)context.ServiceProvider.GetService(typeof(TService));
        }
    }
}
