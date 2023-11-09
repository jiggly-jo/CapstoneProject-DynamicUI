using System;
using Xamarin.Forms;
using Smartroom.Views;
using System.IO;
using SkiaSharp;
using Foundation;
using Smartroom.Utilities;

[assembly: Xamarin.Forms.Dependency(typeof(GetSkiaFont))]
namespace Smartroom.Views
{
    public class GetSkiaFont : SKFontFind
    {

        public GetSkiaFont() { }

        public SKTypeface GetSkiaTypefaceFromAssetFont()
        {
            string fontFile = Path.Combine(NSBundle.MainBundle.BundlePath, "sofiapro-semibold.otf");
            SkiaSharp.SKTypeface typeFace;

            using (var asset = File.OpenRead(fontFile))
            {
                var fontStream = new MemoryStream();
                asset.CopyTo(fontStream);
                fontStream.Flush();
                fontStream.Position = 0;
                typeFace = SKTypeface.FromStream(fontStream);
            }

            return typeFace;
        }
    }
}
