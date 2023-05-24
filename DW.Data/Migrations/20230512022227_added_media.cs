using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DW.Data.Migrations
{
    /// <inheritdoc />
    public partial class added_media : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScriptLines_Projects_ProjectId",
                table: "ScriptLines");

            migrationBuilder.DropForeignKey(
                name: "FK_TranslateHistory_ScriptLines_DwScriptLineId",
                table: "TranslateHistory");

            migrationBuilder.DropIndex(
                name: "IX_TranslateHistory_DwScriptLineId",
                table: "TranslateHistory");

            migrationBuilder.DropColumn(
                name: "DwScriptLineId",
                table: "TranslateHistory");

            migrationBuilder.AddColumn<long>(
                name: "LineId",
                table: "TranslateHistory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedAt",
                table: "Teams",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ScriptLines",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<short>(
                name: "TimeZone",
                table: "AspNetUsers",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: false),
                    UploaderId = table.Column<string>(type: "text", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medias_AspNetUsers_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DwCharacterDwMedia",
                columns: table => new
                {
                    DwCharacterId = table.Column<string>(type: "text", nullable: false),
                    MediasId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwCharacterDwMedia", x => new { x.DwCharacterId, x.MediasId });
                    table.ForeignKey(
                        name: "FK_DwCharacterDwMedia_Characters_DwCharacterId",
                        column: x => x.DwCharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DwCharacterDwMedia_Medias_MediasId",
                        column: x => x.MediasId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DwMediaDwProject",
                columns: table => new
                {
                    DwProjectId = table.Column<string>(type: "text", nullable: false),
                    MediasId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwMediaDwProject", x => new { x.DwProjectId, x.MediasId });
                    table.ForeignKey(
                        name: "FK_DwMediaDwProject_Medias_MediasId",
                        column: x => x.MediasId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DwMediaDwProject_Projects_DwProjectId",
                        column: x => x.DwProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DwMediaDwTeam",
                columns: table => new
                {
                    DwTeamId = table.Column<string>(type: "text", nullable: false),
                    MediasId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwMediaDwTeam", x => new { x.DwTeamId, x.MediasId });
                    table.ForeignKey(
                        name: "FK_DwMediaDwTeam_Medias_MediasId",
                        column: x => x.MediasId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DwMediaDwTeam_Teams_DwTeamId",
                        column: x => x.DwTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DwMediaDwUser",
                columns: table => new
                {
                    ActorsMediasId = table.Column<string>(type: "text", nullable: false),
                    DwUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DwMediaDwUser", x => new { x.ActorsMediasId, x.DwUserId });
                    table.ForeignKey(
                        name: "FK_DwMediaDwUser_AspNetUsers_DwUserId",
                        column: x => x.DwUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DwMediaDwUser_Medias_ActorsMediasId",
                        column: x => x.ActorsMediasId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranslateHistory_LineId",
                table: "TranslateHistory",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_DwCharacterDwMedia_MediasId",
                table: "DwCharacterDwMedia",
                column: "MediasId");

            migrationBuilder.CreateIndex(
                name: "IX_DwMediaDwProject_MediasId",
                table: "DwMediaDwProject",
                column: "MediasId");

            migrationBuilder.CreateIndex(
                name: "IX_DwMediaDwTeam_MediasId",
                table: "DwMediaDwTeam",
                column: "MediasId");

            migrationBuilder.CreateIndex(
                name: "IX_DwMediaDwUser_DwUserId",
                table: "DwMediaDwUser",
                column: "DwUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_UploaderId",
                table: "Medias",
                column: "UploaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptLines_Projects_ProjectId",
                table: "ScriptLines",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TranslateHistory_ScriptLines_LineId",
                table: "TranslateHistory",
                column: "LineId",
                principalTable: "ScriptLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScriptLines_Projects_ProjectId",
                table: "ScriptLines");

            migrationBuilder.DropForeignKey(
                name: "FK_TranslateHistory_ScriptLines_LineId",
                table: "TranslateHistory");

            migrationBuilder.DropTable(
                name: "DwCharacterDwMedia");

            migrationBuilder.DropTable(
                name: "DwMediaDwProject");

            migrationBuilder.DropTable(
                name: "DwMediaDwTeam");

            migrationBuilder.DropTable(
                name: "DwMediaDwUser");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_TranslateHistory_LineId",
                table: "TranslateHistory");

            migrationBuilder.DropColumn(
                name: "LineId",
                table: "TranslateHistory");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "DwScriptLineId",
                table: "TranslateHistory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ScriptLines",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslateHistory_DwScriptLineId",
                table: "TranslateHistory",
                column: "DwScriptLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScriptLines_Projects_ProjectId",
                table: "ScriptLines",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TranslateHistory_ScriptLines_DwScriptLineId",
                table: "TranslateHistory",
                column: "DwScriptLineId",
                principalTable: "ScriptLines",
                principalColumn: "Id");
        }
    }
}
