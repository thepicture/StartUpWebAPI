//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StartUpWebAPI.Entities
{
    using System;
    using System.Collections.Generic;
    [Serializable]
    public partial class TeamOfUser
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public int RoleTypeId { get; set; }
    
        public virtual RoleType RoleType { get; set; }
        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
    }
}
