using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuanLyDaoTao.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Lop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lops",
                columns: table => new
                {
                    MaLop = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenLop = table.Column<string>(type: "text", nullable: false),
                    MaKhoa = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lops", x => x.MaLop);
                    table.ForeignKey(
                        name: "FK_lops_khoas_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "khoas",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lops_MaKhoa",
                table: "lops",
                column: "MaKhoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lops");
        }
    }
}
