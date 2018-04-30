using System;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using RosterApp.Models;
using System.Collections.Generic;


namespace RosterApp.Views
{
    public partial class CalculateDialog : MetroWindow
    {
        Staff Selected;
        public CalculateDialog()
        {
            InitializeComponent();
        }

        private async void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                try
                {
                    Selected = ListManager.StaffList.Find(x => x.Id == int.Parse(txtId.Text));
                    txtName.Text = Selected.Name;
                }
                catch
                {
                    await this.ShowMessageAsync("", "No staff found with ID provided.");
                }
            }
        }

        private async void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Staff ID.");
            }
            else if (txtName.Text == "")
            {
                await this.ShowMessageAsync("", "Please find a valid Staff via ID.");
            }
            else if (dateStart.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Start Date.");
            }
            else if (dateEnd.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter an End Date.");
            }
            else if (DateTime.Parse(dateEnd.Text).CompareTo(DateTime.Parse(dateStart.Text)) < 0)
            {
                await this.ShowMessageAsync("", "Please enter a valid End Date.");
            }
            else
            {
                double weekStart = ListManager.GetWeek(DateTime.Parse(dateStart.Text));
                double weekEnd = ListManager.GetWeek(DateTime.Parse(dateEnd.Text));
                double contractCount = 0.0;
                double appointedCount = 0.0;
                double absenceCount = 0.0;

                List<Staff> SelectedRange = new List<Staff>();
                for(double i = weekStart; i < weekEnd; i++)
                {
                    Staff temp = ListManager.GetStaffRoster(i, Selected.Id);
                    if (temp != null)
                    {
                        SelectedRange.Add(temp);
                    }
                }

                foreach(Staff s in SelectedRange)
                {
                    contractCount += s.ContractHours;
                    appointedCount += s.AppointedHours;
                    absenceCount += s.AbsenceHours;
                }
                txtWeekCount.Text = SelectedRange.Count.ToString();
                txtContractCount.Text = contractCount.ToString();
                txtAppointedCount.Text = appointedCount.ToString();
                txtAbsenceCount.Text = absenceCount.ToString();
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
            DialogResult = false;
        }
    }
}
