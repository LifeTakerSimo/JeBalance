﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeBalance.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    street_number = table.Column<string>(type: "TEXT", nullable: true),
                    street_name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    postal_code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    city_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    is_vip = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_admin = table.Column<bool>(type: "INTEGER", nullable: false),
                    is_fisc = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    rejection = table.Column<int>(type: "INTEGER", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Response",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    response_type = table.Column<bool>(type: "INTEGER", nullable: false),
                    amount = table.Column<decimal>(type: "TEXT", nullable: true),
                    DenonciationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Calomniateur",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    person_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calomniateur", x => x.id);
                    table.ForeignKey(
                        name: "FK_Calomniateur_Person_person_id",
                        column: x => x.person_id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Denonciation",
                columns: table => new
                {
                    denonciation_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InformantId = table.Column<int>(type: "INTEGER", nullable: false),
                    SuspectId = table.Column<int>(type: "INTEGER", nullable: false),
                    offense = table.Column<string>(type: "TEXT", nullable: false),
                    evasion_country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    isTreated = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denonciation", x => x.denonciation_id);
                    table.ForeignKey(
                        name: "FK_Denonciation_Person_InformantId",
                        column: x => x.InformantId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Denonciation_Person_SuspectId",
                        column: x => x.SuspectId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    person_id = table.Column<int>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFisc = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsVip = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Person_person_id",
                        column: x => x.person_id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calomniateur_person_id",
                table: "Calomniateur",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_Denonciation_InformantId",
                table: "Denonciation",
                column: "InformantId");

            migrationBuilder.CreateIndex(
                name: "IX_Denonciation_SuspectId",
                table: "Denonciation",
                column: "SuspectId");

            migrationBuilder.CreateIndex(
                name: "IX_User_person_id",
                table: "User",
                column: "person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calomniateur");

            migrationBuilder.DropTable(
                name: "Denonciation");

            migrationBuilder.DropTable(
                name: "Response");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
