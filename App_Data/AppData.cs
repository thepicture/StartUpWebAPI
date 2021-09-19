using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Entities
{
    /// <summary>
    /// Class for interacting with DbContext singletone and resources for every page in the WebAPI.
    /// </summary>
    public class AppData
    {
        static private StartUpBaseEntities context;

        public static StartUpBaseEntities Context
        {
            get
            {
                if (context == null)
                {
                    context = new StartUpBaseEntities();
                }

                return context;
            }
        }
    }
}