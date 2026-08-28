using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuanLySinhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sinhViens",
                columns: table => new
                {
                    MaSinhVien = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaSoSinhVien = table.Column<string>(type: "text", nullable: true),
                    Hoten = table.Column<string>(type: "text", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GioiTinh = table.Column<bool>(type: "boolean", nullable: true),
                    CanCuocCongDan = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    SoDienThoai = table.Column<string>(type: "text", nullable: true),
                    DiaChi = table.Column<string>(type: "text", nullable: true),
                    NgayNhapHoc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MaLop = table.Column<int>(type: "integer", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sinhViens", x => x.MaSinhVien);
                    table.ForeignKey(
                        name: "FK_sinhViens_lops_MaLop",
                        column: x => x.MaLop,
                        principalTable: "lops",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sinhViens_MaLop",
                table: "sinhViens",
                column: "MaLop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sinhViens");
        }
    }
}
