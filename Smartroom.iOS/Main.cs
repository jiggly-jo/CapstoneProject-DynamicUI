using Smartroom.Models;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using static Smartroom.Utilities.Accessibility;

[assembly: ResolutionGroupName("Smartroom.Views")]
[assembly: ExportEffect(typeof(Smartroom.iOS.AccessibilityStyleEffect), "AccessibilityStyleEffect")]
[assembly: Xamarin.Forms.Dependency(typeof(Smartroom.iOS.Application))]
namespace Smartroom.iOS
{
    public class Application : IXamarinToast
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }

        const double LONG_DELAY = 3.5;
        const double SHORT_DELAY = 2.0;

        NSTimer alertDelay;
        UIAlertController alert;

        public void LongAlert(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }
        public void ShortAlert(string message)
        {
            ShowAlert(message, SHORT_DELAY);
        }

        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }

    public class AccessibilityStyleEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            AddAccessibilityStyle();
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (args.PropertyName == AccessibilityStyleProperty.PropertyName)
            {
                AddAccessibilityStyle();
            }
            else
            {
                base.OnElementPropertyChanged(args);
            }
        }

        void AddAccessibilityStyle()
        {
            var Style = Container.AccessibilityNavigationStyle;

            var newStyle = GetAccessibilityStyle(Element);

            if ((newStyle & AccessibilityStyle.Combined) > 0) Style |= UIAccessibilityNavigationStyle.Combined;

            Container.AccessibilityNavigationStyle = Style;
        }
    }
}