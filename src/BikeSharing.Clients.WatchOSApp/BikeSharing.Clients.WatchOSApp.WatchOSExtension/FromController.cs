using Foundation;
using System;
using System.Collections.Generic;
using WatchKit;

namespace BikeSharing.Clients.WatchOSApp.WatchOSExtension
{
    public partial class FromController : WKInterfaceController
    {
        List<Station> _from;

        protected FromController(IntPtr handle) : base(handle)
        {
            _from = new List<Station>();
        }

        public override void Awake(NSObject context)
        {
            base.Awake(context);

            // Configure interface objects here.
            Console.WriteLine("{0} awake with context", this);

			foreach (var station in Stations.All)
			{
				_from.Add(station);
			}
		}

        public override void WillActivate()
        {
            // This method is called when the watch view controller is about to be visible to the user.
            Console.WriteLine("{0} will activate", this);

            LoadTableRows();
        }

        public override void DidDeactivate()
        {
            // This method is called when the watch view controller is no longer visible to the user.
            Console.WriteLine("{0} did deactivate", this);
        }

        private void LoadTableRows()
        {
            FromTable.SetNumberOfRows((nint)_from.Count, "default");

            for (var i = 0; i < _from.Count; i++)
            {
                var elementRow = (FromRowController)FromTable.GetRowController(i);

				elementRow.FromLabel.SetText(_from[i].Name);
            }
        }

        public override NSObject GetContextForSegue(string segueIdentifier, WKInterfaceTable table, nint rowIndex)
        {
			return new NSString(_from[(int)rowIndex].Id.ToString());
        }

        public override void DidSelectRow(WKInterfaceTable table, nint rowIndex)
        {
            var rowData = _from[(int)rowIndex];
            Console.WriteLine("Row selected:" + rowData);
        }
    }
}