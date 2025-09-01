
using System;
using System.Collections.Generic;

namespace BannyCafe.Core.Utils
{
    public static class CollectionServe
    {
        public static void ActionTwoCollection<T1, T2>(IEnumerable<T1> enumerable, IEnumerable<T2> enumerable1, Action<T1, T2> action)
        {
            using (var enum1 = enumerable.GetEnumerator())
            {
                using (var enum2 = enumerable1.GetEnumerator())
                {
                    while (enum1.MoveNext() && enum2.MoveNext())
                    {
                        var item1 = enum1.Current;
                        var item2 = enum2.Current;
                        action(item1, item2);
                    }
                }
            }
        }

        public static bool FuncSearchDiff<T1, T2>(IEnumerable<T1> enumerable, IEnumerable<T2> enumerable1, Func<T1, T2, bool> predicate)
        {
            using (var enum1 = enumerable.GetEnumerator())
            {
                using (var enum2 = enumerable1.GetEnumerator())
                {
                    while (enum1.MoveNext() && enum2.MoveNext())
                    {
                        var item1 = enum1.Current;
                        var item2 = enum2.Current;
                        var res = predicate(item1, item2);
                    }
                }
            }

            return false;
        }
    }
}