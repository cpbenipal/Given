using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Given.DataContext.Entities
{
    public partial class DBGIVENContext : DbContext
    {
        public DBGIVENContext()
        {
        }

        public DBGIVENContext(DbContextOptions<DBGIVENContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanySize> CompanySize { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<Gift> Gift { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=mssqldatabase.cvfwsbyjvqob.us-west-2.rds.amazonaws.com,1433;Database=dbgiven;user id=rootadmin;password=MJBig6tasdgouashd89asydh8y;Trusted_Connection=False;MultipleActiveResultSets=true;Integrated Security=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.CompanyName)
                    .HasName("IX_Company")
                    .IsUnique();

                entity.Property(e => e.CompanyId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.City).HasMaxLength(200);

                entity.Property(e => e.CompanyName).HasMaxLength(200);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Fax).HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.State).HasMaxLength(150);

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Website).HasMaxLength(200);

                entity.Property(e => e.Zip).HasMaxLength(10);

                entity.HasOne(d => d.CompanySize)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CompanySizeId)
                    .HasConstraintName("FK_Company_CompanySize");
            });

            modelBuilder.Entity<CompanySize>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Size).HasMaxLength(50);
            });

            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.HasIndex(e => e.FirstName)
                    .HasName("IX_Contacts_1");

                entity.HasIndex(e => e.Mobile)
                    .HasName("IX_Contacts")
                    .IsUnique();

                entity.HasIndex(e => e.PrimaryEmail)
                    .HasName("IX_Contacts_2");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PrimaryEmail)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SecondaryEmail).HasMaxLength(100);

                entity.Property(e => e.State).HasMaxLength(150);

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ZipCode).HasMaxLength(10);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Contacts_User");
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.UserId })
                    .HasName("IX_Designation")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Designation)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Designation_Designation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Designation)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Designation_User");
            });

            modelBuilder.Entity<Gift>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GiftDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Gift)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Gift_Contacts");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Gift)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Gift_Designation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Gift)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Gift_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.EmailConfirmedOn).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.InvitedOn).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Otp).HasMaxLength(10);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.UpdatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_User_Company");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_UserRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
