﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(BudgetTrackerContext))]
    partial class BudgetTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("api.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("api.Models.MonthlyBudget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("StartingBalance")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("MonthlyBudgets");
                });

            modelBuilder.Entity("api.Models.PlannedExpense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MonthlyBudgetId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("PlannedAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MonthlyBudgetId");

                    b.ToTable("PlannedExpenses");
                });

            modelBuilder.Entity("api.Models.PlannedIncome", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MonthlyBudgetId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("PlannedAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MonthlyBudgetId");

                    b.ToTable("PlannedIncomes");
                });

            modelBuilder.Entity("api.Models.PlannedExpense", b =>
                {
                    b.HasOne("api.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.MonthlyBudget", "MonthlyBudget")
                        .WithMany("PlannedExpenses")
                        .HasForeignKey("MonthlyBudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("MonthlyBudget");
                });

            modelBuilder.Entity("api.Models.PlannedIncome", b =>
                {
                    b.HasOne("api.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.MonthlyBudget", "MonthlyBudget")
                        .WithMany("PlannedIncomes")
                        .HasForeignKey("MonthlyBudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("MonthlyBudget");
                });

            modelBuilder.Entity("api.Models.MonthlyBudget", b =>
                {
                    b.Navigation("PlannedExpenses");

                    b.Navigation("PlannedIncomes");
                });
#pragma warning restore 612, 618
        }
    }
}
