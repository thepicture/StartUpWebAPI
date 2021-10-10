using StartUpWebAPI.Entities;
using System.Collections.Generic;

namespace StartUpWebAPI.Models
{
    interface IUserGetter<T>
    {
        IEnumerable<User> Get(T input);
    }
}
