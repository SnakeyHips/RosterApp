using System;
using System.Windows;
using RosterApp.Models;
using MahApps.Metro.Controls;

namespace RosterApp.Views
{
    public partial class OverviewWindow : MetroWindow
    {
        public OverviewWindow()
        {
            InitializeComponent();
            tabOverview.Header = ListManager.SelectedDate.ToShortDateString();
            PopulateOverview();
        }

        public void PopulateOverview()
        {
            foreach (Session s in ListManager.SessionList)
            {
                lstOverview.Items.Add(s);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lstOverview.Items.Clear();
            PopulateOverview();
        }
    }
}
