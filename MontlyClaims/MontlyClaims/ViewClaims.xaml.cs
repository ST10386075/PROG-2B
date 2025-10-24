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
    /// Interaction logic for ViewClaims.xaml
    /// </summary>
    public partial class ViewClaims : Window
    {
        private List<Claim> allClaims;
        public ViewClaims()
        {
            InitializeComponent();
            LoadDummyData();
            PopulateYearFilter();
        }

        // Load dummy data to simulate real claims
        private void LoadDummyData()
        {
            allClaims = new List<Claim>
            {
                new Claim { Month = "January", Status = "Approved", Amount = 3500, Notes = "Transport reimbursement" },
                new Claim { Month = "February", Status = "Pending", Amount = 2800, Notes = "Stationery" },
                new Claim { Month = "March", Status = "Rejected", Amount = 2200, Notes = "Late submission" },
                new Claim { Month = "April", Status = "Paid", Amount = 3600, Notes = "Teaching hours" },
                new Claim { Month = "May", Status = "Submitted", Amount = 3900, Notes = "Lab supervision" },
                new Claim { Month = "June", Status = "Pending", Amount = 3100, Notes = "Workshop" }
            };

            lstClaims.ItemsSource = allClaims;
        }

        // Populate the year filter (latest 3 years)
        private void PopulateYearFilter()
        {
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 3; i++)
            {
                cmbYearFilter.Items.Add((currentYear - i).ToString());
            }
        }

        // Apply filters every time a filter changes
        private void cmbStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cmbYearFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            cmbStatusFilter.SelectedIndex = 0;
            cmbYearFilter.SelectedIndex = 0;
            lstClaims.ItemsSource = allClaims;
        }

        // Logic for filtering and searching
        private void ApplyFilters()
        {
            var filtered = allClaims.AsEnumerable();

            // ✅ Filter by Status
            string status = (cmbStatusFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (status != "All Status")
                filtered = filtered.Where(c => c.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            // ✅ Filter by Year (currently no year in dummy data — demo only)
            string year = (cmbYearFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (year != "All Years" && !string.IsNullOrEmpty(year))
            {
                // You could add a Year property to Claim in a real app
            }

            // ✅ Search box
            string searchText = txtSearch.Text?.ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(c =>
                    c.Month.ToLower().Contains(searchText) ||
                    c.Notes.ToLower().Contains(searchText));
            }

            lstClaims.ItemsSource = filtered.ToList();
        }
    }

    // Simple data model
    public class Claim
    {
        public string Month { get; set; }
        public string Status { get; set; }
        public double Amount { get; set; }
        public string Notes { get; set; }


    }
}
