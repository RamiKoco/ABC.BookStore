using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class FirmaParametre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppFirmaParametreler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    SubeId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    DonemId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFirmaParametreler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFirmaParametreler_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppFirmaParametreler_AppDonemler_DonemId",
                        column: x => x.DonemId,
                        principalTable: "AppDonemler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFirmaParametreler_AppSubeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "AppSubeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppFirmaParametreler_DonemId",
                table: "AppFirmaParametreler",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFirmaParametreler_SubeId",
                table: "AppFirmaParametreler",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFirmaParametreler_UserId",
                table: "AppFirmaParametreler",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppFirmaParametreler");
        }
    }
}
