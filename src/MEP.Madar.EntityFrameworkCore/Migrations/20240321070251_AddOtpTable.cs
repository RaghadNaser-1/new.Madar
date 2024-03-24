using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MEP.Madar.Migrations
{
    /// <inheritdoc />
    public partial class AddOtpTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppOtps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserOtp = table.Column<int>(type: "int", nullable: false),
                    OtpExpTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AbpUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOtps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOtps_AbpUsers_AbpUserId",
                        column: x => x.AbpUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppOtps_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppOtps_AbpUserId",
                table: "AppOtps",
                column: "AbpUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOtps_UserId",
                table: "AppOtps",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppOtps");
        }
    }
}
