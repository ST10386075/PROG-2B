# PROG-2B

MonthlyClaims Web Application
📋 Overview
MonthlyClaims is a comprehensive web application designed for educational institutions to manage and process monthly claims submitted by lecturers. The system streamlines the claim submission, review, and approval workflow, providing a seamless experience for both lecturers and administrators.

🚀 Features
👨‍🏫 Lecturer Features
Claim Submission: Submit monthly claims with hours worked and hourly rates

Document Upload: Attach supporting documents (PDF, DOCX, XLSX) up to 5MB

Auto-Calculation: Automatic total amount calculation based on hours and rate

Claim History: View submission history and status

Secure File Storage: Encrypted document storage with unique file naming

👨‍💼 Administrator Features
Dashboard: Overview of all claims with status tracking

Claim Review: Review and process pending claims

Status Management: Update claim status (Pending, Approved, Rejected)

Reporting: Generate reports and analytics

User Management: Manage lecturer accounts and permissions

🛠 Technology Stack
Frontend
WPF (Windows Presentation Foundation) - Desktop client interface

XAML - UI markup language

MVVM Pattern - Architecture pattern for better separation of concerns

Backend
ASP.NET Core - Web API framework

Entity Framework Core - ORM for database operations

SQL Server - Database management system

Security
Authentication - JWT-based authentication

Authorization - Role-based access control

File Encryption - Secure document storage

📥 Installation
Prerequisites
.NET 6.0 SDK or later

SQL Server 2019 or later

Windows 10/11 for WPF client

Visual Studio 2022 or VS Code

Setup Instructions
Clone the Repository

bash
git clone https://github.com/your-organization/monthlyclaims-app.git
cd monthlyclaims-app
Database Setup

sql
-- Create database
CREATE DATABASE MonthlyClaimsDB;

-- Update connection string in appsettings.json
Backend Setup

bash
cd MonthlyClaims.API
dotnet restore
dotnet ef database update
dotnet run
Frontend Setup

bash
cd MonthlyClaims.Client
dotnet restore
dotnet run
⚙ Configuration
AppSettings.json (Backend)
json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MonthlyClaimsDB;Trusted_Connection=true;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-here",
    "Issuer": "MonthlyClaimsApp",
    "Audience": "MonthlyClaimsUsers"
  },
  "FileUploadSettings": {
    "MaxFileSize": 5242880,
    "AllowedExtensions": [".pdf", ".docx", ".xlsx"],
    "UploadPath": "Documents\\Claims"
  }
}
🎯 Usage Guide
For Lecturers
Login to the application using your credentials

Navigate to the Claims section

Click "New Claim" button

Fill in the required details:

Hours Worked

Hourly Rate

Supporting Notes

Upload supporting documents

Review the auto-calculated total amount

Submit the claim for review

For Administrators
Login to the admin dashboard

View pending claims in the queue

Review claim details and documents

Approve/Reject claims with comments

Generate monthly reports

📁 Project Structure
text
MonthlyClaimsApp/
├── MonthlyClaims.API/                 # Backend Web API
│   ├── Controllers/                   # API Controllers
│   ├── Models/                        # Data Models
│   ├── Services/                      # Business Logic
│   ├── Data/                          # DbContext & Migrations
│   └── Program.cs                     # Application Entry
├── MonthlyClaims.Client/              # WPF Desktop Client
│   ├── Views/                         # User Interface
│   ├── ViewModels/                    # Business Logic
│   ├── Models/                        # Client Models
│   └── Services/                      # API Communication
├── MonthlyClaims.Database/            # Database Scripts
└── Documentation/                     # Project Documentation
🔧 Database Schema
Key Tables
Claims - Main claims table with submission details

Users - User authentication and profiles

Documents - Uploaded supporting documents

StatusHistory - Claim status tracking

Sample Entity
csharp
public class Claim
{
    public int Id { get; set; }
    public string LecturerName { get; set; }
    public decimal HoursWorked { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Notes { get; set; }
    public string FilePath { get; set; }
    public string Status { get; set; }
    public DateTime SubmittedDate { get; set; }
}
🐛 Troubleshooting
Common Issues
Database Connection Error

Verify SQL Server is running

Check connection string in appsettings.json

Ensure database exists

File Upload Fails

Check file size (max 5MB)

Verify file format (PDF, DOCX, XLSX only)

Ensure upload directory permissions

Calculation Errors

Verify numeric input in hours and rate fields

Check for negative values
