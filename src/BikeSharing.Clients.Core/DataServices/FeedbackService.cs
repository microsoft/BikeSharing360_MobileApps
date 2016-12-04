using BikeSharing.Clients.Core.DataServices.Base;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Helpers;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.ReportIncident;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.Services;
using System;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IRidesService _ridesService;
        private readonly ILocationProvider _locationProvider;

        public FeedbackService(IRequestProvider requestProvider, IRidesService ridesService, ILocationProvider locationProvider)
        {
            _requestProvider = requestProvider;
            _ridesService = ridesService;
            _locationProvider = locationProvider;
        }

        public async Task SendIssueAsync(ReportedIssue issue)
        {
            await AddUserAndBikeIdsToIssue(issue);
            await AddLocationToIssue(issue);

            UriBuilder builder = new UriBuilder(GlobalSettings.IssuesEndpoint);
            builder.Path = "api/Issues";

            string uri = builder.ToString();
            string result = await _requestProvider.PostAsync<ReportedIssue, string>(uri, issue);
        }

        private async Task AddUserAndBikeIdsToIssue(ReportedIssue issue)
        {
            if (Settings.UserId == 0)
            {
                throw new InvalidOperationException("UserId is not saved");
            }

            if (Settings.CurrentBookingId == 0)
            {
                throw new InvalidOperationException("CurrentBookingId is not saved");
            }

            Booking currentBooking = await _ridesService.GetBooking(Settings.CurrentBookingId);

            issue.BikeId = currentBooking.BikeId;
            issue.UserId = Settings.UserId;
        }

        private async Task AddLocationToIssue(ReportedIssue issue)
        {
            ILocationResponse location = await _locationProvider.GetPositionAsync();

            if (location is GeoLocation)
            {
                var geoLocation = location as GeoLocation;

                issue.Latitude = geoLocation.Latitude;
                issue.Longitude = geoLocation.Longitude;
            }
        }
    }
}
