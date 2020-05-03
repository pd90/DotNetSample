using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace test.Models
{
    public partial class SampleDatabaseContext : DbContext
    {
        public SampleDatabaseContext()
        {
        }

        public SampleDatabaseContext(DbContextOptions<SampleDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //  {
        // if (!optionsBuilder.IsConfigured)
        //  {
        //#warnin//g To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //         optionsBuilder.UseSqlServer("Server=DESKTOP-2S7OVUR\\SQLEXPRESS;Database=SampleDatabase;uid=sa;password=12345;Trusted_Connection=True;");
        //     }
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.Url).IsRequired();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.BlogId);
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.ToTable("user_table");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Firstname).HasColumnName("firstname");

                entity.Property(e => e.Lastname).HasColumnName("lastname");
            });
        }
    }
}
