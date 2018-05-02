using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using RosterApp.Models;
using MahApps.Metro;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RosterApp.Views
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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

            foreach(Absence a in ListManager.AbsenceList)
            {
                lstAbsence.Items.Add(a);
            }

            lstSessions.SelectedIndex = ListManager.SelectedSession;
            lstStaff.SelectedIndex = ListManager.SelectedStaff;
            lstAbsence.SelectedIndex = ListManager.SelectedAbsence;
            dateSelected.SelectedDate = ListManager.SelectedDate;
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

        private void SessionCalendar_SelectedDatesChanged(object sender, RoutedEventArgs e)
        {
            Mouse.Capture(null);
            ListManager.SelectedDate = dateSelected.SelectedDate.Value.Date;
            ListManager.SessionList = ListManager.GetSessions(ListManager.SelectedDate.ToShortDateString());
            lstSessions.Items.Clear();
            foreach (Session s in ListManager.SessionList)
            {
                lstSessions.Items.Add(s);
            }

            lstStaff.Items.Clear();
            foreach (Staff s in ListManager.StaffList)
            {
                s.Status = Staff.GetStatus(s.Id);
                lstStaff.Items.Add(s);
            }

            //Code below if they want per day absence list
            //lstAbsence.Items.Clear();
            //foreach (Absence a in ListManager.AbsenceList)
            //{
            //    if (ListManager.SelectedDate.CompareTo(DateTime.Parse(a.StartDate)) >= 0
            //        && ListManager.SelectedDate.CompareTo(DateTime.Parse(a.EndDate)) <= 0)
            //    {
            //        lstAbsence.Items.Add(a);
            //    }
            //}
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
                Session s = ListManager.SessionList.Find(x => x.Date == Selected.Date 
                && x.Location == Selected.Location && x.StartTime == Selected.StartTime);
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

        private void btnOverviewSession_Click(object sender, RoutedEventArgs e)
        {
            OverviewWindow overviewWindow = new OverviewWindow();
            overviewWindow.Show();
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
                Session s = ListManager.SessionList.Find
                    (x => x.Date == Selected.Date &&x.Location == Selected.Location && x.StartTime == Selected.StartTime);
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
                        lstSessions.Items.Remove(Selected);
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

        private async void btnReportSession_Click(object sender, RoutedEventArgs e)
        {
            StaffReportDialog staffReportDialog = new StaffReportDialog();
            staffReportDialog.Owner = this;
            staffReportDialog.ShowDialog();
            if(staffReportDialog.DialogResult == true)
            {
                List<DateTime> Dates = new List<DateTime>();
                DateTime Start = DateTime.Parse(staffReportDialog.dateStart.Text);
                DateTime End = DateTime.Parse(staffReportDialog.dateEnd.Text);

                for(DateTime dt = Start; dt <= End; dt = dt.AddDays(1))
                {
                    Dates.Add(dt);
                }

                if(Dates.Count() > 0)
                {
                    List<Session> ReportSessions = new List<Session>();
                    foreach (DateTime date in Dates)
                    {
                        ReportSessions.AddRange(ListManager.GetSessions(date.ToShortDateString()));
                    }
                    if(ReportSessions.Count() > 0)
                    {
                        SaveFileDialog saveDialog = new SaveFileDialog();
                        saveDialog.Title = "Choose Report Save Location";
                        saveDialog.FileName = "Session Report";
                        saveDialog.Filter = "PDF document (*.pdf)|*.pdf";
                        bool? result = saveDialog.ShowDialog();

                        if (result == true)
                        {
                            //disable main window and activate progress ring while report is being created
                            this.IsHitTestVisible = false;
                            pRing.IsActive = true;
                            await CreateSessionReport(ReportSessions, saveDialog.FileName);
                        }
                    }
                }
            }
        }

        //MEthod for creating sessions pdf report
        private async Task CreateSessionReport(List<Session> sessions, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                using (Document document = new Document())
                {
                    using (PdfSmartCopy copy = new PdfSmartCopy(document, fs))
                    {
                        document.Open();
                        foreach (Session s in sessions)
                        {
                            //Read in template here
                            PdfReader reader = new PdfReader("Templates/SessionTemplate.pdf");
                            using (MemoryStream ms = new MemoryStream())
                            {
                                using (PdfStamper stamp = new PdfStamper(reader, ms))
                                {
                                    //Put together all fields from pdf template
                                    AcroFields fields = stamp.AcroFields;
                                    fields.SetField("Date", s.Date);
                                    fields.SetField("StartTime", s.StartTime);
                                    fields.SetField("MDC", s.MDC);
                                    fields.SetField("Location", s.Location);
                                    fields.SetField("EndTime", s.EndTime);
                                    fields.SetField("Chairs", s.Chairs.ToString());
                                    fields.SetField("StartTime", s.StartTime);
                                    fields.SetField("SV1Name", s.SV1Name);
                                    fields.SetField("SV1Start", s.SV1Start);
                                    fields.SetField("SV1End", s.SV1End);
                                    fields.SetField("DRI1Name", s.DRI1Name);
                                    fields.SetField("DRI1Start", s.DRI1Start);
                                    fields.SetField("DRI1End", s.DRI1End);
                                    fields.SetField("DRI2Name", s.DRI2Name);
                                    fields.SetField("DRI2Start", s.DRI2Start);
                                    fields.SetField("DRI2End", s.DRI2End);
                                    fields.SetField("RN1Name", s.RN1Name);
                                    fields.SetField("RN1Start", s.RN1Start);
                                    fields.SetField("RN1End", s.RN1End);
                                    fields.SetField("RN2Name", s.RN2Name);
                                    fields.SetField("RN2Start", s.RN2Start);
                                    fields.SetField("RN2End", s.RN2End);
                                    fields.SetField("RN3Name", s.RN3Name);
                                    fields.SetField("RN3Start", s.RN3Start);
                                    fields.SetField("RN3End", s.RN3End);
                                    fields.SetField("CCA1Name", s.CCA1Name);
                                    fields.SetField("CCA1Start", s.CCA1Start);
                                    fields.SetField("CCA1End", s.CCA1End);
                                    fields.SetField("CCA2Name", s.CCA2Name);
                                    fields.SetField("CCA2Start", s.CCA2Start);
                                    fields.SetField("CCA2End", s.CCA2End);
                                    fields.SetField("CCA3Name", s.CCA3Name);
                                    fields.SetField("CCA3Start", s.CCA3Start);
                                    fields.SetField("CCA3End", s.CCA3End);
                                    stamp.FormFlattening = true;
                                }
                                reader = new PdfReader(ms.ToArray());
                                //Add page for each session
                                copy.AddPage(copy.GetImportedPage(reader, 1));
                            }
                        }
                    }
                }
                this.IsHitTestVisible = true;
                await this.ShowMessageAsync("", "Report created successfully.");
                pRing.IsActive = false;
            }
            catch
            {
                this.IsHitTestVisible = true;
                //Most common issue for report not producing is that previous file is already open
                await this.ShowMessageAsync("", "Report failed to create. Please make sure a report is not already open.");
                pRing.IsActive = false;
            }
        }
    }
}
