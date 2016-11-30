using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Macaw.CQRSDemo.Infrastructure.Repositories;

namespace Macaw.CQRSDemo.Infrastructure.Repositories.Migrations
{
    [DbContext(typeof(DemoContext))]
    [Migration("20161130130020_FirstModels")]
    partial class FirstModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Macaw.CQRSDemo.Infrastructure.Repositories.DataModels.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EventType");

                    b.Property<string>("PartitionKey");

                    b.Property<string>("Payload");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Macaw.CQRSDemo.Infrastructure.Repositories.DataModels.Match", b =>
                {
                    b.Property<string>("MatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Period");

                    b.Property<int>("Score1");

                    b.Property<int>("Score2");

                    b.Property<int>("State");

                    b.Property<string>("Team1");

                    b.Property<string>("Team2");

                    b.Property<string>("Timeouts1");

                    b.Property<string>("Timeouts2");

                    b.HasKey("MatchId");

                    b.ToTable("Matches");
                });
        }
    }
}
