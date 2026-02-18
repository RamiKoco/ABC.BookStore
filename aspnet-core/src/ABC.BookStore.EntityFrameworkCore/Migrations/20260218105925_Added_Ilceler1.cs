using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Added_Ilceler1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IlceId",
                table: "AppBooks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_IlceId",
                table: "AppBooks",
                column: "IlceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppIlceler_IlceId",
                table: "AppBooks",
                column: "IlceId",
                principalTable: "AppIlceler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppIlceler_IlceId",
                table: "AppBooks");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_IlceId",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "IlceId",
                table: "AppBooks");
        }
    }
}
