using FFImageLoading.Transformations;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Controls
{
    public partial class UserProfileImageControl
    {
        public static readonly BindableProperty ProfileImageProperty =
            BindableProperty.Create("ProfileImage", typeof(string), typeof(UserProfileImageControl), null);

        public string ProfileImage
        {
            get { return (string)GetValue(ProfileImageProperty); }
            set { SetValue(ProfileImageProperty, value); }
        }

        public static readonly BindableProperty UpdatePhotoCommandProperty =
            BindableProperty.Create("UpdatePhotoCommand", typeof(ICommand), typeof(UserProfileImageControl), null);

        public ICommand UpdatePhotoCommand
        {
            get { return (ICommand)GetValue(UpdatePhotoCommandProperty); }
            set { SetValue(UpdatePhotoCommandProperty, value); }
        }

        public static readonly BindableProperty BorderColorProperty =
           BindableProperty.Create("BorderColor", typeof(string), typeof(UserProfileImageControl), propertyChanged: OnBorderColorChanged);

        private static void OnBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            UserProfileImageControl control = bindable as UserProfileImageControl;
            control?.SetImageBorder(newValue as string);
        }

        public string BorderColor
        {
            get { return (string)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public UserProfileImageControl()
        {
            InitializeComponent();
        }

        private void SetImageBorder(string borderColor)
        {
            var transformation = new CircleTransformation
            {
                BorderHexColor = borderColor
            };

            Device.OnPlatform(
                iOS: () => transformation.BorderSize = 30,
                Default: () => transformation.BorderSize = 20);

            Photo.Transformations.Add(transformation);
        }
    }
}
