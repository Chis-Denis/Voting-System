using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ASP1.Migrations
{
    /// <inheritdoc />
    public partial class AddVotingSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    CountyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EligibleVoters = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.CountyId);
                });

            migrationBuilder.CreateTable(
                name: "ElectionCandidates",
                columns: table => new
                {
                    ElectionCandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionCandidates", x => x.ElectionCandidateId);
                    table.ForeignKey(
                        name: "FK_ElectionCandidates_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectionCandidates_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    VoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    CountyId = table.Column<int>(type: "int", nullable: false),
                    VotedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteId);
                    table.ForeignKey(
                        name: "FK_Votes_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "CountyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "CountyId", "Code", "EligibleVoters", "Name" },
                values: new object[,]
                {
                    { 1, "AB", 250000, "Alba" },
                    { 2, "AR", 380000, "Arad" },
                    { 3, "AG", 520000, "Argeș" },
                    { 4, "BC", 580000, "Bacău" },
                    { 5, "BH", 480000, "Bihor" },
                    { 6, "BN", 250000, "Bistrița-Năsăud" },
                    { 7, "BT", 380000, "Botoșani" },
                    { 8, "BV", 480000, "Brașov" },
                    { 9, "BR", 280000, "Brăila" },
                    { 10, "B", 1800000, "București" },
                    { 11, "BZ", 420000, "Buzău" },
                    { 12, "CS", 280000, "Caraș-Severin" },
                    { 13, "CJ", 620000, "Cluj" },
                    { 14, "CT", 620000, "Constanța" },
                    { 15, "CV", 180000, "Covasna" },
                    { 16, "DB", 480000, "Dâmbovița" },
                    { 17, "DJ", 620000, "Dolj" },
                    { 18, "GL", 480000, "Galați" },
                    { 19, "GR", 250000, "Giurgiu" },
                    { 20, "GJ", 320000, "Gorj" },
                    { 21, "HR", 280000, "Harghita" },
                    { 22, "HD", 380000, "Hunedoara" },
                    { 23, "IL", 250000, "Ialomița" },
                    { 24, "IS", 720000, "Iași" },
                    { 25, "IF", 380000, "Ilfov" },
                    { 26, "MM", 420000, "Maramureș" },
                    { 27, "MH", 220000, "Mehedinți" },
                    { 28, "MS", 480000, "Mureș" },
                    { 29, "NT", 420000, "Neamț" },
                    { 30, "OT", 420000, "Olt" },
                    { 31, "PH", 720000, "Prahova" },
                    { 32, "SJ", 200000, "Sălaj" },
                    { 33, "SM", 320000, "Satu Mare" },
                    { 34, "SB", 380000, "Sibiu" },
                    { 35, "SV", 580000, "Suceava" },
                    { 36, "TR", 320000, "Teleorman" },
                    { 37, "TM", 620000, "Timiș" },
                    { 38, "TL", 200000, "Tulcea" },
                    { 39, "VL", 320000, "Vâlcea" },
                    { 40, "VS", 380000, "Vaslui" },
                    { 41, "VN", 320000, "Vrancea" }
                });

            migrationBuilder.InsertData(
                table: "ElectionCandidates",
                columns: new[] { "ElectionCandidateId", "CandidateId", "ElectionId" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 2 },
                    { 3, 3, 2 },
                    { 4, 4, 2 },
                    { 5, 1, 3 },
                    { 6, 2, 3 },
                    { 7, 3, 3 },
                    { 8, 4, 3 },
                    { 9, 1, 1 },
                    { 10, 2, 1 },
                    { 11, 3, 1 },
                    { 12, 4, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 29, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016), new DateTime(2026, 1, 3, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016), new DateTime(2025, 12, 4, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016) });

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 24, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016), new DateTime(2025, 12, 29, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016), new DateTime(2025, 11, 29, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016) });

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 1, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016), new DateTime(2026, 1, 5, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016), new DateTime(2025, 12, 6, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016) });

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 27, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016), new DateTime(2026, 1, 1, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016), new DateTime(2025, 12, 2, 12, 0, 26, 305, DateTimeKind.Utc).AddTicks(3016) });

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCandidates_CandidateId",
                table: "ElectionCandidates",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionCandidates_ElectionId",
                table: "ElectionCandidates",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CountyId",
                table: "Votes",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_ElectionId",
                table: "Votes",
                column: "ElectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectionCandidates");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 29, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), new DateTime(2026, 1, 3, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), new DateTime(2025, 12, 4, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517) });

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 24, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), new DateTime(2025, 12, 29, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), new DateTime(2025, 11, 29, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517) });

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 12, 1, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), new DateTime(2026, 1, 5, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), new DateTime(2025, 12, 6, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517) });

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 11, 27, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), new DateTime(2026, 1, 1, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), new DateTime(2025, 12, 2, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517) });
        }
    }
}
