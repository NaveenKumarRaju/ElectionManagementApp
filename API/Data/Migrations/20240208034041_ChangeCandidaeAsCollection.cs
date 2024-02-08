using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCandidaeAsCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidates_PartyId",
                table: "Candidates");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PartyId",
                table: "Candidates",
                column: "PartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidates_PartyId",
                table: "Candidates");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PartyId",
                table: "Candidates",
                column: "PartyId",
                unique: true);
        }
    }
}
