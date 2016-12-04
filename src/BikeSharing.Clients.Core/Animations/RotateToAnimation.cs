using BikeSharing.Clients.Core.Animations.Base;
using BikeSharing.Clients.Core.Helpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Animations
{
    public class RotateToAnimation : AnimationBase
    {
        public static readonly BindableProperty RotationProperty =
            BindableProperty.Create("Rotation", typeof(double), typeof(RotateToAnimation), 0.0d,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((RotateToAnimation)bindable).Rotation = (double) newValue);

        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        protected override Task BeginAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Target.RotateTo(Rotation, Convert.ToUInt32(Duration), EasingHelper.GetEasing(Easing));
        }

        protected override Task ResetAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Target.RotateTo(0, 0, null);
        }
    }

    public class RelRotateToAnimation : AnimationBase
    {
        public static readonly BindableProperty RotationProperty =
            BindableProperty.Create("Rotation", typeof(double), typeof(RelRotateToAnimation), 0.0d,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((RelRotateToAnimation)bindable).Rotation = (double)newValue);

        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        protected override Task BeginAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Target.RelRotateTo(Rotation, Convert.ToUInt32(Duration), EasingHelper.GetEasing(Easing));
        }

        protected override Task ResetAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Target.RelRotateTo(Rotation, Convert.ToUInt32(Duration), EasingHelper.GetEasing(Easing));
        }
    }

    public class RotateXToAnimation : AnimationBase
    {
        public static readonly BindableProperty RotationProperty =
            BindableProperty.Create("Rotation", typeof(double), typeof(RotateXToAnimation), 0.0d,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((RotateXToAnimation)bindable).Rotation = (double)newValue);

        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        protected override Task BeginAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Target.RotateXTo(Rotation, Convert.ToUInt32(Duration), EasingHelper.GetEasing(Easing));
        }

        protected override Task ResetAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Target.RotateXTo(Rotation, Convert.ToUInt32(Duration), EasingHelper.GetEasing(Easing));
        }
    }

    public class RotateYToAnimation : AnimationBase
    {
        public static readonly BindableProperty RotationProperty =
            BindableProperty.Create("Rotation", typeof(double), typeof(RotateYToAnimation), 0.0d,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((RotateYToAnimation)bindable).Rotation = (double)newValue);

        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        protected override Task BeginAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Target.RotateYTo(Rotation, Convert.ToUInt32(Duration), EasingHelper.GetEasing(Easing));
        }

        protected override Task ResetAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            return Target.RotateYTo(Rotation, Convert.ToUInt32(Duration), EasingHelper.GetEasing(Easing));
        }
    }
}