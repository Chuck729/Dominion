using System;
using System.Collections.Generic;
using System.Linq;

namespace RHFYP
{
    public static class ExstentionMethods
    {
        public static List<T> Randomize<T>(this IEnumerable<T> source, int seed)
        {
            var rnd = new Random(seed);
            return source.OrderBy<T, int>((item) => rnd.Next()).ToList();
        }
    }
}
