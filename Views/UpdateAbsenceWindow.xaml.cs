using System;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using RosterApp.Models;
using System.Text.RegularExpressions;

namespace RosterApp.Views
{
    public partial class UpdateAbsenceWindow : MetroWindow
    {
        Absence Selected;
        public UpdateAbsenceWindow(Absence a)
        {
            InitializeComponent();
            this.Selected = ListManager.AbsenceList.Find(x => x.StaffId == a.StaffId && x.StartDate == a.StartDate);
            txtId.Text = Selected.StaffId.ToString();
            txtName.Text = Selected.StaffName;
            txtHours.Text = Selected.Length.ToString();
            cboType.Items.Add("Day Off");
            cboType.Items.Add("Annual Leave");
            cboType.Items.Add("Sick Leave");
            cboType.Items.Add("Special Leave");
            cboType.Items.Add("Training");
            cboType.SelectedItem = Selected.Type;
            txtStart.Text = Selected.StartDate;
            txtEnd.Text = Selected.EndDate;
        }

        private async void btnUpdateAbsence_Click(object sender, RoutedEventArgs e)
        {
            if (txtHours.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a valid End Date.");
            }
            else
            {
                Selected.Type = cboType.Text;
                double old = Selected.Length;
                Selected.Length = double.Parse(txtHours.Text);
                ListManager.UpdateAbsence(Selected);
                double absence = Selected.Length - old;
                ListManager.UpdateRoster(Selected.StaffId, 0.0, absence, ListManager.GetWeek(DateTime.Parse(txtStart.Text)));
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        //Method to force only numbers in textbox input
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Use this in the xaml file to only allow numbers in input
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
