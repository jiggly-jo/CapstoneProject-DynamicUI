using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Smartroom.Services;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(Smartroom.iOS.CustomButtonRenderer), new[] { typeof(CustomVisual) })]
namespace Smartroom.iOS
{
    public class CustomButtonRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Cleanup
            }

            if (e.NewElement != null)
            {
                Element.BorderColor = Color.Black;
                Element.BorderWidth = 2.0;
                Control.TitleShadowOffset = new CoreGraphics.CGSize(1, 1);
                Control.SetTitleShadowColor(Color.Black.ToUIColor(), UIKit.UIControlState.Normal);
            }
        }
    }
}
