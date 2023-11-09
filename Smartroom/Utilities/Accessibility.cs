using System;
using System.Linq;
using Xamarin.Forms;

namespace Smartroom.Utilities
{
    public static class Accessibility
    {
        [Flags]
        public enum AccessibilityStyle
        {
            Automatic,
            Combined
        }

        public static readonly BindableProperty AccessibilityStyleProperty =
            BindableProperty.CreateAttached("AccessibilityStyle", typeof(AccessibilityStyle), typeof(Accessibility), AccessibilityStyle.Automatic, propertyChanged: OnAccessibilityStyleChanged);

        public static AccessibilityStyle GetAccessibilityStyle(BindableObject view)
        {
            return (AccessibilityStyle)view.GetValue(AccessibilityStyleProperty);
        }

        public static void SetAccessibilityStyle(BindableObject view, AccessibilityStyle value)
        {
            view.SetValue(AccessibilityStyleProperty, value);
        }

        static void OnAccessibilityStyleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is View view))
            {
                return;
            }

            var newStyle = (AccessibilityStyle)newValue;
            var hasStyle = newStyle != AccessibilityStyle.Automatic;

            if (hasStyle)
            {
                if (!view.Effects.OfType<AccessibilityStyleEffect>().Any())
                {
                    view.Effects.Add(new AccessibilityStyleEffect());
                }
            }
            else
            {
                var accessibilityStyle = view.Effects.OfType<AccessibilityStyleEffect>().FirstOrDefault();
                if (accessibilityStyle != null)
                {
                    view.Effects.Remove(accessibilityStyle);
                }
            }
        }

        public class AccessibilityStyleEffect : RoutingEffect
        {
            public AccessibilityStyleEffect() : base("Smartroom.Views.AccessibilityStyleEffect")
            {
            }
        }
    }
}

