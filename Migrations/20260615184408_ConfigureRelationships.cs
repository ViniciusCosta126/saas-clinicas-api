using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaasClinicas.Api.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professional_Clinics_ClinicId",
                table: "Professional");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Clinics_ClinicId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professional",
                table: "Professional");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Professional",
                newName: "Professionals");

            migrationBuilder.RenameIndex(
                name: "IX_User_ClinicId",
                table: "Users",
                newName: "IX_Users_ClinicId");

            migrationBuilder.RenameIndex(
                name: "IX_Professional_ClinicId",
                table: "Professionals",
                newName: "IX_Professionals_ClinicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professionals",
                table: "Professionals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Professionals_Clinics_ClinicId",
                table: "Professionals",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clinics_ClinicId",
                table: "Users",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professionals_Clinics_ClinicId",
                table: "Professionals");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clinics_ClinicId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professionals",
                table: "Professionals");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Professionals",
                newName: "Professional");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ClinicId",
                table: "User",
                newName: "IX_User_ClinicId");

            migrationBuilder.RenameIndex(
                name: "IX_Professionals_ClinicId",
                table: "Professional",
                newName: "IX_Professional_ClinicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professional",
                table: "Professional",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Professional_Clinics_ClinicId",
                table: "Professional",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Clinics_ClinicId",
                table: "User",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
