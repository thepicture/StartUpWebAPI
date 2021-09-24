using StartUpWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Entities
{
    public partial class StartUpImage
    {
        public string ImageInBase64
        {
            get
            {
                string result = "data:image/jpg;base64,";

                result += Convert.ToBase64String(Image);

                return result;
            }
        }
    }
}