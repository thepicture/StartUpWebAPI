using System;
using System.Collections.Generic;
using System.Linq;

namespace StartUpWebAPI.Models
{
    /// <summary>
    /// Class for getting values from key-value pairs of tuple collection.
    /// </summary>
    public class TupleValueGetter
    {
        /// <summary>
        /// Gets the values from the given tuples collection.
        /// </summary>
        /// <param name="tuples">The tuples to convert into values.</param>
        /// <returns>A collection of the strings.</returns>
        public static IEnumerable<string> GetValues(IEnumerable<Tuple<string, bool>> tuples)
        {
            return tuples.Where(t => t.Item2)
                   .Select(t => t.Item1);
        }
    }
}