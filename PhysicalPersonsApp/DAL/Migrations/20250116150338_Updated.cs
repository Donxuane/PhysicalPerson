using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonModelId",
                table: "RelatedPersons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonModelId",
                table: "PhoneNumber",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPersons_PersonModelId",
                table: "RelatedPersons",
                column: "PersonModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_PersonModelId",
                table: "PhoneNumber",
                column: "PersonModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_PersonModel_PersonModelId",
                table: "PhoneNumber",
                column: "PersonModelId",
                principalTable: "PersonModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_PersonModel_PersonModelId",
                table: "RelatedPersons",
                column: "PersonModelId",
                principalTable: "PersonModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_PersonModel_PersonModelId",
                table: "PhoneNumber");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_PersonModel_PersonModelId",
                table: "RelatedPersons");

            migrationBuilder.DropIndex(
                name: "IX_RelatedPersons_PersonModelId",
                table: "RelatedPersons");

            migrationBuilder.DropIndex(
                name: "IX_PhoneNumber_PersonModelId",
                table: "PhoneNumber");

            migrationBuilder.DropColumn(
                name: "PersonModelId",
                table: "RelatedPersons");

            migrationBuilder.DropColumn(
                name: "PersonModelId",
                table: "PhoneNumber");
        }
    }
}
