using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Data.Migrations
{
    /// <inheritdoc />
    public partial class Confirmation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranslateHistory_ScriptLines_LineId",
                table: "TranslateHistory");

            migrationBuilder.DropIndex(
                name: "IX_TranslateHistory_LineId",
                table: "TranslateHistory");

            migrationBuilder.DropColumn(
                name: "LineId",
                table: "TranslateHistory");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "TranslateHistory",
                newName: "TranslatedText");

            migrationBuilder.AddColumn<long>(
                name: "DwScriptLineId",
                table: "TranslateHistory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalText",
                table: "TranslateHistory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Confirmations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    MailCode = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confirmations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Confirmations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranslateHistory_DwScriptLineId",
                table: "TranslateHistory",
                column: "DwScriptLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Confirmations_UserId",
                table: "Confirmations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TranslateHistory_ScriptLines_DwScriptLineId",
                table: "TranslateHistory",
                column: "DwScriptLineId",
                principalTable: "ScriptLines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranslateHistory_ScriptLines_DwScriptLineId",
                table: "TranslateHistory");

            migrationBuilder.DropTable(
                name: "Confirmations");

            migrationBuilder.DropIndex(
                name: "IX_TranslateHistory_DwScriptLineId",
                table: "TranslateHistory");

            migrationBuilder.DropColumn(
                name: "DwScriptLineId",
                table: "TranslateHistory");

            migrationBuilder.DropColumn(
                name: "OriginalText",
                table: "TranslateHistory");

            migrationBuilder.RenameColumn(
                name: "TranslatedText",
                table: "TranslateHistory",
                newName: "Text");

            migrationBuilder.AddColumn<long>(
                name: "LineId",
                table: "TranslateHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TranslateHistory_LineId",
                table: "TranslateHistory",
                column: "LineId");

            migrationBuilder.AddForeignKey(
                name: "FK_TranslateHistory_ScriptLines_LineId",
                table: "TranslateHistory",
                column: "LineId",
                principalTable: "ScriptLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
