using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Keeper_UserService.Migrations
{
    /// <inheritdoc />
    public partial class ActivationPasswordsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivationPasswords_Users_UserId",
                table: "ActivationPasswords");

            migrationBuilder.DropIndex(
                name: "IX_ActivationPasswords_UserId",
                table: "ActivationPasswords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ActivationPasswords");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ActivationPasswords",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ActivationPasswords");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ActivationPasswords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ActivationPasswords_UserId",
                table: "ActivationPasswords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivationPasswords_Users_UserId",
                table: "ActivationPasswords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
