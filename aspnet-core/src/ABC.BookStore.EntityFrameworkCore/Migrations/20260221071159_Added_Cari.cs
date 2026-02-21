using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Added_Cari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepoId",
                table: "AppFirmaParametreler",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppBirimler",
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
                    table.PrimaryKey("PK_AppBirimler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppBirimler_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBirimler_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBirimler_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBirimler_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBirimler_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppCariler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    Soyad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    Unvan = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    HesapTuru = table.Column<byte>(type: "TinyInt", nullable: false),
                    TCNo = table.Column<string>(type: "VarChar(11)", maxLength: 11, nullable: true),
                    VergiDairesi = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    VergiNo = table.Column<string>(type: "VarChar(10)", maxLength: 10, nullable: true),
                    VDKodu = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: true),
                    LogoImage = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    Telefon = table.Column<string>(type: "VarChar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "VarChar(15)", maxLength: 15, nullable: true),
                    Adres = table.Column<string>(type: "VarChar(150)", maxLength: 150, nullable: true),
                    IlId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    IlceId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppCariler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCariler_AppIlceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "AppIlceler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppCariler_AppIller_IlId",
                        column: x => x.IlId,
                        principalTable: "AppIller",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppCariler_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppCariler_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppCariler_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppCariler_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppCariler_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppDepolar",
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
                    SubeId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppDepolar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDepolar_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppDepolar_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppDepolar_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppDepolar_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppDepolar_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppDepolar_AppSubeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "AppSubeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppKasalar",
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
                    SubeId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppKasalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppKasalar_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppKasalar_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppKasalar_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppKasalar_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppKasalar_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppKasalar_AppSubeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "AppSubeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppHizmetler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    KdvOrani = table.Column<int>(type: "Int", nullable: false),
                    BirimFiyat = table.Column<decimal>(type: "Money", nullable: false),
                    Barkod = table.Column<string>(type: "VarChar(128)", maxLength: 128, nullable: true),
                    BirimId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppHizmetler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppHizmetler_AppBirimler_BirimId",
                        column: x => x.BirimId,
                        principalTable: "AppBirimler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppHizmetler_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppHizmetler_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppHizmetler_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppHizmetler_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppHizmetler_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppMasraflar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    KdvOrani = table.Column<int>(type: "Int", nullable: false),
                    BirimFiyat = table.Column<decimal>(type: "Money", nullable: false),
                    Barkod = table.Column<string>(type: "VarChar(128)", maxLength: 128, nullable: true),
                    BirimId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppMasraflar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMasraflar_AppBirimler_BirimId",
                        column: x => x.BirimId,
                        principalTable: "AppBirimler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMasraflar_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMasraflar_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMasraflar_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMasraflar_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMasraflar_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppStoklar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    KdvOrani = table.Column<int>(type: "Int", nullable: false),
                    BirimFiyat = table.Column<decimal>(type: "Money", nullable: false),
                    Barkod = table.Column<string>(type: "VarChar(128)", maxLength: 128, nullable: true),
                    En = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    Boy = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    Yukseklik = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    Alan = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    NetHacim = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    BrutHacim = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    NetAgirlik = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    BrutAgirlik = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: true),
                    BirimId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    OzelKod1Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod2Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod3Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod4Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod5Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    Aciklama = table.Column<string>(type: "VarChar(200)", maxLength: 200, nullable: true),
                    Aciklama2 = table.Column<string>(type: "VarChar(200)", maxLength: 200, nullable: true),
                    Aciklama3 = table.Column<string>(type: "VarChar(200)", maxLength: 200, nullable: true),
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
                    table.PrimaryKey("PK_AppStoklar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppStoklar_AppBirimler_BirimId",
                        column: x => x.BirimId,
                        principalTable: "AppBirimler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppStoklar_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppStoklar_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppStoklar_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppStoklar_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppStoklar_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppBankaHesaplar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "VarChar(50)", maxLength: 50, nullable: false),
                    HesapTuru = table.Column<byte>(type: "TinyInt", nullable: false),
                    DovizT = table.Column<byte>(type: "TinyInt", nullable: false),
                    HesapNo = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: false),
                    IbanNo = table.Column<string>(type: "VarChar(26)", maxLength: 26, nullable: true),
                    BankaSubeId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CariId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    OzelKod1Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod2Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod3Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod4Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod5Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    SubeId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppBankaHesaplar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppBankaHesaplar_AppBankaSubeler_BankaSubeId",
                        column: x => x.BankaSubeId,
                        principalTable: "AppBankaSubeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppBankaHesaplar_AppCariler_CariId",
                        column: x => x.CariId,
                        principalTable: "AppCariler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppBankaHesaplar_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankaHesaplar_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankaHesaplar_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankaHesaplar_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankaHesaplar_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBankaHesaplar_AppSubeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "AppSubeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCariSubeler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CariId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    HareketTuru = table.Column<byte>(type: "TinyInt", nullable: false),
                    Aciklama = table.Column<string>(type: "VarChar(200)", maxLength: 200, nullable: true),
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
                    table.PrimaryKey("PK_AppCariSubeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCariSubeler_AppCariler_CariId",
                        column: x => x.CariId,
                        principalTable: "AppCariler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppFaturalar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaturaTuru = table.Column<byte>(type: "TinyInt", nullable: false),
                    FaturaNo = table.Column<string>(type: "VarChar(16)", maxLength: 16, nullable: false),
                    Tarih = table.Column<DateTime>(type: "Date", nullable: false),
                    BrutTutar = table.Column<decimal>(type: "Money", nullable: false),
                    IndirimTutar = table.Column<decimal>(type: "Money", nullable: false),
                    KdvHaricTutar = table.Column<decimal>(type: "Money", nullable: false),
                    KdvTutar = table.Column<decimal>(type: "Money", nullable: false),
                    NetTutar = table.Column<decimal>(type: "Money", nullable: false),
                    HareketSayisi = table.Column<int>(type: "Int", nullable: false),
                    CariId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    BaglantiId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    AdresId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    NoktaId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod1Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod2Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod3Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod4Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod5Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    SubeId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    DonemId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Aciklama = table.Column<string>(type: "VarChar(200)", maxLength: 200, nullable: true),
                    Durum = table.Column<bool>(type: "Bit", nullable: false),
                    DepoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_AppFaturalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFaturalar_AppCariler_CariId",
                        column: x => x.CariId,
                        principalTable: "AppCariler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturalar_AppDepolar_DepoId",
                        column: x => x.DepoId,
                        principalTable: "AppDepolar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturalar_AppDonemler_DonemId",
                        column: x => x.DonemId,
                        principalTable: "AppDonemler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturalar_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturalar_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturalar_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturalar_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturalar_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturalar_AppSubeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "AppSubeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppMakbuzlar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MakbuzTuru = table.Column<byte>(type: "TinyInt", nullable: false),
                    MakbuzNo = table.Column<string>(type: "VarChar(16)", maxLength: 16, nullable: false),
                    Tarih = table.Column<DateTime>(type: "Date", nullable: false),
                    AdresId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    CariId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    KasaId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    BankaHesapId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    HareketSayisi = table.Column<int>(type: "Int", nullable: false),
                    CekToplam = table.Column<decimal>(type: "Money", nullable: false),
                    SenetToplam = table.Column<decimal>(type: "Money", nullable: false),
                    PosToplam = table.Column<decimal>(type: "Money", nullable: false),
                    NakitToplam = table.Column<decimal>(type: "Money", nullable: false),
                    BankaToplam = table.Column<decimal>(type: "Money", nullable: false),
                    OzelKod1Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod2Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod3Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod4Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    OzelKod5Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    SubeId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    DonemId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppMakbuzlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMakbuzlar_AppBankaHesaplar_BankaHesapId",
                        column: x => x.BankaHesapId,
                        principalTable: "AppBankaHesaplar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzlar_AppCariler_CariId",
                        column: x => x.CariId,
                        principalTable: "AppCariler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzlar_AppDonemler_DonemId",
                        column: x => x.DonemId,
                        principalTable: "AppDonemler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzlar_AppKasalar_KasaId",
                        column: x => x.KasaId,
                        principalTable: "AppKasalar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzlar_AppOzelKodlar_OzelKod1Id",
                        column: x => x.OzelKod1Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzlar_AppOzelKodlar_OzelKod2Id",
                        column: x => x.OzelKod2Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzlar_AppOzelKodlar_OzelKod3Id",
                        column: x => x.OzelKod3Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzlar_AppOzelKodlar_OzelKod4Id",
                        column: x => x.OzelKod4Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzlar_AppOzelKodlar_OzelKod5Id",
                        column: x => x.OzelKod5Id,
                        principalTable: "AppOzelKodlar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzlar_AppSubeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "AppSubeler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppFaturaHareketler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaturaId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    HareketTuru = table.Column<byte>(type: "TinyInt", nullable: false),
                    StokId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    HizmetId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    MasrafId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    DepoId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    Miktar = table.Column<decimal>(type: "Money", nullable: false),
                    BirimFiyat = table.Column<decimal>(type: "Money", nullable: false),
                    BrutTutar = table.Column<decimal>(type: "Money", nullable: false),
                    IndirimTutar = table.Column<decimal>(type: "Money", nullable: false),
                    KdvOrani = table.Column<int>(type: "Int", nullable: false),
                    KdvHaricTutar = table.Column<decimal>(type: "Money", nullable: false),
                    KdvTutar = table.Column<decimal>(type: "Money", nullable: false),
                    NetTutar = table.Column<decimal>(type: "Money", nullable: false),
                    Aciklama = table.Column<string>(type: "VarChar(200)", maxLength: 200, nullable: true),
                    CariId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_AppFaturaHareketler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFaturaHareketler_AppCariler_CariId",
                        column: x => x.CariId,
                        principalTable: "AppCariler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturaHareketler_AppDepolar_DepoId",
                        column: x => x.DepoId,
                        principalTable: "AppDepolar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturaHareketler_AppFaturalar_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "AppFaturalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppFaturaHareketler_AppHizmetler_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "AppHizmetler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturaHareketler_AppMasraflar_MasrafId",
                        column: x => x.MasrafId,
                        principalTable: "AppMasraflar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFaturaHareketler_AppStoklar_StokId",
                        column: x => x.StokId,
                        principalTable: "AppStoklar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppMakbuzHareketler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MakbuzId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    OdemeTuru = table.Column<byte>(type: "TinyInt", nullable: false),
                    TakipNo = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: true),
                    CekBankaId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    CekBankaSubeId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    CekHesapNo = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: true),
                    BelgeNo = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: true),
                    Vade = table.Column<DateTime>(type: "Date", nullable: false),
                    AsilBorclu = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: true),
                    Ciranta = table.Column<string>(type: "VarChar(20)", maxLength: 20, nullable: true),
                    KasaId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    BankaHesapId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    Tutar = table.Column<decimal>(type: "Money", nullable: false),
                    BelgeDurumu = table.Column<byte>(type: "TinyInt", nullable: false),
                    KendiBelgemiz = table.Column<bool>(type: "Bit", nullable: false),
                    Aciklama = table.Column<string>(type: "VarChar(200)", maxLength: 200, nullable: true),
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
                    table.PrimaryKey("PK_AppMakbuzHareketler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMakbuzHareketler_AppBankaHesaplar_BankaHesapId",
                        column: x => x.BankaHesapId,
                        principalTable: "AppBankaHesaplar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzHareketler_AppBankaSubeler_CekBankaSubeId",
                        column: x => x.CekBankaSubeId,
                        principalTable: "AppBankaSubeler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzHareketler_AppBankalar_CekBankaId",
                        column: x => x.CekBankaId,
                        principalTable: "AppBankalar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzHareketler_AppKasalar_KasaId",
                        column: x => x.KasaId,
                        principalTable: "AppKasalar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMakbuzHareketler_AppMakbuzlar_MakbuzId",
                        column: x => x.MakbuzId,
                        principalTable: "AppMakbuzlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppFirmaParametreler_DepoId",
                table: "AppFirmaParametreler",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaHesaplar_BankaSubeId",
                table: "AppBankaHesaplar",
                column: "BankaSubeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaHesaplar_CariId",
                table: "AppBankaHesaplar",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaHesaplar_Kod",
                table: "AppBankaHesaplar",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaHesaplar_OzelKod1Id",
                table: "AppBankaHesaplar",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaHesaplar_OzelKod2Id",
                table: "AppBankaHesaplar",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaHesaplar_OzelKod3Id",
                table: "AppBankaHesaplar",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaHesaplar_OzelKod4Id",
                table: "AppBankaHesaplar",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaHesaplar_OzelKod5Id",
                table: "AppBankaHesaplar",
                column: "OzelKod5Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBankaHesaplar_SubeId",
                table: "AppBankaHesaplar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBirimler_Kod",
                table: "AppBirimler",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppBirimler_OzelKod1Id",
                table: "AppBirimler",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBirimler_OzelKod2Id",
                table: "AppBirimler",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBirimler_OzelKod3Id",
                table: "AppBirimler",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBirimler_OzelKod4Id",
                table: "AppBirimler",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppBirimler_OzelKod5Id",
                table: "AppBirimler",
                column: "OzelKod5Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppCariler_IlceId",
                table: "AppCariler",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCariler_IlId",
                table: "AppCariler",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCariler_Kod",
                table: "AppCariler",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppCariler_OzelKod1Id",
                table: "AppCariler",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppCariler_OzelKod2Id",
                table: "AppCariler",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppCariler_OzelKod3Id",
                table: "AppCariler",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppCariler_OzelKod4Id",
                table: "AppCariler",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppCariler_OzelKod5Id",
                table: "AppCariler",
                column: "OzelKod5Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppCariSubeler_CariId",
                table: "AppCariSubeler",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDepolar_Kod",
                table: "AppDepolar",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppDepolar_OzelKod1Id",
                table: "AppDepolar",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppDepolar_OzelKod2Id",
                table: "AppDepolar",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppDepolar_OzelKod3Id",
                table: "AppDepolar",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppDepolar_OzelKod4Id",
                table: "AppDepolar",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppDepolar_OzelKod5Id",
                table: "AppDepolar",
                column: "OzelKod5Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppDepolar_SubeId",
                table: "AppDepolar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturaHareketler_CariId",
                table: "AppFaturaHareketler",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturaHareketler_DepoId",
                table: "AppFaturaHareketler",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturaHareketler_FaturaId",
                table: "AppFaturaHareketler",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturaHareketler_HizmetId",
                table: "AppFaturaHareketler",
                column: "HizmetId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturaHareketler_MasrafId",
                table: "AppFaturaHareketler",
                column: "MasrafId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturaHareketler_StokId",
                table: "AppFaturaHareketler",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturalar_CariId",
                table: "AppFaturalar",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturalar_DepoId",
                table: "AppFaturalar",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturalar_DonemId",
                table: "AppFaturalar",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturalar_FaturaNo",
                table: "AppFaturalar",
                column: "FaturaNo");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturalar_OzelKod1Id",
                table: "AppFaturalar",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturalar_OzelKod2Id",
                table: "AppFaturalar",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturalar_OzelKod3Id",
                table: "AppFaturalar",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturalar_OzelKod4Id",
                table: "AppFaturalar",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturalar_OzelKod5Id",
                table: "AppFaturalar",
                column: "OzelKod5Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppFaturalar_SubeId",
                table: "AppFaturalar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppHizmetler_BirimId",
                table: "AppHizmetler",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_AppHizmetler_Kod",
                table: "AppHizmetler",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppHizmetler_OzelKod1Id",
                table: "AppHizmetler",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppHizmetler_OzelKod2Id",
                table: "AppHizmetler",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppHizmetler_OzelKod3Id",
                table: "AppHizmetler",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppHizmetler_OzelKod4Id",
                table: "AppHizmetler",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppHizmetler_OzelKod5Id",
                table: "AppHizmetler",
                column: "OzelKod5Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppKasalar_Kod",
                table: "AppKasalar",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppKasalar_OzelKod1Id",
                table: "AppKasalar",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppKasalar_OzelKod2Id",
                table: "AppKasalar",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppKasalar_OzelKod3Id",
                table: "AppKasalar",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppKasalar_OzelKod4Id",
                table: "AppKasalar",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppKasalar_OzelKod5Id",
                table: "AppKasalar",
                column: "OzelKod5Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppKasalar_SubeId",
                table: "AppKasalar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzHareketler_BankaHesapId",
                table: "AppMakbuzHareketler",
                column: "BankaHesapId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzHareketler_CekBankaId",
                table: "AppMakbuzHareketler",
                column: "CekBankaId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzHareketler_CekBankaSubeId",
                table: "AppMakbuzHareketler",
                column: "CekBankaSubeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzHareketler_KasaId",
                table: "AppMakbuzHareketler",
                column: "KasaId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzHareketler_MakbuzId",
                table: "AppMakbuzHareketler",
                column: "MakbuzId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzHareketler_TakipNo",
                table: "AppMakbuzHareketler",
                column: "TakipNo");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_BankaHesapId",
                table: "AppMakbuzlar",
                column: "BankaHesapId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_CariId",
                table: "AppMakbuzlar",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_DonemId",
                table: "AppMakbuzlar",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_KasaId",
                table: "AppMakbuzlar",
                column: "KasaId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_MakbuzNo",
                table: "AppMakbuzlar",
                column: "MakbuzNo");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_OzelKod1Id",
                table: "AppMakbuzlar",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_OzelKod2Id",
                table: "AppMakbuzlar",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_OzelKod3Id",
                table: "AppMakbuzlar",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_OzelKod4Id",
                table: "AppMakbuzlar",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_OzelKod5Id",
                table: "AppMakbuzlar",
                column: "OzelKod5Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppMakbuzlar_SubeId",
                table: "AppMakbuzlar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMasraflar_BirimId",
                table: "AppMasraflar",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMasraflar_Kod",
                table: "AppMasraflar",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppMasraflar_OzelKod1Id",
                table: "AppMasraflar",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppMasraflar_OzelKod2Id",
                table: "AppMasraflar",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppMasraflar_OzelKod3Id",
                table: "AppMasraflar",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppMasraflar_OzelKod4Id",
                table: "AppMasraflar",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppMasraflar_OzelKod5Id",
                table: "AppMasraflar",
                column: "OzelKod5Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppStoklar_BirimId",
                table: "AppStoklar",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStoklar_Kod",
                table: "AppStoklar",
                column: "Kod");

            migrationBuilder.CreateIndex(
                name: "IX_AppStoklar_OzelKod1Id",
                table: "AppStoklar",
                column: "OzelKod1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppStoklar_OzelKod2Id",
                table: "AppStoklar",
                column: "OzelKod2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppStoklar_OzelKod3Id",
                table: "AppStoklar",
                column: "OzelKod3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppStoklar_OzelKod4Id",
                table: "AppStoklar",
                column: "OzelKod4Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppStoklar_OzelKod5Id",
                table: "AppStoklar",
                column: "OzelKod5Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFirmaParametreler_AppDepolar_DepoId",
                table: "AppFirmaParametreler",
                column: "DepoId",
                principalTable: "AppDepolar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFirmaParametreler_AppDepolar_DepoId",
                table: "AppFirmaParametreler");

            migrationBuilder.DropTable(
                name: "AppCariSubeler");

            migrationBuilder.DropTable(
                name: "AppFaturaHareketler");

            migrationBuilder.DropTable(
                name: "AppMakbuzHareketler");

            migrationBuilder.DropTable(
                name: "AppFaturalar");

            migrationBuilder.DropTable(
                name: "AppHizmetler");

            migrationBuilder.DropTable(
                name: "AppMasraflar");

            migrationBuilder.DropTable(
                name: "AppStoklar");

            migrationBuilder.DropTable(
                name: "AppMakbuzlar");

            migrationBuilder.DropTable(
                name: "AppDepolar");

            migrationBuilder.DropTable(
                name: "AppBirimler");

            migrationBuilder.DropTable(
                name: "AppBankaHesaplar");

            migrationBuilder.DropTable(
                name: "AppKasalar");

            migrationBuilder.DropTable(
                name: "AppCariler");

            migrationBuilder.DropIndex(
                name: "IX_AppFirmaParametreler_DepoId",
                table: "AppFirmaParametreler");

            migrationBuilder.DropColumn(
                name: "DepoId",
                table: "AppFirmaParametreler");
        }
    }
}
