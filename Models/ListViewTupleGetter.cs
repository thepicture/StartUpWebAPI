using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace StartUpWebAPI.Models
{
    /// <summary>
    /// Class for getting tuples from an list view with checkboxes and labels in the item template.
    /// </summary>
    public class ListViewTupleGetter
    {
        /// <summary>
        /// Gets the tuples with list view item template key-value pairs.
        /// </summary>
        /// <returns>A tuple with list view item template key-value pairs.</returns>
        public static IEnumerable<Tuple<Label, HtmlInputCheckBox>> Get(ListView listView)
        {
            return listView.Items
                            .Select(i =>
                            {
                                Label label = (Label)i.FindControl("labelTemplate");
                                HtmlInputCheckBox checkBox = (HtmlInputCheckBox)i.FindControl("checkBoxTemplate");

                                return Tuple.Create(label, checkBox);
                            });
        }
    }
}