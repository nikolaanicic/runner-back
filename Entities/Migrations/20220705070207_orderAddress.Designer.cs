﻿// <auto-generated />
using System;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220705070207_orderAddress")]
    partial class orderAddress
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Contracts.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ConsumerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DelivererId")
                        .HasColumnType("bigint");

                    b.Property<float>("DeliveryTimer")
                        .HasColumnType("real");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerId");

                    b.HasIndex("DelivererId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Contracts.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Contracts.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Rolename")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Rolename")
                        .IsUnique();

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Rolename = "Admin"
                        },
                        new
                        {
                            Id = 2L,
                            Rolename = "Consumer"
                        },
                        new
                        {
                            Id = 3L,
                            Rolename = "Deliverer"
                        });
                });

            modelBuilder.Entity("Contracts.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.Property<long>("OrdersId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProduceId")
                        .HasColumnType("bigint");

                    b.HasKey("OrdersId", "ProduceId");

                    b.HasIndex("ProduceId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("Contracts.Models.Admin", b =>
                {
                    b.HasBaseType("Contracts.Models.User");

                    b.ToTable("Admin");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Address = "Korenita, Josifa Tronosca 25",
                            DateOfBirth = new DateTime(1999, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "nikolaanicic99@gmail.com",
                            ImagePath = "nema slike za sada",
                            LastName = "Anicic",
                            Name = "Nikola",
                            PasswordHash = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=",
                            RoleId = 1L,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Contracts.Models.Consumer", b =>
                {
                    b.HasBaseType("Contracts.Models.User");

                    b.ToTable("Consumer");
                });

            modelBuilder.Entity("Contracts.Models.Deliverer", b =>
                {
                    b.HasBaseType("Contracts.Models.User");

                    b.Property<bool>("Busy")
                        .HasColumnType("bit");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.ToTable("Deliverer");
                });

            modelBuilder.Entity("Contracts.Models.Order", b =>
                {
                    b.HasOne("Contracts.Models.Consumer", "Consumer")
                        .WithMany("Orders")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Contracts.Models.Deliverer", "Deliverer")
                        .WithMany("Orders")
                        .HasForeignKey("DelivererId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Consumer");

                    b.Navigation("Deliverer");
                });

            modelBuilder.Entity("Contracts.Models.User", b =>
                {
                    b.HasOne("Contracts.Models.Role", "Role")
                        .WithMany("UsersInRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.HasOne("Contracts.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Contracts.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProduceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contracts.Models.Admin", b =>
                {
                    b.HasOne("Contracts.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Contracts.Models.Admin", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contracts.Models.Consumer", b =>
                {
                    b.HasOne("Contracts.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Contracts.Models.Consumer", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contracts.Models.Deliverer", b =>
                {
                    b.HasOne("Contracts.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Contracts.Models.Deliverer", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Contracts.Models.Role", b =>
                {
                    b.Navigation("UsersInRole");
                });

            modelBuilder.Entity("Contracts.Models.Consumer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Contracts.Models.Deliverer", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
