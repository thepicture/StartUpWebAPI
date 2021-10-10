using StartUpWebAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace StartUpWebAPI.Models
{
    public class UsersFlowPreparator
    {
        public static List<User> PrepareAndGetInfiniteUsers(IEnumerable<User> usersInput)
        {
            List<User> users = UserDistinctor
                .GetDistinctUsers(usersInput)
                .ToList();
            List<User> result;
            result = GetResultWithInfiniteUsers(users);

            return result;
        }

        private static List<User> GetResultWithInfiniteUsers(List<User> users)
        {
            List<User> result = users.Take(3).ToList();
            InsertInfiniteUsersIfAtLeastOneExists(users, result);
            return result;
        }

        private static void InsertInfiniteUsersIfAtLeastOneExists(List<User> users, List<User> result)
        {
            if (IsExistsAtLeastOneUser(users))
            {
                InsertInfiniteUsers(users, result);
            }
        }

        private static void InsertInfiniteUsers(List<User> users, List<User> result)
        {
            List<User> additonalUsers = new List<User>();
            int howManyUsersToAdd = 6 / result.Count;

            AddRangeOfAdditionalUsers(result, additonalUsers, howManyUsersToAdd);

            result.AddRange(additonalUsers);

            AddResidueUsers(users, result);
        }

        private static void AddResidueUsers(List<User> users, List<User> result)
        {
            if (users.Count < 4)
            {
                AddRepeatingUsers(users, result);
            }
            else
            {
                result.AddRange(users.Take(3));
            }
        }

        private static void AddRangeOfAdditionalUsers(List<User> result, List<User> additonalUsers, int howManyUsersToAdd)
        {
            for (int i = 0; i < howManyUsersToAdd; i++)
            {
                additonalUsers.AddRange(result);
            }
        }

        private static void AddRepeatingUsers(List<User> users, List<User> result)
        {
            for (int i = 0; i < 4; i++)
            {
                result.Add(users.FirstOrDefault());
            }
        }

        private static bool IsExistsAtLeastOneUser(List<User> users)
        {
            return users.Count != 0;
        }
    }
}