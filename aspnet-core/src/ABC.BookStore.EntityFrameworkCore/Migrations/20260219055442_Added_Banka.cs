using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Added_Banka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "AppOzelKodlar",
                type: "VarChar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VarChar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "AppBooks",
                type: "VarChar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VarChar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "AppBankalar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    OzelKod1Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod2Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod3Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod4Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod5Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    Aciklama = table.Column<string>(type: "VarChar(200)", maxLength: 200, nullable: true),
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
                    table.PrimaryKey("PK_AppBankalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppBankalar_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankalar_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankalar_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankalar_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankalar_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppBankalar_Kod",
                table: "AppBankalar",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankalar_OzelKod1Id",
                table: "AppBankalar",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankalar_OzelKod2Id",
                table: "AppBankalar",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankalar_OzelKod3Id",
                table: "AppBankalar",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankalar_OzelKod4Id",
                table: "AppBankalar",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankalar_OzelKod5Id",
                table: "AppBankalar",
                column: "OzelKod5Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppBankalar");

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "AppOzelKodlar",
                type: "VarChar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VarChar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "AppBooks",
                type: "VarChar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VarChar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
