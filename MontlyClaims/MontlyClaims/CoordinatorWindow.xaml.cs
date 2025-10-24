using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MontlyClaims
{
    /// <summary>
    /// Interaction logic for CoordinatorWindow.xaml
    /// </summary>
    public partial class CoordinatorWindow : Window
    {
        private List<ClaimItem> allClaims;
        public CoordinatorWindow()
        {
            InitializeComponent();
            LoadDummyClaims();
            UpdateDashboard();
        }

        // 🟢 Load some dummy claims for demo
        private void LoadDummyClaims()
        {
            allClaims = new List<ClaimItem>
            {
                new ClaimItem { LecturerName = "John Mokoena", ClaimPeriod = "September 2025", HoursWorked = 40, HourlyRate = 150, Notes = "Teaching ITC classes", HasFiles = true },
                new ClaimItem { LecturerName = "Sibongile Ndlovu", ClaimPeriod = "September 2025", HoursWorked = 25, HourlyRate = 180, Notes = "Tutoring Database course", HasFiles = false },
                new ClaimItem { LecturerName = "Thabo Dlamini", ClaimPeriod = "August 2025", HoursWorked = 35, HourlyRate = 160, Notes = "Assisting with moderation", HasFiles = true },
                new ClaimItem { LecturerName = "Nomsa Khumalo", ClaimPeriod = "August 2025", HoursWorked = 20, HourlyRate = 200, Notes = "Lab sessions", HasFiles = false },
            };

            // Calculate totals and bind to list
            foreach (var c in allClaims)
            {
                c.TotalAmount = c.HoursWorked * c.HourlyRate;
                c.HasFilesVisibility = c.HasFiles ? Visibility.Visible : Visibility.Collapsed;
                c.NoFilesVisibility = c.HasFiles ? Visibility.Collapsed : Visibility.Visible;
            }

            lstPendingClaims.ItemsSource = allClaims;
            pnlNoPendingClaims.Visibility = allClaims.Any() ? Visibility.Collapsed : Visibility.Visible;
        }

        // 🟢 Update dashboard counters
        private void UpdateDashboard()
        {
            int pending = allClaims.Count;
            int approved = allClaims.Count(c => c.Status == "Approved");

            txtPendingClaims.Text = pending.ToString();
            txtApprovedThisMonth.Text = approved.ToString();
            txtMonthlyClaims.Text = allClaims.Count(c => c.ClaimPeriod.Contains(DateTime.Now.ToString("MMMM"))).ToString();
            txtPendingAmount.Text = "R " + allClaims.Sum(c => c.TotalAmount).ToString("N2");
        }

        // 🟢 Refresh button
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDummyClaims();
            UpdateDashboard();
        }

        // 🟢 Export button (simulated)
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Claims exported successfully to Excel!", "Export Complete", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // 🟢 Approve claim
        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is ClaimItem claim)
            {
                claim.Status = "Approved";
                allClaims.Remove(claim);
                RefreshUI();
                MessageBox.Show($"Claim for {claim.LecturerName} has been approved.", "Claim Approved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // 🟢 Reject claim
        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is ClaimItem claim)
            {
                claim.Status = "Rejected";
                allClaims.Remove(claim);
                RefreshUI();
                MessageBox.Show($"Claim for {claim.LecturerName} has been rejected.", "Claim Rejected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // 🟢 View Details
        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is ClaimItem claim)
            {
                MessageBox.Show(
                    $"Lecturer: {claim.LecturerName}\nPeriod: {claim.ClaimPeriod}\nHours: {claim.HoursWorked}\nRate: R{claim.HourlyRate}\nTotal: R{claim.TotalAmount}\nNotes: {claim.Notes}",
                    "Claim Details",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        // 🟢 View attached files
        private void btnViewFiles_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is ClaimItem claim)
            {
                if (claim.HasFiles)
                    MessageBox.Show($"Opening attached files for {claim.LecturerName}...", "View Files", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("No files attached for this claim.", "No Files", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // 🟢 Back button
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        // 🟢 Refresh UI after approve/reject
        private void RefreshUI()
        {
            lstPendingClaims.ItemsSource = null;
            lstPendingClaims.ItemsSource = allClaims;
            pnlNoPendingClaims.Visibility = allClaims.Any() ? Visibility.Collapsed : Visibility.Visible;
            UpdateDashboard();
        }
    }

    // ✅ ClaimItem class
    public class ClaimItem
    {
        public string LecturerName { get; set; }
        public string ClaimPeriod { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public double TotalAmount { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; } = "Pending";
        public bool HasFiles { get; set; }

        // For conditional visibility in UI
        public Visibility HasFilesVisibility { get; set; }
        public Visibility NoFilesVisibility { get; set; }

    }
}
