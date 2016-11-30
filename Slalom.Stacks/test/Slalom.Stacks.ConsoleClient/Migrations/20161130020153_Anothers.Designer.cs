using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Slalom.Stacks.ConsoleClient;

namespace Slalom.Stacks.ConsoleClient.Migrations
{
    [DbContext(typeof(SearchContext))]
    [Migration("20161130020153_Anothers")]
    partial class Anothers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Slalom.Stacks.ConsoleClient.ItemSearchResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Slalom.Stacks.EntityFramework.Audit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationName");

                    b.Property<bool>("ChangesState");

                    b.Property<Guid>("CommandId");

                    b.Property<string>("CommandName");

                    b.Property<string>("CommandPayload");

                    b.Property<DateTimeOffset>("CommandTimeStamp");

                    b.Property<Guid>("CorrelationId");

                    b.Property<string>("Environment");

                    b.Property<Guid?>("EventId");

                    b.Property<string>("EventName");

                    b.Property<string>("EventPayload");

                    b.Property<DateTimeOffset?>("EventTimeStamp");

                    b.Property<bool>("IsSuccessful");

                    b.Property<string>("MachineName");

                    b.Property<string>("SessionId");

                    b.Property<bool>("StateChanged");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });
        }
    }
}
