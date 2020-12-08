using Microsoft.EntityFrameworkCore.Migrations;

namespace Introduction.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Judul = table.Column<string>(nullable: true),
                    Penulis = table.Column<string>(nullable: true),
                    Deskripsi = table.Column<string>(nullable: true),
                    Penerbit = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Gambar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
