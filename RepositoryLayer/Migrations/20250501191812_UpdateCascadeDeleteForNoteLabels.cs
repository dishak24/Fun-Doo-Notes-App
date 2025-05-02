using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class UpdateCascadeDeleteForNoteLabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteLabels_Labels_LabelId",
                table: "NoteLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteLabels_Notes_NoteId",
                table: "NoteLabels");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteLabels_Labels_LabelId",
                table: "NoteLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteLabels_Notes_NoteId",
                table: "NoteLabels");

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
    }
}
