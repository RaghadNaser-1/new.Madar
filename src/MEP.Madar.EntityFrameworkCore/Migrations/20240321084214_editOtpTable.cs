using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MEP.Madar.Migrations
{
    /// <inheritdoc />
    public partial class editOtpTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOtps_AbpUsers_UserId",
                table: "AppOtps");

            migrationBuilder.DropIndex(
                name: "IX_AppOtps_UserId",
                table: "AppOtps");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppOtps");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AppOtps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppOtps_UserId",
                table: "AppOtps",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOtps_AbpUsers_UserId",
                table: "AppOtps",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
