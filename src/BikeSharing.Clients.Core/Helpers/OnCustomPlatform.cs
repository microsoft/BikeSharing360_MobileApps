namespace BikeSharing.Clients.Core.Helpers
{
    public sealed class OnCustomPlatform<T>
    {
        public OnCustomPlatform()
        {
            Android = default(T);
            iOS = default(T);
            WinPhone = default(T);
            Windows = default(T);
            Other = default(T);
        }

        public T Android { get; set; }

        public T iOS { get; set; }

        public T WinPhone { get; set; }

        public T Windows { get; set; }

        public T Other { get; set; }

        public static implicit operator T(OnCustomPlatform<T> onPlatform)
        {
            switch (Xamarin.Forms.Device.OS)
            {
                case Xamarin.Forms.TargetPlatform.Android:
                    return onPlatform.Android;
                case Xamarin.Forms.TargetPlatform.iOS:
                    return onPlatform.iOS;
                case Xamarin.Forms.TargetPlatform.WinPhone:
                    return onPlatform.WinPhone;
                case Xamarin.Forms.TargetPlatform.Windows:
                    if(Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Desktop)
                        return onPlatform.Windows;
                    else
                        return onPlatform.WinPhone;
                default:
                    return onPlatform.Other;
            }
        }
    }
}