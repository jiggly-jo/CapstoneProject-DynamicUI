using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Smartroom.Helpers;


namespace Smartroom.Utilities
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }
        private string characterTypeface;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            return imageSource;
        }
    }

    public class SidebarLabel : Label
    {
        public SidebarLabel()
        {
            double inches = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            TextColor = Xamarin.Forms.Color.FromHex("#29282a");
            FontFamily = "SofiaProSemibold";

            if (inches >= 820 && inches <= 834)
            {
                FontSize = 24;
            }
            else if (inches == 1024)
            {
                FontSize = 28;
            }
        }
    }


    public class HeadlineLabel : Label
    {
        public HeadlineLabel()
        {
            double inches = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            TextColor = Xamarin.Forms.Color.FromHex("#29282a");
            FontFamily = "SofiaProSemibold";

            VerticalTextAlignment = TextAlignment.Center;
            HorizontalTextAlignment = TextAlignment.Center;
            if (inches >= 820 && inches <= 834)
            {
                FontSize = 40;
            }
            else if (inches == 1024)
            {
                FontSize = 45;
            }
        }
    }

    public class Sublabel : Label
    {
        public Sublabel()
        {
            double inches = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            TextColor = Xamarin.Forms.Color.FromHex("#29282a");
            FontFamily = "SofiaProRegular";

            VerticalTextAlignment = TextAlignment.Center;
            HorizontalTextAlignment = TextAlignment.Center;
            if (inches >= 820 && inches <= 834)
            {
                FontSize = 34;
            }
            else if (inches == 1024)
            {
                FontSize = 39;
            }
        }
    }

    public class ValueLabel : Label
    {
        public ValueLabel()
        {
            double inches = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;


            FontFamily = "SofiaProRegular";
            TextColor = Xamarin.Forms.Color.FromHex("#29282a");
            VerticalTextAlignment = TextAlignment.Center;
            HorizontalTextAlignment = TextAlignment.Center;
            if (inches >= 820 && inches <= 834)
            {
                FontSize = 40;
            }
            else if (inches == 1024)
            {
                FontSize = 45;
            }

        }
    }

    public class MediaChannelButton : Forms9Patch.Button
    {
        public MediaChannelButton()
        {
            double wide = DeviceDisplay.MainDisplayInfo.Width;

            TextColor = Xamarin.Forms.Color.Black;
            BackgroundColor = Xamarin.Forms.Color.Transparent;
            FontFamily = "SofiaProRegular";
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BorderWidth = 0;

            BackgroundImage = new Forms9Patch.Image
            {
                Source = ImageSource.FromResource("Smartroom.Icons.MediaNumberButtonBackground.png"),
                Fill = Forms9Patch.Fill.AspectFit
            };
            AutomationProperties.SetIsInAccessibleTree(this, true);

        }
    }


    public class SourceButton : ImageButton
    {
        public SourceButton()
        {
            double wide = DeviceDisplay.MainDisplayInfo.Width;

            if (wide == 2388)
            {
                Padding = 20;
            }
            else if (wide == 2224)
            {
                Padding = 10;
            }


        }
    }

    public class SourceFrame : Frame
    {
        public SourceFrame()
        {
            double wide = DeviceDisplay.MainDisplayInfo.Width;

            if (wide == 2388)
            {
                CornerRadius = 30;
                Padding = 30;
            }
            else if (wide == 2224)
            {
                CornerRadius = 20;
                Padding = new Thickness(20, 30, 20, 30);
            }


        }
    }

    public class ControlSwitch : Switch
    {
        public ControlSwitch()
        {
            double dense = DeviceDisplay.MainDisplayInfo.Density;
            OnColor = Xamarin.Forms.Color.Red;
            BackgroundColor = Xamarin.Forms.Color.White;
            if (dense == 1)
            {

            }
            else if (dense == 2)
            {
                Scale = 2;
            }
        }
    }

    public class CircleProgressBar : SKCanvasView
    {



        public static BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string),
            typeof(CircleProgressBar), "5", BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static BindableProperty BarColorProperty = BindableProperty.Create(nameof(BarColor), typeof(Xamarin.Forms.Color),
            typeof(CircleProgressBar), Xamarin.Forms.Color.Black, BindingMode.OneWay,
            validateValue: (_, value) => value != null,
                propertyChanged: OnPropertyChangedInvalidate);

        public Xamarin.Forms.Color BarColor
        {
            get => (Xamarin.Forms.Color)GetValue(BarColorProperty);
            set => SetValue(BarColorProperty, value);
        }

        public static BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Xamarin.Forms.Color),
           typeof(CircleProgressBar), Xamarin.Forms.Color.White, BindingMode.OneWay,
           validateValue: (_, value) => value != null,
           propertyChanged: OnPropertyChangedInvalidate);

        public Xamarin.Forms.Color BackgroundColor
        {
            get => (Xamarin.Forms.Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color),
           typeof(CircleProgressBar), Xamarin.Forms.Color.DarkGray, BindingMode.OneWay,
           validateValue: (_, value) => value != null,
           propertyChanged: OnPropertyChangedInvalidate);

        public Xamarin.Forms.Color BorderColor
        {
            get => (Xamarin.Forms.Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (CircleProgressBar)bindable;

            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;

            float width = e.Info.Width;



            var percentage = float.Parse(Value) / 10;

            var percentDegrees = percentage * 360f;

            var height = e.Info.Height;

            float borderRadius;

            if (width < height)
                borderRadius = width / 2;
            else
                borderRadius = height / 2;

            float outsideRadius = borderRadius * .98f;

            float radius = outsideRadius * .61f;
            canvas.Clear();

            var borderCircle = new SKRoundRect(new SKRect(0, 0, borderRadius * 2, borderRadius * 2), borderRadius, borderRadius);
            var backgroundCircle = new SKRoundRect(new SKRect(borderRadius - outsideRadius, borderRadius - outsideRadius, borderRadius + outsideRadius, borderRadius + outsideRadius), outsideRadius, outsideRadius);
            var valueBounds = new SKRect((borderRadius * 2) * .08f, (borderRadius * 2) * .08f, (borderRadius * 2) * .92f, (borderRadius * 2) * .92f);
            var foreCircle = new SKRoundRect(new SKRect(borderRadius - radius, borderRadius - radius, borderRadius + radius, borderRadius + radius), radius, radius);

            var families = SKFontManager.CreateDefault();
            var fontlist = families.FontFamilies;
            SKPaint valueText = new SKPaint();
            valueText.TextSize = outsideRadius / 4;
            valueText.TextAlign = SKTextAlign.Center;
            valueText.Color = Color.FromHex("#333333").ToSKColor();
            valueText.Typeface = GetCharacterTypeFace();


            var borderColor = new SKPaint { Color = BorderColor.ToSKColor(), IsAntialias = true };
            var backgroundColor = new SKPaint { Color = BackgroundColor.ToSKColor(), IsAntialias = true };
            var valueColor = new SKPaint { Color = BarColor.ToSKColor(), IsAntialias = true };
            var TestColor = new SKPaint { Color = Color.Green.ToSKColor(), IsAntialias = true };

            canvas.DrawRoundRect(borderCircle, borderColor);
            canvas.DrawRoundRect(backgroundCircle, backgroundColor);
            using (SKPath path = new SKPath())
            {
                if (percentDegrees == 360)
                {
                    percentDegrees = 359;

                    SKPath fillPath = new SKPath();
                    fillPath.MoveTo(borderRadius, borderRadius);
                    fillPath.ArcTo(valueBounds, -100, 20, false);
                    fillPath.Close();
                    canvas.DrawPath(fillPath, valueColor);
                }
                path.MoveTo(borderRadius, borderRadius);
                path.ArcTo(valueBounds, -90, percentDegrees, false);
                path.Close();
                canvas.DrawPath(path, valueColor);
            }


            SKFont font = new SKFont(valueText.Typeface, 105f, 1f, 0);
            canvas.DrawRoundRect(foreCircle, backgroundColor);
            canvas.DrawText(Value, outsideRadius, outsideRadius + (valueText.TextSize / 2), font, valueText);


        }
        private SKTypeface GetCharacterTypeFace()
        {
            SKTypeface characterTypeface;



            characterTypeface = DependencyService.Get<SKFontFind>().GetSkiaTypefaceFromAssetFont();

            if (characterTypeface == null)
            {
                var fontManager = SKFontManager.Default;
                characterTypeface = fontManager.MatchCharacter('A');
            }


            return characterTypeface;
        }
    }


    public class FillBar : SKCanvasView
    {

        public static BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string),
            typeof(FillBar), "5", BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float),
            typeof(FillBar), 50f, BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static BindableProperty FillColorProperty = BindableProperty.Create(nameof(FillColor), typeof(Color),
            typeof(FillBar), Color.Black, BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public Color FillColor
        {
            get => (Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        public static BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color),
            typeof(FillBar), Color.White, BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (FillBar)bindable;

            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;

            float width = e.Info.Width;

            float radius = CornerRadius;

            var percentage = float.Parse(Value) / 10;

            if (percentage == .1f)
                percentage = .2f;


            var height = e.Info.Height;

            var percentageHeight = (float)percentage * height;

            canvas.Clear();

            var backgroundBar = new SKRoundRect(new SKRect(0, 0, width, height), radius, radius);
            var fillBar = new SKRoundRect(new SKRect(0, height, width, height - percentageHeight), radius, radius);

            var backsquare = new SKRect(0, 0, width, height * .3f);
            var squarebar = new SKRect(0, (height - percentageHeight) + radius, width, height - percentageHeight);

            var hidebar = new SKRect(0, (height - percentageHeight / 2), width, height - percentageHeight);

            var backgroundColor = new SKPaint { Color = BackgroundColor.ToSKColor(), IsAntialias = true };
            var fillColor = new SKPaint { Color = FillColor.ToSKColor(), IsAntialias = true };
            var transparentColor = new SKPaint { Color = Color.Transparent.ToSKColor(), IsAntialias = true };


            if (Value.Equals("10"))
            {
                canvas.DrawRect(backsquare, transparentColor);
                canvas.DrawRoundRect(fillBar, transparentColor);
            }
            else
            {

                canvas.DrawRoundRect(backgroundBar, backgroundColor);
                canvas.DrawRect(backsquare, backgroundColor);
                canvas.DrawRoundRect(fillBar, fillColor);
            }
            if (percentageHeight != height)
                canvas.DrawRect(squarebar, fillColor);

            if (Value.Equals("1"))
                canvas.DrawRect(hidebar, backgroundColor);

        }


    }

    public class Stage3FrameHoriz : SKCanvasView
    {

        public static BindableProperty PercentProperty = BindableProperty.Create(nameof(Percent), typeof(float),
            typeof(Stage3FrameHoriz), 50f, BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public float Percent
        {
            get => (float)GetValue(PercentProperty);
            set => SetValue(PercentProperty, value);
        }

        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float),
            typeof(Stage3FrameHoriz), 50f, BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (Stage3FrameHoriz)bindable;

            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;

            float width = e.Info.Width;

            float radius = CornerRadius;

            var percentage = Percent;

            var height = e.Info.Height;

            var percentageWidth = (float)percentage * width;

            canvas.Clear();

            var backgroundBar = new SKRoundRect(new SKRect(0, 0, width, height), radius, radius);
            var fillBar = new SKRoundRect(new SKRect(0, 0, percentageWidth * .95f, height), radius, radius);

            var squarebar = new SKRect(percentageWidth * .95f, 0, percentageWidth, height);

            // var hidebar = new SKRect(0, (height - percentageHeight / 2), width, height - percentageHeight);

            var backgroundColor = new SKPaint { Color = Constants.UofULightGray.ToSKColor(), IsAntialias = true };
            var fillColor = new SKPaint
            {
                IsAntialias = true,
                Shader = SKShader.CreateLinearGradient(
                    new SKPoint(squarebar.Left, squarebar.Top),
                    new SKPoint(squarebar.Right, squarebar.Top),
                    new[]
                    {
                        Constants.UofULightGray.ToSKColor(),
                        Color.FromHex("#eeeeee").ToSKColor()
                    },
                    new float[] { 0, 1 },
                    SKShaderTileMode.Clamp)

            };


            canvas.DrawRoundRect(backgroundBar, backgroundColor);
            canvas.DrawRoundRect(fillBar, fillColor);

            if (percentageWidth != Width)
                canvas.DrawRect(squarebar, fillColor);

            //    if (percentageHeight <= 1)
            //      canvas.DrawRect(hidebar, backgroundColor);

        }
    }

    public class Stage3FrameVert : SKCanvasView
    {

        public static BindableProperty PercentProperty = BindableProperty.Create(nameof(Percent), typeof(float),
            typeof(Stage3FrameHoriz), 50f, BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public float Percent
        {
            get => (float)GetValue(PercentProperty);
            set => SetValue(PercentProperty, value);
        }

        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float),
            typeof(Stage3FrameHoriz), 50f, BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (Stage3FrameVert)bindable;

            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;

            float width = e.Info.Width;

            float radius = CornerRadius;

            var percentage = Percent;

            var height = e.Info.Height;

            var percentageHeight = (float)percentage * height;

            canvas.Clear();

            var backgroundBar = new SKRoundRect(new SKRect(0, 0, width, height), radius, radius);
            var fillBar = new SKRoundRect(new SKRect(0, percentageHeight * .95f, width, percentageHeight), radius, radius);

            var squarebar = new SKRect(0, percentageHeight * .95f, width, percentageHeight);

            var hidebar = new SKRect(0, (height - percentageHeight / 2), width, percentageHeight);

            var backgroundColor = new SKPaint { Color = Constants.UofULightGray.ToSKColor(), IsAntialias = true };
            var fillColor = new SKPaint
            {
                IsAntialias = true,
                Shader = SKShader.CreateLinearGradient(
                    new SKPoint(squarebar.Left, squarebar.Top),
                    new SKPoint(squarebar.Left, squarebar.Bottom),
                    new[]
                    {

                        Constants.UofULightGray.ToSKColor(),
                      Color.FromHex("#DDDDDD").ToSKColor()

                    },
                    new float[] { 0, 1 },
                    SKShaderTileMode.Clamp)

            };


            canvas.DrawRoundRect(backgroundBar, backgroundColor);
            canvas.DrawRoundRect(fillBar, fillColor);

            //    if (percentageHeight != Height)
            //      canvas.DrawRect(squarebar, fillColor);

            //   if (percentageHeight <= 1)
            //     canvas.DrawRect(hidebar, backgroundColor); 

        }
    }

    public class ArrowCircles : SKCanvasView
    {


        private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (ArrowCircles)bindable;

            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            float height = e.Info.Height;

            float width = e.Info.Width;

            float halfWidth = width / 2;

            float halfHeight = height / 2;

            float radius1 = (halfWidth * 0.95f);
            float radius2 = (halfWidth * 0.94f);
            float radius3 = (halfWidth * 0.48f);
            float radius4 = (halfWidth * 0.47f);
            float radius5 = (halfWidth * 0.28f);
            float radius6 = (halfWidth * 0.27f);

            if (halfWidth > halfHeight)
            {
                radius1 = (halfHeight * 0.95f);
                radius2 = (halfHeight * 0.94f);
                radius3 = (halfHeight * 0.48f);
                radius4 = (halfHeight * 0.47f);
                radius5 = (halfHeight * 0.28f);
                radius6 = (halfHeight * 0.27f);
            }

            float arrowRadius = (halfWidth * 0.85f);

            int arrowWidthLR = (int)Math.Round(width * 0.087);
            int arrowHeightLR = (int)Math.Round(width * 0.149);
            int arrowWidthUD = (int)Math.Round(width * 0.149);
            int arrowHeightUD = (int)Math.Round(width * 0.087);

            canvas.Clear();


            var Circle1 = new SKRoundRect(new SKRect(halfWidth - radius1, halfHeight - radius1, halfWidth + radius1, halfHeight + radius1), radius1, radius1);
            var Circle2 = new SKRoundRect(new SKRect(halfWidth - radius2, halfHeight - radius2, halfWidth + radius2, halfHeight + radius2), radius2, radius2);
            var Circle3 = new SKRoundRect(new SKRect(halfWidth - radius3, halfHeight - radius3, halfWidth + radius3, halfHeight + radius3), radius3, radius3);
            var Circle4 = new SKRoundRect(new SKRect(halfWidth - radius4, halfHeight - radius4, halfWidth + radius4, halfHeight + radius4), radius4, radius4);
            var Circle5 = new SKRoundRect(new SKRect(halfWidth - radius5, halfHeight - radius5, halfWidth + radius5, halfHeight + radius5), radius5, radius5);
            var Circle6 = new SKRoundRect(new SKRect(halfWidth - radius6, halfHeight - radius6, halfWidth + radius6, halfHeight + radius6), radius6, radius6);

            var arrow1 = new SKRect(halfWidth - arrowRadius, halfHeight - (arrowHeightLR / 2), halfWidth - arrowRadius + arrowWidthLR, halfHeight + (arrowHeightLR / 2));
            var arrow2 = new SKRect(halfWidth + arrowRadius - arrowWidthLR, halfHeight - (arrowHeightLR / 2), halfWidth + arrowRadius, halfHeight + (arrowHeightLR / 2));
            var arrow3 = new SKRect(halfWidth - (arrowWidthUD / 2), halfHeight - arrowRadius, halfWidth + (arrowWidthUD / 2), halfHeight - arrowRadius + arrowHeightUD);
            var arrow4 = new SKRect(halfWidth - (arrowWidthUD / 2), halfHeight + arrowRadius - (arrowHeightUD), halfWidth + (arrowWidthUD / 2), halfHeight + arrowRadius);
            string ArrowLeftResource = "Smartroom.Icons.ArrowLeftRed.png";
            string ArrowRightResource = "Smartroom.Icons.ArrowRightRed.png";
            string ArrowUpResource = "Smartroom.Icons.ArrowUpRed.png";
            string ArrowDownResource = "Smartroom.Icons.ArrowDownRed.png";

            Assembly assembly = GetType().GetTypeInfo().Assembly;

            SKBitmap ArrowLeftBitmap;
            SKBitmap ArrowRightBitmap;
            SKBitmap ArrowUpBitmap;
            SKBitmap ArrowDownBitmap;
            using (Stream stream = assembly.GetManifestResourceStream(ArrowLeftResource))
            {
                ArrowLeftBitmap = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream(ArrowRightResource))
            {
                ArrowRightBitmap = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream(ArrowUpResource))
            {
                ArrowUpBitmap = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream(ArrowDownResource))
            {
                ArrowDownBitmap = SKBitmap.Decode(stream);
            }

            var CircleColor = new SKPaint { Color = Color.DarkGray.ToSKColor(), IsAntialias = true };
            var CircleColor2 = new SKPaint { Color = Color.White.ToSKColor(), IsAntialias = true };
            var CircleColor3 = new SKPaint { Color = Constants.UofULightGray.ToSKColor(), IsAntialias = true };

            canvas.DrawRoundRect(Circle1, CircleColor);
            canvas.DrawRoundRect(Circle2, CircleColor2);
            canvas.DrawRoundRect(Circle3, CircleColor);
            canvas.DrawRoundRect(Circle4, CircleColor3);
            canvas.DrawRoundRect(Circle5, CircleColor);
            canvas.DrawRoundRect(Circle6, CircleColor2);

            canvas.DrawBitmap(ArrowLeftBitmap, arrow1);
            canvas.DrawBitmap(ArrowRightBitmap, arrow2);
            canvas.DrawBitmap(ArrowUpBitmap, arrow3);
            canvas.DrawBitmap(ArrowDownBitmap, arrow4);

        }
    }

    public class ScrollFade : SKCanvasView
    {

        public string Direction
        {
            get => (string)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        public static BindableProperty DirectionProperty = BindableProperty.Create(nameof(Direction), typeof(string),
            typeof(ScrollFade), "Top", BindingMode.OneWay,
            validateValue: (_, value) => value != null,
            propertyChanged: OnPropertyChangedInvalidate);

        private static void OnPropertyChangedInvalidate(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (ScrollFade)bindable;

            if (oldvalue != newvalue)
                control.InvalidateSurface();
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            string direction = Direction;
            float height = e.Info.Height;

            float width = e.Info.Width;

            canvas.Clear();

            var squarebar = new SKRect(0, 0, width, height);

            SKPaint fillColor;

            if (direction.Equals("Top"))
            {

                fillColor = new SKPaint
                {
                    IsAntialias = true,
                    Shader = SKShader.CreateLinearGradient(
                        new SKPoint(squarebar.Left, squarebar.Top),
                        new SKPoint(squarebar.Left, squarebar.Bottom),
                        new[]
                        {
                            Constants.UofULightGray.ToSKColor(),
                            Color.Transparent.ToSKColor()
                        },
                        new float[] { 0, 1 },
                        SKShaderTileMode.Clamp)
                };
            }
            else
            {

                fillColor = new SKPaint
                {
                    IsAntialias = true,
                    Shader = SKShader.CreateLinearGradient(
                        new SKPoint(squarebar.Left, squarebar.Top),
                        new SKPoint(squarebar.Left, squarebar.Bottom),
                        new[]
                        {
                            Color.Transparent.ToSKColor(),
                            Constants.UofULightGray.ToSKColor()

                        },
                        new float[] { 0, 1 },
                        SKShaderTileMode.Clamp)
                };
            }

            canvas.DrawRect(squarebar, fillColor);

        }




    }


}