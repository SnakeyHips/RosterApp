using System;

namespace RosterApp.Models
{
    public class Absence
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double Length { get; set; }

        public Absence()
        {
        }
    }
}
