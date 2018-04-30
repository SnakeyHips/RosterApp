using System;

namespace RosterApp.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public double ContractHours { get; set; }
        public double AppointedHours { get; set; }
        public double AbsenceHours { get; set; }
        public string WorkPattern { get; set; }
        public string Status { get; set; }

        public Staff(int id, string name,  string role, double contracthours, string workpattern)
        {
            this.Id = id;
            this.Name = name;
            this.Role = role;
            this.ContractHours = contracthours;
            this.WorkPattern = workpattern;
        }

        public static string GetStatus(int id)
        {
            try
            {
                return ListManager.AbsenceList.Find(x => x.StaffId == id
                && DateTime.Parse(x.StartDate).CompareTo(ListManager.SelectedDate) <= 0
                && DateTime.Parse(x.EndDate).CompareTo(ListManager.SelectedDate) >= 0).Type;
            }
            catch
            {
                return "Okay";
            }

        }

        public static Staff Copy(Staff s)
        {
            return new Staff(s.Id, s.Name, s.Role, s.ContractHours, s.WorkPattern);
        }
    }
}
