using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Platform;

namespace Shadow_Pixel_Scaling_Bug {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            ContentPage mainPage = new();
            MainPage = mainPage;

            VerticalStackLayout vert = new VerticalStackLayout();
            vert.Margin = new Thickness(0, 30);
            mainPage.Content = vert;

            Border border = new Border();
            border.WidthRequest = border.HeightRequest = 200;
            border.StrokeShape = new RoundRectangle() { CornerRadius = 30 };
            border.BackgroundColor = Colors.Bisque;

            vert.Children.Add(border);

            border.HandlerChanged += delegate {
                setShadow(border, new Point(0, 10), 10, Colors.Red);
            };

        }

        public static void setShadow(View viewToSet, Point offset, float radius, Color color) {
            Point offsetScaled = offset;
            float radiusScaled = radius;

#if ANDROID
            bool scalePixels = true;
            if (scalePixels) {
                
                Android.Views.View viewAndroid = ElementExtensions.ToPlatform(viewToSet, viewToSet.Handler.MauiContext);
                offsetScaled.X = viewAndroid.Context.ToPixels(offsetScaled.X);
                offsetScaled.Y = viewAndroid.Context.ToPixels(offsetScaled.Y);
                radiusScaled = viewAndroid.Context.ToPixels(radius);
            }
#endif
            viewToSet.Shadow = new() { Offset = offsetScaled, Radius = radiusScaled, Brush = color };
        }
    }
}