using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Added_Iller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IlId",
                table: "AppBooks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppIller",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    Durum = table.Column<bool>(type: "Bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppIller", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_IlId",
                table: "AppBooks",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_AppIller_Kod",
                table: "AppIller",
                column: "Kod");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppIller_IlId",
                table: "AppBooks",
                column: "IlId",
                principalTable: "AppIller",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppIller_IlId",
                table: "AppBooks");

            migrationBuilder.DropTable(
                name: "AppIller");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_IlId",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "IlId",
                table: "AppBooks");
        }
    }
}
