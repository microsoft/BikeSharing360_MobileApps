using BikeSharing.Clients.Core.Pages.SignUp;
using BikeSharing.Clients.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Pages
{
    public partial class SignUpPage : ContentPage
    {
        // Define Card Stack movements
        const int NumCards = 6;

        // Scale of back cards
        const float BackCardScale = 0.9f;

        // Card rotation
        const float CardRotationAdjuster = 0.5f;

        // Degrees to radians
        const float DegreesToRadians = 57.2957795f;

        // Swipe animation length
        const int AnimationDuration = 250;

        // Card collection
        private List<ContentView> _cards;

        // The card at the top of the stack
        private int _topCardIndex;

        // The last items index added to the stack of the cards
        private int _itemIndex = 0;

        // Distance the card has been moved
        private float _cardDistance = 0;

        // Flag to manage swipe gestures activation
        private bool _ignoreTouch = false;

        // Flag for page initialization
        private bool _isInitialized = false;

        public SignUpPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            // Initialize card stack
            _cards = new List<ContentView>();

            // Disable gestures (initially)
            AllowGestures = false;

            // Add back cards (3D effect)
            AddBackCards();
        }

        public int CardMoveDistance { get; set; }

        public bool AllowGestures { get; set; }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            // BindingContext
            var vm = BindingContext as SignUpViewModel;

            if (vm == null)
                return;

            // Credentials Card
            var credentialCard = new CredentialPage();
            credentialCard.BindingContext = vm.CredentialViewModel;
            _cards.Add(credentialCard);

            // Account Card
            var accountPage = new AccountPage();
            accountPage.BindingContext = vm.AccountViewModel;
            _cards.Add(accountPage);

            // Genre selection Card
            var genreCard = new GenrePage();
            genreCard.BindingContext = vm.GenreViewModel;
            _cards.Add(genreCard);

            // User data Card
            var userCard = new UserPage();
            userCard.BindingContext = vm.UserViewModel;
            _cards.Add(userCard);

            // Payment data Card
            var paymentCard = new PaymentPage();
            paymentCard.BindingContext = vm.PaymentViewModel;
            _cards.Add(paymentCard);

            // Subscription data Card
            var subscriptionCard = new SubscriptionPage();
            subscriptionCard.BindingContext = vm.SubscriptionViewModel;
            _cards.Add(subscriptionCard);

            for (int i = 0; i < NumCards; i++)
            {
                var card = _cards[i];
                card.InputTransparent = (Device.OS == TargetPlatform.iOS) ? false : true;
                card.IsVisible = false;

                CardStackView.Children.Add(
                    card,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Width;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Height;
                    })
                );
            }

            // Init
            InitializeCards(false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            // Add gestures recognizer (Swipe)
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            SignUpView.GestureRecognizers.Add(panGesture);

            // Programatic Card changes     
            var vm = BindingContext as SignUpViewModel;

            if (vm == null)
                return;

            vm.OnCardChanged += async (sender, args) =>
            {
                await SwipeCard(AnimationDuration);
            };

            if(Device.OS == TargetPlatform.Android)
                await Task.Delay(750);
            else
                await Task.Delay(500);

            for (int i = 0; i < Math.Min(NumCards, _cards.Count); i++)
            {
                if (_itemIndex >= _cards.Count) break;
                var card = _cards[i];
                card.IsVisible = true;
            }
        }

        private async void InitializeCards(bool isCardVisible)
        {
            // Init Cards 
            _itemIndex = 0;

            // Set the top card
            _topCardIndex = 0;

            // Create a stack of cards
            for (int i = 0; i < Math.Min(NumCards, _cards.Count); i++)
            {
                if (_itemIndex >= _cards.Count) break;

                var card = _cards[i];
                card.InputTransparent = (Device.OS == TargetPlatform.iOS) ? false : true;

                if (isCardVisible)
                    card.IsVisible = true;
                else
                    card.IsVisible = (i != 0) ? false : true;

                card.Scale = GetScale(i);
                await card.RotateTo(0, 0);
                await card.TranslateTo(0, -card.Y, 0);

                CardStackView.LowerChild(card);
            }
        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var vm = BindingContext as SignUpViewModel;

            if (vm != null)
            {
                AllowGestures = vm.Validate(_itemIndex);
            }

            if (!AllowGestures)
            {
                return;
            }

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    OnPanStarted();
                    break;
                case GestureStatus.Running:
                    OnPanRunning((float)e.TotalX);
                    break;
                case GestureStatus.Completed:
                    OnPanCompleted();
                    break;
            }
        }

        public void OnPanStarted()
        {
            _cardDistance = 0;
        }

        public void OnPanRunning(float diff_x)
        {
            if (_ignoreTouch)
            {
                return;
            }

            var topCard = _cards[_topCardIndex];
            var backCard = _cards[GetCardIndex(_topCardIndex)];

            // Move the top card
            if (topCard.IsVisible)
            {
                // Move the card
                topCard.TranslationX = (diff_x);

                // Calculate card angle 
                float rotationAngel = (float)(CardRotationAdjuster *
                    Math.Min(diff_x / this.Width, 1.0f));
                topCard.Rotation = rotationAngel * DegreesToRadians;

                // Keep card distance
                _cardDistance = diff_x;
            }

            // Scale the backcard
            if (backCard.IsVisible)
            {
                backCard.Scale = Math.Min(BackCardScale +
                    Math.Abs((_cardDistance / CardMoveDistance) * (1.0f - BackCardScale)), 1.0f);
            }
        }

        public async void OnPanCompleted()
        {
            _ignoreTouch = true;

            if (_itemIndex >= (_cards.Count - 1))
            {
                InitializeCards(true);
                NextCard(_itemIndex - 1);
                _ignoreTouch = false;

                return;
            }

            var topCard = _cards[_topCardIndex];

            // Swiped off
            if (Math.Abs((int)_cardDistance) > CardMoveDistance)
            {
                // Move off the screen
                await topCard.TranslateTo(
                    _cardDistance > 0 ? this.Width : -this.Width, 0, AnimationDuration / 2,
                    Easing.SpringOut);

                topCard.IsVisible = false;

                NextCard(_itemIndex);

                // Next card
                ShowNextCard();

            }
            // Put the card back in the center
            else
            {
                // Move the top card back to the center
                await topCard.TranslateTo((-topCard.X), -topCard.Y, AnimationDuration, Easing.SpringOut);
                await topCard.RotateTo(0, AnimationDuration, Easing.SpringOut);

                // Scale the back card down
                var prevCard = _cards[GetCardIndex(_topCardIndex)];
                await prevCard.ScaleTo(BackCardScale, AnimationDuration, Easing.SpringOut);
            }

            _ignoreTouch = false;
        }

        private async void ShowNextCard()
        {
            var topCard = _cards[_topCardIndex];
            _topCardIndex = GetCardIndex(_topCardIndex);

            // If there are more cards to show, show the next card in to place of 
            // the card that was swipped off the screen
            if (_itemIndex < _cards.Count)
            {
                // Push it to the back z order
                CardStackView.LowerChild(topCard);

                // Reset its scale, opacity and rotation
                topCard.Scale = BackCardScale;
                await topCard.RotateTo(0, 0);
                await topCard.TranslateTo(0, -topCard.Y, 0);

                topCard.IsVisible = true;

                _itemIndex++;
            }
        }

        private int GetCardIndex(int topIndex)
        {
            switch (topIndex)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
                case 3:
                    return 4;
                case 4:
                    return 5;
                default:
                    return 0;
            }
        }

        private float GetScale(int index)
        {
            return (index == _topCardIndex) ? 1.0f : BackCardScale;
        }

        public void NextCard(int cardIndex)
        {
            var signUpViewModel = BindingContext as SignUpViewModel;

            if (signUpViewModel != null)
            {
                // Notify the card swipe gestures to ViewModel
                signUpViewModel.SwipeCardCommand.Execute(cardIndex + 1);
            }
        }

        private async Task SwipeCard(int duration)
        {
            // Programatic card swipe
            var animationDuration = duration / 2;

            OnPanStarted();
            await Task.Delay(50);
            OnPanRunning(-animationDuration);
            await Task.Delay(50);
            OnPanCompleted();
        }

        private async void AddBackCards()
        {
            double margin = 24;

            if (Device.OS == TargetPlatform.iOS)
            {
                margin = 30;
            }

            var backCard1 = new Grid
            {
                BackgroundColor = Color.Gray,
                Padding = new Thickness(0, 0, 0, 1)
            };

            backCard1.Children.Add(new BoxView { BackgroundColor = Color.White });

            backCard1.Scale = 0.9;

            await backCard1.TranslateTo(0, -backCard1.Y + margin, 0);

            BackCardStackView.Children.Add(
                   backCard1,
                   Constraint.Constant(0),
                   Constraint.Constant(0),
                   Constraint.RelativeToParent((parent) =>
                   {
                       return parent.Width;
                   }),
                   Constraint.RelativeToParent((parent) =>
                   {
                       return parent.Height;
                   })
               );

            BackCardStackView.LowerChild(backCard1);

            var backCard2 = new Grid
            {
                BackgroundColor = Color.Gray,
                Padding = new Thickness(0, 0, 0, 1)
            };

            backCard2.Children.Add(new BoxView { BackgroundColor = Color.White });

            backCard2.Scale = 0.8;

            await backCard2.TranslateTo(0, -backCard2.Y + (margin * 2), 0);

            BackCardStackView.Children.Add(
                   backCard2,
                   Constraint.Constant(0),
                   Constraint.Constant(0),
                   Constraint.RelativeToParent((parent) =>
                   {
                       return parent.Width;
                   }),
                   Constraint.RelativeToParent((parent) =>
                   {
                       return parent.Height;
                   })
               );

            BackCardStackView.LowerChild(backCard2);
        }
    }
}