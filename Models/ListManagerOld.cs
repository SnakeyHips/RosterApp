using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace RosterApp.Models
{
    public class ListManager
    {

        public ListManager()
        {
        }

        public static string connString = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;

        public static DateTime SelectedDate = DateTime.Now.Date;

        public static List<Session> SessionList = GetSessions(SelectedDate.ToShortDateString());
        public static List<Staff> StaffList = GetStaff();
        public static List<Absence> AbsenceList = GetAbsences();
        public static HashSet<double> WeekList = new HashSet<double>();
        public static List<Staff> RosterList = new List<Staff>();
        public static List<Staff> AvailableStaffList = new List<Staff>();

        //Value to store which tab was previously selected
        public static int SelectedTab = 0;
        public static int SelectedSession = 0;
        public static int SelectedStaff = 0;
        public static int SelectedAbsence = 0;

        public static List<Session> GetSessions(string date)
        {
            List<Session> list = new List<Session>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM SessionTable" +
                        " WHERE Date=@Date;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Date", date);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Session temp = (new Session(
                                    reader["Date"].ToString(),
                                    reader["StartTime"].ToString(),
                                    reader["EndTime"].ToString(),
                                    Convert.ToDouble(reader["Length"]),
                                    reader["Location"].ToString(),
                                    reader["MDC"].ToString(),
                                    Convert.ToInt32(reader["Chairs"]),
                                    Convert.ToInt32(reader["SV1Id"]),
                                    reader["SV1Name"].ToString(),
                                    reader["SV1Start"].ToString(),
                                    reader["SV1End"].ToString(),
                                    Convert.ToInt32(reader["DRI1Id"]),
                                    reader["DRI1Name"].ToString(),
                                    reader["DRI1Start"].ToString(),
                                    reader["DRI1End"].ToString(),
                                    Convert.ToInt32(reader["DRI2Id"]),
                                    reader["DRI2Name"].ToString(),
                                    reader["DRI2Start"].ToString(),
                                    reader["DRI2End"].ToString(),
                                    Convert.ToInt32(reader["RN1Id"]),
                                    reader["RN1Name"].ToString(),
                                    reader["RN1Start"].ToString(),
                                    reader["RN1End"].ToString(),
                                    Convert.ToInt32(reader["RN2Id"]),
                                    reader["RN2Name"].ToString(),
                                    reader["RN2Start"].ToString(),
                                    reader["RN2End"].ToString(),
                                    Convert.ToInt32(reader["RN3Id"]),
                                    reader["RN3Name"].ToString(),
                                    reader["RN3Start"].ToString(),
                                    reader["RN3End"].ToString(),
                                    Convert.ToInt32(reader["CCA1Id"]),
                                    reader["CCA1Name"].ToString(),
                                    reader["CCA1Start"].ToString(),
                                    reader["CCA1End"].ToString(),
                                    Convert.ToInt32(reader["CCA2Id"]),
                                    reader["CCA2Name"].ToString(),
                                    reader["CCA2Start"].ToString(),
                                    reader["CCA2End"].ToString(),
                                    Convert.ToInt32(reader["CCA3Id"]),
                                    reader["CCA3Name"].ToString(),
                                    reader["CCA3Start"].ToString(),
                                    reader["CCA3End"].ToString(),
                                    reader["State"].ToString()));
                                list.Add(temp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return list;
        }

        public static void AddSession(Session s)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO SessionTable " +
                        "(Date, StartTime, EndTime, Length, Location, MDC, Chairs, SV1Id, SV1Name, SV1Start, SV1End, " +
                        "DRI1Id, DRI1Name, DRI1Start, DRI1End, DRI2Id, DRI2Name, DRI2Start, DRI2End, RN1Id, RN1Name, RN1Start, " +
                        "RN1End, RN2Id, RN2Name, RN2Start, RN2End, RN3Id, RN3Name, RN3Start, RN3End, CCA1Id, CCA1Name, CCA1Start, " +
                        "CCA1End, CCA2Id, CCA2Name, CCA2Start, CCA2End, CCA3Id, CCA3Name, CCA3Start, CCA3End, State) " +
                        "VALUES (@Date, @StartTime, @EndTime, @Length, @Location, @MDC, @Chairs, @SV1Id, @SV1Name, @SV1Start, " +
                        "@SV1End, @DRI1Id, @DRI1Name, @DRI1Start, @DRI1End, @DRI2Id, @DRI2Name, @DRI2Start, @DRI2End, @RN1Id, " +
                        "@RN1Name, @RN1Start, @RN1End, @RN2Id, @RN2Name, @RN2Start, @RN2End, @RN3Id, @RN3Name, @RN3Start, " +
                        "@RN3End, @CCA1Id, @CCA1Name, @CCA1Start, @CCA1End, @CCA2Id, @CCA2Name, @CCA2Start, @CCA2End, " +
                        "@CCA3Id, @CCA3Name, @CCA3Start, @CCA3End, @State);";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Date", s.Date);
                    cmd.Parameters.AddWithValue("@StartTime", s.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", s.EndTime);
                    cmd.Parameters.AddWithValue("@Length", s.Length);
                    cmd.Parameters.AddWithValue("@Location", s.Location);
                    cmd.Parameters.AddWithValue("@MDC", s.MDC);
                    cmd.Parameters.AddWithValue("@Chairs", s.Chairs);
                    cmd.Parameters.AddWithValue("@SV1Id", s.SV1Id);
                    cmd.Parameters.AddWithValue("@SV1Name", s.SV1Name);
                    cmd.Parameters.AddWithValue("@SV1Start", s.SV1Start);
                    cmd.Parameters.AddWithValue("@SV1End", s.SV1End);
                    cmd.Parameters.AddWithValue("@DRI1Id", s.DRI1Id);
                    cmd.Parameters.AddWithValue("@DRI1Name", s.DRI1Name);
                    cmd.Parameters.AddWithValue("@DRI1Start", s.DRI1Start);
                    cmd.Parameters.AddWithValue("@DRI1End", s.DRI1End);
                    cmd.Parameters.AddWithValue("@DRI2Id", s.DRI2Id);
                    cmd.Parameters.AddWithValue("@DRI2Name", s.DRI2Name);
                    cmd.Parameters.AddWithValue("@DRI2Start", s.DRI2Start);
                    cmd.Parameters.AddWithValue("@DRI2End", s.DRI2End);
                    cmd.Parameters.AddWithValue("@RN1Id", s.RN1Id);
                    cmd.Parameters.AddWithValue("@RN1Name", s.RN1Name);
                    cmd.Parameters.AddWithValue("@RN1Start", s.RN1Start);
                    cmd.Parameters.AddWithValue("@RN1End", s.RN1End);
                    cmd.Parameters.AddWithValue("@RN2Id", s.RN2Id);
                    cmd.Parameters.AddWithValue("@RN2Name", s.RN2Name);
                    cmd.Parameters.AddWithValue("@RN2Start", s.RN2Start);
                    cmd.Parameters.AddWithValue("@RN2End", s.RN2End);
                    cmd.Parameters.AddWithValue("@RN3Id", s.RN3Id);
                    cmd.Parameters.AddWithValue("@RN3Name", s.RN3Name);
                    cmd.Parameters.AddWithValue("@RN3Start", s.RN3Start);
                    cmd.Parameters.AddWithValue("@RN3End", s.RN3End);
                    cmd.Parameters.AddWithValue("@CCA1Id", s.CCA1Id);
                    cmd.Parameters.AddWithValue("@CCA1Name", s.CCA1Name);
                    cmd.Parameters.AddWithValue("@CCA1Start", s.CCA1Start);
                    cmd.Parameters.AddWithValue("@CCA1End", s.CCA1End);
                    cmd.Parameters.AddWithValue("@CCA2Id", s.CCA2Id);
                    cmd.Parameters.AddWithValue("@CCA2Name", s.CCA2Name);
                    cmd.Parameters.AddWithValue("@CCA2Start", s.CCA2Start);
                    cmd.Parameters.AddWithValue("@CCA2End", s.CCA2End);
                    cmd.Parameters.AddWithValue("@CCA3Id", s.CCA3Id);
                    cmd.Parameters.AddWithValue("@CCA3Name", s.CCA3Name);
                    cmd.Parameters.AddWithValue("@CCA3Start", s.CCA3Start);
                    cmd.Parameters.AddWithValue("@CCA3End", s.CCA3End);
                    cmd.Parameters.AddWithValue("@State", s.State);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void UpdateSession(Session s)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE SessionTable" +
                        " SET StartTime=@StartTime, EndTime=@EndTime, Length=@Length, Location=@Location, MDC=@MDC, Chairs=@Chairs" +
                        " WHERE Date=@Date AND StartTime=@StartTime;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Date", s.Date);
                    cmd.Parameters.AddWithValue("@StartTime", s.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", s.EndTime);
                    cmd.Parameters.AddWithValue("@Length", s.Length);
                    cmd.Parameters.AddWithValue("@Location", s.Location);
                    cmd.Parameters.AddWithValue("@MDC", s.MDC);
                    cmd.Parameters.AddWithValue("@Chairs", s.Chairs);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void DeleteSession(Session s)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM SessionTable" +
                        " WHERE Date=@Date AND Location=@Location AND StartTime=@StartTime;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Date", s.Date);
                    cmd.Parameters.AddWithValue("@Location", s.Location);
                    cmd.Parameters.AddWithValue("@StartTime", s.StartTime);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void UpdateSessionStaff(Session s)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE SessionTable" +
                        " SET SV1Id=@SV1Id, SV1Name=@SV1Name, SV1Start=@SV1Start, SV1End=@SV1End," +
                        " DRI1Id=@DRI1Id, DRI1Name=@DRI1Name, DRI1Start=@DRI1Start, DRI1End=@DRI1End," +
                        " DRI2Id=@DRI2Id, DRI2Name=@DRI2Name, DRI2Start=@DRI2Start, DRI2End=@DRI2End," +
                        " RN1Id=@RN1Id, RN1Name=@RN1Name, RN1Start=@RN1Start, RN1End=@RN1End," +
                        " RN2Id=@RN2Id, RN2Name=@RN2Name, RN2Start=@RN2Start, RN2End=@RN2End," +
                        " RN3Id=@RN3Id, RN3Name=@RN3Name, RN3Start=@RN3Start, RN3End=@RN3End," +
                        " CCA1Id=@CCA1Id, CCA1Name=@CCA1Name, CCA1Start=@CCA1Start, CCA1End=@CCA1End," +
                        " CCA2Id=@CCA2Id, CCA2Name=@CCA2Name, CCA2Start=@CCA2Start, CCA2End=@CCA2End," +
                        " CCA3Id=@CCA3Id, CCA3Name=@CCA3Name, CCA3Start=@CCA3Start, CCA3End=@CCA3End," +
                        " State=@State WHERE Date=@Date AND StartTime=@StartTime AND Location=@Location;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Date", s.Date);
                    cmd.Parameters.AddWithValue("@StartTime", s.StartTime);
                    cmd.Parameters.AddWithValue("@Location", s.Location);
                    cmd.Parameters.AddWithValue("@SV1Id", s.SV1Id);
                    cmd.Parameters.AddWithValue("@SV1Name", s.SV1Name);
                    cmd.Parameters.AddWithValue("@SV1Start", s.SV1Start);
                    cmd.Parameters.AddWithValue("@SV1End", s.SV1End);
                    cmd.Parameters.AddWithValue("@DRI1Id", s.DRI1Id);
                    cmd.Parameters.AddWithValue("@DRI1Name", s.DRI1Name);
                    cmd.Parameters.AddWithValue("@DRI1Start", s.DRI1Start);
                    cmd.Parameters.AddWithValue("@DRI1End", s.DRI1End);
                    cmd.Parameters.AddWithValue("@DRI2Id", s.DRI2Id);
                    cmd.Parameters.AddWithValue("@DRI2Name", s.DRI2Name);
                    cmd.Parameters.AddWithValue("@DRI2Start", s.DRI2Start);
                    cmd.Parameters.AddWithValue("@DRI2End", s.DRI2End);
                    cmd.Parameters.AddWithValue("@RN1Id", s.RN1Id);
                    cmd.Parameters.AddWithValue("@RN1Name", s.RN1Name);
                    cmd.Parameters.AddWithValue("@RN1Start", s.RN1Start);
                    cmd.Parameters.AddWithValue("@RN1End", s.RN1End);
                    cmd.Parameters.AddWithValue("@RN2Id", s.RN2Id);
                    cmd.Parameters.AddWithValue("@RN2Name", s.RN2Name);
                    cmd.Parameters.AddWithValue("@RN2Start", s.RN2Start);
                    cmd.Parameters.AddWithValue("@RN2End", s.RN2End);
                    cmd.Parameters.AddWithValue("@RN3Id", s.RN3Id);
                    cmd.Parameters.AddWithValue("@RN3Name", s.RN3Name);
                    cmd.Parameters.AddWithValue("@RN3Start", s.RN3Start);
                    cmd.Parameters.AddWithValue("@RN3End", s.RN3End);
                    cmd.Parameters.AddWithValue("@CCA1Id", s.CCA1Id);
                    cmd.Parameters.AddWithValue("@CCA1Name", s.CCA1Name);
                    cmd.Parameters.AddWithValue("@CCA1Start", s.CCA1Start);
                    cmd.Parameters.AddWithValue("@CCA1End", s.CCA1End);
                    cmd.Parameters.AddWithValue("@CCA2Id", s.CCA2Id);
                    cmd.Parameters.AddWithValue("@CCA2Name", s.CCA2Name);
                    cmd.Parameters.AddWithValue("@CCA2Start", s.CCA2Start);
                    cmd.Parameters.AddWithValue("@CCA2End", s.CCA2End);
                    cmd.Parameters.AddWithValue("@CCA3Id", s.CCA3Id);
                    cmd.Parameters.AddWithValue("@CCA3Name", s.CCA3Name);
                    cmd.Parameters.AddWithValue("@CCA3Start", s.CCA3Start);
                    cmd.Parameters.AddWithValue("@CCA3End", s.CCA3End);
                    cmd.Parameters.AddWithValue("@State", s.State);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        public static List<Staff> GetStaff()
        {
            List<Staff> list = new List<Staff>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM StaffTable";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Staff temp = (new Staff(
                                    Convert.ToInt32(reader["Id"]),
                                    reader["Name"].ToString(),
                                    reader["Role"].ToString(),
                                    Convert.ToDouble(reader["ContractHours"]),
                                    reader["WorkPattern"].ToString()));
                                list.Add(temp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return list;
        }

        public static void AddStaff(Staff s)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO StaffTable (Id, Name, Role, ContractHours, WorkPattern)" +
                        "VALUES (@Id, @Name, @Role, @ContractHours, @WorkPattern);";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", s.Id);
                    cmd.Parameters.AddWithValue("@Name", s.Name);
                    cmd.Parameters.AddWithValue("@Role", s.Role);
                    cmd.Parameters.AddWithValue("@ContractHours", s.ContractHours);
                    cmd.Parameters.AddWithValue("@WorkPattern", s.WorkPattern);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void UpdateStaff(Staff s)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE StaffTable" +
                        " SET Role=@Role, ContractHours=@ContractHours" +
                        " WHERE Id=@Id;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Role", s.Role);
                    cmd.Parameters.AddWithValue("@ContractHours", s.ContractHours);
                    cmd.Parameters.AddWithValue("@Id", s.Id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static List<Absence> GetAbsences()
        {
            List<Absence> list = new List<Absence>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM AbsenceTable";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Absence temp = (new Absence(
                                    Convert.ToInt32(reader["StaffId"]),
                                    reader["StaffName"].ToString(),
                                    reader["Type"].ToString(),
                                    reader["StartDate"].ToString(),
                                    reader["EndDate"].ToString(),
                                    Convert.ToDouble(reader["Length"])));
                                list.Add(temp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return list;
        }

        public static void AddAbsence(Absence a)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO AbsenceTable (StaffId, StaffName, Type, StartDate, EndDate, Length)" +
                        "VALUES (@StaffId, @StaffName, @Type, @StartDate, @EndDate, @Length);";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StaffId", a.StaffId);
                    cmd.Parameters.AddWithValue("@StaffName", a.StaffName);
                    cmd.Parameters.AddWithValue("@Type", a.Type);
                    cmd.Parameters.AddWithValue("@StartDate", a.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", a.EndDate);
                    cmd.Parameters.AddWithValue("@Length", a.Length);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void UpdateAbsence(Absence a)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE AbsenceTable" +
                        " SET Type=@Type, Length=@Length" +
                        " WHERE StaffId=@StaffId AND StartDate=@StartDate;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Type", a.Type);
                    cmd.Parameters.AddWithValue("@Length", a.Length);
                    cmd.Parameters.AddWithValue("@StaffId", a.StaffId);
                    cmd.Parameters.AddWithValue("@StartDate", a.StartDate);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void DeleteAbsence(Absence a)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM AbsenceTable" +
                        " WHERE StaffId=@StaffId AND StartDate=@StartDate;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StaffId", a.StaffId);
                    cmd.Parameters.AddWithValue("@StartDate", a.StartDate);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static HashSet<double> GetRosterWeeks()
        {
            HashSet<double> hash = new HashSet<double>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Week FROM RosterTable;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hash.Add(Convert.ToDouble(reader["Week"]));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return hash;
        }

        public static List<Staff> GetRoster(double week)
        {
            List<Staff> list = new List<Staff>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM RosterTable" +
                        " WHERE Week=@Week;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Week", week.ToString());
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Staff temp = (new Staff(
                                    Convert.ToInt16(reader["StaffId"]),
                                    reader["StaffName"].ToString(),
                                    reader["Role"].ToString(),
                                    Convert.ToDouble(reader["ContractHours"]),
                                    ""));
                                temp.AppointedHours = Convert.ToDouble(reader["AppointedHours"]);
                                temp.AbsenceHours = Convert.ToDouble(reader["AbsenceHours"]);
                                list.Add(temp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return list;
        }

        public static Staff GetStaffRoster(double week, int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM RosterTable" +
                        " WHERE Week=@Week AND StaffId=@StaffId;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Week", week.ToString());
                        cmd.Parameters.AddWithValue("@StaffId", id.ToString());
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Staff temp = (new Staff(
                                    Convert.ToInt16(reader["StaffId"]),
                                    reader["StaffName"].ToString(),
                                    reader["Role"].ToString(),
                                    Convert.ToDouble(reader["ContractHours"]),
                                    ""));
                                temp.AppointedHours = Convert.ToDouble(reader["AppointedHours"]);
                                temp.AbsenceHours = Convert.ToDouble(reader["AbsenceHours"]);
                                return temp;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return null;
        }

        public static void AddRoster(int id, double appointed, double absence, double week)
        {
            Staff s = StaffList.Find(x => x.Id == id);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO RosterTable (Week, StaffId, StaffName, Role, ContractHours, AppointedHours, AbsenceHours)" +
                        " VALUES (@Week, @StaffId, @StaffName, @Role, @ContractHours, @Appointed, @Absence);";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Week", week);
                    cmd.Parameters.AddWithValue("@StaffId", s.Id);
                    cmd.Parameters.AddWithValue("@StaffName", s.Name);
                    cmd.Parameters.AddWithValue("@Role", s.Role);
                    cmd.Parameters.AddWithValue("@ContractHours", s.ContractHours);
                    cmd.Parameters.AddWithValue("@Appointed", appointed);
                    cmd.Parameters.AddWithValue("@Absence", absence);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void UpdateRoster(int id, double appointed, double absence, double week)
        {
            if(id != 0)
            {
                int rows = 0;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE RosterTable" +
                            " SET AppointedHours=AppointedHours+@Appointed, AbsenceHours=AbsenceHours+@Absence " +
                            " WHERE Week=@Week AND StaffId=@StaffId;";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Appointed", appointed);
                        cmd.Parameters.AddWithValue("@Absence", absence);
                        cmd.Parameters.AddWithValue("@Week", week);
                        cmd.Parameters.AddWithValue("@StaffId", id);
                        rows = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (rows == 0)
                    {
                        //Add in new roster if update fails
                        AddRoster(id, appointed, absence, week);
                    }
                }
            }
        }

        public static void DeleteRoster(int id, double week)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM RosterTable" +
                        " WHERE Week=@Week AND StaffId=@StaffId;";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Week", week);
                    cmd.Parameters.AddWithValue("@StaffId", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void GetAvailableStaff()
        {
            AvailableStaffList = StaffList
                .Where(x => x.Status == "Okay" && x.WorkPattern.Contains(SelectedDate.DayOfWeek.ToString().Substring(0, 3)))
                .ToList();
        }

        public static double GetLength(string starttime, string endtime)
        {
            try
            {
                return DateTime.Parse(endtime).Subtract(DateTime.Parse(starttime)).TotalHours;
            }
            catch
            {
                return 0.0;
            }
        }

        public static double GetWeek(DateTime date)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            return double.Parse(cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString()
                + "." + date.Year.ToString());
        }

        //Method for checking duplicates
        public static bool HasDuplicates(List<int> idList)
        {
            HashSet<int> hs = new HashSet<int>();
            foreach (int id in idList)
            {
                if (!hs.Add(id))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
