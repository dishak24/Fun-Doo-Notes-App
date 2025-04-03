using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class FixedLabelsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_NoteUserUserId",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteLabels_Labels_LabelId",
                table: "NoteLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteLabels_Notes_NoteId",
                table: "NoteLabels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_NoteUserUserId",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "NoteUserUserId",
                table: "Labels");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_UserId",
                table: "Labels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Users_UserId",
                table: "Labels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteLabels_Labels_LabelId",
                table: "NoteLabels",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "LabelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteLabels_Notes_NoteId",
                table: "NoteLabels",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NotesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_UserId",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteLabels_Labels_LabelId",
                table: "NoteLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteLabels_Notes_NoteId",
                table: "NoteLabels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_UserId",
                table: "Labels");

            migrationBuilder.AddColumn<int>(
                name: "NoteUserUserId",
                table: "Labels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Labels_NoteUserUserId",
                table: "Labels",
                column: "NoteUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Users_NoteUserUserId",
                table: "Labels",
                column: "NoteUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteLabels_Labels_LabelId",
                table: "NoteLabels",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "LabelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteLabels_Notes_NoteId",
                table: "NoteLabels",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NotesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
