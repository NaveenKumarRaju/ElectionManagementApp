using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangePartyVotesAsCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PartyVotes_PartyId",
                table: "PartyVotes");

            migrationBuilder.CreateIndex(
                name: "IX_PartyVotes_PartyId",
                table: "PartyVotes",
                column: "PartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PartyVotes_PartyId",
                table: "PartyVotes");

            migrationBuilder.CreateIndex(
                name: "IX_PartyVotes_PartyId",
                table: "PartyVotes",
                column: "PartyId",
                unique: true);
        }
    }
}
