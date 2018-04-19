using System;
using System.Windows;
using MahApps.Metro.Controls;
using RosterApp.Models;

namespace RosterApp.Views
{
    public partial class StaffDialog : MetroWindow
    {
        Session Selected;
        public StaffDialog(Session s)
        {
            InitializeComponent();
            this.Selected = s;
            txtDate.Text = Selected.Date;
            txtLocation.Text = Selected.Location;
            txtStartTime.Text = Selected.StartTime;
            txtEndTime.Text = Selected.EndTime;
            txtMDC.Text = Selected.MDC;
            txtChairs.Text = Selected.Chairs.ToString();

            //Autopopulate cbos from StaffList
            if (Selected.SV1Id != 0)
            {
                txtSV1.Text = ListManager.StaffList.Find(x => x.Id == Selected.SV1Id).Name;
                txtSV1Start.Text = Selected.SV1Start;
                txtSV1End.Text = Selected.SV1End;
            }
            if (Selected.DRI1Id != 0)
            {
                txtDRI1.Text = ListManager.StaffList.Find(x => x.Id == Selected.DRI1Id).Name;
                txtDRI1Start.Text = Selected.DRI1Start;
                txtDRI1End.Text = Selected.DRI1End;
            }
            if (Selected.DRI2Id != 0)
            {
                txtDRI2.Text = ListManager.StaffList.Find(x => x.Id == Selected.DRI2Id).Name;
                txtDRI2Start.Text = Selected.DRI2Start;
                txtDRI2End.Text = Selected.DRI2End;
            }
            if (Selected.RN1Id != 0)
            {
                txtRN1.Text = ListManager.StaffList.Find(x => x.Id == Selected.RN1Id).Name;
                txtRN1Start.Text = Selected.RN1Start;
                txtRN1End.Text = Selected.RN1End;
            }
            if (Selected.RN2Id != 0)
            {
                txtRN2.Text = ListManager.StaffList.Find(x => x.Id == Selected.RN2Id).Name;
                txtRN2Start.Text = Selected.RN2Start;
                txtRN2End.Text = Selected.RN2End;
            }
            if (Selected.RN3Id != 0)
            {
                txtRN3.Text = ListManager.StaffList.Find(x => x.Id == Selected.RN3Id).Name;
                txtRN3Start.Text = Selected.RN3Start;
                txtRN3End.Text = Selected.RN3End;
            }
            if (Selected.CCA1Id != 0)
            {
                txtCCA1.Text = ListManager.StaffList.Find(x => x.Id == Selected.CCA1Id).Name;
                txtCCA1Start.Text = Selected.CCA1Start;
                txtCCA1End.Text = Selected.CCA1End;
            }
            if (Selected.CCA2Id != 0)
            {
                txtCCA2.Text = ListManager.StaffList.Find(x => x.Id == Selected.CCA2Id).Name;
                txtCCA2Start.Text = Selected.CCA2Start;
                txtCCA2End.Text = Selected.CCA2End;
            }
            if (Selected.CCA3Id != 0)
            {
                txtCCA3.Text = ListManager.StaffList.Find(x => x.Id == Selected.CCA3Id).Name;
                txtCCA3Start.Text = Selected.CCA3Start;
                txtCCA3End.Text = Selected.CCA3End;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
