using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace StartUpWebAPI.Models
{
    /// <summary>
    /// Finds the controls in asp: elements by id.
    /// </summary>
    public class RecursiveControlFinder
    {
        /// <summary>
        /// Finds control recursively.
        /// </summary>
        /// <param name="rootControl">The ancestor.</param>
        /// <param name="controlID">The id of the control to look.</param>
        /// <returns></returns>
        public static Control FindControlRecursive(Control rootControl, string controlID)
        {
            if (rootControl.ID == controlID) return rootControl;

            foreach (Control controlToSearch in rootControl.Controls)
            {
                Control controlToReturn = FindControlRecursive(controlToSearch, controlID);
                if (controlToReturn != null) return controlToReturn;
            }
            return null;
        }

        /// <summary>
        /// Finds all controls requested by the controlID.
        /// </summary>
        /// <param name="rootControl">The parent control.</param>
        /// <param name="controlID">The control id.</param>
        /// <returns></returns>
        public static List<Control> FindControlRecursiveAll(Control rootControl, string controlID)
        {
            List<Control> result = new List<Control>();

            if (rootControl.ID == controlID)
            {
                result.Add(rootControl);
            }

            foreach (Control controlToSearch in rootControl.Controls)
            {
                List<Control> listOfControlToReturn = FindControlRecursiveAll(controlToSearch, controlID);
                bool isHasNewElements = listOfControlToReturn.Count > 0;

                if (isHasNewElements) return result.Union(listOfControlToReturn)
                        .Distinct()
                        .ToList();
            }

            return result;
        }
    }
}