using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Added_Kisi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppKisiler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    Image = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    IlId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    IlceId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    AcikAdres = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    OzelKod1Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod2Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    TCNo = table.Column<string>(type: "VarChar(11)", maxLength: 11, nullable: true),
                    Telefon = table.Column<string>(type: "VarChar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "VarChar(30)", maxLength: 30, nullable: true),
                    DogumTarihi = table.Column<DateTime>(type: "Date", nullable: false),
                    DogumYeri = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    KanGrubu = table.Column<byte>(type: "TinyInt", nullable: false),
                    MedeniHal = table.Column<byte>(type: "TinyInt", nullable: false),
                    Cinsiyet = table.Column<byte>(type: "TinyInt", nullable: false),
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
                    table.PrimaryKey("PK_AppKisiler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppKisiler_AppIlceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "AppIlceler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppKisiler_AppIller_IlId",
                        column: x => x.IlId,
                        principalTable: "AppIller",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppKisiler_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppKisiler_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppKisiler_IlceId",
                table: "AppKisiler",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppKisiler_IlId",
                table: "AppKisiler",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_AppKisiler_Kod",
                table: "AppKisiler",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppKisiler_OzelKod1Id",
                table: "AppKisiler",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppKisiler_OzelKod2Id",
                table: "AppKisiler",
                column: "OzelKod2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppKisiler");
        }
    }
}
