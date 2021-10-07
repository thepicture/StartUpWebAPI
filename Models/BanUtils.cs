using StartUpWebAPI.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StartUpWebAPI.Models
{
    public class BanUtils
    {
        /// <param name="user">Which user will be banned or unbanned.</param>
        /// <param name="entity">Entity from DbContext with members.</param>
        public static DbContext BanOrUnban(User user, object entity)
        {
            if (entity is Team)
            {
                BanOrUnban(user, entity as Team);
                return null;
            }
            else if (entity is StartUp)
            {
                return BanOrUnban(user, entity as StartUp);
            }

            return null;
        }

        private static void BanOrUnban(User user, Team team)
        {
            List<TeamOfUser> teamsOfUser = AppData.Context.TeamOfUser
                  .Where(s => s.User.Login.Equals(user.Login)
                              && s.TeamId == team.Id).ToList();

            bool isTryingToUnbanUser = teamsOfUser.Select(s => s.RoleType.Name).Contains("Забанен");

            List<TeamOfUser> teamsWhereIsNotBanned = teamsOfUser
                .Where(s => !s.RoleType.Name.Equals("Забанен")).ToList();

            if (isTryingToUnbanUser)
            {
                AppData.Context.TeamOfUser.RemoveRange(teamsOfUser);
            }
            else
            {
                TeamOfUser bannedOfTeam = new TeamOfUser();

                bannedOfTeam = new TeamOfUser
                {
                    RoleTypeId = AppData.Context.RoleType.First(r => r.Name.Equals("Забанен")).Id,
                    UserId = user.Id,
                    TeamId = team.Id
                };

                team.TeamOfUser.Add(bannedOfTeam);

                bool hasAnyTuples = teamsWhereIsNotBanned.Count != 0;

                if (hasAnyTuples)
                {
                    AppData.Context.TeamOfUser.RemoveRange(teamsWhereIsNotBanned);
                }
            }
        }
        private static DbContext BanOrUnban(User user, StartUp startUp)
        {
            using (StartUpBaseEntities context = new StartUpBaseEntities())
            {
                startUp = context.StartUp.Find(startUp.Id);

                List<StartUpOfUser> startUpsOfUser = context.StartUpOfUser
                        .Where(s => s.User.Login.Equals(user.Login)
                                    && s.StartUpId == startUp.Id).ToList();

                bool isTryingToUnbanUser = startUpsOfUser.Select(s => s.RoleType.Name).Contains("Забанен");

                List<StartUpOfUser> startUpsWhereIsNotBanned = startUpsOfUser
                    .Where(s => !s.RoleType.Name.Equals("Забанен")).ToList();

                if (isTryingToUnbanUser)
                {
                    context.StartUpOfUser.RemoveRange(startUpsOfUser);
                }
                else
                {
                    StartUpOfUser bannedOfStartUp = new StartUpOfUser();

                    bannedOfStartUp = new StartUpOfUser
                    {
                        RoleTypeId = context.RoleType.First(r => r.Name.Equals("Забанен")).Id,
                        UserId = user.Id,
                        StartUpId = startUp.Id
                    };

                    startUp.StartUpOfUser.Add(bannedOfStartUp);

                    bool hasAnyTuples = startUpsWhereIsNotBanned.Count != 0;

                    if (hasAnyTuples)
                    {
                        context.StartUpOfUser.RemoveRange(startUpsWhereIsNotBanned);
                    }
                }

                return context;
            }
        }
    }
}