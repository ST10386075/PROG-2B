using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    /// Interaction logic for SubmitMonthlyClaims.xaml
    /// </summary>
    public partial class SubmitMonthlyClaims : Window
    {
        public SubmitMonthlyClaims()
        {
            InitializeComponent();
            LoadYears();
            AttachEventHandlers();
        }

        // Load years dynamically (from current year going 10 years back)
        private void LoadYears()
        {
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 10; i++)
            {
                cmbYear.Items.Add((currentYear - i).ToString());
            }
            cmbYear.SelectedIndex = 0;
        }

        // Attach handlers to update calculations automatically
        private void AttachEventHandlers()
        {
            txtHoursWorked.TextChanged += CalculateTotal;
            txtHourlyRate.TextChanged += CalculateTotal;
            txtNotes.TextChanged += TxtNotes_TextChanged;
        }

        // Validate integer input (for hours worked)
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        // Validate decimal input (for hourly rate)
        private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]*\\.?[0-9]*$");
        }

        // Automatically calculate total = hours * rate
        private void CalculateTotal(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(txtHoursWorked.Text, out double hours) &&
                double.TryParse(txtHourlyRate.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double rate))
            {
                double total = hours * rate;
                txtTotalAmount.Text = $"R{total:N2}";
            }
            else
            {
                txtTotalAmount.Text = "R0.00";
            }
        }

        // Character count for notes box
        private void TxtNotes_TextChanged(object sender, TextChangedEventArgs e)
        {
            int count = txtNotes.Text.Length;
            txtCharacterCount.Text = $"{count}/500 characters";
        }

        // Upload document button
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Supported files (*.pdf;*.docx;*.xlsx)|*.pdf;*.docx;*.xlsx",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                lstUploadedFiles.Items.Clear();
                foreach (string file in openFileDialog.FileNames)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.Length > 10 * 1024 * 1024)
                    {
                        MessageBox.Show($"File '{fi.Name}' exceeds 10MB and was skipped.", "File Too Large", MessageBoxButton.OK, MessageBoxImage.Warning);
                        continue;
                    }

                    lstUploadedFiles.Items.Add(fi.Name);
                }

                txtNoFiles.Visibility = lstUploadedFiles.Items.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        // Cancel button closes window
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel? Unsaved data will be lost.",
                                         "Cancel Confirmation",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        // Submit button validation + confirmation
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMonth.SelectedItem == null)
            {
                MessageBox.Show("Please select a claim month.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cmbYear.SelectedItem == null)
            {
                MessageBox.Show("Please select a claim year.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(txtHoursWorked.Text, out double hours) || hours <= 0)
            {
                MessageBox.Show("Please enter a valid number of hours worked.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(txtHourlyRate.Text, out double rate) || rate <= 0)
            {
                MessageBox.Show("Please enter a valid hourly rate.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string month = (cmbMonth.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "";
            string year = cmbYear.SelectedItem.ToString();
            string total = txtTotalAmount.Text;

            MessageBox.Show($"Claim submitted successfully!\n\nMonth: {month}\nYear: {year}\nTotal: {total}",
                            "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Optionally clear fields or close window
            //this.Close();
        }


    }
}
