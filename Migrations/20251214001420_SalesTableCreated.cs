using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpticianWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SalesTableCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_name = table.Column<string>(type: "text", nullable: false),
                    customer_phone = table.Column<string>(type: "text", nullable: false),
                    sale_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    sold_price = table.Column<decimal>(type: "numeric", nullable: false),
                    glasses_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.id);
                    table.ForeignKey(
                        name: "FK_sales_Glasses_glasses_id",
                        column: x => x.glasses_id,
                        principalTable: "Glasses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sales_glasses_id",
                table: "sales",
                column: "glasses_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sales");
        }
    }
}
