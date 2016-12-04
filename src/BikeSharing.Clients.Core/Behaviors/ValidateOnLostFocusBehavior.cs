using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Behaviors
{
    public class ValidateOnLostFocusBehavior : Behavior<VisualElement>
    {
        private VisualElement _element;

        public static readonly BindableProperty ValidateCommandProperty =
                BindableProperty.Create("ValidateCommand", typeof(ICommand), 
                    typeof(ValidateOnLostFocusBehavior), default(ICommand), 
                    BindingMode.OneWay, null);

        public ICommand ValidateCommand
        {
            get { return (ICommand)GetValue(ValidateCommandProperty); }
            set { SetValue(ValidateCommandProperty, value); }
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            _element = bindable;
            bindable.Unfocused += Bindable_Unfocused;
            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            _element = null;
            BindingContext = null;
            bindable.Unfocused -= Bindable_Unfocused;
            bindable.BindingContextChanged -= OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, System.EventArgs e)
        {
            BindingContext = _element?.BindingContext;
        }

        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            if (ValidateCommand != null && ValidateCommand.CanExecute(null))
            {
                ValidateCommand.Execute(null);
            }
        }
    }
}