using Foundation;
using System;
using System.Collections.Generic;
using WatchKit;
using System.Linq;

namespace BikeSharing.Clients.WatchOSApp.WatchOSExtension
{
    public partial class ToController : WKInterfaceController
    {
		private	string _from;
        private List<Station> _to;

        public ToController (IntPtr handle) : base (handle)
        {
			_to = new List<Station>();
        }

        public override void Awake(NSObject context)
        {
            base.Awake(context);
				
            // Configure interface objects here.
            Console.WriteLine("{0} awake with context", this);
    		
			foreach (var station in Stations.All)
			{
				_to.Add(station);
			}

			if (context is NSString)
			{
				_from = context.ToString();
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
            ToTable.SetNumberOfRows((nint)_to.Count, "default");

            for (var i = 0; i < _to.Count; i++)
            {
                var elementRow = (ToRowController)ToTable.GetRowController(i);

				elementRow.ToLabel.SetText(_to[i].Name);
            }
        }

        public override NSObject GetContextForSegue(string segueIdentifier, WKInterfaceTable table, nint rowIndex)
        {
			return new NSString(string.Format("{0},{1}", _from, _to[(int)rowIndex].Id));
        }

        public override void DidSelectRow(WKInterfaceTable table, nint rowIndex)
        {
            var rowData = _to[(int)rowIndex];
            Console.WriteLine("Row selected:" + rowData);
        }
    }
}