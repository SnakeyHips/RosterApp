using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using RosterApp.Models;

namespace RosterApp.Views
{
    public partial class AddSessionWindow : MetroWindow
    {
        public AddSessionWindow()
        {
            InitializeComponent();
            cboStartTime.Items.Add("08:00");
            cboStartTime.Items.Add("09:00");
            cboStartTime.Items.Add("10:00");
            cboStartTime.Items.Add("11:00");
            cboStartTime.Items.Add("12:00");
            cboEndTime.Items.Add("14:00");
            cboEndTime.Items.Add("15:00");
            cboEndTime.Items.Add("16:00");
            cboEndTime.Items.Add("17:00");
            cboEndTime.Items.Add("18:00");
            cboMDC.Items.Add("No");
            cboMDC.Items.Add("Yes");
            PopulateChairs(cboMDC.Text);
        }

        public void PopulateChairs(string MDC)
        {
            cboChairs.Items.Clear();
            if(MDC.Equals("Yes"))
            {
                cboChairs.Items.Add("3");
                cboChairs.Items.Add("6");
            }
            else
            {
                cboChairs.Items.Add("4");
                cboChairs.Items.Add("6");
                cboChairs.Items.Add("8");
                cboChairs.Items.Add("9");
                cboChairs.Items.Add("10");
            }
        }

        private void cboMDC_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(cboMDC.SelectedIndex == 0)
            {
                PopulateChairs("No");
            }
            else
            {
                PopulateChairs("Yes");
            }
        }

        private async void btnAddSession_Click(object sender, RoutedEventArgs e)
        {
            //Makes sure each input has an input before being made. Possibly better way to do this but this works and is simple to update
            if (dateSession.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Date.");
            }
            else if (DateTime.Parse(dateSession.Text).CompareTo(DateTime.Now) < 0)
            {
                await this.ShowMessageAsync("", "Please enter a valid Date.");
            }
            else if (cboStartTime.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Start Time.");
            }
            else if (cboEndTime.Text == "")
            {
                await this.ShowMessageAsync("", "Please select an End Time.");
            }
            else if (txtLocation.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Location.");
            }
            else if (cboMDC.Text == "")
            {
                await this.ShowMessageAsync("", "Please select if session is MDC or not.");
            }
            else if (cboChairs.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Chair amount.");
            }
            else
            {
                int chairs = int.Parse(cboChairs.Text);
                Session temp = new Session(
                        dateSession.Text,
                        cboStartTime.Text,
                        cboEndTime.Text,
                        ListManager.GetLength(cboStartTime.Text, cboEndTime.Text),
                        txtLocation.Text,
                        cboMDC.Text == "Yes" ? "Yes" : "No",
                        chairs,
                        0, "", "",
                        0, "", "",
                        0, "", "",
                        0, "", "",
                        0, "", "",
                        0, "", "",
                        0, "", "",
                        0, "", "",
                        0, "", "",
                        "Incomplete");
                ListManager.AddSession(temp);
                ListManager.SelectedDate = DateTime.Parse(dateSession.Text);
                ListManager.SessionList.Add(temp);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
