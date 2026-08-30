using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyDaoTao.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedSinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "sinhViens",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "sinhViens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayNhapHoc",
                table: "sinhViens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaSoSinhVien",
                table: "sinhViens",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hoten",
                table: "sinhViens",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "GioiTinh",
                table: "sinhViens",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "sinhViens",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiaChi",
                table: "sinhViens",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CanCuocCongDan",
                table: "sinhViens",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenLop",
                table: "lops",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TenKhoa",
                table: "khoas",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MoTa",
                table: "khoas",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "hocKys",
                columns: table => new
                {
                    MaHocKy = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenHocKy = table.Column<string>(type: "text", nullable: false),
                    NamHoc = table.Column<int>(type: "integer", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hocKys", x => x.MaHocKy);
                });

            migrationBuilder.CreateTable(
                name: "monHocs",
                columns: table => new
                {
                    MaMonHoc = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenMonHoc = table.Column<string>(type: "text", nullable: false),
                    SoTinChi = table.Column<int>(type: "integer", nullable: false),
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

            migrationBuilder.InsertData(
                table: "sinhViens",
                columns: new[] { "MaSinhVien", "AnhDaiDien", "CanCuocCongDan", "DiaChi", "Email", "GioiTinh", "Hoten", "MaLop", "MaSoSinhVien", "NgayNhapHoc", "NgaySinh", "SoDienThoai" },
                values: new object[,]
                {
                    { 1, "an.jpg", "079205001234", "Đắk Lắk", "nguyenvanan@gmail.com", true, "Nguyễn Văn An", 3, "SV001", new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "0901234567" },
                    { 2, "binh.jpg", "079205002345", "Gia Lai", "tranthibinh@gmail.com", false, "Trần Thị Bình", 3, "SV002", new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "0912345678" },
                    { 3, "cuong.jpg", "079204003456", "Kon Tum", "levancuong@gmail.com", true, "Lê Văn Cường", 3, "SV003", new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2004, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), "0923456789" },
                    { 4, "dung.jpg", "079205004567", "Đắk Nông", "phamthidung@gmail.com", false, "Phạm Thị Dung", 3, "SV004", new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "0934567890" },
                    { 5, "em.jpg", "079205005678", "Phú Yên", "hoangvanem@gmail.com", true, "Hoàng Văn Em", 3, "SV005", new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), "0945678901" }
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
                name: "hocKys");

            migrationBuilder.DropTable(
                name: "monHocs");

            migrationBuilder.DeleteData(
                table: "sinhViens",
                keyColumn: "MaSinhVien",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "sinhViens",
                keyColumn: "MaSinhVien",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "sinhViens",
                keyColumn: "MaSinhVien",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "sinhViens",
                keyColumn: "MaSinhVien",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "sinhViens",
                keyColumn: "MaSinhVien",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "SoDienThoai",
                table: "sinhViens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "sinhViens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayNhapHoc",
                table: "sinhViens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "MaSoSinhVien",
                table: "sinhViens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Hoten",
                table: "sinhViens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "GioiTinh",
                table: "sinhViens",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "sinhViens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DiaChi",
                table: "sinhViens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CanCuocCongDan",
                table: "sinhViens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TenLop",
                table: "lops",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "TenKhoa",
                table: "khoas",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "MoTa",
                table: "khoas",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);
        }
    }
}
