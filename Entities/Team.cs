//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StartUpWebAPI.Entities
{
    using System;
    using System.Collections.Generic;
    [Serializable]
    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            this.StartUpOfTeam = new HashSet<StartUpOfTeam>();
            this.TeamComment = new HashSet<TeamComment>();
            this.TeamOfUser = new HashSet<TeamOfUser>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int MaxMembersCount { get; set; }
        public string Description { get; set; }
        public Nullable<int> RegionId { get; set; }
    
        public virtual Region Region { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StartUpOfTeam> StartUpOfTeam { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamComment> TeamComment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamOfUser> TeamOfUser { get; set; }
    }
}
