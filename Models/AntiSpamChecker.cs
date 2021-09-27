using StartUpWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Models
{
    public class AntiSpamChecker
    {
        public static bool IsLastCommentRecentThat(int seconds, DateTime lastComment)
        {
            return DateTime.Now - lastComment < TimeSpan.FromSeconds(5);
        }
    }
}