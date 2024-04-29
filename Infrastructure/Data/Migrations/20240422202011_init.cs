using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "channels",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Уникальный идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false, comment: "Заголовок канала новостей"),
                    description = table.Column<string>(type: "text", nullable: false, comment: "Описание канала новостей"),
                    link = table.Column<string>(type: "text", nullable: false, comment: "URL RSS канала"),
                    is_fixed = table.Column<bool>(type: "boolean", nullable: false, comment: "Флаг фиксации. Позволяет защитить канал от удаления."),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_channels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Уникальный идентификатор")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false, comment: "Заголовок"),
                    description = table.Column<string>(type: "text", nullable: false, comment: "Описание"),
                    link = table.Column<string>(type: "text", nullable: false, comment: "Ссылка на новость"),
                    pub_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата публикации"),
                    channel_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID канала"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата создания"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_news", x => x.id);
                    table.ForeignKey(
                        name: "fk_news_channels_channel_id",
                        column: x => x.channel_id,
                        principalTable: "channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_news_channel_id",
                table: "news",
                column: "channel_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "channels");
        }
    }
}
