using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Text.RegularExpressions;
using RosterApp.Models;

namespace RosterApp.Views
{
    public partial class AddStaffWindow : MetroWindow
    {
        public AddStaffWindow()
        {
            InitializeComponent();
            cboRole.Items.Add("SV");
            cboRole.Items.Add("DRI");
            cboRole.Items.Add("RN");
            cboRole.Items.Add("CCA");
            cboHours.Items.Add("20");
            cboHours.Items.Add("25");
            cboHours.Items.Add("30");
            cboHours.Items.Add("37.5");
        }

        private async void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            //Makes sure each input has an input before being made
            if (txtId.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter an Id.");
            }
            else if (ListManager.StaffList.Find(x => x.Id == int.Parse(txtId.Text)) != null)
            {
                await this.ShowMessageAsync("", "Staff Id already in use.");
            }
            else if (txtFirstName.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a First Name.");
            }
            else if (txtLastName.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Last Name.");
            }
            else if (cboRole.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Role.");
            }
            else if (cboHours.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter Contract Hours.");
            }
            else
            {
                Staff temp = new Staff(
                    int.Parse(txtId.Text),
                    txtFirstName.Text + " " + txtLastName.Text,
                    cboRole.Text,
                    double.Parse(cboHours.Text),
                    GetWorkPattern());
                ListManager.AddStaff(temp);
                ListManager.StaffList.Add(temp);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }

        }

        private string GetWorkPattern()
        {
            StringBuilder workpattern = new StringBuilder();
            grdAddStaff.FindChildren<CheckBox>().ToList().ForEach(x =>
            {
                if (x.IsChecked == true)
                {
                    workpattern.Append(x.Content);
                    workpattern.Append(",");
                }
            });
            if(workpattern.Length > 0)
            {
                workpattern.Remove(workpattern.Length - 1, 1);
            }
            return workpattern.ToString();
        }

        //Method to force only numbers in textbox input
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Use this in the xaml file to only allow numbers in input
            Regex regex = new Regex("[^0-9]+");
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
