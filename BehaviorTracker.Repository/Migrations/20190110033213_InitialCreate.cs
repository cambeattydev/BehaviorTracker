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

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentKey", "StudentFirstName", "StudentLastName" },
                values: new object[] { 1L, "Student", "1" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentKey", "StudentFirstName", "StudentLastName" },
                values: new object[] { 2L, "Student", "2" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentKey", "StudentFirstName", "StudentLastName" },
                values: new object[] { 3L, "Student", "3" });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "GoalKey", "GoalDescription", "GoalType", "StudentKey" },
                values: new object[] { 1L, "I can speak respectfully.", 1, 1L });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "GoalKey", "GoalDescription", "GoalType", "StudentKey" },
                values: new object[] { 2L, "I can act appropriately.", 2, 1L });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "GoalKey", "GoalDescription", "GoalType", "StudentKey" },
                values: new object[] { 3L, "I am a numeric goal type", 2, 2L });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "GoalKey", "GoalDescription", "GoalType", "StudentKey" },
                values: new object[] { 4L, "I am a yes/no goal type", 1, 3L });

            migrationBuilder.InsertData(
                table: "GoalAnswers",
                columns: new[] { "GoalAnswerKey", "Answer", "Date", "GoalKey", "Notes" },
                values: new object[] { 1L, "True", new DateTime(2019, 1, 9, 15, 30, 0, 0, DateTimeKind.Unspecified), 1L, null });

            migrationBuilder.InsertData(
                table: "GoalAnswers",
                columns: new[] { "GoalAnswerKey", "Answer", "Date", "GoalKey", "Notes" },
                values: new object[] { 2L, "1", new DateTime(2019, 1, 9, 15, 30, 0, 0, DateTimeKind.Unspecified), 2L, null });

            migrationBuilder.InsertData(
                table: "GoalAnswers",
                columns: new[] { "GoalAnswerKey", "Answer", "Date", "GoalKey", "Notes" },
                values: new object[] { 3L, "2", new DateTime(2019, 1, 9, 15, 30, 0, 0, DateTimeKind.Unspecified), 3L, null });

            migrationBuilder.InsertData(
                table: "GoalAvailableAnswer",
                columns: new[] { "GoalAvailableAnswerKey", "GoalKey", "OptionValue" },
                values: new object[] { 1L, 2L, "1" });

            migrationBuilder.InsertData(
                table: "GoalAvailableAnswer",
                columns: new[] { "GoalAvailableAnswerKey", "GoalKey", "OptionValue" },
                values: new object[] { 2L, 2L, "2" });

            migrationBuilder.InsertData(
                table: "GoalAvailableAnswer",
                columns: new[] { "GoalAvailableAnswerKey", "GoalKey", "OptionValue" },
                values: new object[] { 3L, 2L, "3" });

            migrationBuilder.InsertData(
                table: "GoalAvailableAnswer",
                columns: new[] { "GoalAvailableAnswerKey", "GoalKey", "OptionValue" },
                values: new object[] { 4L, 2L, "4" });

            migrationBuilder.InsertData(
                table: "GoalAvailableAnswer",
                columns: new[] { "GoalAvailableAnswerKey", "GoalKey", "OptionValue" },
                values: new object[] { 5L, 3L, "0" });

            migrationBuilder.InsertData(
                table: "GoalAvailableAnswer",
                columns: new[] { "GoalAvailableAnswerKey", "GoalKey", "OptionValue" },
                values: new object[] { 6L, 3L, "0.5" });

            migrationBuilder.InsertData(
                table: "GoalAvailableAnswer",
                columns: new[] { "GoalAvailableAnswerKey", "GoalKey", "OptionValue" },
                values: new object[] { 7L, 3L, "1" });

            migrationBuilder.InsertData(
                table: "GoalAvailableAnswer",
                columns: new[] { "GoalAvailableAnswerKey", "GoalKey", "OptionValue" },
                values: new object[] { 8L, 3L, "1.5" });

            migrationBuilder.InsertData(
                table: "GoalAvailableAnswer",
                columns: new[] { "GoalAvailableAnswerKey", "GoalKey", "OptionValue" },
                values: new object[] { 9L, 3L, "2" });

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
