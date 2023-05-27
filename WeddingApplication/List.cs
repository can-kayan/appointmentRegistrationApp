using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeddingApplication
{
    public partial class List : DevExpress.XtraEditors.XtraForm
    {
        Form1 f1;
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-LR9KKE2U\\SQLEXPRESS;Initial Catalog=WeddingApp;Integrated Security=True");
        public List(Form1 frm1)
        {
            InitializeComponent();
            this.f1 = frm1;
        }
        private void Translate()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) * from dbo.DisplaySettings where Name='Language'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0]["Description"].ToString() == "Turkish")
            {
                lblName.Text = "Ad Soyad";
                teNullText.Properties.NullText = "Aranacak İsmi Giriniz...";
                cardView1.Columns["StartDate"].Caption = "R. Tarihi";
                cardView1.Columns["Subject"].Caption = "Ad Soyad";
                cardView1.Columns["PhoneNumber"].Caption = "Telefon Numarası";
                cardView1.Columns["Mail"].Caption = "Mail";
                cardView1.Columns["Price"].Caption = "Anlaşma Tutarı";
                cardView1.Columns["GivenPrice"].Caption = "Alınan Tutar";
                cardView1.Columns["RemainingPrice"].Caption = "Kalan Tutar";
            }
            else if (dt.Rows[0]["Description"].ToString() == "English")
            {
                lblName.Text = "Name";
                teNullText.Properties.NullText = "Enter Name to Search...";
                cardView1.Columns["StartDate"].Caption = "Appointment Date";
                cardView1.Columns["Subject"].Caption = "Name";
                cardView1.Columns["PhoneNumber"].Caption = "Phone Number";
                cardView1.Columns["Mail"].Caption = "Mail";
                cardView1.Columns["Price"].Caption = "Deal Price";
                cardView1.Columns["GivenPrice"].Caption = "Price Received";
                cardView1.Columns["RemainingPrice"].Caption = "Remaining Price";
            }
        }
        private void List_Load(object sender, EventArgs e)
        {
            Translate();
            AdminMail();

            gridControl1.DataSource = f1.appointmentsBindingSource;
        }
        private string BodyDeleteAdmin(string Name,string PhoneNumber,string DateTimes)
        {
            return Name.ToUpper() + " isimli " + PhoneNumber + " telefon numaralı kişinin "+DateTimes+" tarihli rezervasyonu silindi." ;
        }
        private string BodyDeleteUser(string Name,string DateTimes)
        {
            return "Sayın " + Name.ToUpper() + " " + DateTimes + " tarihine oluşturduğunuz rezervasyonunuz silinmiştir";
        }
        bool contactAdmin;
        private string AdminMail()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) Description,TandF from dbo.DisplaySettings where Name='Mail'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            contactAdmin = (bool)dt.Rows[0]["TandF"];
            return dt.Rows[0]["Description"].ToString();
        }
        private bool ContactAdmin
        {
            get { return contactAdmin; }
            set { contactAdmin=value;  }
        }
        DisplaySettings DS;
        private void ContactTandF(string Mail,string UniqueID,string Name,string PhoneNumber,string DateTimes)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) MailCheck from dbo.Appointments where UniqueID='" + UniqueID + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DS = new DisplaySettings(this.f1);
            if ((bool)dt.Rows[0]["MailCheck"] == true)
                DS.SendEmail(Mail, "Kayıt Silme", BodyDeleteUser(Name, DateTimes));
            if (ContactAdmin == true)
                DS.SendEmail(AdminMail(), "Kayıt Silme", BodyDeleteAdmin(Name, PhoneNumber, DateTimes));
        }
        private void TextEdit1_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.Appointments where Subject Like '%" + teNullText.Text + "%'", con);
            DataSet dt = new DataSet();
            sda.Fill(dt, "dbo.Appointments");

            gridControl1.DataSource = dt.Tables["dbo.Appointments"];
        }

        private void SimpleButton1_MouseHover(object sender, EventArgs e)
        {
            labelControl1.Width = 302;
        }
        private void SimpleButton1_MouseLeave(object sender, EventArgs e)
        {
            labelControl1.Width = 35;
        }
        public bool ShowDelete=false;
        public void HistoryInsert(string process, string Name, string PhoneNumber, string RemainingPrice)
        {
            SqlCommand cmd = new SqlCommand("Insert into dbo.History(Process,DateTime,Name,PhoneNumber,RemainingPrice) values (@Process,@DateTime,@Name,@PhoneNumber,@RemainingPrice)", con);
            cmd.Parameters.AddWithValue("@Process", process);
            cmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            cmd.Parameters.AddWithValue("@RemainingPrice", Convert.ToInt32(RemainingPrice));
            try
            {
                con.Open(); cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            finally { con.Close(); }
        }
        private void CardView1_DoubleClick(object sender, EventArgs e)
        {
            string SelectingID = cardView1.GetFocusedRowCellValue("UniqueID").ToString();
            DateTime SelectingDate = Convert.ToDateTime(cardView1.GetFocusedRowCellValue("StartDate"));
            string SelectingSubject = cardView1.GetFocusedRowCellValue("Subject").ToString();
            string SelectingPhoneNumber = cardView1.GetFocusedRowCellValue("PhoneNumber").ToString();
            string SelectingRemainingPrice = cardView1.GetFocusedRowCellValue("RemainingPrice").ToString();
            string SelectingMail = cardView1.GetFocusedRowCellValue("Mail").ToString();
            if (ShowDelete == true)
            {
                //ContactTandF(SelectingMail, SelectingID, SelectingSubject, SelectingPhoneNumber, SelectingDate.ToShortDateString());
                SqlCommand cmd1 = new SqlCommand("Delete from dbo.Appointments where UniqueID='" + SelectingID + "'", con);
                try
                {
                    con.Open(); cmd1.ExecuteNonQuery();
                }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally
                {
                    con.Close();
                }
                SqlCommand cmd2 = new SqlCommand("Delete from dbo.Information where ID='" + SelectingID + "'", con);
                try
                {
                    con.Open(); cmd2.ExecuteNonQuery();
                }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }

                finally
                {
                    con.Close();
                    HistoryInsert("Kayıt Silindi", SelectingSubject, SelectingPhoneNumber, SelectingRemainingPrice);
                    f1.DataRefres();
                    gridControl1.DataSource = null;
                    SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.Appointments", con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "dbo.Appointments");
                    gridControl1.DataSource = ds.Tables[0];
                    f1.DefaultValues();
                    f1.AppointmentCount();
                    f1.PriceRGivn();
                    f1.ShowDefaultChartControl();
                    f1.GriID();
                }
                OutlookAppointmentForm oaf = new OutlookAppointmentForm();
                oaf.DeleteButton();
                f1.appointmentsTableAdapter.Fill(f1.weddingAppDataSet.Appointments);
                
                f1.UpdateTableAdapter();
            }
            else
            {
                f1.schedulerControl1.SelectedAppointments.Clear();
                Appointment apt = null;

                int iD = -1;
                for (int i = 0; i < f1.schedulerStorage1.Appointments.Count; i++)
                {
                    if (f1.schedulerStorage1.Appointments[i].Start.ToString() == SelectingDate.ToString() && f1.schedulerStorage1.Appointments[i].Subject.ToString() == SelectingSubject && f1.schedulerStorage1.Appointments[i].CustomFields["PhoneNumber"].ToString() == SelectingPhoneNumber)
                    {
                        iD = i;
                    }
                }
                if (iD != -1)
                    apt = f1.schedulerControl1.DataStorage.Appointments[iD];
                if (apt != null)
                    f1.schedulerControl1.SelectedAppointments.Add(apt);
                if (f1.schedulerControl1.SelectedAppointments.Count > 0)
                {
                    apt = f1.schedulerControl1.SelectedAppointments[0];
                }
                else
                {
                    apt = f1.schedulerControl1.DataStorage.CreateAppointment(AppointmentType.Normal);
                    apt.Start = f1.schedulerControl1.SelectedInterval.Start;
                    apt.End = f1.schedulerControl1.SelectedInterval.End;
                }
                f1.schedulerControl1.ShowEditAppointmentForm(apt);
                
            }
            
        }
        private void List_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
