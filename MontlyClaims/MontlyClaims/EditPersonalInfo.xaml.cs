using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EditPersonalInfo.xaml
    /// </summary>
    public partial class EditPersonalInfo : Window
    {
        public EditPersonalInfo()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Confirm before closing the window
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to cancel? Unsaved changes will be lost.",
                "Cancel Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        // Event handler for Save Changes button
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validate all mandatory fields
            if (!ValidateInputs())
                return;

            // Collect updated information
            string fullName = txtFullName.Text.Trim();
            string lecturerID = txtLecturerID.Text.Trim();
            string department = (cmbDepartment.SelectedItem as ComboBoxItem)?.Content.ToString();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string office = txtOffice.Text.Trim();
            string bankName = txtBankName.Text.Trim();
            string accountNumber = txtAccountNumber.Text.Trim();
            string accountHolder = txtAccountHolder.Text.Trim();

            // Here you could save data to a database or file
            // Example: Database.SaveLecturerInfo(...);

            // For demonstration:
            MessageBox.Show(
                $"Personal information for {fullName} has been successfully updated.\n\n" +
                $"Department: {department}\nEmail: {email}\nPhone: {phone}\n" +
                $"Bank: {bankName} ({accountNumber})",
                "Information Saved",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            this.Close();
        }

        // Validation method for input fields
        private bool ValidateInputs()
        {
            // Validate Full Name
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                ShowError("Full Name is required.");
                txtFullName.Focus();
                return false;
            }

            // Validate Department
            if (cmbDepartment.SelectedItem == null)
            {
                ShowError("Please select a department.");
                cmbDepartment.Focus();
                return false;
            }

            // Validate Email format
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !IsValidEmail(txtEmail.Text))
            {
                ShowError("Please enter a valid email address.");
                txtEmail.Focus();
                return false;
            }

            // Validate Phone
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                ShowError("Phone number is required.");
                txtPhone.Focus();
                return false;
            }

            // Validate Bank Info
            if (string.IsNullOrWhiteSpace(txtBankName.Text))
            {
                ShowError("Bank Name is required.");
                txtBankName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text))
            {
                ShowError("Account Number is required.");
                txtAccountNumber.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAccountHolder.Text))
            {
                ShowError("Account Holder Name is required.");
                txtAccountHolder.Focus();
                return false;
            }

            return true;
        }

        // Helper method to validate email using regex
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        // Display error messages
        private void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "Validation Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
    }
}
