using Android.Content;
using Xamarin.Forms;
using Smartroom.Services;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(Smartroom.Droid.CustomButtonRenderer), new[] { typeof(CustomVisual) })]
namespace Smartroom.Droid
    {
        public class CustomButtonRenderer : ButtonRenderer
        {
            public CustomButtonRenderer(Context context) : base(context)
            {
            }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                Element.BorderColor = Color.Black;
                Element.BorderWidth = 2.0;
                Control.SetShadowLayer(5, 3, 3, Color.Black.ToAndroid());
            }
        }
    }
}
