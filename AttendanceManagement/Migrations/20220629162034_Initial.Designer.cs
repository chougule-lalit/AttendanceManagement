﻿// <auto-generated />
using System;
using AttendanceManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AttendanceManagement.Migrations
{
    [DbContext(typeof(AttendanceManagementDbContext))]
    [Migration("20220629162034_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("AttendanceManagement.Entities.AttendanceDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("AttendanceTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LeaveFromDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LeaveToDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("TimeIn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("TimeOut")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AttendanceTypeId");

                    b.ToTable("AttendanceDetails");
                });

            modelBuilder.Entity("AttendanceManagement.Entities.AttendanceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AttendanceTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Present",
                            Type = "Present"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Personal Leave",
                            Type = "Personal Leave"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Sick Leave",
                            Type = "Sick Leave"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Casual Leave",
                            Type = "Casual Leave"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Comp Off",
                            Type = "Comp Off"
                        });
                });

            modelBuilder.Entity("AttendanceManagement.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "HR"
                        },
                        new
                        {
                            Id = 3,
                            Name = "IT"
                        });
                });

            modelBuilder.Entity("AttendanceManagement.Entities.Designation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Designations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("AttendanceManagement.Entities.Enquiry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Enquiries");
                });

            modelBuilder.Entity("AttendanceManagement.Entities.RoleMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RoleMasters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Manageer"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Associate"
                        });
                });

            modelBuilder.Entity("AttendanceManagement.Entities.UserMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DesignationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DesignationId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserMasters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentId = 1,
                            DesignationId = 1,
                            Email = "admin@admin.com",
                            FirstName = "admin",
                            LastName = "admin",
                            Password = "admin",
                            Phone = "9892318706",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("AttendanceManagement.Entities.AttendanceDetail", b =>
                {
                    b.HasOne("AttendanceManagement.Entities.AttendanceType", "AttendanceType")
                        .WithMany()
                        .HasForeignKey("AttendanceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AttendanceType");
                });

            modelBuilder.Entity("AttendanceManagement.Entities.UserMaster", b =>
                {
                    b.HasOne("AttendanceManagement.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttendanceManagement.Entities.Designation", "Designation")
                        .WithMany()
                        .HasForeignKey("DesignationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttendanceManagement.Entities.RoleMaster", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Designation");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}