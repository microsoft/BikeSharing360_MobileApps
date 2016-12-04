using BikeSharing.Clients.Core.Helpers;
using BikeSharing.Clients.Core.Models.ReportIncident;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.ViewModels.Base;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSharing.Clients.Core.Pages
{
    public partial class iOSMainPage : TabbedPage
    {
        private Page _previousPage;

        public iOSMainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            CurrentPageChanged += OnCurrentPageChanged;
            MessagingCenter.Subscribe<Booking>(this, MessengerKeys.BookingRequested, OnBookingRequested);
            MessagingCenter.Subscribe<Booking>(this, MessengerKeys.BookingFinished, OnBookingFinished);
            MessagingCenter.Subscribe<ReportedIssue>(this, MessengerKeys.GoBackFromReportRequest, OnGoBackFromReportRequested);
        }

        public void AddPage(Page page, string title)
        {
            var navigationPage = new CustomNavigationPage(page)
            {
                Title = title,
                Icon = GetIconForPage(page)
            };

			if (page is BookingPage || page is ReportIncidentPage)
            {
                navigationPage.IsEnabled = Settings.CurrentBookingId != 0;
            }

            if (_previousPage == null)
            {
                _previousPage = page;
            }

            Children.Add(navigationPage);
        }

        public bool TrySetCurrentPage(Page requiredPage)
        {
            return TrySetCurrentPage(requiredPage.GetType());
        }

        public bool TrySetCurrentPage(Type requiredPageType)
        {
            CustomNavigationPage page = GetTabPageWithInitial(requiredPageType);

            if (page != null)
            {
                CurrentPage = null;
                CurrentPage = page;
            }

            return page != null;
        }

        public async Task ClearNavigationForPage(Type type)
        {
            CustomNavigationPage page = GetTabPageWithInitial(type);

            if (page != null)
            {
                await page.Navigation.PopToRootAsync(false);
            }
        }

        private CustomNavigationPage GetTabPageWithInitial(Type type)
        {
            CustomNavigationPage page = Children.OfType<CustomNavigationPage>()
                                                .FirstOrDefault(p =>
                                                {
                                                    return p.CurrentPage.Navigation.NavigationStack.Count > 0
                                                        ? p.CurrentPage.Navigation.NavigationStack[0].GetType() == type
                                                        : false;
                                                });

            return page;
        }

        private string GetIconForPage(Page page)
        {
            string icon = string.Empty;

            if (page is HomePage)
            {
                icon = "menu_ic_home";
            }
            else if (page is ProfilePage)
            {
                icon = "menu_ic_profile";
            }
            else if (page is MyRidesPage)
            {
                icon = "menu_ic_bike";
            }
            else if (page is ReportIncidentPage)
            {
                icon = "menu_ic_report_incident";
            }
            else if (page is BookingPage)
            {
                icon = "menu_ic_upcoming_ride";
            }

            return icon;
        }

        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            if (CurrentPage == null)
            {
                return;
            }

            if (!CurrentPage.IsEnabled)
            {
                CurrentPage = _previousPage;
            }
            else
            {
                _previousPage = CurrentPage;
                MessagingCenter.Send(this, MessengerKeys.iOSMainPageCurrentChanged);
            }
        }

        private void OnBookingRequested(Booking booking)
        {
            SetMenuItemStatus(typeof(BookingPage), true);
            SetMenuItemStatus(typeof(ReportIncidentPage), true);
        }

        private void OnBookingFinished(Booking booking)
        {
            SetMenuItemStatus(typeof(BookingPage), false);
        }

        private void SetMenuItemStatus(Type pageType, bool enabled)
        {
            Page page = Children.OfType<CustomNavigationPage>()
                                .Where(nav => nav.CurrentPage.GetType() == pageType)
                                .FirstOrDefault();

            if (page != null)
            {
                page.IsEnabled = enabled;
            }
        }

        private void OnGoBackFromReportRequested(ReportedIssue issue)
        {
            TrySetCurrentPage(typeof(HomePage));
        }
    }
}