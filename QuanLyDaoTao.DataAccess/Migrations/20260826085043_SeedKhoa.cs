using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySinhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedKhoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "khoas",
                columns: new[] { "MaKhoa", "MoTa", "TenKhoa" },
                values: new object[] { 1, "Khoa công nghệ thông tin", "Hạ tầng kỹ thuật" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "khoas",
                keyColumn: "MaKhoa",
                keyValue: 1);
        }
    }
}
