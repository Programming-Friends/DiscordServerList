using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordServerList_MVC.Data.Migrations
{
    public partial class AddTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_DiscordServer_DiscordServerId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_DiscordServerId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DiscordServerId",
                table: "Category");

            migrationBuilder.CreateTable(
                name: "CategoryDiscordServer",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    DiscordServersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDiscordServer", x => new { x.CategoriesId, x.DiscordServersId });
                    table.ForeignKey(
                        name: "FK_CategoryDiscordServer_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryDiscordServer_DiscordServer_DiscordServersId",
                        column: x => x.DiscordServersId,
                        principalTable: "DiscordServer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscordServerTag",
                columns: table => new
                {
                    DiscordServersId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscordServerTag", x => new { x.DiscordServersId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_DiscordServerTag_DiscordServer_DiscordServersId",
                        column: x => x.DiscordServersId,
                        principalTable: "DiscordServer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscordServerTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDiscordServer_DiscordServersId",
                table: "CategoryDiscordServer",
                column: "DiscordServersId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscordServerTag_TagsId",
                table: "DiscordServerTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDiscordServer");

            migrationBuilder.DropTable(
                name: "DiscordServerTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.AddColumn<int>(
                name: "DiscordServerId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_DiscordServerId",
                table: "Category",
                column: "DiscordServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_DiscordServer_DiscordServerId",
                table: "Category",
                column: "DiscordServerId",
                principalTable: "DiscordServer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
