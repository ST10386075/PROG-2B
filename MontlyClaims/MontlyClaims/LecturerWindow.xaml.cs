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
    /// Interaction logic for LecturerWindow.xaml
    /// </summary>
    public partial class LecturerWindow : Window
    {
        public LecturerWindow()
        {
            InitializeComponent();
        }

        // 🔙 Back Button
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Example: Navigate back to a Login or Main Menu window
            MessageBox.Show("Returning to main menu...", "Back", MessageBoxButton.OK, MessageBoxImage.Information);

            // You can uncomment this if you have another window to go back to:
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        // ✏️ Edit Personal Information
        private void btnEditInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit Personal Information feature coming soon!", "Edit Info", MessageBoxButton.OK, MessageBoxImage.Information);

            // Example logic: Open Edit Info Window (if created)
            EditPersonalInfo editWindow = new EditPersonalInfo();
            editWindow.ShowDialog();
        }

        // 📤 Submit Monthly Claims
        private void btnSubmitClaims_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Opening Monthly Claims submission form...", "Submit Claims", MessageBoxButton.OK, MessageBoxImage.Information);

            // Example navigation (if you have another page/window)
            SubmitMonthlyClaims claimsWindow = new SubmitMonthlyClaims();
            claimsWindow.Show();
            this.Close();
        }

        // 👀 View Previous Claims
        private void btnViewClaims_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Optional: Show a short message before opening
                MessageBox.Show("Opening your previous claims...", "View Claims", MessageBoxButton.OK, MessageBoxImage.Information);

                // Open the ViewClaims window
                ViewClaims viewWindow = new ViewClaims();
                viewWindow.Show();

                // Option 1: Keep this window open (recommended)
                // Option 2: Close current window after opening new one
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening View Claims window:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    }
