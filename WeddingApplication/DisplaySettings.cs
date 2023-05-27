using DevExpress.LookAndFeel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppApi;

namespace WeddingApplication
{
    public partial class DisplaySettings : DevExpress.XtraEditors.XtraForm
    {
        Form1 f1;
        
        string[] AppointmentsColumns = { "colStartDate", "colEndDate", "colSubject", "colLocation", "colDescription", "colPhoneNumber", "colMail", "colPrice", "colGivenPrice", "colRemainingPrice", "colFood", "colHall","colMailCheck" };
        string[] InformationColumns = { "colStartDate1", "colSubject1", "colPrice1", "colGivenPrice1", "colRemainingPrice1", "colPhoneNumber1", "colMail1", "colLocation1", "colDescription1", "colHall1" };
        public DisplaySettings(Form1 frm1)
        {
            InitializeComponent();
            f1 = frm1;
            
        }
        public string MailAdmin()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select top(1) Description from dbo.DisplaySettings where Name='Mail'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt.Rows[0]["Description"].ToString();
        }
        private int HallCount()
        {
            int ID;
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.HallCustom", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("Update dbo.HallCustom set ID='" + (i + 1) + "' where UniqueID='" + dt.Rows[i]["UniqueID"] + "'",f1.con);
                try { f1.con.Open(); cmd.ExecuteNonQuery(); }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { f1.con.Close(); }
            }
            SqlDataAdapter sad = new SqlDataAdapter("Select Top(1) ID from dbo.HallCustom order by ID desc", f1.con);
            DataTable td = new DataTable();
            sad.Fill(td);
            ID = 1;
            if(dt.Rows.Count>0)
                ID = Convert.ToInt32(td.Rows[0]["ID"]) + 1;
            return ID;
        }
        private void Save_Click(object sender, EventArgs e)
        {
            SystemSettings();
            ContactSettings();
            TableSettings();
            GraphicsSettings();
           
            HistorySettings();
            DialogResult retturn= DevExpress.XtraEditors.XtraMessageBox.Show(ExitMessage, InformationMessage, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (retturn == DialogResult.OK)
                Application.Restart();
            else if (retturn != DialogResult.OK) { }
               
        }
        private void HistorySettings()
        {
            string timeToDelete = string.Empty;
            if (thirty.Checked == true)
                timeToDelete = "-30";
            else if (Sixty.Checked == true)
                timeToDelete = "-60";
            else if (Ninety.Checked == true)
                timeToDelete = "-90";
            SqlCommand cmd = new SqlCommand("Update dbo.DisplaySettings set Description='"+timeToDelete+"' where Name='History'", f1.con);
            try { f1.con.Open();cmd.ExecuteNonQuery(); }
            catch(Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            finally { f1.con.Close(); }
        }
        
        private void HallSettings()
        {
            if(string.IsNullOrEmpty(BarHallColor.Color.ToArgb().ToString()) || string.IsNullOrEmpty(BarHallName.Text) || string.IsNullOrEmpty(BarPersonCount.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Hatta");
            }
            else
            {
                var Color = BarHallColor.Color;
                string C = Color.ToString();
                SqlCommand cmd = new SqlCommand("insert into HallCustom(ID,Name,Color,PersonCount) values (@HallNumber,@HallName,@HallColor,@HallPersonCount)", f1.con);
                cmd.Parameters.AddWithValue("@HallNumber", HallCount());
                cmd.Parameters.AddWithValue("@HallName", BarHallName.Text.ToString());
                cmd.Parameters.AddWithValue("@HallColor", C);
                cmd.Parameters.AddWithValue("@HallPersonCount", BarPersonCount.Text.ToString());
                try { f1.con.Open(); cmd.ExecuteNonQuery(); }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show("Hata :" + ex); }
                finally { f1.con.Close(); }
            }
        }
        private void TableSettings()
        {
            object[] AppoCheckBox = { AppoStartDate.Checked, AppoEndDate.Checked, AppoSubject.Checked, AppoLocation.Checked, AppoDescription.Checked, AppoPhoneNumber.Checked, AppoMail.Checked, AppoPrice.Checked, AppoGiven.Checked, AppoRemaining.Checked, AppoFood.Checked, AppoHall.Checked,AppoMailCheck.Checked };
            object[] InfoCheckBox = { InfoDate.Checked, InfoSubject.Checked, InfoPrice.Checked, InfoGiven.Checked, InfoRemaining.Checked, InfoPhoneNumber.Checked, InfoMail.Checked, InfoLocation.Checked, InfoDescription.Checked, InfoHall.Checked };



            for (int i = 0; i < AppoCheckBox.Length; i++)
            {
                SqlCommand cmd4 = new SqlCommand("Update dbo.DisplaySettings set TandF='" + AppoCheckBox[i] + "' where Description='Appointments' and Name='" + AppointmentsColumns[i] + "'", f1.con);
                try { f1.con.Open(); cmd4.ExecuteNonQuery(); }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { f1.con.Close(); }
            }
            for (int i = 0; i < InfoCheckBox.Length; i++)
            {
                SqlCommand cmd5 = new SqlCommand("Update dbo.DisplaySettings set TandF='" + InfoCheckBox[i] + "' where Description='Information' and Name='" + InformationColumns[i] + "'", f1.con);
                try { f1.con.Open(); cmd5.ExecuteNonQuery(); }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { f1.con.Close(); }
            }

            
        }
        private void ContactSettings()
        {
            SqlCommand cmd3 = new SqlCommand("Update dbo.DisplaySettings set TandF='" + CheckClick.Checked + "', Description='" + Mail.Text + "' where Name='Mail'", f1.con);
            try { f1.con.Open(); cmd3.ExecuteNonQuery(); }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            finally { f1.con.Close(); }
            SqlCommand cmd7 = new SqlCommand("Update dbo.DisplaySettings set TandF='" + CheckClick.Checked + "', Description='" + Password.Text + "' where Name='Password' ", f1.con);
            try { f1.con.Open(); cmd7.ExecuteNonQuery(); }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            finally { f1.con.Close(); }
        }
        private void SystemSettings()
        {
            string Tema = "Dark";
            if (RadioDark.Checked == false)
                Tema = "Light";
            SqlCommand cmd1 = new SqlCommand("Update dbo.DisplaySettings set TandF='" + RadioDark.Checked + "', Description='" + Tema + "' where Name='Theme'", f1.con);
            try { f1.con.Open(); cmd1.ExecuteNonQuery(); }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            finally { f1.con.Close(); }
            string Language = "Turkish";
            if (RadioTurkish.Checked == false)
                Language = "English";
            SqlCommand cmd2 = new SqlCommand("Update dbo.DisplaySettings set TandF='" + RadioTurkish.Checked + "', Description='" + Language + "' where Name='Language'", f1.con);
            try { f1.con.Open(); cmd2.ExecuteNonQuery(); }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            finally { f1.con.Close(); }
        }
        private void GraphicsSettings()
        {
            f1.con.Open();
            SqlCommand cmd1 = new SqlCommand("Update dbo.ChartOption set Typee ='" + CBERTable.Text + "', Showi ='" + Convert.ToBoolean(CERtable.Checked.ToString()) + "' where Name='" + "Randevu Tablosu" + "'", f1.con);
            try { cmd1.ExecuteNonQuery(); }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            f1.con.Close();
            f1.con.Open();
            SqlCommand cmd2 = new SqlCommand("Update dbo.ChartOption set Typee ='" + CBEMRTable.Text + "', Showi ='" + Convert.ToBoolean(CEMRtable.Checked.ToString()) + "' where Name='" + "Aylık Randevu" + "'", f1.con);
            try { cmd2.ExecuteNonQuery(); }

            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            f1.con.Close();
            f1.con.Open();
            SqlCommand cmd3 = new SqlCommand("Update dbo.ChartOption set Typee ='" + CBEYRTable.Text + "', Showi ='" + Convert.ToBoolean(CEYRtable.Checked.ToString()) + "' where Name='" + "Yıllık Randevu" + "'", f1.con);
            try { cmd3.ExecuteNonQuery(); }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            f1.con.Close();
            f1.con.Open();
            SqlCommand cmd4 = new SqlCommand("Update dbo.ChartOption set Typee ='" + CBERGTable.Text + "', Showi ='" + Convert.ToBoolean(CERGPricetable.Checked.ToString()) + "' where Name='" + "Alınan-Kalan" + "'", f1.con);
            try { cmd4.ExecuteNonQuery(); }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            f1.con.Close();
            f1.con.Open();
            SqlCommand cmd5 = new SqlCommand("Update dbo.ChartOption set Typee ='" + CBEMPriceTable.Text + "', Showi ='" + Convert.ToBoolean(CEMPricetable.Checked.ToString()) + "' where Name='" + "Aylık Tutar" + "'", f1.con);
            try { cmd5.ExecuteNonQuery(); }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            f1.con.Close();
            f1.con.Open();
            SqlCommand cmd6 = new SqlCommand("Update dbo.ChartOption set Typee ='" + CBEYPriceTable.Text + "', Showi ='" + Convert.ToBoolean(CEYPricetable.Checked.ToString()) + "' where Name='" + "Yıllık Tutar" + "'", f1.con);
            try { cmd6.ExecuteNonQuery(); }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            f1.con.Close();
            f1.ShowDefaultChartControl();
        }
        private void AppointmentDisplaySettingsLoad()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Name,TandF from dbo.DisplaySettings where Description='Appointments'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < AppointmentsColumns.Length; i++)
            {
                f1.gridView1.Columns.ColumnByName(AppointmentsColumns[i]).Visible = (bool)dt.Rows[i]["TandF"];
                if(dt.Rows[i]["Name"].ToString()==AppointmentsColumns[0])
                    AppoStartDate.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[1])
                    AppoEndDate.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[2])
                    AppoSubject.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[3])
                    AppoLocation.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[4])
                    AppoDescription.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[5])
                    AppoPhoneNumber.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[6])
                    AppoMail.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[7])
                    AppoPrice.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[8])
                    AppoGiven.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[9])
                    AppoRemaining.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[10])
                    AppoFood.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[11])
                    AppoHall.Checked = (bool)dt.Rows[i]["TandF"];
                else if (dt.Rows[i]["Name"].ToString() == AppointmentsColumns[12])
                    AppoMailCheck.Checked = (bool)dt.Rows[i]["TandF"];
            }
        }
        private void InformationDisplaySettingsLoad()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Name,TandF from dbo.DisplaySettings where Description='Information'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < InformationColumns.Length; i++)
            {
                
                f1.gridView2.Columns.ColumnByName(InformationColumns[i]).Visible = (bool)dt.Rows[i]["TandF"];
                if (dt.Rows[i]["Name"].ToString() == InformationColumns[0])
                    InfoDate.Checked = (bool)dt.Rows[i]["TandF"];
                if (dt.Rows[i]["Name"].ToString() == InformationColumns[1])
                    InfoSubject.Checked = (bool)dt.Rows[i]["TandF"];
                if (dt.Rows[i]["Name"].ToString() == InformationColumns[2])
                    InfoPrice.Checked = (bool)dt.Rows[i]["TandF"];
                if (dt.Rows[i]["Name"].ToString() == InformationColumns[3])
                    InfoGiven.Checked = (bool)dt.Rows[i]["TandF"];
                if (dt.Rows[i]["Name"].ToString() == InformationColumns[4])
                    InfoRemaining.Checked = (bool)dt.Rows[i]["TandF"];
                if (dt.Rows[i]["Name"].ToString() == InformationColumns[5])
                    InfoPhoneNumber.Checked = (bool)dt.Rows[i]["TandF"];
                if (dt.Rows[i]["Name"].ToString() == InformationColumns[6])
                    InfoMail.Checked = (bool)dt.Rows[i]["TandF"];
                if (dt.Rows[i]["Name"].ToString() == InformationColumns[7])
                    InfoLocation.Checked = (bool)dt.Rows[i]["TandF"];
                if (dt.Rows[i]["Name"].ToString() == InformationColumns[8])
                    InfoDescription.Checked = (bool)dt.Rows[i]["TandF"];
                if (dt.Rows[i]["Name"].ToString() == InformationColumns[9])
                    InfoHall.Checked = (bool)dt.Rows[i]["TandF"];
            }
                
        }
        public void LoadGridView()
        {
            AppointmentDisplaySettingsLoad();InformationDisplaySettingsLoad(); Theme();
        }
        private void Theme()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.DisplaySettings where Name='Theme'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0]["TandF"].ToString() == "False")
                RadioLight.Checked = true;
            else if (dt.Rows[0]["TandF"].ToString() == "True")
                RadioDark.Checked = true;
            if (RadioDark.Checked == true)
                UserLookAndFeel.Default.SetSkinStyle(SkinStyle.VisualStudio2013Dark);
            else if (RadioDark.Checked == false)
                UserLookAndFeel.Default.SetSkinStyle(SkinStyle.VisualStudio2013Light);
        }
        private string GMail()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) Description from dbo.DisplaySettings where Name='Mail'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt.Rows[0]["Description"].ToString();
        }
        private string GPassword()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) Description from dbo.DisplaySettings where Name='Password'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt.Rows[0]["Description"].ToString();
        }
        private bool CheckCL()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) TandF from dbo.DisplaySettings where Name='Mail' or Name='Password'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return (bool)(dt.Rows[0]["TandF"]);
        }
        private void Language()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select TandF from dbo.DisplaySettings where Name='Language'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0]["TandF"].ToString() == "True")
                RadioTurkish.Checked = true;
            if (dt.Rows[0]["TandF"].ToString() == "False")
                RadioEnglish.Checked = true;
        }
        string ExitMessage = string.Empty;
        string InformationMessage = string.Empty;
        string historyCleared = string.Empty;
        private void Translate()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) * from dbo.DisplaySettings where Name='Language'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0]["Description"].ToString() == "Turkish")
            {
                historyCleared = "Geçmiş temizlendi";
                ExitMessage = "Kaydetmek istediğinize emin misiniz ?\nUygulama Kendini Yeniden Başlatacak";
                InformationMessage = "Bilgilendirme";
                Systemss.Caption = "Sistem";
                Communication.Caption = "Bilgilendirme";
                Table.Caption = "Tablo";
                Grapichs.Caption = "Analiz & Grafik";
                Hallsss.Caption = "Salon";
                ///////////
                Save.Text = "Kaydet";
                lblTheme.Text = "Tema Rengi";
                lblLanguage.Text = "Uygulama Dili";
                lblRestartHistory.Text = "Geçmiş Sıfırlama";
                RadioDark.Text = "Koyu";
                RadioLight.Text = "Açık";
                RadioTurkish.Text = "Türkçe";
                RadioEnglish.Text = "İngilizce";
                thirty.Text = "30 Gün";
                Sixty.Text = "60 Gün";
                Ninety.Text = "90 Gün";
                NowDelete.Text = "Şimdi";
                //////
                lblContact.Text = "Bilgilendirme";
                lblMail.Text = "Mail Adresi";
                lblPassword.Text = "Şifre";
                btnHelp.Text = "Yardım";
                /////
                groupControl1.Text = "Randevu Tablosu";
                AppoStartDate.Text = "R.Başlangıç T.";
                AppoEndDate.Text = "R.Bitiş T.";
                AppoSubject.Text = "Ad Soyad";
                AppoLocation.Text = "Kişi Sayısı";
                AppoDescription.Text = "Açıklama";
                AppoPhoneNumber.Text = "Telefon Numarası";
                AppoMail.Text = "Mail";
                AppoPrice.Text = "Anlaşma Tutarı";
                AppoGiven.Text = "Alınan Tutar";
                AppoRemaining.Text = "Kalan Tutar";
                AppoFood.Text = "Yemek";
                AppoHall.Text = "Salon";
                AppoMailCheck.Text = "Bilgilendirme";
                //////
                groupControl2.Text = "İşlem Tablosu";
                InfoDate.Text = "Tarih";
                InfoSubject.Text = "Ad Soyad";
                InfoPrice.Text = "Anlaşma Tutarı";
                InfoGiven.Text = "Alınan Tutar";
                InfoRemaining.Text = "Kalan Tutar";
                InfoPhoneNumber.Text = "Telefon Numarası";
                InfoMail.Text = "Mail";
                InfoLocation.Text = "Kişi Sayısı";
                InfoDescription.Text = "Açıklama";
                InfoHall.Text = "Salon";
                ///////
                lblMRTable.Text = "R. Sayılarının Aylara Dağılımı";
                lblYRTable.Text = "R. Sayılarının Yıllara Dağılımı";
                lblRGPtable.Text = "Alınan/Kalan Fiyat Analizi";
                lblMPriceTable.Text = "Fiyatların Aylara Dağılımı";
                lblYPriceTable.Text = "Fiyatların Yıllara Dağılımı";
                string Shows = "Göster";
                CEMRtable.Text = Shows;
                CEYRtable.Text = Shows;
                CERGPricetable.Text = Shows;
                CEMPricetable.Text = Shows;
                CEYPricetable.Text = Shows;
                ///////
                lblHallName.Text = "Salon Adı";
                lblHallColor.Text = "Renk";
                lblPersonCount.Text = "Kişi Sayısı";
                cardView1.Columns["Name"].Caption = "Salon Adı";
                cardView1.Columns["PersonCount"].Caption = "Kişi Sayısı";
                //////
                cardView1.Columns["Name"].Caption = "Salon Adı:";
                cardView1.Columns["PersonCount"].Caption = "Kişi Sayısı";
                btnAdd.Text = "Ekle";
            }
            else if (dt.Rows[0]["Description"].ToString() == "English")
            {
                ExitMessage = "Are you sure you want to save?\nApplication Will Restart Itself";
                InformationMessage = "Information";
                Systemss.Caption = "System";
                Communication.Caption = "Communication";
                Table.Caption = "Table";
                Grapichs.Caption = "Analysis & Chart";
                Hallsss.Caption = "Hall";
                ///////////
                Save.Text = "Save";
                lblTheme.Text = "Theme Color";
                lblLanguage.Text = "Application Language";
                lblRestartHistory.Text = "Reset History";
                RadioDark.Text = "Dark";
                RadioLight.Text = "Light";
                RadioTurkish.Text = "Turkish";
                RadioEnglish.Text = "English";
                thirty.Text = "30 Day";
                Sixty.Text = "60 Day";
                Ninety.Text = "90 Day";
                NowDelete.Text = "Now";
                //////
                lblContact.Text = "Communication";
                lblMail.Text = "Mail Address";
                lblPassword.Text = "Password";
                btnHelp.Text = "Help";
                /////
                groupControl1.Text = "Appointment Table";
                AppoStartDate.Text = "A.Start Date";
                AppoEndDate.Text = "A.End Date";
                AppoSubject.Text = "Name";
                AppoLocation.Text = "Person Count";
                AppoDescription.Text = "Description";
                AppoPhoneNumber.Text = "Phone Number";
                AppoMail.Text = "Mail";
                AppoPrice.Text = "Deal Price";
                AppoGiven.Text = "Price Received";
                AppoRemaining.Text = "Remaining Price";
                AppoFood.Text = "Food";
                AppoHall.Text = "Hall";
                AppoMailCheck.Text = "Communication";
                //////
                groupControl2.Text = "Transaction Table";
                InfoDate.Text = "Date";
                InfoSubject.Text = "Name";
                InfoPrice.Text = "Deal Price";
                InfoGiven.Text = "Price Received";
                InfoRemaining.Text = "Remaining Price";
                InfoPhoneNumber.Text = "Phone Number";
                InfoMail.Text = "Mail";
                InfoLocation.Text = "Person Count";
                InfoDescription.Text = "Description";
                InfoHall.Text = "Hall";
                ///////
                lblFRTable.Text = "To Do/Finished/Total Appointment Analysis";
                lblMRTable.Text = "Distribution of Appointments by Month";
                lblYRTable.Text = "Distribution of Appointments by Year";
                lblRGPtable.Text = "Received/Remaining Price Analysis";
                lblMPriceTable.Text = "Distribution of Prices by Months";
                lblYPriceTable.Text = "Distribution of Prices by Years";
                string Shows = "Show";
                CERtable.Text = Shows;
                CEMRtable.Text = Shows;
                CEYRtable.Text = Shows;
                CERGPricetable.Text = Shows;
                CEMPricetable.Text = Shows;
                CEYPricetable.Text = Shows;
                ///////
                lblHallName.Text = "Hall Name";
                lblHallColor.Text = "Color";
                lblPersonCount.Text = "Person Count";
                cardView1.Columns["Name"].Caption = "Hall Name";
                cardView1.Columns["PersonCount"].Caption = "Person Count";
                //////
                cardView1.Columns["Name"].Caption = "Hall Name:";
                cardView1.Columns["PersonCount"].Caption = "Person Count";
                btnAdd.Text = "Add";
                historyCleared = "History cleared";
            }

        }
        private void LoadChartSettings()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.ChartOption", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i]["Name"].ToString()== "Randevu Tablosu")
                {
                    CBERTable.Text = dt.Rows[i]["Typee"].ToString();
                    CERtable.Checked = (bool)dt.Rows[i]["Showi"];
                }
                else if(dt.Rows[i]["Name"].ToString()=="Aylık Randevu")
                {
                    CBEMRTable.Text = dt.Rows[i]["Typee"].ToString();
                    CEMRtable.Checked = (bool)dt.Rows[i]["Showi"];
                }
                else if (dt.Rows[i]["Name"].ToString() == "Yıllık Randevu")
                {
                    CBEYRTable.Text = dt.Rows[i]["Typee"].ToString();
                    CEYRtable.Checked = (bool)dt.Rows[i]["Showi"];
                }
                else if (dt.Rows[i]["Name"].ToString() == "Alınan-Kalan")
                {
                    CBERGTable.Text = dt.Rows[i]["Typee"].ToString();
                    CERGPricetable.Checked = (bool)dt.Rows[i]["Showi"];
                }
                else if (dt.Rows[i]["Name"].ToString() == "Aylık Tutar")
                {
                    CBEMPriceTable.Text = dt.Rows[i]["Typee"].ToString();
                    CEMPricetable.Checked = (bool)dt.Rows[i]["Showi"];
                }
                else if (dt.Rows[i]["Name"].ToString() == "Yıllık Tutar")
                {
                    CBEYPriceTable.Text = dt.Rows[i]["Typee"].ToString();
                    CEYPricetable.Checked = (bool)dt.Rows[i]["Showi"];
                }

            }
        }
        private void LoadCBEChartControlType()
        {
            List<string> Types = new List<string> { "Bar", "Line", "Step Line", "Spline", "Area", "Spline Area", "Pie", "Doughnut" };
            foreach (string a in Types)
            {
                CBERTable.Properties.Items.Add(a);
                CBEMRTable.Properties.Items.Add(a);
                CBEYRTable.Properties.Items.Add(a);
                CBERGTable.Properties.Items.Add(a);
                CBEMPriceTable.Properties.Items.Add(a);
                CBEYPriceTable.Properties.Items.Add(a);
            }
        }
        private void LoadHistorySett()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) * from dbo.DisplaySettings where Name='History'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string Rows = dt.Rows[0]["Description"].ToString();
            if (Rows == "-30")
                thirty.Checked = true;
            else if (Rows == "-60")
                Sixty.Checked = true;
            else if (Rows == "-90")
                Ninety.Checked = true;
        }
        private void DisplaySettings_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'weddingAppDataSet.HallCustom' table. You can move, or remove it, as needed.
            this.hallCustomTableAdapter.Fill(this.weddingAppDataSet.HallCustom);
            Translate();
            if (CheckClick.Checked == false)
            { Mail.Enabled = false; Password.Enabled = false; }
            Theme();
            LoadCBEChartControlType();
            LoadGridView();
            LoadChartSettings();
            LoadHistorySett();
            CheckClick.Checked = CheckCL();
            Mail.Text = GMail();
            Password.Text = GPassword();
            Language();

        }
        private bool InternetKontrol()
        {
            try
            {
                System.Net.Sockets.TcpClient kontrol_client = new System.Net.Sockets.TcpClient("www.google.com.tr", 80);
                kontrol_client.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        private void CheckClick_CheckedChanged(object sender, EventArgs e)
        {
            bool kontrol = InternetKontrol();
           
            if (kontrol == true)
            {
                if (CheckClick.Checked.ToString() == "True")
                {
                    Mail.Enabled = true; Password.Enabled = true;

                }
                if (CheckClick.Checked.ToString() == "False")
                { Mail.Enabled = false; Password.Enabled = false; }
            }
            else
            {
                labelControl1.Text="No Internet!";
                CheckClick.Checked = false;
            }
        }
        private  string EMail()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.DisplaySettings where Name='Mail'", f1.con);
            DataTable dT = new DataTable();
            sda.Fill(dT);
            string Maile = dT.Rows[0]["Description"].ToString();
            return Maile;
        }
        private string EPassword()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.DisplaySettings where Name='Password'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string Passworde = dt.Rows[0]["Description"].ToString();
            return Passworde;
        }
        public string SendEmail(string toMail ,string subject, string body)
        {
            string result = "Message Sent Successfully..!!";
            string senderID = EMail();
            string senderPassword = EPassword();
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(senderID, toMail,subject, body);
                smtp.Send(message);
        }
            catch
            {
                result = "Error sending email.!!!";
            }

            return result;
        }
        
        Help HP;
        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            HP = new Help(this.f1);
            try { HP.navigationFrame1.SelectedPage = HP.navigationPage6; HP.ShowDialog();} finally { HP.Dispose(); }
        }

        private void SimpleButton3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from dbo.History", f1.con);
            try { f1.con.Open(); cmd.ExecuteNonQuery(); }
            catch(Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            finally { f1.con.Close(); DevExpress.XtraEditors.XtraMessageBox.Show(historyCleared); }
        }
        void LoadHallData()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.HallCustom", f1.con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "dbo.HallCustom");
            gridControl1.DataSource = ds.Tables[0];
        }
        private void CardView1_DoubleClick(object sender, EventArgs e)
        {
            string iD = cardView1.GetFocusedRowCellValue("UniqueID").ToString();
            SqlCommand cmd = new SqlCommand("Delete from dbo.HallCustom where UniqueID='" + iD + "'", f1.con);
            try { f1.con.Open(); cmd.ExecuteNonQuery(); }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            finally { f1.con.Close(); LoadHallData(); f1.HallCustom(); }

            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.HallCustom", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlCommand cmd1 = new SqlCommand("Update dbo.HallCustom set ID='" + (i + 1) + "' where UniqueID='" + dt.Rows[i]["UniqueID"] + "'", f1.con);
                try { f1.con.Open(); cmd1.ExecuteNonQuery(); }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { f1.con.Close(); }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            HallSettings();
            
            f1.HallCustom();
            BarHallName.Text = string.Empty;
            BarPersonCount.Text = string.Empty;
            BarHallColor.Text = string.Empty;
            LoadHallData();
        }
    }
}
