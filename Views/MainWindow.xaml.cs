using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro;
using MahApps.Metro.Controls;
using RosterApp.Models;
using MahApps.Metro.Controls.Dialogs;

namespace RosterApp.Views
{

    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach(Session s in ListManager.SessionList)
            {
                lstSessions.Items.Add(s);
            }

            foreach (Staff s in ListManager.StaffList)
            {
                lstStaff.Items.Add(s);
            }

            foreach (Absence a in ListManager.AbsenceList)
            {
                lstAbsence.Items.Add(a);
            }
           
            ListManager.SetStatuses();
            ListManager.GetAvailableStaff();

            //Switch to select last selected tab
            switch (ListManager.SelectedTab)
            {
                case 0:
                    tabSessions.IsSelected = true;
                    break;
                case 1:
                    tabStaff.IsSelected = true;
                    break;
                case 2:
                    tabAbsence.IsSelected = true;
                    break;
            }

            lstSessions.SelectedIndex = ListManager.SelectedSession;
            lstStaff.SelectedIndex = ListManager.SelectedStaff;
            lstAbsence.SelectedIndex = ListManager.SelectedAbsence;
            dateSessions.SelectedDate = ListManager.SelectedDate;
        }

        //TabControl selection handler for saving which tab was last selected and app accent
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabSessions.IsSelected)
            {
                ListManager.SelectedTab = 0;
                ChangeAccent(ListManager.SelectedTab);
            }
            else if (tabStaff.IsSelected)
            {
                ListManager.SelectedTab = 1;
                ChangeAccent(ListManager.SelectedTab);
            }
            else if (tabAbsence.IsSelected)
            {
                ListManager.SelectedTab = 2;
                ChangeAccent(ListManager.SelectedTab);
            }
        }

        //Method used to change app's accent depending on which tab selected
        private void ChangeAccent(int tab)
        {
            switch (tab)
            {
                case 0:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.Accents.First(x => x.Name == "Crimson"),
                        ThemeManager.AppThemes.First(x => x.Name == "BaseLight"));
                    break;
                case 1:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.Accents.First(x => x.Name == "Mauve"),
                        ThemeManager.AppThemes.First(x => x.Name == "BaseLight"));
                    break;
                case 2:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.Accents.First(x => x.Name == "Olive"),
                        ThemeManager.AppThemes.First(x => x.Name == "BaseLight"));
                    break;
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, RoutedEventArgs e)
        {
            ListManager.SelectedDate = dateSessions.SelectedDate.Value.Date;
            ListManager.SessionList = ListManager.GetSessions();
            lstSessions.Items.Clear();
            foreach (Session s in ListManager.SessionList)
            {
                lstSessions.Items.Add(s);
            }
        }

        private void lstSession_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstSessions.SelectedItem != null)
            {
                ListManager.SelectedSession = lstSessions.SelectedIndex;
            }
        }

        private void lstStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstStaff.SelectedItem != null)
            {
                ListManager.SelectedStaff= lstStaff.SelectedIndex;
            }
        }

        private void lstAbsence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAbsence.SelectedItem != null)
            {
                ListManager.SelectedAbsence = lstAbsence.SelectedIndex;
            }
        }

        private void btnAddSession_Click(object sender, RoutedEventArgs e)
        {
            AddSessionWindow addSessionWindow = new AddSessionWindow();
            addSessionWindow.Show();
            this.Close();
        }

        private async void btnUpdateSession_Click(object sender, RoutedEventArgs e)
        {
            if(lstSessions.SelectedIndex > -1)
            {
                Session Selected = (Session)lstSessions.SelectedItem;
                Session s = ListManager.SessionList.Find(x => x.Date == Selected.Date && x.StartTime == Selected.StartTime);
                if (s.SV1Id == 0 && s.DRI1Id == 0 && s.DRI2Id == 0 && s.RN1Id == 0 && s.RN2Id == 0
                    && s.RN3Id == 0 && s.CCA1Id == 0 && s.CCA2Id == 0 && s.CCA3Id == 0)
                {
                    UpdateSessionWindow updateSessionWindow = new UpdateSessionWindow((Session)lstSessions.SelectedItem);
                    updateSessionWindow.Show();
                    this.Close();
                }
                else
                {
                    await this.ShowMessageAsync("", "Please unappoint all staff before updating Session.");
                }

            }
        }

        private void btnViewSession_Click(object sender, RoutedEventArgs e)
        {
            if(lstSessions.SelectedIndex > -1)
            {
                StaffDialog staffDialog = new StaffDialog((Session)lstSessions.SelectedItem);
                staffDialog.Owner = this;
                staffDialog.ShowDialog();
            }
        }

        private void btnStaffSession_Click(object sender, RoutedEventArgs e)
        {
            if(lstSessions.SelectedIndex > -1)
            {
                StaffSessionWindow staffSessionWindow = new StaffSessionWindow((Session)lstSessions.SelectedItem);
                staffSessionWindow.Show();
                this.Close();
            }
        }

        private async void btnDelSession_Click(object sender, RoutedEventArgs e)
        {
            if (lstSessions.SelectedIndex > -1)
            {
                Session Selected = (Session)lstSessions.SelectedItem;
                Session s = ListManager.SessionList.Find(x => x.Date == Selected.Date && x.StartTime == Selected.StartTime);
                if(s.SV1Id == 0 && s.DRI1Id == 0 && s.DRI2Id == 0 && s.RN1Id == 0 && s.RN2Id == 0 
                    && s.RN3Id == 0 && s.CCA1Id == 0 && s.CCA2Id == 0 && s.CCA3Id == 0)
                {
                    MessageDialogResult choice = await this.ShowMessageAsync("",
                            "Are you sure you want to delete this Session?",
                            MessageDialogStyle.AffirmativeAndNegative);
                    if (choice == MessageDialogResult.Affirmative)
                    {
                        ListManager.DeleteSession(s);
                        ListManager.SessionList.Remove(s);
                        lstSessions.Items.Remove(lstSessions.SelectedItem);
                    }
                }
                else
                {
                    await this.ShowMessageAsync("", "Please unappoint all staff before deleting Session.");
                }
            }
        }

        private void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            AddStaffWindow addStaffWindow = new AddStaffWindow();
            addStaffWindow.Show();
            this.Close();
        }

        private void btnUpdateStaff_Click(object sender, RoutedEventArgs e)
        {
            if(lstStaff.SelectedIndex > -1)
            {
                UpdateStaffWindow updateStaffWindow = new UpdateStaffWindow((Staff)lstStaff.SelectedItem);
                updateStaffWindow.Show();
                this.Close();
            }
        }

        private void btnArchiveStaff_Click(object sender, RoutedEventArgs e)
        {
            ArchiveStaffWindow archiveStaffWindow = new ArchiveStaffWindow();
            archiveStaffWindow.Show();
            this.Close();
        }

        private void btnAddAbsence_Click(object sender, RoutedEventArgs e)
        {
            AddAbsenceWindow addAbsenceWindow = new AddAbsenceWindow();
            addAbsenceWindow.Show();
            this.Close();
        }

        private void btnUpdateAbsence_Click(object sender, RoutedEventArgs e)
        {
            if (lstAbsence.SelectedIndex > -1)
            {
                UpdateAbsenceWindow updateAbsenceWindow = new UpdateAbsenceWindow((Absence)lstAbsence.SelectedItem);
                updateAbsenceWindow.Show();
                this.Close();
            }
        }

        private async void btnDelAbsence_Click(object sender, RoutedEventArgs e)
        {
            if(lstAbsence.SelectedIndex > -1)
            {
                MessageDialogResult choice = await this.ShowMessageAsync("",
                            "Are you sure you want to delete this Absence?",
                            MessageDialogStyle.AffirmativeAndNegative);
                if (choice == MessageDialogResult.Affirmative)
                {
                    Absence Selected = (Absence)lstAbsence.SelectedItem;
                    ListManager.UpdateRoster(Selected.StaffId, 0.0, -Selected.Length, ListManager.GetWeek(DateTime.Parse(Selected.StartDate)));
                    ListManager.DeleteAbsence(Selected);
                    ListManager.AbsenceList.Remove(Selected);
                    lstAbsence.Items.Remove(lstAbsence.SelectedItem);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
