using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Added_BankaSube : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppBankaSubeler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    BankaId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppBankaSubeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppBankaSubeler_AppBankalar_BankaId",
                        column: x => x.BankaId,
                        principalTable: "AppBankalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppBankaSubeler_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankaSubeler_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankaSubeler_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankaSubeler_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankaSubeler_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaSubeler_BankaId",
                table: "AppBankaSubeler",
                column: "BankaId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaSubeler_Kod",
                table: "AppBankaSubeler",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaSubeler_OzelKod1Id",
                table: "AppBankaSubeler",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaSubeler_OzelKod2Id",
                table: "AppBankaSubeler",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaSubeler_OzelKod3Id",
                table: "AppBankaSubeler",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaSubeler_OzelKod4Id",
                table: "AppBankaSubeler",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaSubeler_OzelKod5Id",
                table: "AppBankaSubeler",
                column: "OzelKod5Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppBankaSubeler");
        }
    }
}
