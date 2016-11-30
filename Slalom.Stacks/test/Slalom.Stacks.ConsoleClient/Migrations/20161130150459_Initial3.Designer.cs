using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Slalom.Stacks.ConsoleClient;

namespace Slalom.Stacks.ConsoleClient.Migrations
{
    [DbContext(typeof(SearchContext))]
    [Migration("20161130150459_Initial3")]
    partial class Initial3
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

                    b.Property<Guid>("CorrelationId");

                    b.Property<string>("Environment");

                    b.Property<Guid?>("EventId");

                    b.Property<string>("EventName");

                    b.Property<string>("Host");

                    b.Property<string>("MachineName");

                    b.Property<string>("Path");

                    b.Property<string>("Payload");

                    b.Property<string>("SessionId");

                    b.Property<int>("ThreadId");

                    b.Property<DateTimeOffset?>("TimeStamp");

                    b.Property<string>("UserHostAddress");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("Slalom.Stacks.EntityFramework.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationName");

                    b.Property<Guid>("CommandId");

                    b.Property<string>("CommandName");

                    b.Property<Guid>("CorrelationId");

                    b.Property<string>("Environment");

                    b.Property<string>("Host");

                    b.Property<bool>("IsSuccessful");

                    b.Property<string>("MachineName");

                    b.Property<string>("Path");

                    b.Property<string>("Payload");

                    b.Property<string>("SessionId");

                    b.Property<int>("ThreadId");

                    b.Property<DateTimeOffset?>("TimeStamp");

                    b.Property<string>("UserHostAddress");

                    b.Property<string>("UserName");

                    b.Property<string>("ValidationErrors");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });
        }
    }
}
