using System.Linq;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Effects
{
    public static class LineColorEffect
    {
        public static readonly BindableProperty ApplyLineColorProperty =
            BindableProperty.CreateAttached("ApplyLineColor", typeof(bool), typeof(LineColorEffect), false, propertyChanged: OnApplyLineColorChanged);

        public static bool GetApplyLineColor(BindableObject view)
        {
            return (bool)view.GetValue(ApplyLineColorProperty);
        }

        public static void SetApplyLineColor(BindableObject view, bool value)
        {
            view.SetValue(ApplyLineColorProperty, value);
        }

        private static void OnApplyLineColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;

            if (view == null)
            {
                return;
            }

            bool hasShadow = (bool)newValue;

            if (hasShadow)
            {
                view.Effects.Add(new EntryLineColorEffect());
                view.Effects.Add(new DatePickerLineColorEffect());
                view.Effects.Add(new PickerLineColorEffect());
            }
            else
            {
                var entryLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is EntryLineColorEffect);
                if (entryLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(entryLineColorEffectToRemove);
                }

                var datePickerLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is DatePickerLineColorEffect);
                if (datePickerLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(datePickerLineColorEffectToRemove);
                }

                var pickerLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is PickerLineColorEffect);
                if (pickerLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(pickerLineColorEffectToRemove);
                }
            }
        }

        public static readonly BindableProperty LineColorProperty =
            BindableProperty.CreateAttached("LineColor", typeof(Color), typeof(LineColorEffect), Color.Default);

        public static Color GetLineColor(BindableObject view)
        {
            return (Color)view.GetValue(LineColorProperty);
        }

        public static void SetLineColor(BindableObject view, Color value)
        {
            view.SetValue(LineColorProperty, value);
        }

        class EntryLineColorEffect : RoutingEffect
        {
            public EntryLineColorEffect() : base("BikeSharing.EntryLineColorEffect")
            {
            }
        }

        class DatePickerLineColorEffect : RoutingEffect
        {
            public DatePickerLineColorEffect() : base("BikeSharing.DatePickerLineColorEffect")
            {
            }
        }

        class PickerLineColorEffect : RoutingEffect
        {
            public PickerLineColorEffect() : base("BikeSharing.PickerLineColorEffect")
            {
            }
        }
    }
}
