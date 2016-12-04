using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Behaviors
{
    public class ValidateOnTextChangedBehavior : Behavior<Entry>
    {
        private VisualElement _element;

        public static readonly BindableProperty ValidateCommandProperty =
                BindableProperty.Create("ValidateCommand", typeof(ICommand),
                    typeof(ValidateOnTextChangedBehavior), default(ICommand),
                    BindingMode.OneWay, null);

        public ICommand ValidateCommand
        {
            get { return (ICommand)GetValue(ValidateCommandProperty); }
            set { SetValue(ValidateCommandProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            _element = bindable;
            bindable.TextChanged += Bindable_TextChanged;
            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            _element = null;
            BindingContext = null;
            bindable.TextChanged -= Bindable_TextChanged;
            bindable.BindingContextChanged -= OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, System.EventArgs e)
        {
            BindingContext = _element?.BindingContext;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidateCommand != null && ValidateCommand.CanExecute(null))
            {
                ValidateCommand.Execute(null);
            }
        }
    }
}
