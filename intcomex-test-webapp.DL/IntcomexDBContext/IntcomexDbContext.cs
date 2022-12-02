using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using intcomex_test_webapp.DL.Entities;

namespace intcomex_test_webapp.DL.IntcomexDBContext
{
    public partial class IntcomexDbContext : DbContext
    {
        public IntcomexDbContext()
        {
        }

        public IntcomexDbContext(DbContextOptions<IntcomexDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DeptEmp> DeptEmps { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=35.223.30.36;port=3306;database=intcomexdb;user=admin522;password=admin522", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.26-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.HasKey(e => e.ContactTypeNo)
                    .HasName("PRIMARY");

                entity.ToTable("contact_type");

                entity.HasIndex(e => e.ContactTypeName, "contact_type_name")
                    .IsUnique();

                entity.Property(e => e.ContactTypeNo)
                    .HasMaxLength(2)
                    .HasColumnName("contact_type_no")
                    .IsFixedLength();

                entity.Property(e => e.ContactTypeName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("contact_type_name");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptNo)
                    .HasName("PRIMARY");

                entity.ToTable("departments");

                entity.HasIndex(e => e.DeptName, "dept_name")
                    .IsUnique();

                entity.Property(e => e.DeptNo)
                    .HasMaxLength(4)
                    .HasColumnName("dept_no")
                    .IsFixedLength();

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("dept_name");
            });

            modelBuilder.Entity<DeptEmp>(entity =>
            {
                entity.HasKey(e => new { e.DeptNo, e.EmpNo })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("dept_emp");

                entity.HasIndex(e => e.EmpNo, "emp_no");

                entity.Property(e => e.DeptNo)
                    .HasMaxLength(4)
                    .HasColumnName("dept_no")
                    .IsFixedLength();

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate).HasColumnName("from_date");

                entity.Property(e => e.ToDate).HasColumnName("to_date");

                entity.HasOne(d => d.DeptNoNavigation)
                    .WithMany(p => p.DeptEmps)
                    .HasForeignKey(d => d.DeptNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dept_emp_ibfk_1");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.DeptEmps)
                    .HasForeignKey(d => d.EmpNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dept_emp_ibfk_2");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpNo)
                    .HasName("PRIMARY");

                entity.ToTable("employees");

                entity.Property(e => e.EmpNo)
                    .ValueGeneratedNever()
                    .HasColumnName("emp_no");

                entity.Property(e => e.BirthDate).HasColumnName("birth_date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnType("enum('M','F')")
                    .HasColumnName("gender");

                entity.Property(e => e.HireDate).HasColumnName("hire_date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("last_name");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.FromDate })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("salaries");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate).HasColumnName("from_date");

                entity.Property(e => e.Salary1)
                    .HasPrecision(15, 2)
                    .HasColumnName("salary");

                entity.Property(e => e.ToDate).HasColumnName("to_date");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmpNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("salaries_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserNo)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.HasIndex(e => e.ContactTypeNo, "contact_type_no");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "username")
                    .IsUnique();

                entity.Property(e => e.UserNo).HasColumnName("user_no");

                entity.Property(e => e.CodUser)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("cod_user");

                entity.Property(e => e.ContactTypeNo)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("contact_type_no")
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.IsAutorizeOrders).HasColumnName("is_autorize_orders");

                entity.Property(e => e.IsAutorizeWebstore).HasColumnName("is_autorize_webstore");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("phone");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("title");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("username");

                entity.HasOne(d => d.ContactTypeNoNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ContactTypeNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
