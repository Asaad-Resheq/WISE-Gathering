namespace ProjectRequirements.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProjectsRequirementsDBContext : DbContext
    {
        public ProjectsRequirementsDBContext()
            : base("name=ProjectsRequirementsDBContext")
        {
        }

        public virtual DbSet<C_Users> C_Users { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
       
        public virtual DbSet<Requirement> Requirements { get; set; }
        public virtual DbSet<Stickholder> Stickholders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<C_Users>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Requirements)
                .WithOptional(e => e.Project)
                .WillCascadeOnDelete();

         
        }
    }
}
