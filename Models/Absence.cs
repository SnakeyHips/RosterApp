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

        public Absence(int staffid, string staffname, string type, string startdate, string enddate, double length)
        {
            this.StaffId = staffid;
            this.StaffName = staffname;
            this.Type = type;
            this.StartDate = startdate;
            this.EndDate = enddate;
            this.Length = length;
        }
    }
}
