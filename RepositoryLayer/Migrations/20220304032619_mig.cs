using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTables",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotesTable",
                columns: table => new
                {
                    NotesId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    NoteBody = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPinned = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    BImage = table.Column<string>(nullable: true),
                    Reminder = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesTable", x => x.NotesId);
                    table.ForeignKey(
                        name: "FK_NotesTable_UserTables_Id",
                        column: x => x.Id,
                        principalTable: "UserTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CollabTable",
                columns: table => new
                {
                    CollabId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollabEmail = table.Column<string>(nullable: true),
                    NotesId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabTable", x => x.CollabId);
                    table.ForeignKey(
                        name: "FK_CollabTable_UserTables_Id",
                        column: x => x.Id,
                        principalTable: "UserTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollabTable_NotesTable_NotesId",
                        column: x => x.NotesId,
                        principalTable: "NotesTable",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_Id",
                table: "CollabTable",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_NotesId",
                table: "CollabTable",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesTable_Id",
                table: "NotesTable",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabTable");

            migrationBuilder.DropTable(
                name: "NotesTable");

            migrationBuilder.DropTable(
                name: "UserTables");
        }
    }
}
