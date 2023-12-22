using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeBalance.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PERSON",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    street_number = table.Column<string>(type: "TEXT", nullable: false),
                    street_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    postal_code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    city_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    is_vip = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ADMIN",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    person_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMIN", x => x.id);
                    table.ForeignKey(
                        name: "FK_ADMIN_PERSON_person_id",
                        column: x => x.person_id,
                        principalTable: "PERSON",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CALOMNIATEUR",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    person_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CALOMNIATEUR", x => x.id);
                    table.ForeignKey(
                        name: "FK_CALOMNIATEUR_PERSON_person_id",
                        column: x => x.person_id,
                        principalTable: "PERSON",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DENONCIATION",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InformantId = table.Column<int>(type: "INTEGER", nullable: false),
                    SuspectId = table.Column<int>(type: "INTEGER", nullable: false),
                    offense = table.Column<string>(type: "TEXT", nullable: false),
                    evasion_country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DENONCIATION", x => x.id);
                    table.ForeignKey(
                        name: "FK_DENONCIATION_PERSON_InformantId",
                        column: x => x.InformantId,
                        principalTable: "PERSON",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DENONCIATION_PERSON_SuspectId",
                        column: x => x.SuspectId,
                        principalTable: "PERSON",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESPONSE",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    response_type = table.Column<string>(type: "TEXT", nullable: false),
                    amount = table.Column<decimal>(type: "TEXT", nullable: true),
                    denonciation_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESPONSE", x => x.id);
                    table.ForeignKey(
                        name: "FK_RESPONSE_DENONCIATION_denonciation_id",
                        column: x => x.denonciation_id,
                        principalTable: "DENONCIATION",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADMIN_person_id",
                table: "ADMIN",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_CALOMNIATEUR_person_id",
                table: "CALOMNIATEUR",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_DENONCIATION_InformantId",
                table: "DENONCIATION",
                column: "InformantId");

            migrationBuilder.CreateIndex(
                name: "IX_DENONCIATION_SuspectId",
                table: "DENONCIATION",
                column: "SuspectId");

            migrationBuilder.CreateIndex(
                name: "IX_RESPONSE_denonciation_id",
                table: "RESPONSE",
                column: "denonciation_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADMIN");

            migrationBuilder.DropTable(
                name: "CALOMNIATEUR");

            migrationBuilder.DropTable(
                name: "RESPONSE");

            migrationBuilder.DropTable(
                name: "DENONCIATION");

            migrationBuilder.DropTable(
                name: "PERSON");
        }
    }
}
