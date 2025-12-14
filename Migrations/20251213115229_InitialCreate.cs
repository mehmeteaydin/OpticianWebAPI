using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpticianWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        
            migrationBuilder.CreateTable(
                name: "frames",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    brand = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    modelCode = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    color = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    material = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    createdAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_frames", x => x.id);
                });
        
            migrationBuilder.CreateTable(
                name: "lenses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    degree_left = table.Column<double>(type: "double precision", nullable: false),
                    degree_right = table.Column<double>(type: "double precision", nullable: false),
                    cost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    createdAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lenses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Glasses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    frame_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lens_id = table.Column<Guid>(type: "uuid", nullable: false),
                    glasses_type = table.Column<int>(type: "integer", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    createdAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glasses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Glasses_frames_frame_id",
                        column: x => x.frame_id,
                        principalTable: "frames",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Glasses_lenses_lens_id",
                        column: x => x.lens_id,
                        principalTable: "lenses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Glasses_frame_id",
                table: "Glasses",
                column: "frame_id");

            migrationBuilder.CreateIndex(
                name: "IX_Glasses_lens_id",
                table: "Glasses",
                column: "lens_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Glasses");

            migrationBuilder.DropTable(
                name: "frames");

            migrationBuilder.DropTable(
                name: "lenses");
        }
    }
}
