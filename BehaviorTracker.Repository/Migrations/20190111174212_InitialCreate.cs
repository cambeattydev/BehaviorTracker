using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BehaviorTracker.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentKey = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentFirstName = table.Column<string>(nullable: true),
                    StudentLastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentKey);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalKey = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentKey = table.Column<long>(nullable: false),
                    GoalDescription = table.Column<string>(nullable: true),
                    GoalType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalKey);
                    table.ForeignKey(
                        name: "FK_Goals_Students_StudentKey",
                        column: x => x.StudentKey,
                        principalTable: "Students",
                        principalColumn: "StudentKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalAnswers",
                columns: table => new
                {
                    GoalAnswerKey = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GoalKey = table.Column<long>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalAnswers", x => x.GoalAnswerKey);
                    table.ForeignKey(
                        name: "FK_GoalAnswers_Goals_GoalKey",
                        column: x => x.GoalKey,
                        principalTable: "Goals",
                        principalColumn: "GoalKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalAvailableAnswer",
                columns: table => new
                {
                    GoalAvailableAnswerKey = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GoalKey = table.Column<long>(nullable: false),
                    OptionValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalAvailableAnswer", x => x.GoalAvailableAnswerKey);
                    table.ForeignKey(
                        name: "FK_GoalAvailableAnswer_Goals_GoalKey",
                        column: x => x.GoalKey,
                        principalTable: "Goals",
                        principalColumn: "GoalKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalAnswers_GoalKey",
                table: "GoalAnswers",
                column: "GoalKey");

            migrationBuilder.CreateIndex(
                name: "IX_GoalAvailableAnswer_GoalKey",
                table: "GoalAvailableAnswer",
                column: "GoalKey");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_StudentKey",
                table: "Goals",
                column: "StudentKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalAnswers");

            migrationBuilder.DropTable(
                name: "GoalAvailableAnswer");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
