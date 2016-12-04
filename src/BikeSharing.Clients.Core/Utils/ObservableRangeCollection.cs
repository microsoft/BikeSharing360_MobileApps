using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.Utils
{
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (var i in collection) Items.Add(i);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void RemoveRange(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (var i in collection) Items.Remove(i);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Replace(T item)
        {
            ReplaceRange(new T[] { item });
        }

        public void ReplaceRange(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            Items.Clear();
            foreach (var i in collection) Items.Add(i);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public ObservableRangeCollection()
            : base() { }

        public ObservableRangeCollection(IEnumerable<T> collection)
            : base(collection) { }
    }
}
