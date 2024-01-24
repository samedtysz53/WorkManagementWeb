using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagementWeb.Migrations
{
    /// <inheritdoc />
    public partial class dbmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gorev",
                columns: table => new
                {
                    GorevID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsEmriID = table.Column<int>(type: "int", nullable: false),
                    CalisanID = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaslamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DigerIlgiliBilgiler = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gorev", x => x.GorevID);
                });

            migrationBuilder.CreateTable(
                name: "MalzemeVeStok",
                columns: table => new
                {
                    MalzemeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsEmriID = table.Column<int>(type: "int", nullable: false),
                    MalzemeAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miktar = table.Column<int>(type: "int", nullable: false),
                    BirimFiyati = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToplamMaliyet = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalzemeVeStok", x => x.MalzemeID);
                });

            migrationBuilder.CreateTable(
                name: "Musteri",
                columns: table => new
                {
                    MusteriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IletisimBilgileri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteri", x => x.MusteriID);
                });

            migrationBuilder.CreateTable(
                name: "TIsEmrieam",
                columns: table => new
                {
                    IsEmriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriID = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DigerIlgiliBilgiler = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIsEmrieam", x => x.IsEmriID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.KullaniciID);
                });

            migrationBuilder.CreateTable(
                name: "ZamanTakibi",
                columns: table => new
                {
                    ZamanTakibiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GorevID = table.Column<int>(type: "int", nullable: false),
                    CalisanID = table.Column<int>(type: "int", nullable: false),
                    BaslangicZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToplamCalismaSuresi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZamanTakibi", x => x.ZamanTakibiID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gorev");

            migrationBuilder.DropTable(
                name: "MalzemeVeStok");

            migrationBuilder.DropTable(
                name: "Musteri");

            migrationBuilder.DropTable(
                name: "TIsEmrieam");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ZamanTakibi");
        }
    }
}
