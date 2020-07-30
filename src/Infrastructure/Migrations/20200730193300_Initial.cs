using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arbetsprov.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceDetail",
                columns: table => new
                {
                    PriceValueId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CatalogEntryCode = table.Column<string>(nullable: true),
                    MarketId = table.Column<string>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    ValidFrom = table.Column<DateTime>(nullable: false),
                    ValidUntil = table.Column<DateTime>(nullable: true),
                    UnitPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceDetail", x => x.PriceValueId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceDetail_CatalogEntryCode",
                table: "PriceDetail",
                column: "CatalogEntryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceDetail");
        }
    }
}
