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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StartUpBaseEntities : DbContext
    {
        public StartUpBaseEntities()
            : base("name=StartUpBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DocumentOfStartUp> DocumentOfStartUp { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<RoleType> RoleType { get; set; }
        public virtual DbSet<StartUp> StartUp { get; set; }
        public virtual DbSet<StartUpComment> StartUpComment { get; set; }
        public virtual DbSet<StartUpImage> StartUpImage { get; set; }
        public virtual DbSet<StartUpOfTeam> StartUpOfTeam { get; set; }
        public virtual DbSet<StartUpOfUser> StartUpOfUser { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamComment> TeamComment { get; set; }
        public virtual DbSet<TeamOfUser> TeamOfUser { get; set; }
        public virtual DbSet<TypeOfUser> TypeOfUser { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
