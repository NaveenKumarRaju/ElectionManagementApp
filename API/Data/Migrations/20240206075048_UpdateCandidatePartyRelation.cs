using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCandidatePartyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyName",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "Candidates",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PartyId",
                table: "Candidates",
                column: "PartyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Parties_PartyId",
                table: "Candidates",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Parties_PartyId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_PartyId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Candidates");

            migrationBuilder.AddColumn<string>(
                name: "PartyName",
                table: "Candidates",
                type: "TEXT",
                nullable: true);
        }
    }
}
