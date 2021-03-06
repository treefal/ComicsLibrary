// <auto-generated />
using System;
using ComicsLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ComicsLibrary.Data.Migrations
{
    [DbContext(typeof(ComicsLibraryContext))]
    [Migration("20210131202310_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ComicsLibrary.Data.Models.Checkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComicId");

                    b.Property<int?>("LibraryCardId");

                    b.Property<DateTime>("Since");

                    b.Property<DateTime>("Until");

                    b.HasKey("Id");

                    b.HasIndex("ComicId");

                    b.HasIndex("LibraryCardId");

                    b.ToTable("Checkouts");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.CheckoutHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CheckedIn");

                    b.Property<DateTime>("CheckedOut");

                    b.Property<int>("ComicId");

                    b.Property<int>("LibraryCardId");

                    b.HasKey("Id");

                    b.HasIndex("ComicId");

                    b.HasIndex("LibraryCardId");

                    b.ToTable("CheckoutsHistories");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.Comic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost");

                    b.Property<string>("Editor")
                        .IsRequired();

                    b.Property<int>("GCIN");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Issue")
                        .IsRequired();

                    b.Property<int?>("LocationId");

                    b.Property<int>("NumberOfCopies");

                    b.Property<string>("Publisher")
                        .IsRequired();

                    b.Property<int>("StatusId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("StatusId");

                    b.ToTable("Comics");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<int?>("HomeLibraryLocationId");

                    b.Property<string>("LastName");

                    b.Property<int?>("LibraryCardId");

                    b.Property<string>("TelephoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("HomeLibraryLocationId");

                    b.HasIndex("LibraryCardId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.LibraryCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<decimal>("Fees");

                    b.HasKey("Id");

                    b.ToTable("LibraryCards");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.LibraryLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("OpenDate");

                    b.Property<string>("Telephone")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("LibraryLocations");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.LibraryLocationHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CloseTime");

                    b.Property<int>("DayOfWeek");

                    b.Property<int?>("LocationId");

                    b.Property<int>("OpenTime");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("LibraryLocationHours");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ComicId");

                    b.Property<int?>("LibraryCardId");

                    b.Property<DateTime>("LoanPlaced");

                    b.HasKey("Id");

                    b.HasIndex("ComicId");

                    b.HasIndex("LibraryCardId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.Checkout", b =>
                {
                    b.HasOne("ComicsLibrary.Data.Models.Comic", "Comic")
                        .WithMany()
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ComicsLibrary.Data.Models.LibraryCard", "LibraryCard")
                        .WithMany("Checkouts")
                        .HasForeignKey("LibraryCardId");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.CheckoutHistory", b =>
                {
                    b.HasOne("ComicsLibrary.Data.Models.Comic", "Comic")
                        .WithMany()
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ComicsLibrary.Data.Models.LibraryCard", "LibraryCard")
                        .WithMany()
                        .HasForeignKey("LibraryCardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.Comic", b =>
                {
                    b.HasOne("ComicsLibrary.Data.Models.LibraryLocation", "Location")
                        .WithMany("Comics")
                        .HasForeignKey("LocationId");

                    b.HasOne("ComicsLibrary.Data.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.Customer", b =>
                {
                    b.HasOne("ComicsLibrary.Data.Models.LibraryLocation", "HomeLibraryLocation")
                        .WithMany("Customers")
                        .HasForeignKey("HomeLibraryLocationId");

                    b.HasOne("ComicsLibrary.Data.Models.LibraryCard", "LibraryCard")
                        .WithMany()
                        .HasForeignKey("LibraryCardId");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.LibraryLocationHours", b =>
                {
                    b.HasOne("ComicsLibrary.Data.Models.LibraryLocation", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("ComicsLibrary.Data.Models.Loan", b =>
                {
                    b.HasOne("ComicsLibrary.Data.Models.Comic", "Comic")
                        .WithMany()
                        .HasForeignKey("ComicId");

                    b.HasOne("ComicsLibrary.Data.Models.LibraryCard", "LibraryCard")
                        .WithMany()
                        .HasForeignKey("LibraryCardId");
                });
#pragma warning restore 612, 618
        }
    }
}
