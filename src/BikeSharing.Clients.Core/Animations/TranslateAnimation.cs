using BikeSharing.Clients.Core.Animations.Base;
using BikeSharing.Clients.Core.Helpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Animations
{
    public class TranslateToAnimation : AnimationBase
    {
        public static readonly BindableProperty TranslateXProperty =
           BindableProperty.Create("TranslateX", typeof(double), typeof(TranslateToAnimation), 0.0d,
           propertyChanged: (bindable, oldValue, newValue) =>
           ((TranslateToAnimation)bindable).TranslateX = (double)newValue);

        public double TranslateX
        {
            get { return (double)GetValue(TranslateXProperty); }
            set { SetValue(TranslateXProperty, value); }
        }

        public static readonly BindableProperty TranslateYProperty =
           BindableProperty.Create("TranslateY", typeof(double), typeof(TranslateToAnimation), 0.0d,
           propertyChanged: (bindable, oldValue, newValue) =>
           ((TranslateToAnimation)bindable).TranslateY = (double)newValue);

        public double TranslateY
        {
            get { return (double)GetValue(TranslateYProperty); }
            set { SetValue(TranslateYProperty, value); }
        }

        protected override Task BeginAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Target.TranslateTo(TranslateX, TranslateY, Convert.ToUInt32(Duration), EasingHelper.GetEasing(Easing));
        }

        protected override Task ResetAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Target.TranslateTo(TranslateX, TranslateY, Convert.ToUInt32(Duration), EasingHelper.GetEasing(Easing));
        }
    }
}