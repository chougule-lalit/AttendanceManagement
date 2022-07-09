using Microsoft.EntityFrameworkCore;
using AttendanceManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Data
{
    public class AttendanceManagementDbContext : DbContext
    {
        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<AttendanceDetail> AttendanceDetails { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<AttendanceType> AttendanceTypes { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }

        public AttendanceManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleMaster>().HasData(
                   new RoleMaster { Id = Convert.ToInt32(RoleEnum.Admin), Name = RoleEnum.Admin.ToString() },
                   new RoleMaster { Id = Convert.ToInt32(RoleEnum.Manager), Name = RoleEnum.Manager.ToString() },
                   new RoleMaster { Id = Convert.ToInt32(RoleEnum.Associate), Name = RoleEnum.Associate.ToString() }
                   );

            modelBuilder.Entity<Department>().HasData(
                    new Department { Id = 1, Name = "Admin" },
                    new Department { Id = 2, Name = "HR" },
                    new Department { Id = 3, Name = "IT" }
                );

            modelBuilder.Entity<Designation>().HasData(
                new Designation { Id = 1, Name = "Admin" }
                );

            modelBuilder.Entity<AttendanceType>().HasData(
                new AttendanceType { Id = 1, Description = "Present", Type = "Present" },
                new AttendanceType { Id = 2, Description = "Personal Leave", Type = "Personal Leave" },
                new AttendanceType { Id = 3, Description = "Sick Leave", Type = "Sick Leave" },
                new AttendanceType { Id = 4, Description = "Casual Leave", Type = "Casual Leave" },
                new AttendanceType { Id = 5, Description = "Comp Off", Type = "Comp Off" }
                );

            modelBuilder.Entity<UserMaster>()
                .HasOne(s => s.Role)
                .WithMany().HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<UserMaster>()
                .HasOne(s => s.Designation)
                .WithMany().HasForeignKey(x => x.DesignationId);

            modelBuilder.Entity<UserMaster>()
                .HasOne(s => s.Department)
                .WithMany().HasForeignKey(x => x.DepartmentId);

            modelBuilder.Entity<UserMaster>().HasData(
                new UserMaster
                {
                    DepartmentId = 1,
                    FirstName = "admin",
                    LastName = "admin",
                    Id = 1,
                    DesignationId = 1,
                    Email = "admin@admin.com",
                    Password = "admin",
                    Phone = "9892318706",
                    RoleId = 1
                });
        }
    }
}
