using Xamarin.Forms;

/// <summary>
/// https://github.com/billreiss/xamlnative/tree/master/XamarinForms/CircularProgress
/// </summary>
namespace BikeSharing.Clients.Core.Controls
{
    public class CircularProgressControl : Grid
    {
        View progress1;
        View progress2;
        View background1;
        View background2;

        public CircularProgressControl()
        {
            progress1 = Device.OS == TargetPlatform.Windows ? CreateImage("Assets/progress_done") : CreateImage("progress_done");
            background1 = Device.OS == TargetPlatform.Windows ? CreateImage("Assets/progress_pending") : CreateImage("progress_pending");
            background2 = Device.OS == TargetPlatform.Windows ? CreateImage("Assets/progress_pending") : CreateImage("progress_pending");
            progress2 = Device.OS == TargetPlatform.Windows ? CreateImage("Assets/progress_done") : CreateImage("progress_done");
            HandleProgressChanged(1, 0);
        }

        private View CreateImage(string v1)
        {
            var img = new Image();
            img.Source = ImageSource.FromFile(v1 + ".png");
            this.Children.Add(img);
            return img;
        }

        public static BindableProperty ProgressProperty =
    BindableProperty.Create("Progress", typeof(double), typeof(CircularProgressControl), 0d, propertyChanged: ProgressChanged);

        private static void ProgressChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var c = bindable as CircularProgressControl;
            c.HandleProgressChanged(Clamp((double)oldValue, 0, 1), Clamp((double)newValue, 0, 1));
        }

        static double Clamp(double value, double min, double max)
        {
            if (value <= max && value >= min) return value;
            else if (value > max) return max;
            else return min;
        }

        private void HandleProgressChanged(double oldValue, double p)
        {
            if (p < .5)
            {
                if (oldValue >= .5)
                {
                    // this code is CPU intensive so only do it if we go from >=50% to <50%
                    background1.IsVisible = true;
                    progress2.IsVisible = false;
                    background2.Rotation = 180;
                    progress1.Rotation = 0;
                }
                double rotation = 360 * p;
                background1.Rotation = rotation;
            }
            else
            {
                if (oldValue < .5)
                {
                    // this code is CPU intensive so only do it if we go from <50% to >=50%
                    background1.IsVisible = false;
                    progress2.IsVisible = true;
                    progress1.Rotation = 180;
                }
                double rotation = 360 * p;
                background2.Rotation = rotation;
            }
        }

        public double Progress
        {
            get { return (double)this.GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }
    }
}
