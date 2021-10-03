using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace StartUpWebAPI.Models
{
    /// <summary>
    /// Class for converting tuples to new representation.
    /// </summary>
    public class TupleToTextAndBoolConverter
    {
        /// <summary>
        /// Convert the tuples to text and bool representation.
        /// </summary>
        /// <param name="tuples">The tuples to convert.</param>
        /// <returns>A new representation of tuple in a string and bool format.</returns>
        public static IEnumerable<Tuple<string, bool>> ConvertToTextAndBoolTuple(IEnumerable<Tuple<Label, HtmlInputCheckBox>> tuples)
        {
            return tuples
                           .Select(i =>
                           {
                               string label = i.Item1.Text;
                               bool isChecked = i.Item2.Checked;

                               return Tuple.Create(label, isChecked);
                           });
        }

    }
}