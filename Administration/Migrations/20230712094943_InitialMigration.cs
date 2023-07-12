using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DMs",
                columns: table => new
                {
                    DmID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DmLogin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DmPassword = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMs", x => x.DmID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoomPassword = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    Started = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Rooms_DMs_DmId",
                        column: x => x.DmId,
                        principalTable: "DMs",
                        principalColumn: "DmID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DMs_DmLogin",
                table: "DMs",
                column: "DmLogin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_DmId",
                table: "Rooms",
                column: "DmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "DMs");
        }
    }
}
