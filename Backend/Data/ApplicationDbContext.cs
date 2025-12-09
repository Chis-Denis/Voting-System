using Microsoft.EntityFrameworkCore;
using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // public DbSet<Flight> Flights { get; set; }
    // public DbSet<Hotel> Hotels { get; set; }
    // public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Election> Elections { get; set; }
    public DbSet<County> Counties { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<ElectionCandidate> ElectionCandidates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Start of change: Seed Romanian parties and candidates *@
        modelBuilder.Entity<Party>().HasData(
            new Party { PartyId = 1, Name = "PSD", Description = "Partidul Social Democrat", LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/6/6e/Logo_PSD_2020.png" },
            new Party { PartyId = 2, Name = "PNL", Description = "Partidul Național Liberal", LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Logo_PNL_2014.png" },
            new Party { PartyId = 3, Name = "USR", Description = "Uniunea Salvați România", LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Logo_USR_2020.png" },
            new Party { PartyId = 4, Name = "AUR", Description = "Alianța pentru Unirea Românilor", LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2d/Logo_AUR_2020.png" }
        );
        modelBuilder.Entity<Candidate>().HasData(
            new Candidate { CandidateId = 1, Name = "Ion Popescu", Description = "Candidat PSD la Camera Deputaților.", ImageUrl = "https://randomuser.me/api/portraits/men/1.jpg", PartyId = 1, Age = 45, Position = "Deputat" },
            new Candidate { CandidateId = 2, Name = "Maria Ionescu", Description = "Candidat PNL la Senat.", ImageUrl = "https://randomuser.me/api/portraits/women/2.jpg", PartyId = 2, Age = 52, Position = "Senator" },
            new Candidate { CandidateId = 3, Name = "Andrei Vasilescu", Description = "Candidat USR la Camera Deputaților.", ImageUrl = "https://randomuser.me/api/portraits/men/3.jpg", PartyId = 3, Age = 38, Position = "Deputat" },
            new Candidate { CandidateId = 4, Name = "Elena Dumitru", Description = "Candidat AUR la Senat.", ImageUrl = "https://randomuser.me/api/portraits/women/4.jpg", PartyId = 4, Age = 41, Position = "Senator" }
        );
        
        // Seed sample elections
        var now = DateTime.UtcNow;
        modelBuilder.Entity<Election>().HasData(
            new Election 
            { 
                ElectionId = 1, 
                Title = "Local Elections 2024", 
                Description = "Elections for mayors, county council presidents, and local council members across all Romanian counties and municipalities.", 
                Type = ElectionType.Local, 
                StartDate = now.AddDays(-5), 
                EndDate = now.AddDays(25), 
                IsActive = true,
                CreatedAt = now.AddDays(-10)
            },
            new Election 
            { 
                ElectionId = 2, 
                Title = "Parliamentary Elections 2024", 
                Description = "Elections for members of the Chamber of Deputies and the Senate of Romania.", 
                Type = ElectionType.Parliamentarian, 
                StartDate = now.AddDays(-10), 
                EndDate = now.AddDays(20), 
                IsActive = true,
                CreatedAt = now.AddDays(-15)
            },
            new Election 
            { 
                ElectionId = 3, 
                Title = "Presidential Elections 2024", 
                Description = "Election for the President of Romania. Citizens vote for the head of state who serves a five-year term.", 
                Type = ElectionType.Presidential, 
                StartDate = now.AddDays(-3), 
                EndDate = now.AddDays(27), 
                IsActive = true,
                CreatedAt = now.AddDays(-8)
            },
            new Election 
            { 
                ElectionId = 4, 
                Title = "Constitutional Referendum 2024", 
                Description = "National referendum on proposed constitutional amendments regarding judicial reform and anti-corruption measures.", 
                Type = ElectionType.Referendum, 
                StartDate = now.AddDays(-7), 
                EndDate = now.AddDays(23), 
                IsActive = true,
                CreatedAt = now.AddDays(-12)
            }
        );

        // Seed Romanian counties
        modelBuilder.Entity<County>().HasData(
            new County { CountyId = 1, Name = "Alba", Code = "AB", EligibleVoters = 250000 },
            new County { CountyId = 2, Name = "Arad", Code = "AR", EligibleVoters = 380000 },
            new County { CountyId = 3, Name = "Argeș", Code = "AG", EligibleVoters = 520000 },
            new County { CountyId = 4, Name = "Bacău", Code = "BC", EligibleVoters = 580000 },
            new County { CountyId = 5, Name = "Bihor", Code = "BH", EligibleVoters = 480000 },
            new County { CountyId = 6, Name = "Bistrița-Năsăud", Code = "BN", EligibleVoters = 250000 },
            new County { CountyId = 7, Name = "Botoșani", Code = "BT", EligibleVoters = 380000 },
            new County { CountyId = 8, Name = "Brașov", Code = "BV", EligibleVoters = 480000 },
            new County { CountyId = 9, Name = "Brăila", Code = "BR", EligibleVoters = 280000 },
            new County { CountyId = 10, Name = "București", Code = "B", EligibleVoters = 1800000 },
            new County { CountyId = 11, Name = "Buzău", Code = "BZ", EligibleVoters = 420000 },
            new County { CountyId = 12, Name = "Caraș-Severin", Code = "CS", EligibleVoters = 280000 },
            new County { CountyId = 13, Name = "Cluj", Code = "CJ", EligibleVoters = 620000 },
            new County { CountyId = 14, Name = "Constanța", Code = "CT", EligibleVoters = 620000 },
            new County { CountyId = 15, Name = "Covasna", Code = "CV", EligibleVoters = 180000 },
            new County { CountyId = 16, Name = "Dâmbovița", Code = "DB", EligibleVoters = 480000 },
            new County { CountyId = 17, Name = "Dolj", Code = "DJ", EligibleVoters = 620000 },
            new County { CountyId = 18, Name = "Galați", Code = "GL", EligibleVoters = 480000 },
            new County { CountyId = 19, Name = "Giurgiu", Code = "GR", EligibleVoters = 250000 },
            new County { CountyId = 20, Name = "Gorj", Code = "GJ", EligibleVoters = 320000 },
            new County { CountyId = 21, Name = "Harghita", Code = "HR", EligibleVoters = 280000 },
            new County { CountyId = 22, Name = "Hunedoara", Code = "HD", EligibleVoters = 380000 },
            new County { CountyId = 23, Name = "Ialomița", Code = "IL", EligibleVoters = 250000 },
            new County { CountyId = 24, Name = "Iași", Code = "IS", EligibleVoters = 720000 },
            new County { CountyId = 25, Name = "Ilfov", Code = "IF", EligibleVoters = 380000 },
            new County { CountyId = 26, Name = "Maramureș", Code = "MM", EligibleVoters = 420000 },
            new County { CountyId = 27, Name = "Mehedinți", Code = "MH", EligibleVoters = 220000 },
            new County { CountyId = 28, Name = "Mureș", Code = "MS", EligibleVoters = 480000 },
            new County { CountyId = 29, Name = "Neamț", Code = "NT", EligibleVoters = 420000 },
            new County { CountyId = 30, Name = "Olt", Code = "OT", EligibleVoters = 420000 },
            new County { CountyId = 31, Name = "Prahova", Code = "PH", EligibleVoters = 720000 },
            new County { CountyId = 32, Name = "Sălaj", Code = "SJ", EligibleVoters = 200000 },
            new County { CountyId = 33, Name = "Satu Mare", Code = "SM", EligibleVoters = 320000 },
            new County { CountyId = 34, Name = "Sibiu", Code = "SB", EligibleVoters = 380000 },
            new County { CountyId = 35, Name = "Suceava", Code = "SV", EligibleVoters = 580000 },
            new County { CountyId = 36, Name = "Teleorman", Code = "TR", EligibleVoters = 320000 },
            new County { CountyId = 37, Name = "Timiș", Code = "TM", EligibleVoters = 620000 },
            new County { CountyId = 38, Name = "Tulcea", Code = "TL", EligibleVoters = 200000 },
            new County { CountyId = 39, Name = "Vâlcea", Code = "VL", EligibleVoters = 320000 },
            new County { CountyId = 40, Name = "Vaslui", Code = "VS", EligibleVoters = 380000 },
            new County { CountyId = 41, Name = "Vrancea", Code = "VN", EligibleVoters = 320000 }
        );

        // Seed Election-Candidate relationships (associate candidates with elections)
        modelBuilder.Entity<ElectionCandidate>().HasData(
            // Parliamentary Elections - all candidates
            new ElectionCandidate { ElectionCandidateId = 1, ElectionId = 2, CandidateId = 1 },
            new ElectionCandidate { ElectionCandidateId = 2, ElectionId = 2, CandidateId = 2 },
            new ElectionCandidate { ElectionCandidateId = 3, ElectionId = 2, CandidateId = 3 },
            new ElectionCandidate { ElectionCandidateId = 4, ElectionId = 2, CandidateId = 4 },
            // Presidential Elections - all candidates
            new ElectionCandidate { ElectionCandidateId = 5, ElectionId = 3, CandidateId = 1 },
            new ElectionCandidate { ElectionCandidateId = 6, ElectionId = 3, CandidateId = 2 },
            new ElectionCandidate { ElectionCandidateId = 7, ElectionId = 3, CandidateId = 3 },
            new ElectionCandidate { ElectionCandidateId = 8, ElectionId = 3, CandidateId = 4 },
            // Local Elections - all candidates
            new ElectionCandidate { ElectionCandidateId = 9, ElectionId = 1, CandidateId = 1 },
            new ElectionCandidate { ElectionCandidateId = 10, ElectionId = 1, CandidateId = 2 },
            new ElectionCandidate { ElectionCandidateId = 11, ElectionId = 1, CandidateId = 3 },
            new ElectionCandidate { ElectionCandidateId = 12, ElectionId = 1, CandidateId = 4 }
        );
        // End of change: Seed Romanian parties and candidates *@
    }
} 