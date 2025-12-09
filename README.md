# 🗳️ Romanian Online Voting System

A modern, secure, and user-friendly online voting platform designed for Romanian elections. This system allows citizens to participate in various types of elections from the comfort of their homes, with real-time statistics and transparent results.

## ✨ What Makes This Special?

We've built this system with **you** in mind. No complicated forms, no confusing interfaces—just a clean, beautiful design that makes voting as simple as clicking a few buttons. Whether you're voting in local elections, parliamentary elections, presidential elections, or referendums, everything is right at your fingertips.

## 🔐 Secure Authentication

**Login with Your ID Card** - Your security is our top priority. The system uses your Romanian national ID card for authentication, ensuring that only eligible voters can participate. This two-factor verification process protects the integrity of every election and gives you confidence that your vote matters.

*Note: The ID card authentication system is currently being finalized and will be available in the next update.*

## 🎯 Features

### 📊 View Ongoing Elections
- Browse all active elections in one place
- Filter by election type (Local, Parliamentarian, Presidential, Referendum)
- See election details, dates, and current status at a glance

### 🗳️ Cast Your Vote
- Simple, intuitive voting interface
- Select your county and preferred candidate
- Real-time vote confirmation
- Your vote is recorded securely and anonymously

### 📈 Real-Time Statistics
Once you click on any election, you'll see comprehensive statistics including:

- **Overall Statistics**
  - Total votes cast
  - Total eligible voters
  - Voter turnout percentage

- **Candidate Results**
  - Ranked list of all candidates
  - Individual vote counts
  - Percentage of total votes
  - Visual progress bars for easy comparison

- **Party Statistics**
  - Total votes per party
  - Party vote percentages
  - Number of candidates per party
  - Clear breakdown of party performance

- **County Statistics**
  - Votes cast per county
  - Eligible voters per county
  - Turnout percentage for each of Romania's 41 counties
  - Sorted by highest turnout for easy analysis

### 🏛️ Manage Political Parties
- View all registered political parties
- See party descriptions and logos
- Manage party information

### 👥 Manage Candidates
- View all election candidates
- See candidate profiles with photos
- View party affiliations
- Manage candidate information

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server (LocalDB or full SQL Server instance)
- A modern web browser

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd MPP-SMTH
   ```

2. **Configure the database**
   - Open `appsettings.json`
   - Update the `DefaultConnection` string with your SQL Server connection details:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RomanianVotingDB;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Run database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open your browser**
   - Navigate to `https://localhost:7262` (or the port shown in your terminal)
   - Start exploring!

## 📁 Project Structure

```
MPP-SMTH/
├── Backend/
│   ├── Controllers/          # MVC Controllers
│   ├── Data/                 # Database context and repositories
│   ├── Domain/               # Core entities (Election, Candidate, Party, Vote, County)
│   ├── Services/             # Business logic layer
│   └── ViewModels/           # Data transfer objects
├── Frontend/
│   ├── Views/                # Razor views
│   └── wwwroot/              # Static files (CSS, JS, images)
└── appsettings.json          # Configuration file
```

## 🎨 Design Philosophy

We believe that voting should be **beautiful** and **accessible**. That's why we've designed this system with:

- **Dark Theme** - Easy on the eyes, especially for long browsing sessions
- **Minimalist Design** - Clean, uncluttered interface that focuses on what matters
- **Responsive Layout** - Works perfectly on desktop, tablet, and mobile devices
- **Intuitive Navigation** - Find what you need in seconds, not minutes

## 🗺️ Romanian Counties

The system includes all 41 Romanian counties with realistic eligible voter counts:
- Alba, Arad, Argeș, Bacău, Bihor, Bistrița-Năsăud, Botoșani, Brașov, Brăila
- București (with the highest eligible voter count)
- And 32 more counties...

Each county's statistics are tracked individually, giving you detailed insights into regional voting patterns.

## 🔄 How Voting Works

1. **Browse Elections** - Start on the home page and see all ongoing elections
2. **Select an Election** - Click "View Details & Vote" on any election that interests you
3. **Review Candidates** - See all candidates running in that election with their party affiliations
4. **Cast Your Vote** - Select your county and your preferred candidate
5. **Confirm** - Your vote is recorded securely
6. **View Results** - See real-time statistics update as votes come in

## 📊 Statistics Explained

- **Voter Turnout**: The percentage of eligible voters who have actually cast their vote
- **Candidate Percentage**: What percentage of total votes each candidate has received
- **Party Percentage**: What percentage of total votes each party has received
- **County Turnout**: How engaged voters are in each county

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core MVC (.NET 8.0)
- **Database**: SQL Server with Entity Framework Core
- **Frontend**: Razor Views with custom CSS
- **Architecture**: Clean Architecture with Repository Pattern

## 🤝 Contributing

We welcome contributions! Whether it's fixing bugs, adding features, or improving documentation, every contribution helps make Romanian democracy more accessible.

## 📝 License

This project is part of an academic/research initiative to improve digital democracy in Romania.

## 🙏 Acknowledgments

Built with ❤️ for the Romanian people, to make voting more accessible, transparent, and engaging.

---

**Remember**: Every vote counts. Your participation shapes the future of Romania. 🇷🇴
