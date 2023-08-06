using Microsoft.EntityFrameworkCore;
using minimalAPI.Entities;

namespace minimalAPI.Context
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Teacher> Teachers { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder builder = new();

            builder.AddJsonFile("appsettings.json");

            IConfigurationRoot config = builder.Build();

            var connectionString = config.GetConnectionString("MyAcademy");
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.PersonId, "IX_Employees_PersonId").IsUnique();

                entity.Property(e => e.Salary)
                    .HasDefaultValue(3000.0m)
                    .HasColumnType("smallmoney");

                entity.HasOne(d => d.Person).WithOne(p => p.Employee).HasForeignKey<Employee>(d => d.PersonId);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Year).HasDefaultValue(1L);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.GroupId, "IX_Students_GroupId");

                entity.HasIndex(e => e.PersonId, "IX_Students_PersonId").IsUnique();

                entity.Property(e => e.Gpa).HasColumnName("GPA");

                entity.HasOne(d => d.Group).WithMany(p => p.Students).HasForeignKey(d => d.GroupId);

                entity.HasOne(d => d.Person).WithOne(p => p.Student).HasForeignKey<Student>(d => d.PersonId);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId, "IX_Teachers_EmployeeId").IsUnique();

                entity.HasIndex(e => e.GroupId, "IX_Teachers_GroupId");

                entity.HasOne(d => d.Employee).WithOne(p => p.Teacher).HasForeignKey<Teacher>(d => d.EmployeeId);

                entity.HasOne(d => d.Group).WithMany(p => p.Teachers).HasForeignKey(d => d.GroupId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07403A853C");

                entity.HasIndex(e => e.Email, "UQ__Users__A9D105340B98884D").IsUnique();

                entity.Property(e => e.Email).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
