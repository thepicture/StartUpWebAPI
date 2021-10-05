using System;

namespace StartUpWebAPI.Models
{
    /// <summary>
    /// Class for checking if the comment poster posts too much.
    /// </summary>
    public class AntiSpamChecker
    {
        /// <summary>
        /// Checks if the last comment of the user is more recent than the given parameters.
        /// </summary>
        /// <param name="seconds">A delay between comments in seconds.</param>
        /// <param name="lastComment">The last comment's DateTime.</param>
        /// <returns>True if the last comment of the user is more recent than seconds, false otherwise.</returns>
        public static bool IsLastCommentRecentThat(int seconds, DateTime lastComment)
        {
            return DateTime.Now - lastComment < TimeSpan.FromSeconds(seconds);
        }
    }
}