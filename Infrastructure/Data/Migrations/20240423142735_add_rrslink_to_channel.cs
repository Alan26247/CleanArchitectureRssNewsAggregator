using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_rrslink_to_channel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "link",
                table: "channels",
                type: "text",
                nullable: false,
                comment: "URL канала",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "URL RSS канала");

            migrationBuilder.AddColumn<string>(
                name: "rss_link",
                table: "channels",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "URL RSS канала");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rss_link",
                table: "channels");

            migrationBuilder.AlterColumn<string>(
                name: "link",
                table: "channels",
                type: "text",
                nullable: false,
                comment: "URL RSS канала",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "URL канала");
        }
    }
}
