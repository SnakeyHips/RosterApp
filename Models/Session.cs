using System;

namespace RosterApp.Models
{
    public class Session
    {
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double Length { get; set; }
        public string Location { get; set; }
        public string MDC { get; set; }
        public int Chairs { get; set; }
        public int SV1Id { get; set; }
        public string SV1Start { get; set; }
        public string SV1End { get; set; }
        public int DRI1Id { get; set; }
        public string DRI1Start { get; set; }
        public string DRI1End { get; set; }
        public int DRI2Id { get; set; }
        public string DRI2Start { get; set; }
        public string DRI2End { get; set; }
        public int RN1Id { get; set; }
        public string RN1Start { get; set; }
        public string RN1End { get; set; }
        public int RN2Id { get; set; }
        public string RN2Start { get; set; }
        public string RN2End { get; set; }
        public int RN3Id { get; set; }
        public string RN3Start { get; set; }
        public string RN3End { get; set; }
        public int CCA1Id { get; set; }
        public string CCA1Start { get; set; }
        public string CCA1End { get; set; }
        public int CCA2Id { get; set; }
        public string CCA2Start { get; set; }
        public string CCA2End { get; set; }
        public int CCA3Id { get; set; }
        public string CCA3Start { get; set; }
        public string CCA3End { get; set; }
        public string State { get; set; }

        public Session(string date, string starttime, string endtime, double length, string location, string mdc, int chairs,
            int svi1id, string svi1start, string svi1end, int dri1id, string dri1start, string dri1end, int dri2id, string dri2start, string dri2end,
            int rn1id, string rn1start, string rn1end, int rn2id, string rn2start, string rn2end, int rn3id, string rn3start, string rn3end,
            int cca1id, string cca1start, string cca1end, int cca2id, string cca2start, string cca2end, int cca3id, string cca3start, string cca3end,
            string state)
        {
            this.Date = date;
            this.StartTime = starttime;
            this.EndTime = endtime;
            this.Length = length;
            this.Location = location;
            this.MDC = mdc;
            this.Chairs = chairs;
            this.SV1Id = svi1id;
            this.SV1Start = svi1start;
            this.SV1End = svi1end;
            this.DRI1Id = dri1id;
            this.DRI1Start = dri1start;
            this.DRI1End = dri1end;
            this.DRI2Id = dri2id;
            this.DRI2Start = dri2start;
            this.DRI2End = dri2end;
            this.RN1Id = rn1id;
            this.RN1Start = rn1start;
            this.RN1End = rn1end;
            this.RN2Id = rn2id;
            this.RN2Start = rn2start;
            this.RN2End = rn2end;
            this.RN3Id = rn3id;
            this.RN3Start = rn3start;
            this.RN3End = rn3end;
            this.CCA1Id = cca1id;
            this.CCA1Start = cca1start;
            this.CCA1End = cca1end;
            this.CCA2Id = cca2id;
            this.CCA2Start = cca2start;
            this.CCA2End = cca2end;
            this.CCA3Id = cca3id;
            this.CCA3Start = cca3start;
            this.CCA3End = cca3end;
            this.State = state;
        }
    }

}
