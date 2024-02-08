using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateIdToVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "PartyVotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PartyVotes_CandidateId",
                table: "PartyVotes",
                column: "CandidateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PartyVotes_Candidates_CandidateId",
                table: "PartyVotes",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyVotes_Candidates_CandidateId",
                table: "PartyVotes");

            migrationBuilder.DropIndex(
                name: "IX_PartyVotes_CandidateId",
                table: "PartyVotes");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "PartyVotes");
        }
    }
}
