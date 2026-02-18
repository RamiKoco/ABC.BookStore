using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Added_OzelKod_To_Book : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Kod",
                table: "AppBooks",
                type: "VarChar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<bool>(
                name: "Durum",
                table: "AppBooks",
                type: "Bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "AppBooks",
                type: "VarChar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "AppBooks",
                type: "VarChar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<Guid>(
                name: "OzelKod1Id",
                table: "AppBooks",
                type: "UniqueIdentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OzelKod2Id",
                table: "AppBooks",
                type: "UniqueIdentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OzelKod3Id",
                table: "AppBooks",
                type: "UniqueIdentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OzelKod4Id",
                table: "AppBooks",
                type: "UniqueIdentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OzelKod5Id",
                table: "AppBooks",
                type: "UniqueIdentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppOzelKodlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    KodTuru = table.Column<byte>(type: "TinyInt", nullable: false),
                    KartTuru = table.Column<byte>(type: "TinyInt", nullable: false),
                    Aciklama = table.Column<string>(type: "VarChar(200)", maxLength: 200, nullable: false),
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
                    table.PrimaryKey("PK_AppOzelKodlar", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_Kod",
                table: "AppBooks",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_OzelKod1Id",
                table: "AppBooks",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_OzelKod2Id",
                table: "AppBooks",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_OzelKod3Id",
                table: "AppBooks",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_OzelKod4Id",
                table: "AppBooks",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_OzelKod5Id",
                table: "AppBooks",
                column: "OzelKod5Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppOzelKodlar_Kod",
                table: "AppOzelKodlar",
                column: "Kod");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppOzelKodlar_OzelKod1Id",
                table: "AppBooks",
                column: "OzelKod1Id",
                principalTable: "AppOzelKodlar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppOzelKodlar_OzelKod2Id",
                table: "AppBooks",
                column: "OzelKod2Id",
                principalTable: "AppOzelKodlar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppOzelKodlar_OzelKod3Id",
                table: "AppBooks",
                column: "OzelKod3Id",
                principalTable: "AppOzelKodlar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppOzelKodlar_OzelKod4Id",
                table: "AppBooks",
                column: "OzelKod4Id",
                principalTable: "AppOzelKodlar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBooks_AppOzelKodlar_OzelKod5Id",
                table: "AppBooks",
                column: "OzelKod5Id",
                principalTable: "AppOzelKodlar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppOzelKodlar_OzelKod1Id",
                table: "AppBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppOzelKodlar_OzelKod2Id",
                table: "AppBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppOzelKodlar_OzelKod3Id",
                table: "AppBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppOzelKodlar_OzelKod4Id",
                table: "AppBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AppBooks_AppOzelKodlar_OzelKod5Id",
                table: "AppBooks");

            migrationBuilder.DropTable(
                name: "AppOzelKodlar");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_Kod",
                table: "AppBooks");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_OzelKod1Id",
                table: "AppBooks");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_OzelKod2Id",
                table: "AppBooks");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_OzelKod3Id",
                table: "AppBooks");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_OzelKod4Id",
                table: "AppBooks");

            migrationBuilder.DropIndex(
                name: "IX_AppBooks_OzelKod5Id",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "OzelKod1Id",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "OzelKod2Id",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "OzelKod3Id",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "OzelKod4Id",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "OzelKod5Id",
                table: "AppBooks");

            migrationBuilder.AlterColumn<string>(
                name: "Kod",
                table: "AppBooks",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VarChar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<bool>(
                name: "Durum",
                table: "AppBooks",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "Bit");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "AppBooks",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VarChar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "AppBooks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VarChar(200)",
                oldMaxLength: 200);
        }
    }
}
