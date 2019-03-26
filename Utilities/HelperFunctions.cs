using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Utilities
{
    public static class HelperFunctions
    {
        public static void PutInTheRightPlace<T>(ObservableCollection<T> collection, T element)
            where T : IComparable<T>
        {
            int place = FindRightPlace(collection, element);

            collection.Insert(place, element);
        }

        public static void RemoveElement<T>(ObservableCollection<T> collection, T element)
        {
            collection.Remove(element);
        }

        private static int FindRightPlace<T>(ObservableCollection<T> collection, T element)
            where T : IComparable<T>
        {
            int start = 0;
            int end = collection.Count - 1;

            while (start <= end)
            {
                int mid = (start + end) / 2;

                if (collection[mid].CompareTo(element) > 0)
                    end = mid - 1;
                else
                    start = mid + 1;
            }

            return start;
        }
    }
}
