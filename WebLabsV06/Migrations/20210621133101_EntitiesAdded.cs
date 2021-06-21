using Microsoft.EntityFrameworkCore.Migrations;

namespace WebLabsV06.DAL.Migrations
{
    public partial class EntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedGroups",
                columns: table => new
                {
                    FeedGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedGroups", x => x.FeedGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    FeedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    FeedGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.FeedId);
                    table.ForeignKey(
                        name: "FK_Feeds_FeedGroups_FeedGroupId",
                        column: x => x.FeedGroupId,
                        principalTable: "FeedGroups",
                        principalColumn: "FeedGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_FeedGroupId",
                table: "Feeds",
                column: "FeedGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feeds");

            migrationBuilder.DropTable(
                name: "FeedGroups");
        }
    }
}
