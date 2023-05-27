using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeddingApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            DefaultDisplaySettins();
            DefaultChartOption();
            

            LoadLanguage();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            
        }
        private static void LoadLanguage()
        {

            SqlConnection con = new SqlConnection("Data Source=LAPTOP-LR9KKE2U\\SQLEXPRESS;Initial Catalog=WeddingApp;Integrated Security=True");

            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) * from dbo.DisplaySettings where Name='Language'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0]["TandF"].ToString() == "True")
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("tr-TR");
                WeddingApplication.Properties.Resources.Culture=(new CultureInfo("tr-TR"));
                Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
                
                
            }
                
            if (dt.Rows[0]["TandF"].ToString() == "False")
            {
               
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                WeddingApplication.Properties.Resources.Culture = (new CultureInfo("en"));
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            }
        }
        private static void DefaultChartOption()
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-LR9KKE2U\\SQLEXPRESS;Initial Catalog=WeddingApp;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.ChartOption", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string[] TableName = { "Randevu Tablosu", "Aylık Randevu", "Yıllık Randevu", "Alınan-Kalan", "Aylık Tutar", "Yıllık Tutar" };
            string[] TableType = { "Bar", "Spline", "Spline", "Bar", "Doughnut", "Pie" };
            bool[] TableVisible = { true, true, true, true, true, true };
            if (dt.Rows.Count <TableName.Length)
            {
                SqlCommand cmd = new SqlCommand("Delete from dbo.ChartOption",con);
                try { con.Open();cmd.ExecuteNonQuery(); }catch(Exception ex) { MessageBox.Show(ex.Message); } finally { con.Close(); }
                for (int i = 0; i < TableName.Length; i++)
                {
                    SqlCommand cmd1 = new SqlCommand("Insert Into dbo.ChartOption(Name,Typee,Showi) values('" + TableName[i] + "','" + TableType[i] + "','" + TableVisible[i] + "')", con);
                    try { con.Open(); cmd1.ExecuteNonQuery(); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    finally { con.Close(); }
                }
            }
        }
        private static void DefaultDisplaySettins()
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-LR9KKE2U\\SQLEXPRESS;Initial Catalog=WeddingApp;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.DisplaySettings", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string[] TableName = { "colStartDate", "colEndDate", "colSubject", "colLocation", "colDescription", "colPhoneNumber", "colMail", "colPrice", "colGivenPrice", "colRemainingPrice", "colFood", "colHall", "colMailCheck", "colStartDate1", "colSubject1", "colPrice1", "colGivenPrice1", "colRemainingPrice1", "colPhoneNumber1", "colMail1", "colLocation1", "colDescription1", "colHall1", "Mail", "History", "Password", "Theme", "Language" };
            bool[] TableTandF = { true, false, true, true, false, true, true, true, true, true, false, false, true, true, true, true, true, true, true, true, false, false, false, true, true, true, true, true };
            string[] TableDescription = { "Appointments", "Appointments", "Appointments", "Appointments", "Appointments", "Appointments", "Appointments", "Appointments", "Appointments", "Appointments", "Appointments", "Appointments", "Appointments", "Information", "Information", "Information", "Information", "Information", "Information", "Information", "Information", "Information", "Information", "", "-30", "", "Dark", "Turkish" };
            if (dt.Rows.Count <TableName.Length)
            {
                SqlCommand cmd = new SqlCommand("Delete from dbo.DisplaySettings", con);
                try { con.Open(); cmd.ExecuteNonQuery(); } catch (Exception ex) { MessageBox.Show(ex.Message); } finally { con.Close(); }
                for (int i = 0; i < TableName.Length; i++)
                {
                    SqlCommand cmd1 = new SqlCommand("Insert Into dbo.DisplaySettings(Name,TandF,Description) values('" + TableName[i] + "','" + TableTandF[i] + "','" + TableDescription[i] + "')", con);
                    try { con.Open();cmd1.ExecuteNonQuery(); }catch(Exception ex) { MessageBox.Show(ex.Message); }
                    finally { con.Close(); }
                }
                
            } 
        }
    }
}
