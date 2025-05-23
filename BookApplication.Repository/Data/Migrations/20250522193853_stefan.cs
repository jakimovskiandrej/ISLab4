using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApplication.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class stefan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Books");
        }
    }
}
