using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuanLySinhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MonHoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "monHocs",
                columns: table => new
                {
                    MaMonHoc = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenMonHoc = table.Column<string>(type: "text", nullable: false),
                    SoTinChi = table.Column<string>(type: "text", nullable: false),
                    MaKhoa = table.Column<int>(type: "integer", nullable: false),
                    MoTa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monHocs", x => x.MaMonHoc);
                    table.ForeignKey(
                        name: "FK_monHocs_khoas_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "khoas",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_monHocs_MaKhoa",
                table: "monHocs",
                column: "MaKhoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "monHocs");
        }
    }
}
