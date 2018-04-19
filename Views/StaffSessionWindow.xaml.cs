using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using RosterApp.Models;

namespace RosterApp.Views
{
    public partial class StaffSessionWindow : MetroWindow
    {
        Session Selected;
        public List<Staff> SVList { get; set; }
        public List<Staff> DSVList { get; set; }
        public List<Staff> DRIList { get; set; }
        public List<Staff> RNList { get; set; }
        public List<Staff> CCAList { get; set; }
        public List<string> TimesList { get; set; }
        double Week = ListManager.GetWeek(ListManager.SelectedDate);

        public StaffSessionWindow(Session s)
        {
            InitializeComponent();
            DataContext = this;
            //Put all relevant roles in respective list
            SVList = ListManager.AvailableStaffList.Where(x => x.Role == "SV").ToList();
            DSVList = ListManager.AvailableStaffList.Where(x => x.Role == "DSV").ToList();
            DRIList = ListManager.AvailableStaffList.Where(x => x.Role == "DRI").ToList();
            RNList = ListManager.AvailableStaffList.Where(x => x.Role == "RN").ToList();
            CCAList = ListManager.AvailableStaffList.Where(x => x.Role == "CCA").ToList();

            this.Selected = ListManager.SessionList.Find(x => x.Date == s.Date && x.Location == s.Location && x.StartTime == s.StartTime);

            int start = int.Parse(Selected.StartTime.Substring(0, 2));
            int end = int.Parse(Selected.EndTime.Substring(0, 2));
            TimesList = new List<string>();
            for (int i = start - 1; i < end + 1; i++)
            {
                TimesList.Add(i + ":00");
                TimesList.Add(i + ":15");
                TimesList.Add(i + ":30");
                TimesList.Add(i + ":45");
            }

            //Enable amount of positions needed based on number of chairs
            if (Selected.Chairs > 5)
            {
                if (Selected.MDC != "Yes")
                {
                    cboDRI2.IsEnabled = true;
                    //cboDRI2.BorderBrush = (SolidColorBrush)Application.Current.Resources["AccentColorBrush"]; - if want to change borderbrush
                    cboRN2.IsEnabled = true;
                    cboCCA2.IsEnabled = true;
                }
                if (Selected.Chairs > 8)
                {
                    cboRN3.IsEnabled = true;
                    cboCCA3.IsEnabled = true;
                }
            }

            //Set UI to match selected
            txtDate.Text = Selected.Date;
            txtLocation.Text = Selected.Location;
            txtStartTime.Text = Selected.StartTime;
            txtEndTime.Text = Selected.EndTime;
            txtMDC.Text = Selected.MDC;
            txtChairs.Text = Selected.Chairs.ToString();

            //Autopopulate cbos from StaffList
            if(Selected.SV1Id != 0)
            {
                cboSV1.SelectedValue = Selected.SV1Id;
                cboSV1Start.SelectedItem = Selected.SV1Start;
                cboSV1End.SelectedItem = Selected.SV1End;
            }
            if (Selected.DRI1Id != 0)
            {
                cboDRI1.SelectedValue = Selected.DRI1Id;
                cboDRI1Start.SelectedItem = Selected.DRI1Start;
                cboDRI1End.SelectedItem = Selected.DRI1End;
            }
            if (Selected.DRI2Id != 0)
            {
                cboDRI2.SelectedValue = Selected.DRI2Id;
                cboDRI2Start.SelectedItem = Selected.DRI2Start;
                cboDRI2End.SelectedItem = Selected.DRI2End;
            }
            if (Selected.RN1Id != 0)
            {
                cboRN1.SelectedValue = Selected.RN1Id;
                cboRN1Start.SelectedItem = Selected.RN1Start;
                cboRN1End.SelectedItem = Selected.RN1End;
            }
            if (Selected.RN2Id != 0)
            {
                cboRN2.SelectedValue = Selected.RN2Id;
                cboRN2Start.SelectedItem = Selected.RN2Start;
                cboRN2End.SelectedItem = Selected.RN2End;
            }
            if (Selected.RN3Id != 0)
            {
                cboRN3.SelectedValue = Selected.RN3Id;
                cboRN3Start.SelectedItem = Selected.RN3Start;
                cboRN3End.SelectedItem = Selected.RN3End;
            }
            if (Selected.CCA1Id != 0)
            {
                cboCCA1.SelectedValue = Selected.CCA1Id;
                cboCCA1Start.SelectedItem = Selected.CCA1Start;
                cboCCA1End.SelectedItem = Selected.CCA1End;
            }
            if (Selected.CCA2Id != 0)
            {
                cboCCA2.SelectedValue = Selected.CCA2Id;
                cboCCA2Start.SelectedItem = Selected.CCA2Start;
                cboCCA2End.SelectedItem = Selected.CCA2End;
            }
            if (Selected.CCA3Id != 0)
            {
                cboCCA3.SelectedValue = Selected.CCA3Id;
                cboCCA3Start.SelectedItem = Selected.CCA3Start;
                cboCCA3End.SelectedItem = Selected.CCA3End;
            }
        }

        //Listeners to enable corresponding time cbos
        private void cboSV1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboSV1Start.IsEnabled = true;
            cboSV1End.IsEnabled = true;
        }

        private void cboDRI1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboDRI1Start.IsEnabled = true;
            cboDRI1End.IsEnabled = true;
        }

        private void cboDRI2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboDRI2Start.IsEnabled = true;
            cboDRI2End.IsEnabled = true;
        }

        private void cboRN1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboRN1Start.IsEnabled = true;
            cboRN1End.IsEnabled = true;
        }

        private void cboRN2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboRN2Start.IsEnabled = true;
            cboRN2End.IsEnabled = true;
        }

        private void cboRN3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboRN3Start.IsEnabled = true;
            cboRN3End.IsEnabled = true;
        }

        private void cboCCA1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboCCA1Start.IsEnabled = true;
            cboCCA1End.IsEnabled = true;
        }

        private void cboCCA2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboCCA2Start.IsEnabled = true;
            cboCCA2End.IsEnabled = true;
        }

        private void cboCCA3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboCCA3Start.IsEnabled = true;
            cboCCA3End.IsEnabled = true;
        }

        //Update Staff info - lots of if statements as properties can't be passed through methods
        //First one is commented as rest are same
        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //First check if there is a selected item
            if (cboSV1.SelectedItem != null)
            {
                //Check if different staff selected from before
                if (Selected.SV1Id == (int)cboSV1.SelectedValue)
                {
                    //Check if different times selected from before
                    if (Selected.SV1Start != cboSV1Start.Text || Selected.SV1End != cboSV1End.Text)
                    {
                        //If so, update times and appointed hours to use times selected
                        double appointed = ListManager.GetLength(cboSV1Start.Text, cboSV1End.Text)
                            - ListManager.GetLength(Selected.SV1Start, Selected.SV1End);
                        ListManager.UpdateRoster(Selected.SV1Id, appointed, 0.0, Week);
                        Selected.SV1Start = cboSV1Start.Text;
                        Selected.SV1End = cboSV1End.Text;
                    }
                }
                //Check if no previous staff selected
                else if (Selected.SV1Id != (int)cboSV1.SelectedValue)
                {
                    //Update old staff's appointed hours to remove length
                    ListManager.UpdateRoster(Selected.SV1Id, -ListManager.GetLength(Selected.SV1Start, Selected.SV1End), 0.0, Week);

                    //Assign new appoint staff
                    Selected.SV1Id = (int)cboSV1.SelectedValue;
                    Selected.SV1Start = cboSV1Start.Text;
                    Selected.SV1End = cboSV1End.Text;
                    //Add onto new staff's appointed hours/New record created in sql method
                    ListManager.UpdateRoster(Selected.SV1Id, ListManager.GetLength(Selected.SV1Start, Selected.SV1End), 0.0, Week);
                }
                else
                {
                    //Assign new appoint staff
                    Selected.SV1Id = (int)cboSV1.SelectedValue;
                    Selected.SV1Start = cboSV1Start.Text;
                    Selected.SV1End = cboSV1End.Text;
                    //Add onto new staff's appointed hours/New record created in sql method
                    ListManager.UpdateRoster(Selected.SV1Id, ListManager.GetLength(Selected.SV1Start, Selected.SV1End), 0.0, Week);
                }
            }
            //Else remove if reset staff has been pressed
            else if (cboSV1.SelectedItem == null && Selected.SV1Id != 0)
            {
                //Remove staff's appointed hours
                ListManager.UpdateRoster(Selected.SV1Id, -ListManager.GetLength(Selected.SV1Start, Selected.SV1End), 0.0, Week);
                //Remove any saved staff info
                Selected.SV1Id = 0;
                Selected.SV1Start = "";
                Selected.SV1End = "";
            }

            //Get DRI selected info
            if (cboDRI1.SelectedItem != null)
            {
                if (Selected.DRI1Id == (int)cboDRI1.SelectedValue)
                {
                    if (Selected.DRI1Start != cboDRI1Start.Text || Selected.DRI1End != cboDRI1End.Text)
                    {
                        double appointed = ListManager.GetLength(cboDRI1Start.Text, cboDRI1End.Text)
                            - ListManager.GetLength(Selected.DRI1Start, Selected.DRI1End);
                        ListManager.UpdateRoster(Selected.DRI1Id, appointed, 0.0, Week);
                        Selected.DRI1Start = cboDRI1Start.Text;
                        Selected.DRI1End = cboDRI1End.Text;
                    }
                }
                else if (Selected.DRI1Id != (int)cboDRI1.SelectedValue)
                {
                    ListManager.UpdateRoster(Selected.DRI1Id, -ListManager.GetLength(Selected.DRI1Start, Selected.DRI1End), 0.0, Week);
                    Selected.DRI1Id = (int)cboDRI1.SelectedValue;
                    Selected.DRI1Start = cboDRI1Start.Text;
                    Selected.DRI1End = cboDRI1End.Text;
                    ListManager.UpdateRoster(Selected.DRI1Id, ListManager.GetLength(Selected.DRI1Start, Selected.DRI1End), 0.0, Week);
                }
                else
                {
                    Selected.DRI1Id = (int)cboDRI1.SelectedValue;
                    Selected.DRI1Start = cboDRI1Start.Text;
                    Selected.DRI1End = cboDRI1End.Text;
                    ListManager.UpdateRoster(Selected.DRI1Id, ListManager.GetLength(Selected.DRI1Start, Selected.DRI1End), 0.0, Week);
                }
            }
            else if (cboDRI1.SelectedItem == null && Selected.DRI1Id != 0)
            {
                ListManager.UpdateRoster(Selected.DRI1Id, -ListManager.GetLength(Selected.DRI1Start, Selected.DRI1End), 0.0, Week);
                Selected.DRI1Id = 0;
                Selected.DRI1Start = "";
                Selected.DRI1End = "";
            }

            if (cboDRI2.SelectedItem != null)
            {
                if(cboDRI2.Text != cboDRI1.Text)
                {
                    if (cboDRI2.SelectedItem != null)
                    {
                        if (Selected.DRI2Id == (int)cboDRI2.SelectedValue)
                        {
                            if (Selected.DRI2Start != cboDRI2Start.Text || Selected.DRI2End != cboDRI2End.Text)
                            {
                                double appointed = ListManager.GetLength(cboDRI2Start.Text, cboDRI2End.Text)
                                    - ListManager.GetLength(Selected.DRI2Start, Selected.DRI2End);
                                ListManager.UpdateRoster(Selected.DRI2Id, appointed, 0.0, Week);
                                Selected.DRI2Start = cboDRI2Start.Text;
                                Selected.DRI2End = cboDRI2End.Text;
                            }
                        }
                        else if (Selected.DRI2Id != (int)cboDRI2.SelectedValue)
                        {
                            ListManager.UpdateRoster(Selected.DRI2Id, -ListManager.GetLength(Selected.DRI2Start, Selected.DRI2End), 0.0, Week);
                            Selected.DRI2Id = (int)cboDRI2.SelectedValue;
                            Selected.DRI2Start = cboDRI2Start.Text;
                            Selected.DRI2End = cboDRI2End.Text;
                            ListManager.UpdateRoster(Selected.DRI2Id, ListManager.GetLength(Selected.DRI2Start, Selected.DRI2End), 0.0, Week);
                        }
                        else
                        {
                            Selected.DRI2Id = (int)cboDRI2.SelectedValue;
                            Selected.DRI2Start = cboDRI2Start.Text;
                            Selected.DRI2End = cboDRI2End.Text;
                            ListManager.UpdateRoster(Selected.DRI2Id, ListManager.GetLength(Selected.DRI2Start, Selected.DRI2End), 0.0, Week);
                        }
                    }
                }
                else
                {
                    await this.ShowMessageAsync("", "Driver 2 duplicate selected.");
                    return;
                }
            }
            else if (cboDRI2.SelectedItem == null && Selected.DRI2Id != 0)
            {
                ListManager.UpdateRoster(Selected.DRI2Id, -ListManager.GetLength(Selected.DRI2Start, Selected.DRI2End), 0.0, Week);
                Selected.DRI2Id = 0;
                Selected.DRI2Start = "";
                Selected.DRI2End = "";
            }

            //Get RN selected info
            if (cboRN1.SelectedItem != null)
            {
                if (Selected.RN1Id == (int)cboRN1.SelectedValue)
                {
                    if (Selected.RN1Start != cboRN1Start.Text || Selected.RN1End != cboRN1End.Text)
                    {
                        double appointed = ListManager.GetLength(cboRN1Start.Text, cboRN1End.Text)
                            - ListManager.GetLength(Selected.RN1Start, Selected.RN1End);
                        ListManager.UpdateRoster(Selected.RN1Id, appointed, 0.0, Week);
                        Selected.RN1Start = cboRN1Start.Text;
                        Selected.RN1End = cboRN1End.Text;
                    }
                }
                else if (Selected.RN1Id != (int)cboRN1.SelectedValue)
                {
                    ListManager.UpdateRoster(Selected.RN1Id, -ListManager.GetLength(Selected.RN1Start, Selected.RN1End), 0.0, Week);
                    Selected.RN1Id = (int)cboRN1.SelectedValue;
                    Selected.RN1Start = cboRN1Start.Text;
                    Selected.RN1End = cboRN1End.Text;
                    ListManager.UpdateRoster(Selected.RN1Id, ListManager.GetLength(Selected.RN1Start, Selected.RN1End), 0.0, Week);
                }
                else
                {
                    Selected.RN1Id = (int)cboRN1.SelectedValue;
                    Selected.RN1Start = cboRN1Start.Text;
                    Selected.RN1End = cboRN1End.Text;
                    ListManager.UpdateRoster(Selected.RN1Id, ListManager.GetLength(Selected.RN1Start, Selected.RN1End), 0.0, Week);
                }
            }
            else if (cboRN1.SelectedItem == null && Selected.RN1Id != 0)
            {
                ListManager.UpdateRoster(Selected.RN1Id, -ListManager.GetLength(Selected.RN1Start, Selected.RN1End), 0.0, Week);
                Selected.RN1Id = 0;
                Selected.RN1Start = "";
                Selected.RN1End = "";
            }

            if (cboRN2.SelectedItem != null)
            {
                if (cboRN2.Text != cboRN1.Text)
                {
                    if (cboRN2.SelectedItem != null)
                    {
                        if (Selected.RN2Id == (int)cboRN2.SelectedValue)
                        {
                            if (Selected.RN2Start != cboRN2Start.Text || Selected.RN2End != cboRN2End.Text)
                            {
                                double appointed = ListManager.GetLength(cboRN2Start.Text, cboRN2End.Text)
                                    - ListManager.GetLength(Selected.RN2Start, Selected.RN2End);
                                ListManager.UpdateRoster(Selected.RN2Id, appointed, 0.0, Week);
                                Selected.RN2Start = cboRN2Start.Text;
                                Selected.RN2End = cboRN2End.Text;
                            }
                        }
                        else if (Selected.RN2Id != (int)cboRN2.SelectedValue)
                        {
                            ListManager.UpdateRoster(Selected.RN2Id, -ListManager.GetLength(Selected.RN2Start, Selected.RN2End), 0.0, Week);
                            Selected.RN2Id = (int)cboRN2.SelectedValue;
                            Selected.RN2Start = cboRN2Start.Text;
                            Selected.RN2End = cboRN2End.Text;
                            ListManager.UpdateRoster(Selected.RN2Id, ListManager.GetLength(Selected.RN2Start, Selected.RN2End), 0.0, Week);
                        }
                        else
                        {
                            Selected.RN2Id = (int)cboRN2.SelectedValue;
                            Selected.RN2Start = cboRN2Start.Text;
                            Selected.RN2End = cboRN2End.Text;
                            ListManager.UpdateRoster(Selected.RN2Id, ListManager.GetLength(Selected.RN2Start, Selected.RN2End), 0.0, Week);
                        }
                    }
                }
                else
                {
                    await this.ShowMessageAsync("", "RN 2 duplicate selected.");
                    return;
                }
            }
            else if (cboRN2.SelectedItem == null && Selected.RN2Id != 0)
            {
                ListManager.UpdateRoster(Selected.RN2Id, -ListManager.GetLength(Selected.RN2Start, Selected.RN2End), 0.0, Week);
                Selected.RN2Id = 0;
                Selected.RN2Start = "";
                Selected.RN2End = "";
            }

            if (cboRN3.SelectedItem != null)
            {
                if (cboRN3.Text != cboRN1.Text)
                {
                    if (cboRN3.SelectedItem != null)
                    {
                        if (Selected.RN3Id == (int)cboRN3.SelectedValue)
                        {
                            if (Selected.RN3Start != cboRN3Start.Text || Selected.RN3End != cboRN3End.Text)
                            {
                                double appointed = ListManager.GetLength(cboRN3Start.Text, cboRN3End.Text)
                                    - ListManager.GetLength(Selected.RN3Start, Selected.RN3End);
                                ListManager.UpdateRoster(Selected.RN3Id, appointed, 0.0, Week);
                                Selected.RN3Start = cboRN3Start.Text;
                                Selected.RN3End = cboRN3End.Text;
                            }
                        }
                        else if (Selected.RN3Id != (int)cboRN3.SelectedValue)
                        {
                            ListManager.UpdateRoster(Selected.RN3Id, -ListManager.GetLength(Selected.RN3Start, Selected.RN3End), 0.0, Week);
                            Selected.RN3Id = (int)cboRN3.SelectedValue;
                            Selected.RN3Start = cboRN3Start.Text;
                            Selected.RN3End = cboRN3End.Text;
                            ListManager.UpdateRoster(Selected.RN3Id, ListManager.GetLength(Selected.RN3Start, Selected.RN3End), 0.0, Week);
                        }
                        else
                        {
                            Selected.RN3Id = (int)cboRN3.SelectedValue;
                            Selected.RN3Start = cboRN3Start.Text;
                            Selected.RN3End = cboRN3End.Text;
                            ListManager.UpdateRoster(Selected.RN3Id, ListManager.GetLength(Selected.RN3Start, Selected.RN3End), 0.0, Week);
                        }
                    }
                }
                else
                {
                    await this.ShowMessageAsync("", "RN 3 duplicate selected.");
                    return;
                }
            }
            else if (cboRN3.SelectedItem == null && Selected.RN3Id != 0)
            {
                ListManager.UpdateRoster(Selected.RN3Id, -ListManager.GetLength(Selected.RN3Start, Selected.RN3End), 0.0, Week);
                Selected.RN3Id = 0;
                Selected.RN3Start = "";
                Selected.RN3End = "";
            }

            //Get CCA selected info
            if (cboCCA1.SelectedItem != null)
            {
                if (Selected.CCA1Id == (int)cboCCA1.SelectedValue)
                {
                    if (Selected.CCA1Start != cboCCA1Start.Text || Selected.CCA1End != cboCCA1End.Text)
                    {
                        double appointed = ListManager.GetLength(cboCCA1Start.Text, cboCCA1End.Text)
                            - ListManager.GetLength(Selected.CCA1Start, Selected.CCA1End);
                        ListManager.UpdateRoster(Selected.CCA1Id, appointed, 0.0, Week);
                        Selected.CCA1Start = cboCCA1Start.Text;
                        Selected.CCA1End = cboCCA1End.Text;
                    }
                }
                else if (Selected.CCA1Id != (int)cboCCA1.SelectedValue)
                {
                    ListManager.UpdateRoster(Selected.CCA1Id, -ListManager.GetLength(Selected.CCA1Start, Selected.CCA1End), 0.0, Week);
                    Selected.CCA1Id = (int)cboCCA1.SelectedValue;
                    Selected.CCA1Start = cboCCA1Start.Text;
                    Selected.CCA1End = cboCCA1End.Text;
                    ListManager.UpdateRoster(Selected.CCA1Id, ListManager.GetLength(Selected.CCA1Start, Selected.CCA1End), 0.0, Week);
                }
                else
                {
                    Selected.CCA1Id = (int)cboCCA1.SelectedValue;
                    Selected.CCA1Start = cboCCA1Start.Text;
                    Selected.CCA1End = cboCCA1End.Text;
                    ListManager.UpdateRoster(Selected.CCA1Id, ListManager.GetLength(Selected.CCA1Start, Selected.CCA1End), 0.0, Week);
                }
            }
            else if (cboCCA1.SelectedItem == null && Selected.CCA1Id != 0)
            {
                ListManager.UpdateRoster(Selected.CCA1Id, -ListManager.GetLength(Selected.CCA1Start, Selected.CCA1End), 0.0, Week);
                Selected.CCA1Id = 0;
                Selected.CCA1Start = "";
                Selected.CCA1End = "";
            }

            if (cboCCA2.SelectedItem != null)
            {
                if (cboCCA2.Text != cboCCA1.Text)
                {
                    if (cboCCA2.SelectedItem != null)
                    {
                        if (Selected.CCA2Id == (int)cboCCA2.SelectedValue)
                        {
                            if (Selected.CCA2Start != cboCCA2Start.Text || Selected.CCA2End != cboCCA2End.Text)
                            {
                                double appointed = ListManager.GetLength(cboCCA2Start.Text, cboCCA2End.Text)
                                    - ListManager.GetLength(Selected.CCA2Start, Selected.CCA2End);
                                ListManager.UpdateRoster(Selected.CCA2Id, appointed, 0.0, Week);
                                Selected.CCA2Start = cboCCA2Start.Text;
                                Selected.CCA2End = cboCCA2End.Text;
                            }
                        }
                        else if (Selected.CCA2Id != (int)cboCCA2.SelectedValue)
                        {
                            ListManager.UpdateRoster(Selected.CCA2Id, -ListManager.GetLength(Selected.CCA2Start, Selected.CCA2End), 0.0, Week);
                            Selected.CCA2Id = (int)cboCCA2.SelectedValue;
                            Selected.CCA2Start = cboCCA2Start.Text;
                            Selected.CCA2End = cboCCA2End.Text;
                            ListManager.UpdateRoster(Selected.CCA2Id, ListManager.GetLength(Selected.CCA2Start, Selected.CCA2End), 0.0, Week);
                        }
                        else
                        {
                            Selected.CCA2Id = (int)cboCCA2.SelectedValue;
                            Selected.CCA2Start = cboCCA2Start.Text;
                            Selected.CCA2End = cboCCA2End.Text;
                            ListManager.UpdateRoster(Selected.CCA2Id, ListManager.GetLength(Selected.CCA2Start, Selected.CCA2End), 0.0, Week);
                        }
                    }
                }
                else
                {
                    await this.ShowMessageAsync("", "CCA 2 duplicate selected.");
                    return;
                }
            }
            else if (cboCCA2.SelectedItem == null && Selected.CCA2Id != 0)
            {
                ListManager.UpdateRoster(Selected.CCA2Id, -ListManager.GetLength(Selected.CCA2Start, Selected.CCA2End), 0.0, Week);
                Selected.CCA2Id = 0;
                Selected.CCA2Start = "";
                Selected.CCA2End = "";
            }

            if (cboCCA3.SelectedItem != null)
            {
                if (cboCCA3.Text != cboCCA1.Text)
                {
                    if (cboCCA3.SelectedItem != null)
                    {
                        if (Selected.CCA3Id == (int)cboCCA3.SelectedValue)
                        {
                            if (Selected.CCA3Start != cboCCA3Start.Text || Selected.CCA3End != cboCCA3End.Text)
                            {
                                double appointed = ListManager.GetLength(cboCCA3Start.Text, cboCCA3End.Text)
                                    - ListManager.GetLength(Selected.CCA3Start, Selected.CCA3End);
                                ListManager.UpdateRoster(Selected.CCA3Id, appointed, 0.0, Week);
                                Selected.CCA3Start = cboCCA3Start.Text;
                                Selected.CCA3End = cboCCA3End.Text;
                            }
                        }
                        else if (Selected.CCA3Id != (int)cboCCA3.SelectedValue)
                        {
                            ListManager.UpdateRoster(Selected.CCA3Id, -ListManager.GetLength(Selected.CCA3Start, Selected.CCA3End), 0.0, Week);
                            Selected.CCA3Id = (int)cboCCA3.SelectedValue;
                            Selected.CCA3Start = cboCCA3Start.Text;
                            Selected.CCA3End = cboCCA3End.Text;
                            ListManager.UpdateRoster(Selected.CCA3Id, ListManager.GetLength(Selected.CCA3Start, Selected.CCA3End), 0.0, Week);
                        }
                        else
                        {
                            Selected.CCA3Id = (int)cboCCA3.SelectedValue;
                            Selected.CCA3Start = cboCCA3Start.Text;
                            Selected.CCA3End = cboCCA3End.Text;
                            ListManager.UpdateRoster(Selected.CCA3Id, ListManager.GetLength(Selected.CCA3Start, Selected.CCA3End), 0.0, Week);
                        }
                    }
                }
                else
                {
                    await this.ShowMessageAsync("", "CCA 3 duplicate selected.");
                    return;
                }
            }
            else if (cboCCA3.SelectedItem == null && Selected.CCA3Id != 0)
            {
                ListManager.UpdateRoster(Selected.CCA3Id, -ListManager.GetLength(Selected.CCA3Start, Selected.CCA3End), 0.0, Week);
                Selected.CCA3Id = 0;
                Selected.CCA3Start = "";
                Selected.CCA3End = "";
            }

            //Go through and check if all roles are filled or not
            bool RolesFilled = true;
            grdStaff.FindChildren<ComboBox>().ToList().ForEach(x =>
            {
                if (x.IsEnabled && x.Text == "")
                {
                    RolesFilled = false;
                }
            });
            Selected.State = RolesFilled ? "Complete" : "Incomplete";
            ListManager.UpdateSessionStaff(Selected);
            //Go back to main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        //Method to reset cbos if need to reselect staff
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cboSV1.SelectedValue = null;
            cboSV1Start.SelectedValue = null;
            cboSV1End.SelectedValue = null;
            cboSV1Start.IsEnabled = false;
            cboDRI1.SelectedValue = null;
            cboSV1End.IsEnabled = false;

            cboDRI1Start.SelectedValue = null;
            cboDRI1Start.IsEnabled = false;
            cboDRI2End.SelectedValue = null;
            cboDRI1End.IsEnabled = false;
            cboDRI2.SelectedValue = null;
            cboDRI2Start.SelectedValue = null;
            cboDRI2Start.IsEnabled = false;
            cboDRI1End.SelectedValue = null;
            cboDRI2End.IsEnabled = false;

            cboRN1.SelectedValue = null;
            cboRN1Start.SelectedValue = null;
            cboRN1Start.IsEnabled = false;
            cboRN1End.SelectedValue = null;
            cboRN1End.IsEnabled = false;
            cboRN2.SelectedValue = null;
            cboRN2Start.SelectedValue = null;
            cboRN2Start.IsEnabled = false;
            cboRN2End.SelectedValue = null;
            cboRN2End.IsEnabled = false;
            cboRN3.SelectedValue = null;
            cboRN3Start.SelectedValue = null;
            cboRN3Start.IsEnabled = false;
            cboRN3End.SelectedValue = null;
            cboRN3End.IsEnabled = false;

            cboCCA1.SelectedValue = null;
            cboCCA1Start.SelectedValue = null;
            cboCCA1Start.IsEnabled = false;
            cboCCA1End.SelectedValue = null;
            cboCCA1End.IsEnabled = false;
            cboCCA2.SelectedValue = null;
            cboCCA2Start.SelectedValue = null;
            cboCCA2Start.IsEnabled = false;
            cboCCA2End.SelectedValue = null;
            cboCCA2End.IsEnabled = false;
            cboCCA3.SelectedValue = null;
            cboCCA3Start.SelectedValue = null;
            cboCCA3Start.IsEnabled = false;
            cboCCA3End.SelectedValue = null;
            cboCCA3End.IsEnabled = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
