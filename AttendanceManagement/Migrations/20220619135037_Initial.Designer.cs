﻿// <auto-generated />
using System;
using AttendanceManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AttendanceManagement.Migrations
{
    [DbContext(typeof(AttendanceManagementDbContext))]
    [Migration("20220619135037_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("AttendanceManagement.Entities.AttendanceDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("AttendanceTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("TimeIn")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("TimeOut")
                        .HasColumnType("interval");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AttendanceTypeId");

                    b.ToTable("AttendanceDetails");
                });

            modelBuilder.Entity("AttendanceManagement.Entities.AttendanceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

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
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

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
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Designations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("AttendanceManagement.Entities.RoleMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

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
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<int>("DesignationId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

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
