using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using RosterApp.Models;
using MahApps.Metro.Controls.Dialogs;

namespace RosterApp.Views
{
    public partial class ArchiveStaffWindow : MetroWindow
    {
        public ArchiveStaffWindow()
        {
            InitializeComponent();
            ListManager.WeekList = ListManager.GetRosterWeeks();
            foreach (double week in ListManager.WeekList)
            {
                lstWeeks.Items.Add(week);
            }
        }

        private void lstWeeks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstStaff.Items.Clear();
            ListManager.RosterList = ListManager.GetRoster((double)lstWeeks.SelectedValue);
            foreach (Staff s in ListManager.RosterList)
            {
                lstStaff.Items.Add(s);
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstStaff.SelectedIndex > -1)
            {
                MessageDialogResult choice = await this.ShowMessageAsync("",
                    "Are you sure you want to delete this Archive?",
                    MessageDialogStyle.AffirmativeAndNegative);
                if (choice == MessageDialogResult.Affirmative)
                {
                    Staff Selected = (Staff)lstStaff.SelectedItem;
                    ListManager.DeleteRoster(Selected.Id, (double)lstWeeks.SelectedValue);
                    lstStaff.Items.Remove(Selected);
                }
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            CalculateDialog calculateDialog = new CalculateDialog();
            calculateDialog.Owner = this;
            calculateDialog.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
