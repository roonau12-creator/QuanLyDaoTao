using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyDaoTao.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Hoten = table.Column<string>(type: "text", nullable: false),
                    MaSinhVien = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hocKys",
                columns: table => new
                {
                    MaHocKy = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenHocKy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NamHoc = table.Column<int>(type: "integer", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hocKys", x => x.MaHocKy);
                });

            migrationBuilder.CreateTable(
                name: "khoas",
                columns: table => new
                {
                    MaKhoa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenKhoa = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khoas", x => x.MaKhoa);
                });

            migrationBuilder.CreateTable(
                name: "phongHocs",
                columns: table => new
                {
                    MaPhongHoc = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenPhong = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ToaNha = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SucChua = table.Column<int>(type: "integer", nullable: false),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phongHocs", x => x.MaPhongHoc);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "giangViens",
                columns: table => new
                {
                    MaGiangVien = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaSoGiangVien = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GioiTinh = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    DiaChi = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    HocVi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ChucVu = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MaKhoa = table.Column<int>(type: "integer", nullable: false),
                    TrangThai = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giangViens", x => x.MaGiangVien);
                    table.ForeignKey(
                        name: "FK_giangViens_khoas_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "khoas",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lops",
                columns: table => new
                {
                    MaLop = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenLop = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "sinhViens",
                columns: table => new
                {
                    MaSinhVien = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaSoSinhVien = table.Column<string>(type: "text", nullable: false),
                    Hoten = table.Column<string>(type: "text", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GioiTinh = table.Column<bool>(type: "boolean", nullable: false),
                    CanCuocCongDan = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    SoDienThoai = table.Column<string>(type: "text", nullable: false),
                    DiaChi = table.Column<string>(type: "text", nullable: false),
                    NgayNhapHoc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "hocPhans",
                columns: table => new
                {
                    MaHocPhan = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaHocPhanCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MaMonHoc = table.Column<int>(type: "integer", nullable: false),
                    MaHocKy = table.Column<int>(type: "integer", nullable: false),
                    MaGiangVien = table.Column<int>(type: "integer", nullable: true),
                    SiSoToiDa = table.Column<int>(type: "integer", nullable: false),
                    SiSoHienTai = table.Column<int>(type: "integer", nullable: false),
                    NgayMoDangKy = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayDongDangKy = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TrangThai = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    GhiChu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hocPhans", x => x.MaHocPhan);
                    table.ForeignKey(
                        name: "FK_hocPhans_giangViens_MaGiangVien",
                        column: x => x.MaGiangVien,
                        principalTable: "giangViens",
                        principalColumn: "MaGiangVien");
                    table.ForeignKey(
                        name: "FK_hocPhans_hocKys_MaHocKy",
                        column: x => x.MaHocKy,
                        principalTable: "hocKys",
                        principalColumn: "MaHocKy",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hocPhans_monHocs_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "monHocs",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lichHocs",
                columns: table => new
                {
                    MaLichHoc = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaMonHoc = table.Column<int>(type: "integer", nullable: false),
                    MaLop = table.Column<int>(type: "integer", nullable: false),
                    Thu = table.Column<int>(type: "integer", nullable: false),
                    TietBatDau = table.Column<int>(type: "integer", nullable: false),
                    SoTiet = table.Column<int>(type: "integer", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GhiChu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lichHocs", x => x.MaLichHoc);
                    table.ForeignKey(
                        name: "FK_lichHocs_lops_MaLop",
                        column: x => x.MaLop,
                        principalTable: "lops",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lichHocs_monHocs_MaMonHoc",
                        column: x => x.MaMonHoc,
                        principalTable: "monHocs",
                        principalColumn: "MaMonHoc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dangKyHocPhans",
                columns: table => new
                {
                    MaDangKy = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaSinhVien = table.Column<int>(type: "integer", nullable: false),
                    MaHocPhan = table.Column<int>(type: "integer", nullable: false),
                    MaHocKy = table.Column<int>(type: "integer", nullable: false),
                    NgayDangKy = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TrangThai = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    GhiChu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dangKyHocPhans", x => x.MaDangKy);
                    table.ForeignKey(
                        name: "FK_dangKyHocPhans_hocKys_MaHocKy",
                        column: x => x.MaHocKy,
                        principalTable: "hocKys",
                        principalColumn: "MaHocKy",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dangKyHocPhans_hocPhans_MaHocPhan",
                        column: x => x.MaHocPhan,
                        principalTable: "hocPhans",
                        principalColumn: "MaHocPhan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dangKyHocPhans_sinhViens_MaSinhVien",
                        column: x => x.MaSinhVien,
                        principalTable: "sinhViens",
                        principalColumn: "MaSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "hocKys",
                columns: new[] { "MaHocKy", "NamHoc", "NgayBatDau", "NgayKetThuc", "TenHocKy" },
                values: new object[] { 1, 2023, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Học kỳ 1" });

            migrationBuilder.InsertData(
                table: "khoas",
                columns: new[] { "MaKhoa", "MoTa", "TenKhoa" },
                values: new object[] { 1, "Khoa công nghệ thông tin", "Hạ tầng kỹ thuật" });

            migrationBuilder.InsertData(
                table: "lops",
                columns: new[] { "MaLop", "MaKhoa", "TenLop" },
                values: new object[] { 3, 1, "CNTT-K64" });

            migrationBuilder.InsertData(
                table: "monHocs",
                columns: new[] { "MaMonHoc", "MaKhoa", "MoTa", "SoTinChi", "TenMonHoc" },
                values: new object[] { 1, 1, "Lập trình cơ bản trong python", 3, "Lập trình cơ bản" });

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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dangKyHocPhans_MaHocKy",
                table: "dangKyHocPhans",
                column: "MaHocKy");

            migrationBuilder.CreateIndex(
                name: "IX_dangKyHocPhans_MaHocPhan",
                table: "dangKyHocPhans",
                column: "MaHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_dangKyHocPhans_MaSinhVien",
                table: "dangKyHocPhans",
                column: "MaSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_giangViens_MaKhoa",
                table: "giangViens",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_hocPhans_MaGiangVien",
                table: "hocPhans",
                column: "MaGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_hocPhans_MaHocKy",
                table: "hocPhans",
                column: "MaHocKy");

            migrationBuilder.CreateIndex(
                name: "IX_hocPhans_MaMonHoc",
                table: "hocPhans",
                column: "MaMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_lichHocs_MaLop",
                table: "lichHocs",
                column: "MaLop");

            migrationBuilder.CreateIndex(
                name: "IX_lichHocs_MaMonHoc",
                table: "lichHocs",
                column: "MaMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_lops_MaKhoa",
                table: "lops",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_monHocs_MaKhoa",
                table: "monHocs",
                column: "MaKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_sinhViens_MaLop",
                table: "sinhViens",
                column: "MaLop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "dangKyHocPhans");

            migrationBuilder.DropTable(
                name: "lichHocs");

            migrationBuilder.DropTable(
                name: "phongHocs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "hocPhans");

            migrationBuilder.DropTable(
                name: "sinhViens");

            migrationBuilder.DropTable(
                name: "giangViens");

            migrationBuilder.DropTable(
                name: "hocKys");

            migrationBuilder.DropTable(
                name: "monHocs");

            migrationBuilder.DropTable(
                name: "lops");

            migrationBuilder.DropTable(
                name: "khoas");
        }
    }
}
