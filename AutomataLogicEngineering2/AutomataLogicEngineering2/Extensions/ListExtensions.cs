namespace AutomataLogicEngineering2.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, Func<T, bool> condition)
        {
            var splitList = new List<T>();
            var listCount = list.Count();

            for (var index = 0; index < listCount; index++)
            {
                var value = list.ElementAt(index);
                if (condition(value) || index == listCount - 1)
                {
                    if (index == listCount - 1)
                    {
                        splitList.Add(value);
                    }
                    yield return splitList.ToList();
                    splitList.Clear();
                }
                else
                {
                    splitList.Add(value);
                }
            }
        }
    }
}
