using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ASP1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    ElectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elections", x => x.ElectionId);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    PartyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.PartyId);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateId);
                    table.ForeignKey(
                        name: "FK_Candidates_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId");
                });

            migrationBuilder.InsertData(
                table: "Elections",
                columns: new[] { "ElectionId", "CreatedAt", "Description", "EndDate", "IsActive", "StartDate", "Title", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 29, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), "Elections for mayors, county council presidents, and local council members across all Romanian counties and municipalities.", new DateTime(2026, 1, 3, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), true, new DateTime(2025, 12, 4, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), "Local Elections 2024", 1, null },
                    { 2, new DateTime(2025, 11, 24, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), "Elections for members of the Chamber of Deputies and the Senate of Romania.", new DateTime(2025, 12, 29, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), true, new DateTime(2025, 11, 29, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), "Parliamentary Elections 2024", 2, null },
                    { 3, new DateTime(2025, 12, 1, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), "Election for the President of Romania. Citizens vote for the head of state who serves a five-year term.", new DateTime(2026, 1, 5, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), true, new DateTime(2025, 12, 6, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), "Presidential Elections 2024", 3, null },
                    { 4, new DateTime(2025, 11, 27, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), "National referendum on proposed constitutional amendments regarding judicial reform and anti-corruption measures.", new DateTime(2026, 1, 1, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), true, new DateTime(2025, 12, 2, 11, 21, 3, 241, DateTimeKind.Utc).AddTicks(8517), "Constitutional Referendum 2024", 4, null }
                });

            migrationBuilder.InsertData(
                table: "Parties",
                columns: new[] { "PartyId", "Description", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Partidul Social Democrat", "https://upload.wikimedia.org/wikipedia/commons/6/6e/Logo_PSD_2020.png", "PSD" },
                    { 2, "Partidul Național Liberal", "https://upload.wikimedia.org/wikipedia/commons/7/7e/Logo_PNL_2014.png", "PNL" },
                    { 3, "Uniunea Salvați România", "https://upload.wikimedia.org/wikipedia/commons/2/2a/Logo_USR_2020.png", "USR" },
                    { 4, "Alianța pentru Unirea Românilor", "https://upload.wikimedia.org/wikipedia/commons/2/2d/Logo_AUR_2020.png", "AUR" }
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "CandidateId", "Age", "Description", "ImageUrl", "Name", "PartyId", "Position" },
                values: new object[,]
                {
                    { 1, 45, "Candidat PSD la Camera Deputaților.", "https://randomuser.me/api/portraits/men/1.jpg", "Ion Popescu", 1, "Deputat" },
                    { 2, 52, "Candidat PNL la Senat.", "https://randomuser.me/api/portraits/women/2.jpg", "Maria Ionescu", 2, "Senator" },
                    { 3, 38, "Candidat USR la Camera Deputaților.", "https://randomuser.me/api/portraits/men/3.jpg", "Andrei Vasilescu", 3, "Deputat" },
                    { 4, 41, "Candidat AUR la Senat.", "https://randomuser.me/api/portraits/women/4.jpg", "Elena Dumitru", 4, "Senator" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PartyId",
                table: "Candidates",
                column: "PartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Elections");

            migrationBuilder.DropTable(
                name: "Parties");
        }
    }
}
