using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDaoTao.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddLoaiPhongMoTaToPhongHoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoaiPhong",
                table: "phongHocs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "phongHocs",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiPhong",
                table: "phongHocs");

            migrationBuilder.DropColumn(
                name: "MoTa",
                table: "phongHocs");
        }
    }
}
