using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuanLyDaoTao.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ThongBao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "diemDanhs",
                columns: table => new
                {
                    MaDiemDanh = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaLichHoc = table.Column<int>(type: "integer", nullable: false),
                    MaSinhVien = table.Column<int>(type: "integer", nullable: false),
                    TrangThai = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    GhiChu = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diemDanhs", x => x.MaDiemDanh);
                    table.ForeignKey(
                        name: "FK_diemDanhs_lichHocs_MaLichHoc",
                        column: x => x.MaLichHoc,
                        principalTable: "lichHocs",
                        principalColumn: "MaLichHoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_diemDanhs_sinhViens_MaSinhVien",
                        column: x => x.MaSinhVien,
                        principalTable: "sinhViens",
                        principalColumn: "MaSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "diems",
                columns: table => new
                {
                    MaDiem = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaDangKy = table.Column<int>(type: "integer", nullable: false),
                    DiemChuyenCan = table.Column<decimal>(type: "numeric", nullable: true),
                    DiemNhanXet = table.Column<decimal>(type: "numeric", nullable: true),
                    DiemGiuaKy = table.Column<decimal>(type: "numeric", nullable: true),
                    DiemCuoiKy = table.Column<decimal>(type: "numeric", nullable: true),
                    DiemTongKet = table.Column<decimal>(type: "numeric", nullable: true),
                    KetQua = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diems", x => x.MaDiem);
                    table.ForeignKey(
                        name: "FK_diems_dangKyHocPhans_MaDangKy",
                        column: x => x.MaDangKy,
                        principalTable: "dangKyHocPhans",
                        principalColumn: "MaDangKy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thongBaos",
                columns: table => new
                {
                    MaThongBao = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TieuDe = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NoiDung = table.Column<string>(type: "text", nullable: false),
                    LoaiNoiDung = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NgayDang = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NguoiDang = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thongBaos", x => x.MaThongBao);
                });

            migrationBuilder.CreateIndex(
                name: "IX_diemDanhs_MaLichHoc",
                table: "diemDanhs",
                column: "MaLichHoc");

            migrationBuilder.CreateIndex(
                name: "IX_diemDanhs_MaSinhVien",
                table: "diemDanhs",
                column: "MaSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_diems_MaDangKy",
                table: "diems",
                column: "MaDangKy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "diemDanhs");

            migrationBuilder.DropTable(
                name: "diems");

            migrationBuilder.DropTable(
                name: "thongBaos");
        }
    }
}
