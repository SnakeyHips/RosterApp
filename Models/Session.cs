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
        public string SV1Name { get; set; }
        public string SV1Start { get; set; }
        public string SV1End { get; set; }
        public int DRI1Id { get; set; }
        public string DRI1Name { get; set; }
        public string DRI1Start { get; set; }
        public string DRI1End { get; set; }
        public int DRI2Id { get; set; }
        public string DRI2Name { get; set; }
        public string DRI2Start { get; set; }
        public string DRI2End { get; set; }
        public int RN1Id { get; set; }
        public string RN1Name { get; set; }
        public string RN1Start { get; set; }
        public string RN1End { get; set; }
        public int RN2Id { get; set; }
        public string RN2Name { get; set; }
        public string RN2Start { get; set; }
        public string RN2End { get; set; }
        public int RN3Id { get; set; }
        public string RN3Name { get; set; }
        public string RN3Start { get; set; }
        public string RN3End { get; set; }
        public int CCA1Id { get; set; }
        public string CCA1Name { get; set; }
        public string CCA1Start { get; set; }
        public string CCA1End { get; set; }
        public int CCA2Id { get; set; }
        public string CCA2Name { get; set; }
        public string CCA2Start { get; set; }
        public string CCA2End { get; set; }
        public int CCA3Id { get; set; }
        public string CCA3Name { get; set; }
        public string CCA3Start { get; set; }
        public string CCA3End { get; set; }
        public string State { get; set; }

        public Session(string date, string starttime, string endtime, double length, string location, string mdc, int chairs,
            int sv1id, string sv1name,  string svi1start, string svi1end, int dri1id, string dri1name, string dri1start, 
            string dri1end, int dri2id, string dri2name, string dri2start, string dri2end, int rn1id, string rn1name, string rn1start,
            string rn1end, int rn2id, string rn2name, string rn2start, string rn2end, int rn3id, string rn3name, string rn3start,
            string rn3end, int cca1id, string cca1name, string cca1start, string cca1end, int cca2id, string cca2name, string cca2start,
            string cca2end, int cca3id, string cca3name, string cca3start, string cca3end, string state)
        {
            this.Date = date;
            this.StartTime = starttime;
            this.EndTime = endtime;
            this.Length = length;
            this.Location = location;
            this.MDC = mdc;
            this.Chairs = chairs;
            this.SV1Id = sv1id;
            this.SV1Name = sv1name;
            this.SV1Start = svi1start;
            this.SV1End = svi1end;
            this.DRI1Id = dri1id;
            this.DRI1Name = dri1name;
            this.DRI1Start = dri1start;
            this.DRI1End = dri1end;
            this.DRI2Id = dri2id;
            this.DRI2Name = dri2name;
            this.DRI2Start = dri2start;
            this.DRI2End = dri2end;
            this.RN1Id = rn1id;
            this.RN1Name = rn1name;
            this.RN1Start = rn1start;
            this.RN1End = rn1end;
            this.RN2Id = rn2id;
            this.RN2Name = rn2name;
            this.RN2Start = rn2start;
            this.RN2End = rn2end;
            this.RN3Id = rn3id;
            this.RN3Name = rn3name;
            this.RN3Start = rn3start;
            this.RN3End = rn3end;
            this.CCA1Id = cca1id;
            this.CCA1Name = cca1name;
            this.CCA1Start = cca1start;
            this.CCA1End = cca1end;
            this.CCA2Id = cca2id;
            this.CCA2Name = cca2name;
            this.CCA2Start = cca2start;
            this.CCA2End = cca2end;
            this.CCA3Id = cca3id;
            this.CCA3Name = cca3name;
            this.CCA3Start = cca3start;
            this.CCA3End = cca3end;
            this.State = state;
        }
    }

}
