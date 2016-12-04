namespace BikeSharing.Clients.Core.Models.ReportIncident
{
    public class ReportedIssue
    {
        public ReportedIssueType Type { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int BikeId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
