using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace StartUpWebAPI.Models
{
    /// <summary>
    /// Class for getting region template.
    /// </summary>
    public class RegionTemplateBuilder
    {
        /// <summary>
        /// Gets the regions template.
        /// </summary>
        /// <returns>The regions template.</returns>
        public static string GetTemplate()
        {
            StringBuilder template = new StringBuilder();

            foreach (Region region in AppData.Context.Region.ToList())
            {
                template.Append("<li>" +
                    "<input name='" + region.Id + "' type='checkbox' id='region-input'" +
                    "<label for='" + region.Id + "'>" + region.Name + "</label>" +
                    "</li>");
            }

            return template.ToString();
        }
    }
}