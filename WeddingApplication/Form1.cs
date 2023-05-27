using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraScheduler;
using System.Data.Entity;
namespace WeddingApplication
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        public Form1()
        {
            InitializeComponent();
        }
        public SqlConnection con = new SqlConnection("Data Source=LAPTOP-LR9KKE2U\\SQLEXPRESS;Initial Catalog=WeddingApp;Integrated Security=True");
        History HY;
        public void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'weddingAppDataSet.Resources' table. You can move, or remove it, as needed.
            this.resourcesTableAdapter.Fill(this.weddingAppDataSet.Resources);
            // TODO: This line of code loads data into the 'weddingAppDataSet.Information' table. You can move, or remove it, as needed.
            this.informationTableAdapter.Fill(this.weddingAppDataSet.Information);
            // TODO: This line of code loads data into the 'weddingAppDataSet.Appointments' table. You can move, or remove it, as needed.
            this.appointmentsTableAdapter.Fill(this.weddingAppDataSet.Appointments);
            Translate();
            // TODO: This line of code loads data into the 'weddingAppDataSet.HallCustom' table. You can move, or remove it, as needed.
            this.hallCustomTableAdapter.Fill(this.weddingAppDataSet.HallCustom);
            // TODO: This line of code loads data into the 'weddingAppDataSet.Resources' table. You can move, or remove it, as needed.
            this.resourcesTableAdapter.Fill(this.weddingAppDataSet.Resources);
            // TODO: This line of code loads data into the 'weddingAppDataSet.Information' table. You can move, or remove it, as needed.
            this.informationTableAdapter.Fill(this.weddingAppDataSet.Information);
            // TODO: This line of code loads data into the 'weddingAppDataSet.Appointments' table. You can move, or remove it, as needed.
            this.appointmentsTableAdapter.Fill(this.weddingAppDataSet.Appointments);
            DefaultValues();
            AppointmentCount();
            PriceRGivn();
            ShowDefaultChartControl();
            HallCustom();
            DS = new DisplaySettings(this);
            DS.LoadGridView();
            GriID();

        }
        string[] ChartName = new string[6];
        string FRecord = string.Empty;
        string SRecord = string.Empty;
        string TRecord = string.Empty;
        string Finish = string.Empty;
        string Start = string.Empty;
        string Total = string.Empty;
        string[] Months = new string[12];
        string Monthss = string.Empty;
        string Yearss = string.Empty;
        string Given = string.Empty;
        string Remaining = string.Empty;
        string Deal = string.Empty;
        string Hall = string.Empty;
        string UpdateList = string.Empty;
        string DeleteList = string.Empty;
        string Names = string.Empty;
        string PhoneNumbers = string.Empty;
        string Mails = string.Empty;
        string Prices = string.Empty;
        string Rema = string.Empty;
        string PersonCount = string.Empty;
        string Food = string.Empty;
        string Open = string.Empty;
        string GoToToday = string.Empty;
        string NewAppo = string.Empty;
        string SalHal = string.Empty;
        private void Translate()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) * from dbo.DisplaySettings where Name='Language'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0]["Description"].ToString() == "Turkish")
            {
                Names = "Ad Soyad:  ";
                PhoneNumbers = "Telefon Numarası:  ";
                Mails = "Mail:  ";
                Prices = "Hesap Ödendi";
                Rema = "Kalan Tutar:  ";
                Food = "Yemek Olacak";
                PersonCount = "Kişi Sayısı:  ";
                Open = "Aç";
                GoToToday = "Bugün'e Git";
                NewAppo = "Yeni Kayıt";
                SalHal = "Salon";
                barButtonItem1.Caption = "Kayıt Ekle";
                barButtonItem7.Caption = "Kayıt Güncelle";
                barButtonItem3.Caption = "Kayıt Sil";
                barSubItem1.Caption = "Dışa Aktar";
                barButtonItem4.Caption = "Randevu Listesi";
                barButtonItem6.Caption = "İşlem Listesi";
                barButtonItem2.Caption = "Geçmiş İşlemler";
                barButtonItem10.Caption = "Ayarlar";
                barButtonItem8.Caption = "Yardım";
                /////////
                navigationPage1.Caption = "Kayıt Listesi";
                navigationPage2.Caption = "Takvim";
                navigationPage3.Caption = "Analiz";

                ////////
                gridView1.Columns["StartDate"].Caption = "R.Başlanıç T.";
                gridView1.Columns["EndDate"].Caption = "R. Bitiş T.";
                gridView1.Columns["Subject"].Caption = "Ad Soyad";
                gridView1.Columns["PhoneNumber"].Caption = "Telefon Numarası";
                gridView1.Columns["Mail"].Caption = "Mail";
                gridView1.Columns["MailCheck"].Caption = "Bilgilendirme";
                gridView1.Columns["Location"].Caption = "Kişi Sayısı";
                gridView1.Columns["Hall"].Caption = "Salon";
                gridView1.Columns["Food"].Caption = "Yemek";
                gridView1.Columns["Description"].Caption = "Açıklama";
                gridView1.Columns["Price"].Caption = "Anlaşma Fiyatı";
                gridView1.Columns["GivenPrice"].Caption = "Alınan Fiyat";
                gridView1.Columns["RemainingPrice"].Caption = "Kalan Fiyat";
                ////////
                gridView2.Columns["StartDate"].Caption = "Tarih";
                gridView2.Columns["Subject"].Caption = "Ad Soyad";
                gridView2.Columns["PhoneNumber"].Caption = "Telefon Numarası";
                gridView2.Columns["Mail"].Caption = "Mail";
                gridView2.Columns["Hall"].Caption = "Salon";
                gridView2.Columns["Location"].Caption = "Kişi Sayısı";
                gridView2.Columns["Description"].Caption = "Açıklama";
                gridView2.Columns["Price"].Caption = "Anlaşma Fiyatı";
                gridView2.Columns["GivenPrice"].Caption = "Alınan Fiyat";
                gridView2.Columns["RemainingPrice"].Caption = "Kalan Fiyat";
                ////////
                textEdit1.Properties.NullText = "Aranacak Adı Giriniz...";
                ///////
                ChartName[0] = "Yapılacak/Bitmiş/Toplam R. Analizi";
                ChartName[1] = "R. Sayılarının Aylara Dağılımı";
                ChartName[2] = "R. Sayılarının Yıllara Dağılımı";
                ChartName[3] = "Alınan/Kalan Fiyat Analizi";
                ChartName[4] = "Aylık Fiyat Analizi";
                ChartName[5] = "Yıllık Fiyat Analizi";
                //////////////
                FRecord = "Bitmiş Randevu";
                SRecord = "Yapılacak Randevu";
                TRecord = "Toplam Randevu";
                Finish = "Bitmiş";
                Start = "Yapılacak";
                Total = "Toplam";
                Months[0] = "Ocak";
                Months[1] = "Şubat";
                Months[2] = "Mart";
                Months[3] = "Nisan";
                Months[4] = "Mayıs";
                Months[5] = "Haziran";
                Months[6] = "Temmuz";
                Months[7] = "Ağustos";
                Months[8] = "Eylül";
                Months[9] = "Ekim";
                Months[10] = "Kasım";
                Months[11] = "Aralık";
                Monthss = "Aylar";
                Yearss = "Yıllar";
                Given = "Alınan";
                Remaining = "Kalan";
                Deal = "Anlaşma";
                Hall = "Hepsi";
                DeleteList = "Silmek istediiğiniz randevuya çift tıklayın.";
                UpdateList = "Güncellemek istediiğiniz randevuya çift tıklayın.";
            }
            else if (dt.Rows[0]["Description"].ToString() == "English")
            {
                Names = "Name:  ";
                PhoneNumbers = "Phone Number:  ";
                Mails = "Mail:  ";
                Prices = "account paid";
                Rema = "Remaining Price:  ";
                PersonCount = "Person Count:  ";
                Food = "will have dining";
                Open = "Open";
                GoToToday = "Go To Today";
                NewAppo = "New Appointment";
                SalHal = "Hall";
                barButtonItem1.Caption = "Add Record";
                barButtonItem7.Caption = "Update Record";
                barButtonItem3.Caption = "Delete Record";
                barSubItem1.Caption = "Export";
                barButtonItem4.Caption = "Appointment List";
                barButtonItem6.Caption = "Transaction List";
                barButtonItem2.Caption = "Past Transactions";
                barButtonItem10.Caption = "Settings";
                barButtonItem8.Caption = "Help";
                /////////
                navigationPage1.Caption = "Appointment List";
                navigationPage2.Caption = "Calendar";
                navigationPage3.Caption = "Analysis";
                officeNavigationBar1.Items[0].Text= "Appointment List";
                officeNavigationBar1.Items[1].Text = "Calendar";
                officeNavigationBar1.Items[2].Text = "Analysis";
                ////////
                gridView1.Columns["StartDate"].Caption = "A.Start Date";
                gridView1.Columns["EndDate"].Caption = "A.End Date";
                gridView1.Columns["Subject"].Caption = "Name";
                gridView1.Columns["PhoneNumber"].Caption = "Phone Number";
                gridView1.Columns["Mail"].Caption = "Mail";
                gridView1.Columns["MailCheck"].Caption = "Communication";
                gridView1.Columns["Location"].Caption = "Person Count";
                gridView1.Columns["Hall"].Caption = "Hall";
                gridView1.Columns["Food"].Caption = "Food";
                gridView1.Columns["Description"].Caption = "Description";
                gridView1.Columns["Price"].Caption = "Deal Price";
                gridView1.Columns["GivenPrice"].Caption = "Price Received";
                gridView1.Columns["RemainingPrice"].Caption = "RemainingPrice";
                ////////
                gridView2.Columns["StartDate"].Caption = "Date";
                gridView2.Columns["Subject"].Caption = "Name";
                gridView2.Columns["PhoneNumber"].Caption = "Phone Number";
                gridView2.Columns["Mail"].Caption = "Mail";
                gridView2.Columns["Hall"].Caption = "Hall";
                gridView2.Columns["Location"].Caption = "Person Count";
                gridView2.Columns["Description"].Caption = "Description";
                gridView2.Columns["Price"].Caption = "Deal Price";
                gridView2.Columns["GivenPrice"].Caption = "Price Received";
                gridView2.Columns["RemainingPrice"].Caption = "Remaining Price";
                ////////
                textEdit1.Properties.NullText = "Enter Search Name...";
                ///////
                ChartName[0] = "To Do/Finished/Total Appointment Analysis";
                ChartName[1] = "Distribution of Appointments by Month";
                ChartName[2] = "Distribution of Appointments by Year";
                ChartName[3] = "Received/Remaining Price Analysis";
                ChartName[4] = "Distribution of Prices by Months";
                ChartName[5] = "Distribution of Prices by Years";
                ////////////
                FRecord = "Finished Appointment";
                SRecord = "Unfinished Appointment";
                TRecord = "Total Appointment";
                Finish = "Finished";
                Start = "Unfinished";
                Total = "Total";
                Months[0] = "January";
                Months[1] = "February";
                Months[2] = "March";
                Months[3] = "April";
                Months[4] = "May";
                Months[5] = "June";
                Months[6] = "July";
                Months[7] = "August";
                Months[8] = "September";
                Months[9] = "October";
                Months[10] = "November";
                Months[11] = "December";
                Monthss = "Months";
                Yearss = "Years";
                Given = "received";
                Remaining = "Remainder";
                Deal = "Agreement";
                Hall = "All";
                DeleteList= "Double-click the appointment you want to delete.";
                UpdateList = "Double-click the appointment you want to update.";
            }
        }
        private void SchedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            DevExpress.XtraScheduler.SchedulerControl scheduler = ((DevExpress.XtraScheduler.SchedulerControl)(sender));
            WeddingApplication.OutlookAppointmentForm form = new WeddingApplication.OutlookAppointmentForm(scheduler, e.Appointment, e.OpenRecurrenceForm, this);
            try
            {
                e.DialogResult = form.ShowDialog();
                e.Handled = true;
            }
            finally
            {
                form.Dispose();
            }
        }
        private int totalAppo ;
        private int AppointmentFınısh;
        private int AppointmentsS;
        private List<int> totalMonth;
        private List<int> totalYear;
        private List<string> Month;
        private List<string> Year;
        private int TotalPrice ;
        private int TotalGiven ;
        private int TotalRemaining ;
        private List<int> MonthPricePoint ;
        private List<int> MonthRPoint ;
        private List<int> MonthGPoint ;
        private List<int> YearPricePoint;
        private List<int> YearRPoint;
        private List<int> YearGPoint;
        public void DefaultValues()
        {
            totalAppo = 0;AppointmentFınısh = 0; AppointmentsS = 0;
            totalMonth = new List<int> { 0, 0, 0, 0, 0 };
            totalYear = new List<int> { 0, 0, 0, 0, 0 };
            Month = new List<string> { DateTime.Now.AddMonths(-3).Month.ToString() + " " + DateTime.Now.Year.ToString(), DateTime.Now.AddMonths(-2).Month.ToString() + " " + DateTime.Now.Year.ToString(), DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString(), DateTime.Now.AddMonths(1).Month.ToString() + " " + DateTime.Now.Year.ToString() };
            Year = new List<string> { DateTime.Now.AddYears(-4).Year.ToString(), DateTime.Now.AddYears(-3).Year.ToString(), DateTime.Now.AddYears(-2).Year.ToString(), DateTime.Now.AddYears(-1).Year.ToString(), DateTime.Now.Year.ToString() };
            TotalPrice = 0; TotalGiven = 0; TotalRemaining = 0;
            MonthPricePoint = new List<int> { 0, 0, 0, 0, 0 }; MonthRPoint = new List<int> { 0, 0, 0, 0, 0 }; MonthGPoint = new List<int> { 0, 0, 0, 0, 0 };
            YearPricePoint = new List<int> { 0, 0, 0, 0, 0 }; YearRPoint = new List<int> { 0, 0, 0, 0, 0 }; YearGPoint = new List<int> { 0, 0, 0, 0, 0 };
        }
        private void AppointmentCountFS(string Type, bool View)
        {
            if (View == false) { }
            //
            //
            //Type=>Bar
            //
            //
            if (Type == "Bar" && View == true)
            {
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                    
                };
                
                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[0]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Series series1 = new Series
                {
                    Name = SRecord
                }; series1.ChangeView(ViewType.Bar);
                series1.Points.Add(new SeriesPoint(Start, AppointmentFınısh));
                chartControl1.Series.Add(series1);

                Series series3 = new Series
                {
                    Name = TRecord
                }; series3.ChangeView(ViewType.Bar);
                series3.Points.Add(new SeriesPoint(Total, totalAppo));
                chartControl1.Series.Add(series3);

                Series series2 = new Series
                {
                    Name = FRecord
                }; series2.ChangeView(ViewType.Bar);
                series2.Points.Add(new SeriesPoint(Finish, AppointmentsS));
                chartControl1.Series.Add(series2);
                XYDiagram diagram1 = chartControl1.Diagram as XYDiagram;
                chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                chartControl1.Legend.Direction = LegendDirection.LeftToRight;
                diagram1.AxisY.Label.TextPattern = "{V}";
                series1.CrosshairLabelPattern = "{A} : {V}";
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }

            //
            //
            //Type=>Line or Spline or Step Line
            //
            //
            if ((Type == "Line" || Type == "Step Line" || Type == "Spline") && View == true)
            {
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[0]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Series series4 = new Series
                {
                    Name = SRecord
                };
                if (Type == "Line")
                    series4.ChangeView(ViewType.Line);
                else if (Type == "Step Line")
                    series4.ChangeView(ViewType.StepLine);
                else if (Type == "Spline")
                    series4.ChangeView(ViewType.Spline);
                series4.Points.Add(new SeriesPoint(Start, AppointmentFınısh));
                series4.Points.Add(new SeriesPoint(Total, totalAppo));
                series4.Points.Add(new SeriesPoint(Finish, AppointmentsS));
                chartControl1.Series.Add(series4);
                XYDiagram diagram2 = chartControl1.Diagram as XYDiagram;
                ((LineSeriesView)series4.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                ((LineSeriesView)series4.View).LineStyle.DashStyle = DashStyle.Dash;
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                diagram2.AxisY.Label.TextPattern = "{V}";
                series4.CrosshairLabelPattern = "{A} : {V}";
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }

            //
            //
            //Type => Area or Spline Area
            //
            //
            if ((Type == "Area" || Type == "Spline Area") && View == true)
            {
                Series series5 = new Series
                {
                    Name = SRecord
                };
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[0]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                if (Type == "Area")
                {
                    series5.ChangeView(ViewType.Area);
                }
                else if (Type == "Spline Area")
                {
                    series5.ChangeView(ViewType.SplineArea);
                }
                series5.Points.Add(new SeriesPoint(Start, AppointmentFınısh));
                series5.Points.Add(new SeriesPoint(Total, totalAppo));
                series5.Points.Add(new SeriesPoint(Finish, AppointmentsS));
                chartControl1.Series.Add(series5);
                XYDiagram diagram3 = chartControl1.Diagram as XYDiagram;
                series5.CrosshairLabelPattern = "{A} : {V}";
                diagram3.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
               
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }

            //
            //
            //Type => Pie or doughnut
            //
            //
            if ((Type == "Pie" || Type == "Doughnut") && View == true)
            {
                Series series6 = new Series
                {
                    Name = SRecord
                };
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[0]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                series6.Points.Add(new SeriesPoint(Start, AppointmentFınısh));
                series6.Points.Add(new SeriesPoint(Total, totalAppo));
                series6.Points.Add(new SeriesPoint(Finish, AppointmentsS));
                chartControl1.Series.Add(series6);
                if (Type == "Pie")
                {
                    series6.ChangeView(ViewType.Pie);
                    series6.Label.TextPattern = "{A} ({VP:p0})";
                    series6.LegendTextPattern = "{A} ({V})";
                    ((PieSeriesLabel)series6.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((PieSeriesLabel)series6.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    PieSeriesView myView1 = (PieSeriesView)series6.View;
                    myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                        DataFilterCondition.GreaterThanOrEqual, 9));
                    myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "Others"));
                    myView1.ExplodeMode = PieExplodeMode.UseFilters;
                    myView1.ExplodedDistancePercentage = 15;
                    myView1.RuntimeExploding = true;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

                }
                if (Type == "Doughnut")
                {
                    series6.ChangeView(ViewType.Doughnut);
                    ((DoughnutSeriesLabel)series6.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((DoughnutSeriesLabel)series6.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((DoughnutSeriesLabel)series6.Label).ResolveOverlappingMinIndent = 5;
                    series6.Label.TextPattern = "{A} ({VP:p0})";
                    series6.LegendTextPattern = "{A} ({V})";
                    series6.SeriesPointsSorting = SortingMode.Ascending;
                    series6.SeriesPointsSortingKey = SeriesPointKey.Argument;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

                }
                chartControl1.AutoLayout = true;
               
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }

        }
        private void AppointmentMonth(string Type, bool View)
        {
            
            if (View == false) { }
            //
            //
            //Type => Bar 
            //
            //
            if (Type == "Bar" && View == true)
            {
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[1]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Series sereis1;
                
                for (int i = 0; i < Month.Count; i++)
                {
                    string[] aa = Month[i].Split(' ', '.', ':','/');
                    string b = "";
                    switch (aa[0])
                    {
                        case "1": b = Months[0] +" "+ aa[1]; break;
                        case "2": b = Months[1] + " " + aa[1]; break;
                        case "3": b = Months[2] + " " + aa[1]; break;
                        case "4": b = Months[3] + " " + aa[1]; break;
                        case "5": b = Months[4] + " " + aa[1]; break;
                        case "6": b = Months[5] + " " + aa[1]; break;
                        case "7": b = Months[6] + " " + aa[1]; break;
                        case "8": b = Months[7] + " " + aa[1]; break;
                        case "9": b = Months[8] + " " + aa[1]; break;
                        case "10": b = Months[9] + " " + aa[1]; break;
                        case "11": b = Months[10] + " " + aa[1]; break;
                        case "12": b = Months[11] + " " + aa[1]; break;
                        default: break;
                    }
                    sereis1 = new Series
                    {
                        Name = b.ToString()
                    };
                    sereis1.ChangeView(ViewType.Bar);
                    sereis1.Points.Add(new SeriesPoint(b.ToString(), totalMonth[i]));
                    chartControl1.Series.Add(sereis1);
                    sereis1.ArgumentScaleType = ScaleType.Qualitative;
                    XYDiagram diagram1 = chartControl1.Diagram as XYDiagram;
                    diagram1.AxisY.Label.TextPattern = "{V}";
                    sereis1.CrosshairLabelPattern = "{A} : {V}";
                }
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }

            //
            //
            //Type=>Line or spline or Step Line
            //
            //
            if ((Type == "Line" || Type == "Spline" || Type == "Step Line") && View == true)
            {
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[1]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Series sereis2;
                sereis2 = new Series();
                if (Type == "Line")
                    sereis2.ChangeView(ViewType.Line);
                else if (Type == "Step Line")
                    sereis2.ChangeView(ViewType.StepLine);
                else if (Type == "Spline")
                    sereis2.ChangeView(ViewType.Spline);
                sereis2.Name = Monthss;
                for (int i = 0; i < Month.Count; i++)
                {
                    string[] aa = Month[i].Split(' ', '.', ':','/');
                    string b = "";
                    switch (aa[0])
                    {
                        case "1": b = Months[0] + " " + aa[1]; break;
                        case "2": b = Months[1] + " " + aa[1]; break;
                        case "3": b = Months[2] + " " + aa[1]; break;
                        case "4": b = Months[3] + " " + aa[1]; break;
                        case "5": b = Months[4] + " " + aa[1]; break;
                        case "6": b = Months[5] + " " + aa[1]; break;
                        case "7": b = Months[6] + " " + aa[1]; break;
                        case "8": b = Months[7] + " " + aa[1]; break;
                        case "9": b = Months[8] + " " + aa[1]; break;
                        case "10": b = Months[9] + " " + aa[1]; break;
                        case "11": b = Months[10] + " " + aa[1]; break;
                        case "12": b = Months[11] + " " + aa[1]; break;
                        default: break;
                    }
                   
                    sereis2.Points.AddPoint(b.ToString(), totalMonth[i]);
                    chartControl1.Series.Add(sereis2);
                    sereis2.ArgumentScaleType = ScaleType.Qualitative;
                    ((LineSeriesView)sereis2.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                    ((LineSeriesView)sereis2.View).LineStyle.DashStyle = DashStyle.Dash;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                    XYDiagram diagram2 = chartControl1.Diagram as XYDiagram;
                    diagram2.AxisY.Label.TextPattern = "{V}";
                    sereis2.CrosshairLabelPattern = "{A} : {V}";
                }
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }

            //
            //
            //Type => Area Spline Area
            //
            //
            if ((Type == "Area" || Type == "Spline Area") && View == true)
            {
                Series series3 = new Series
                {
                    Name =Monthss
                };
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    Height = 26,
                    AutoSizeInLayoutControl = false,
                    Text = ChartName[1]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                if (Type == "Area")
                {
                    series3.ChangeView(ViewType.Area);
                    ((AreaSeriesView)series3.View).Transparency = 100;
                }

                else if (Type == "Spline Area")
                {

                    series3.ChangeView(ViewType.SplineArea);
                    ((SplineAreaSeriesView)series3.View).LineTensionPercent = 100;
                }
                for (int i = 0; i < Month.Count; i++)
                {

                    string[] aa = Month[i].Split(' ', '.', ':','/');
                    string b = "";
                    switch (aa[0])
                    {
                        case "1": b = Months[0] + " " + aa[1]; break;
                        case "2": b = Months[1] + " " + aa[1]; break;
                        case "3": b = Months[2] + " " + aa[1]; break;
                        case "4": b = Months[3] + " " + aa[1]; break;
                        case "5": b = Months[4] + " " + aa[1]; break;
                        case "6": b = Months[5] + " " + aa[1]; break;
                        case "7": b = Months[6] + " " + aa[1]; break;
                        case "8": b = Months[7] + " " + aa[1]; break;
                        case "9": b = Months[8] + " " + aa[1]; break;
                        case "10": b = Months[9] + " " + aa[1]; break;
                        case "11": b = Months[10] + " " + aa[1]; break;
                        case "12": b = Months[11] + " " + aa[1]; break;
                        default: break;
                    }
                    series3.Points.AddPoint(b, totalMonth[i]);
                    chartControl1.Series.Add(series3);
                    series3.ArgumentScaleType = ScaleType.Qualitative;
                }
                XYDiagram diagram3 = chartControl1.Diagram as XYDiagram;
                diagram3.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                series3.CrosshairLabelPattern = "{A} : {V}";
                series3.CrosshairLabelPattern.Distinct();
                series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }

            //
            //
            //Type=>Pie or Doughnut
            //
            //
            if ((Type == "Pie" || Type == "Doughnut") && View == true)
            {
                Series series4 = new Series();
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[1]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                for (int i = 0; i < Month.Count; i++)
                {
                    string[] aa = Month[i].Split(' ', '.', ':','/');
                    string b = "";
                    switch (aa[0])
                    {
                        case "1": b = Months[0] + " " + aa[1]; break;
                        case "2": b = Months[1] + " " + aa[1]; break;
                        case "3": b = Months[2] + " " + aa[1]; break;
                        case "4": b = Months[3] + " " + aa[1]; break;
                        case "5": b = Months[4] + " " + aa[1]; break;
                        case "6": b = Months[5] + " " + aa[1]; break;
                        case "7": b = Months[6] + " " + aa[1]; break;
                        case "8": b = Months[7] + " " + aa[1]; break;
                        case "9": b = Months[8] + " " + aa[1]; break;
                        case "10": b = Months[9] + " " + aa[1]; break;
                        case "11": b = Months[10] + " " + aa[1]; break;
                        case "12": b = Months[11] + " " + aa[1]; break;
                        default: break;
                    }
                    series4.Points.AddPoint(b.ToString(), totalMonth[i]);
                    chartControl1.Series.Add(series4);
                    series4.ArgumentScaleType = ScaleType.Qualitative;
                }
                if (Type == "Pie")
                {
                    series4.ChangeView(ViewType.Pie);
                    series4.Label.TextPattern = "{A} ({VP:p0})";
                    series4.LegendTextPattern = "{A} ({V})";
                    ((PieSeriesLabel)series4.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((PieSeriesLabel)series4.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    PieSeriesView myView1 = (PieSeriesView)series4.View;
                    myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                        DataFilterCondition.GreaterThanOrEqual, 9));
                    myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "Others"));
                    myView1.ExplodeMode = PieExplodeMode.UseFilters;
                    myView1.ExplodedDistancePercentage = 15;
                    myView1.RuntimeExploding = true;

                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                }
                if (Type == "Doughnut")
                {
                    series4.ChangeView(ViewType.Doughnut);
                    ((DoughnutSeriesLabel)series4.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((DoughnutSeriesLabel)series4.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((DoughnutSeriesLabel)series4.Label).ResolveOverlappingMinIndent = 5;
                    series4.Label.TextPattern = "{A} ({VP:p0})";
                    series4.LegendTextPattern = "{A} ({V})";
                    series4.SeriesPointsSorting = SortingMode.Ascending;
                    series4.SeriesPointsSortingKey = SeriesPointKey.Argument;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

                }

                chartControl1.AutoLayout = true;
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }

        }
        public void AppointmentCount()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.Appointments", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            totalAppo = dt.Rows.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime date = DateTime.Now;
                string ddaattee = Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString()).ToString(new CultureInfo("tr-TR"));
               
                string[] split = ddaattee.Split(' ', '-','.','/');
                if (Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString()) > date)//Tamamlanmamış Randevu
                    AppointmentFınısh++;
                if (Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString()) < date)//Tamamlanmış Randevu
                    AppointmentsS++;
                switch (split[1])
                {
                    case "01": split[1] = "1"; break;
                    case "02": split[1] = "2"; break;
                    case "03": split[1] = "3"; break;
                    case "04": split[1] = "4"; break;
                    case "05": split[1] = "5"; break;
                    case "06": split[1] = "6"; break;
                    case "07": split[1] = "7"; break;
                    case "08": split[1] = "8"; break;
                    case "09": split[1] = "9"; break;
                }
                if (split[1].ToString() == date.AddMonths(-3).Month.ToString())
                {
                    totalMonth[0]+=1; Month[0] = split[1] +"."+ split[2];
                }
                else if (split[1].ToString() == date.AddMonths(-2).Month.ToString())
                {
                    totalMonth[1] += 1;Month[1] = split[1] +"."+ split[2];
                }
                else if (split[1].ToString() == date.AddMonths(-1).Month.ToString())
                {
                    totalMonth[2] += 1;Month[2] = split[1] +"."+ split[2];
                }
                else if (split[1].ToString() == date.Month.ToString())
                {
                    totalMonth[3] += 1;Month[3] = split[1] +"."+ split[2];
                }
                else if (split[1].ToString() == date.AddMonths(1).Month.ToString())
                {
                    totalMonth[4] += 1;Month[4] = split[1] +"."+ split[2];
                }
                if (split[2].ToString() == date.AddYears(-4).Year.ToString())
                    totalYear[0]++;
                else if (split[2].ToString() == date.AddYears(-3).Year.ToString())
                    totalYear[1]++;
                else if (split[2].ToString() == date.AddYears(-2).Year.ToString())
                    totalYear[2]++;
                else if (split[2].ToString() == date.AddYears(-1).Year.ToString())
                    totalYear[3]++;
                else if (split[2].ToString() == date.Year.ToString())
                    totalYear[4]++;
            }
        }
        public void PriceRGivn()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.Appointments", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime date = DateTime.Now;
                string ddaattee = Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString()).ToString(new CultureInfo("tr-TR"));
                string[] sp = ddaattee.Split(' ', '-', '.', '/');
                if (sp[1].Trim('0').ToString() == date.AddMonths(-3).Month.ToString())
                {
                    MonthPricePoint[0] += Convert.ToInt32(dt.Rows[i]["Price"]);
                    MonthGPoint[0] += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                    MonthRPoint[0] += Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
                }
                else if (sp[1].Trim('0').ToString() == date.AddMonths(-2).Month.ToString())
                {
                    MonthPricePoint[1] += Convert.ToInt32(dt.Rows[i]["Price"]);
                    MonthGPoint[1] += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                    MonthRPoint[1] += Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
                }
                else if (sp[1].Trim('0').ToString() == date.AddMonths(-1).Month.ToString())
                {
                    MonthPricePoint[2] += Convert.ToInt32(dt.Rows[i]["Price"]);
                    MonthGPoint[2] += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                    MonthRPoint[2] += Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
                }
                else if (sp[1].Trim('0').ToString() == date.Month.ToString())
                {
                    MonthPricePoint[3] += Convert.ToInt32(dt.Rows[i]["Price"]);
                    MonthGPoint[3] += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                    MonthRPoint[3] += Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
                }
                else if (sp[1].ToString().Trim('0').ToString() == date.AddMonths(1).Month.ToString())
                {
                    MonthPricePoint[4] += Convert.ToInt32(dt.Rows[i]["Price"]);
                    MonthGPoint[4] += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                    MonthRPoint[4] += Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
                }
                if (sp[2].ToString() == date.AddYears(-4).Year.ToString())
                {
                    YearPricePoint[0] += Convert.ToInt32(dt.Rows[i]["Price"]);
                    YearGPoint[0] += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                    YearRPoint[0] +=  Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
                }
                else if (sp[2].ToString() == date.AddYears(-3).Year.ToString())
                {
                    YearPricePoint[1] += Convert.ToInt32(dt.Rows[i]["Price"]);
                    YearGPoint[1] += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                    YearRPoint[1] += Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
                }
                else if (sp[2].ToString() == date.AddYears(-2).Year.ToString())
                {
                    YearPricePoint[2] += Convert.ToInt32(dt.Rows[i]["Price"]);
                    YearGPoint[2] += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                    YearRPoint[2] += Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
                }
                else if (sp[2].ToString() == date.AddYears(-1).Year.ToString())
                {
                    YearPricePoint[3] += Convert.ToInt32(dt.Rows[i]["Price"]);
                    YearGPoint[3] += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                    YearRPoint[3] += Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
                }
                else if (sp[2].ToString() == date.Year.ToString())
                {
                    YearPricePoint[4] += Convert.ToInt32(dt.Rows[i]["Price"]);
                    YearGPoint[4] += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                    YearRPoint[4] += Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
                }
                TotalPrice += Convert.ToInt32(dt.Rows[i]["Price"]);
                TotalGiven += Convert.ToInt32(dt.Rows[i]["GivenPrice"]);
                TotalRemaining += Convert.ToInt32(dt.Rows[i]["RemainingPrice"]);
            }
        }
        private void AppointmentYear(string Type, bool View)
        {
            if (View == false) { }
            //
            //
            //Type=>Bar
            //
            //
            if (Type == "Bar" && View == true)
            {
                Series series1;
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[2]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                for (int i = 0; i < Year.Count; i++)
                {
                    series1 = new Series
                    {
                        Name = Year[i].ToString()
                    };
                    series1.ChangeView(ViewType.Bar);
                    series1.Points.AddPoint(Year[i].ToString(), Convert.ToInt32(totalYear[i]));
                    chartControl1.Series.Add(series1);
                    series1.ArgumentScaleType = ScaleType.Qualitative;
                    series1.CrosshairLabelPattern = "{A} : {V}";
                }
                XYDiagram diagram1 = chartControl1.Diagram as XYDiagram;
                diagram1.AxisY.Label.TextPattern = "{V}";
               
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>Line or Step Line or Spline
            //
            //
            if ((Type == "Line" || Type == "Step Line" || Type == "Spline") && View == true)
            {
                Series series2;
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[2]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                series2 = new Series
                {
                    Name =Yearss
                };
                if (Type == "Line")
                    series2.ChangeView(ViewType.Line);
                else if (Type == "Step Line")
                    series2.ChangeView(ViewType.StepLine);
                else if (Type == "Spline")
                    series2.ChangeView(ViewType.Spline);
                for (int i = 0; i < Year.Count; i++)
                {
                    series2.Points.AddPoint(Year[i].ToString(), Convert.ToInt32(totalYear[i]));
                    chartControl1.Series.Add(series2);
                    series2.ArgumentScaleType = ScaleType.Qualitative;
                }
                ((LineSeriesView)series2.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                ((LineSeriesView)series2.View).LineStyle.DashStyle = DashStyle.Dash;
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                XYDiagram diagram2 = chartControl1.Diagram as XYDiagram;
                diagram2.AxisY.Label.TextPattern = "{V}";
                series2.CrosshairLabelPattern = "{A} : {V}";
               
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>Area or Spline Area
            //
            //
            if ((Type == "Area" || Type == "Spline Area") && View == true)
            {
                Series series3;
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[2]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                series3 = new Series
                {
                    Name = Yearss
                };
                if (Type == "Area")
                    series3.ChangeView(ViewType.Area);
                else if (Type == "Spline Area")
                    series3.ChangeView(ViewType.SplineArea);
                for (int i = 0; i < Year.Count; i++)
                {
                    string yea = Year[i];
                    series3.Points.AddPoint(yea, Convert.ToInt32(totalYear[i]));
                    chartControl1.Series.Add(series3);
                    series3.ArgumentScaleType = ScaleType.Qualitative;
                }
                XYDiagram diagram3 = chartControl1.Diagram as XYDiagram;
                diagram3.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
                diagram3.AxisX.Tickmarks.Visible = true;
                series3.CrosshairLabelPattern = "{A} : {V}";
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
               
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>Pie or Doughnut
            //
            //
            if ((Type == "Pie" || Type == "Doughnut") && View == true)
            {
                Series series4 = new Series();
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[2]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                series4.Name = Yearss;
                for (int i = 0; i < Year.Count; i++)
                {
                    series4.Points.AddPoint(Year[i].ToString(), Convert.ToInt32(totalYear[i]));
                    chartControl1.Series.Add(series4);
                    series4.ArgumentScaleType = ScaleType.Qualitative;
                }
                if (Type == "Pie")
                {
                    series4.ChangeView(ViewType.Pie);
                    series4.Label.TextPattern = "{A} ({VP:p0})";
                    series4.LegendTextPattern = "{A} ({V})";
                    ((PieSeriesLabel)series4.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((PieSeriesLabel)series4.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    PieSeriesView myView1 = (PieSeriesView)series4.View;
                    myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                        DataFilterCondition.GreaterThanOrEqual, 9));
                    myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "Others"));
                    myView1.ExplodeMode = PieExplodeMode.UseFilters;
                    myView1.ExplodedDistancePercentage = 15;
                    myView1.RuntimeExploding = true;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                }
                if (Type == "Doughnut")
                {
                    series4.ChangeView(ViewType.Doughnut);
                    ((DoughnutSeriesLabel)series4.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((DoughnutSeriesLabel)series4.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((DoughnutSeriesLabel)series4.Label).ResolveOverlappingMinIndent = 5;
                    series4.Label.TextPattern = "{A} ({VP:p0})";
                    series4.LegendTextPattern = "{A} ({V})";
                    series4.SeriesPointsSorting = SortingMode.Ascending;
                    series4.SeriesPointsSortingKey = SeriesPointKey.Argument;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                }
                chartControl1.AutoLayout = true;
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
        }
        private void GivenandRemaining(string Type, bool View)
        {
            if (View == false) { }
            //
            //
            //Type=>Bar
            //
            //
            if (Type == "Bar" && View == true)
            {
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[3]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Series series1 = new Series
                {
                    Name = Given
                };
                series1.ChangeView(ViewType.Bar);
                series1.Points.Add(new SeriesPoint(Given, TotalGiven));
                chartControl1.Series.Add(series1);
                Series series2 = new Series
                {
                    Name = Remaining
                };
                series2.ChangeView(ViewType.Bar);
                series2.Points.Add(new SeriesPoint(Remaining, TotalRemaining));
                chartControl1.Series.Add(series2);
                XYDiagram diagram1 = chartControl1.Diagram as XYDiagram;
                diagram1.AxisY.Label.TextPattern = "{V:C2}";
                series1.CrosshairLabelPattern = "{A} : {V:C2}";
                series2.CrosshairLabelPattern = "{A} : {V:C2}";
                chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                chartControl1.Legend.Direction = LegendDirection.LeftToRight;
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>Line or Spline or Step Line
            //
            //
            if ((Type == "Line" || Type == "Step Line" || Type == "Spline") && View == true)
            {
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[3]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Series series3 = new Series
                {
                    Name = Given
                };
                if (Type == "Line")
                    series3.ChangeView(ViewType.Line);
                else if (Type == "Step Line")
                    series3.ChangeView(ViewType.StepLine);
                else if (Type == "Spline")
                    series3.ChangeView(ViewType.Spline);
                series3.Points.Add(new SeriesPoint(Given, TotalGiven));
                series3.Points.Add(new SeriesPoint(Remaining, TotalRemaining));
                chartControl1.Series.Add(series3);
                XYDiagram diagram2 = chartControl1.Diagram as XYDiagram;
                diagram2.AxisY.Label.TextPattern = "{V:C2}";
                series3.CrosshairLabelPattern = "{A} : {V:C2}";
                ((LineSeriesView)series3.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                ((LineSeriesView)series3.View).LineStyle.DashStyle = DashStyle.Dash;
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>Area or Spline Area
            //
            //
            if ((Type == "Area" || Type == "Spline Area") && View == true)
            {
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[3]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Series series4 = new Series();
                if (Type == "Area")
                    series4.ChangeView(ViewType.Area);
                else if (Type == "Spline Area")
                    series4.ChangeView(ViewType.SplineArea);
                series4.Points.Add(new SeriesPoint(Given, TotalGiven));
                series4.Points.Add(new SeriesPoint(Remaining ,TotalRemaining));
                chartControl1.Series.Add(series4);
                XYDiagram diagram3 =chartControl1.Diagram as XYDiagram;
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>Pie or doughnut
            //
            //
            if ((Type == "Pie" || Type == "Doughnut") && View == true)
            {
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Text = ChartName[3]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Series series5 = new Series
                {
                    Name = Given
                };
                series5.Points.Add(new SeriesPoint(Given, TotalGiven));
                series5.Points.Add(new SeriesPoint(Remaining, TotalRemaining));
                chartControl1.Series.Add(series5);
                if (Type == "Pie")
                {
                    series5.ChangeView(ViewType.Pie);
                    series5.Label.TextPattern = "{A} ({VP:p0}) - {V:C2}";
                    series5.LegendTextPattern = "{A} ({V:C2})";
                    ((PieSeriesLabel)series5.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((PieSeriesLabel)series5.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    PieSeriesView myView1 = (PieSeriesView)series5.View;
                    myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                        DataFilterCondition.GreaterThanOrEqual, 9));
                    myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                        DataFilterCondition.NotEqual, "Others"));
                    myView1.ExplodeMode = PieExplodeMode.UseFilters;
                    myView1.ExplodedDistancePercentage = 15;
                    myView1.RuntimeExploding = true;
                }
                if (Type == "Doughnut")
                {
                    series5.ChangeView(ViewType.Doughnut);
                    ((DoughnutSeriesLabel)series5.Label).Position = PieSeriesLabelPosition.TwoColumns;
                    ((DoughnutSeriesLabel)series5.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    ((DoughnutSeriesLabel)series5.Label).ResolveOverlappingMinIndent = 5;
                    series5.Label.TextPattern = "{A} ({VP:p0}) - {V:C2}";
                    series5.LegendTextPattern = "{A} ({V:C2})";
                    series5.SeriesPointsSorting = SortingMode.Ascending;
                    series5.SeriesPointsSortingKey = SeriesPointKey.Argument;
                }
                chartControl1.AutoLayout = true;
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
        }
        private void MonthRGPrice(string Type, bool View)
        {
            if (View == false) { }
            //
            //
            //Type=>Bar
            //
            //
            if (Type == "Bar" && View == true)
            {
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[4]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                Series sereis1;
                Series sereis2;
                Series sereis3;

                for (int i = 0; i < Month.Count; i++)
                {
                    string[] aa = Month[i].Split(' ', '.', ':','/');
                    string b = "";
                    switch (aa[0])
                    {
                        case "1": b = Months[0] + " " + aa[1]; break;
                        case "2": b = Months[1] + " " + aa[1]; break;
                        case "3": b = Months[2] + " " + aa[1]; break;
                        case "4": b = Months[3] + " " + aa[1]; break;
                        case "5": b = Months[4] + " " + aa[1]; break;
                        case "6": b = Months[5] + " " + aa[1]; break;
                        case "7": b = Months[6] + " " + aa[1]; break;
                        case "8": b = Months[7] + " " + aa[1]; break;
                        case "9": b = Months[8] + " " + aa[1]; break;
                        case "10": b = Months[9] + " " + aa[1]; break;
                        case "11": b = Months[10] + " " + aa[1]; break;
                        case "12": b = Months[11] + " " + aa[1]; break;
                        default: break;
                    }
                    sereis1 = new Series
                    {
                        Name = Given
                    };
                    sereis2 = new Series
                    {
                        Name = Deal
                    };
                    sereis3 = new Series
                    {
                        Name = Remaining
                    };
                    sereis1.ChangeView(ViewType.Bar);
                    sereis2.ChangeView(ViewType.Bar);
                    sereis3.ChangeView(ViewType.Bar);
                    sereis1.Points.Add(new SeriesPoint(b, MonthGPoint[i]));
                    chartControl1.Series.Add(sereis1);
                    sereis2.Points.Add(new SeriesPoint(b, MonthPricePoint[i]));
                    chartControl1.Series.Add(sereis2);
                    sereis3.Points.Add(new SeriesPoint(b, MonthRPoint[i]));
                    chartControl1.Series.Add(sereis3);
                    sereis1.ArgumentScaleType = ScaleType.Qualitative;
                    sereis2.ArgumentScaleType = ScaleType.Qualitative;
                    sereis3.ArgumentScaleType = ScaleType.Qualitative;
                    XYDiagram diagram1 =chartControl1.Diagram as XYDiagram;
                    diagram1.AxisY.Label.TextPattern = "{V:C2}";
                    diagram1.AxisX.Label.TextPattern = "{S}";
                    sereis1.CrosshairLabelPattern = "{S} : {V:C2}";
                    sereis2.CrosshairLabelPattern = "{S} : {V:C2}";
                    sereis3.CrosshairLabelPattern = "{S} : {V:C2}";
                    chartControl1.Legend.TextVisible = false;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                }
               
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type =>Line or spline or Step Line
            //
            //
            if ((Type == "Line" || Type == "Spline" || Type == "Step Line") && View == true)
            {
                Series sereis4;
                Series sereis5;
                Series sereis6;
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[4]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                sereis4 = new Series
                {
                    Name = Given
                };
                sereis5 = new Series
                {
                    Name = Deal
                };
                sereis6 = new Series
                {
                    Name = Remaining
                };
                if (Type == "Line")
                { sereis4.ChangeView(ViewType.Line); sereis5.ChangeView(ViewType.Line); sereis6.ChangeView(ViewType.Line); }
                else if (Type == "Step Line")
                { sereis4.ChangeView(ViewType.StepLine); sereis5.ChangeView(ViewType.StepLine); sereis6.ChangeView(ViewType.StepLine); }
                else if (Type == "Spline")
                { sereis4.ChangeView(ViewType.Spline); sereis5.ChangeView(ViewType.Spline); sereis6.ChangeView(ViewType.Spline); }
                for (int i = 0; i < Month.Count; i++)
                {
                    string[] aa = Month[i].Split(' ', '.', ':','/');
                    string b = "";
                    switch (aa[0])
                    {
                        case "1": b = Months[0] + " " + aa[1]; break;
                        case "2": b = Months[1] + " " + aa[1]; break;
                        case "3": b = Months[2] + " " + aa[1]; break;
                        case "4": b = Months[3] + " " + aa[1]; break;
                        case "5": b = Months[4] + " " + aa[1]; break;
                        case "6": b = Months[5] + " " + aa[1]; break;
                        case "7": b = Months[6] + " " + aa[1]; break;
                        case "8": b = Months[7] + " " + aa[1]; break;
                        case "9": b = Months[8] + " " + aa[1]; break;
                        case "10": b = Months[9] + " " + aa[1]; break;
                        case "11": b = Months[10] + " " + aa[1]; break;
                        case "12": b = Months[11] + " " + aa[1]; break;
                        default: break;
                    }
                    sereis4.Points.Add(new SeriesPoint(b, MonthGPoint[i]));
                    chartControl1.Series.Add(sereis4);
                    sereis5.Points.Add(new SeriesPoint(b, MonthPricePoint[i]));
                    chartControl1.Series.Add(sereis5);
                    sereis6.Points.Add(new SeriesPoint(b, MonthRPoint[i]));
                    chartControl1.Series.Add(sereis6);
                    sereis4.ArgumentScaleType = ScaleType.Qualitative;
                    sereis5.ArgumentScaleType = ScaleType.Qualitative;
                    sereis6.ArgumentScaleType = ScaleType.Qualitative;
                    XYDiagram diagram2 = chartControl1.Diagram as XYDiagram;
                    // Access the type-specific options of the diagram.
                    diagram2.AxisY.Label.TextPattern = "{V:C2}";
                    diagram2.AxisX.Label.TextPattern = "{S}";
                    sereis4.CrosshairLabelPattern = "{S} : {V:C2}";
                    sereis5.CrosshairLabelPattern = "{S} : {V:C2}";
                    sereis6.CrosshairLabelPattern = "{S} : {V:C2}";
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                    diagram2.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                }
                    ((LineSeriesView)sereis4.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                ((LineSeriesView)sereis4.View).LineStyle.DashStyle = DashStyle.Dash;
                ((LineSeriesView)sereis5.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                ((LineSeriesView)sereis5.View).LineStyle.DashStyle = DashStyle.Dash;
                ((LineSeriesView)sereis6.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                ((LineSeriesView)sereis6.View).LineStyle.DashStyle = DashStyle.Dash;
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>Area or Spline Area
            //
            //
            if ((Type == "Area" || Type == "Spline Area") && View == true)
            {
                Series sereis7;
                Series sereis8;
                Series sereis9;
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[4]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                sereis7 = new Series
                {
                    Name = Given
                };
                sereis8 = new Series
                {
                    Name = Deal
                };
                sereis9 = new Series
                {
                    Name = Remaining
                };
                if (Type == "Area")
                {
                    sereis7.ChangeView(ViewType.Area); sereis8.ChangeView(ViewType.Area); sereis9.ChangeView(ViewType.Area);
                    ((AreaSeriesView)sereis7.View).Transparency = 100;
                    ((AreaSeriesView)sereis8.View).Transparency = 80;
                    ((AreaSeriesView)sereis9.View).Transparency = 90;
                }
                else if (Type == "Spline Area")
                {
                    sereis7.ChangeView(ViewType.SplineArea); sereis8.ChangeView(ViewType.SplineArea); sereis9.ChangeView(ViewType.SplineArea);
                    ((SplineAreaSeriesView)sereis7.View).LineTensionPercent = 100;
                    ((SplineAreaSeriesView)sereis8.View).LineTensionPercent = 70;
                    ((SplineAreaSeriesView)sereis9.View).LineTensionPercent = 80;
                }
                sereis7.ArgumentScaleType = ScaleType.Qualitative;
                sereis8.ArgumentScaleType = ScaleType.Qualitative;
                sereis9.ArgumentScaleType = ScaleType.Qualitative;
                for (int i = 0; i < Month.Count; i++)
                {
                    string[] aa = Month[i].Split(' ', '.', ':','/');
                    string b = "";
                    switch (aa[0])
                    {
                        case "1": b = Months[0] + " " + aa[1]; break;
                        case "2": b = Months[1] + " " + aa[1]; break;
                        case "3": b = Months[2] + " " + aa[1]; break;
                        case "4": b = Months[3] + " " + aa[1]; break;
                        case "5": b = Months[4] + " " + aa[1]; break;
                        case "6": b = Months[5] + " " + aa[1]; break;
                        case "7": b = Months[6] + " " + aa[1]; break;
                        case "8": b = Months[7] + " " + aa[1]; break;
                        case "9": b = Months[8] + " " + aa[1]; break;
                        case "10": b = Months[9] + " " + aa[1]; break;
                        case "11": b = Months[10] + " " + aa[1]; break;
                        case "12": b = Months[11] + " " + aa[1]; break;
                        default: break;
                    }
                    sereis7.Points.AddPoint(b, MonthGPoint[i]);
                    chartControl1.Series.Add(sereis7);
                    sereis8.Points.AddPoint(b, MonthPricePoint[i]);
                    chartControl1.Series.Add(sereis8);
                    sereis9.Points.AddPoint(b, MonthRPoint[i]);
                    chartControl1.Series.Add(sereis9);
                    sereis7.ArgumentScaleType = ScaleType.Qualitative;
                    sereis8.ArgumentScaleType = ScaleType.Qualitative;
                    sereis9.ArgumentScaleType = ScaleType.Qualitative;
                }
                XYDiagram diagram3 = chartControl1.Diagram as XYDiagram;
                // Access the type-specific options of the diagram.
                diagram3.AxisY.Label.TextPattern = "{V:C2}";
                diagram3.AxisX.Label.TextPattern = "{S}";
                sereis7.CrosshairLabelPattern = "{S} : {V:C2}";
                sereis8.CrosshairLabelPattern = "{S} : {V:C2}";
                sereis9.CrosshairLabelPattern = "{S} : {V:C2}";
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>Pie or Doughnut
            //
            //
            if ((Type == "Pie" || Type == "Doughnut") && View == true)
            {
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[4]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                for (int i = 0; i < Month.Count; i++)
                {
                    string[] aa = Month[i].Split(' ', '.', ':','/');
                    string b = "";
                    switch (aa[0])
                    {
                        case "1": b = Months[0] + " " + aa[1]; break;
                        case "2": b = Months[1] + " " + aa[1]; break;
                        case "3": b = Months[2] + " " + aa[1]; break;
                        case "4": b = Months[3] + " " + aa[1]; break;
                        case "5": b = Months[4] + " " + aa[1]; break;
                        case "6": b = Months[5] + " " + aa[1]; break;
                        case "7": b = Months[6] + " " + aa[1]; break;
                        case "8": b = Months[7] + " " + aa[1]; break;
                        case "9": b = Months[8] + " " + aa[1]; break;
                        case "10": b = Months[9] + " " + aa[1]; break;
                        case "11": b = Months[10] + " " + aa[1]; break;
                        case "12": b = Months[11] + " " + aa[1]; break;
                        default: break;
                    }
                    Series series10 = new Series
                    {
                        Name = b
                    };
                    series10.Points.Add(new SeriesPoint(Given, MonthGPoint[i]));
                    series10.Points.Add(new SeriesPoint(Deal, MonthPricePoint[i]));
                    series10.Points.Add(new SeriesPoint(Remaining, MonthRPoint[i]));
                    chartControl1.Series.Add(series10);
                    series10.ArgumentScaleType = ScaleType.Qualitative;
                    if (MonthGPoint[i] == 0 && MonthPricePoint[i] == 0 && MonthRPoint[i] == 0)
                        series10.Visible = false;
                    if (Type == "Pie")
                    {
                        series10.ChangeView(ViewType.Pie);
                        series10.Label.TextPattern = "{A} {V:C2} - {S} ({VP:p0})";
                        ((PieSeriesLabel)series10.Label).Position = PieSeriesLabelPosition.TwoColumns;
                        ((PieSeriesLabel)series10.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                        PieSeriesView myView1 = (PieSeriesView)series10.View;
                        myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                            DataFilterCondition.GreaterThanOrEqual, 9));
                        myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                            DataFilterCondition.NotEqual, "Others"));
                        myView1.ExplodeMode = PieExplodeMode.UseFilters;
                        myView1.ExplodedDistancePercentage = 15;
                        myView1.RuntimeExploding = true;
                        chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                        myView1.TotalLabel.Visible = true;
                        myView1.TotalLabel.TextPattern = "{S}";
                    }
                    if (Type == "Doughnut")
                    {
                        series10.ChangeView(ViewType.Doughnut);
                        series10.Label.TextPattern = "{A} {V:C2} - {S} ({VP:p0})";
                        series10.LegendTextPattern = "{A}";
                        ((DoughnutSeriesLabel)series10.Label).Position = PieSeriesLabelPosition.TwoColumns;
                        ((DoughnutSeriesLabel)series10.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                        ((DoughnutSeriesLabel)series10.Label).ResolveOverlappingMinIndent = 5;
                        series10.SeriesPointsSorting = SortingMode.Ascending;
                        series10.SeriesPointsSortingKey = SeriesPointKey.Argument;
                        chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                        chartControl1.Legends.Distinct();
                        DoughnutSeriesView myView1 = (DoughnutSeriesView)series10.View;
                        myView1.TotalLabel.Visible = true;
                        myView1.TotalLabel.TextPattern = "{S}";
                    }
                    chartControl1.AutoLayout = true;
                }
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
        }
        private void YearRGPrice(string Type, bool View)
        {
            if (View == false)
            {

            }
            //
            //
            //Type=>Bar
            //
            //
            if (Type == "Bar" && View == true)
            {
                Series sereis1;
                Series sereis2;
                Series sereis3;
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[5]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                for (int i = 0; i < Year.Count; i++)
                {
                    sereis1 = new Series
                    {
                        Name = "Alınan"
                    };
                    sereis2 = new Series
                    {
                        Name = "Anlaşma"
                    };
                    sereis3 = new Series
                    {
                        Name = "Kalan"
                    };
                    sereis1.ChangeView(ViewType.Bar);
                    sereis2.ChangeView(ViewType.Bar);
                    sereis3.ChangeView(ViewType.Bar);
                    sereis1.Points.Add(new SeriesPoint(Year[i], YearGPoint[i]));
                    chartControl1.Series.Add(sereis1);
                    sereis2.Points.Add(new SeriesPoint(Year[i], YearPricePoint[i]));
                    chartControl1.Series.Add(sereis2);
                    sereis3.Points.Add(new SeriesPoint(Year[i], YearRPoint[i]));
                    chartControl1.Series.Add(sereis3);
                    sereis1.ArgumentScaleType = ScaleType.Qualitative;
                    sereis2.ArgumentScaleType = ScaleType.Qualitative;
                    sereis3.ArgumentScaleType = ScaleType.Qualitative;
                    XYDiagram diagram1 = chartControl1.Diagram as XYDiagram;
                    diagram1.AxisY.Label.TextPattern = "{V:C2}";
                    diagram1.AxisX.Label.TextPattern = "{S}";
                    sereis1.CrosshairLabelPattern = "{S} : {V:C2}";
                    sereis2.CrosshairLabelPattern = "{S} : {V:C2}";
                    sereis3.CrosshairLabelPattern = "{S} : {V:C2}";
                    chartControl1.Legend.TextVisible = false;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                }
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>Line or spline or step Line
            //
            //
            if ((Type == "Line" || Type == "Spline" || Type == "Step Line") && View == true)
            {
                Series sereis4;
                Series sereis5;
                Series sereis6;
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[5]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                sereis4 = new Series
                {
                    Name = "Alınan"
                };
                sereis5 = new Series
                {
                    Name = "Anlaşma"
                };
                sereis6 = new Series
                {
                    Name = "Kalan"
                };
                if (Type == "Line")
                { sereis4.ChangeView(ViewType.Line); sereis5.ChangeView(ViewType.Line); sereis6.ChangeView(ViewType.Line); }
                else if (Type == "Step Line")
                { sereis4.ChangeView(ViewType.StepLine); sereis6.ChangeView(ViewType.StepLine); sereis5.ChangeView(ViewType.StepLine); }
                else if (Type == "Spline")
                { sereis4.ChangeView(ViewType.Spline); sereis5.ChangeView(ViewType.Spline); sereis6.ChangeView(ViewType.Spline); }
                for (int i = 0; i < Year.Count; i++)
                {
                    sereis4.Points.Add(new SeriesPoint(Year[i], YearGPoint[i]));
                    chartControl1.Series.Add(sereis4);
                    sereis5.Points.Add(new SeriesPoint(Year[i], YearPricePoint[i]));
                    chartControl1.Series.Add(sereis5);
                    sereis6.Points.Add(new SeriesPoint(Year[i], YearRPoint[i]));
                    chartControl1.Series.Add(sereis6);
                    sereis4.ArgumentScaleType = ScaleType.Qualitative;
                    sereis5.ArgumentScaleType = ScaleType.Qualitative;
                    sereis6.ArgumentScaleType = ScaleType.Qualitative;
                    XYDiagram diagram2 = chartControl1.Diagram as XYDiagram;
                    diagram2.AxisY.Label.TextPattern = "{V:C2}";
                    diagram2.AxisX.Label.TextPattern = "{S}";
                    sereis4.CrosshairLabelPattern = "{S} : {V:C2}";
                    sereis5.CrosshairLabelPattern = "{S} : {V:C2}";
                    sereis6.CrosshairLabelPattern = "{S} : {V:C2}";
                    chartControl1.Legend.TextVisible = false;
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                    ((LineSeriesView)sereis4.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                    ((LineSeriesView)sereis4.View).LineStyle.DashStyle = DashStyle.Dash;
                    ((LineSeriesView)sereis5.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                    ((LineSeriesView)sereis5.View).LineStyle.DashStyle = DashStyle.Dash;
                    ((LineSeriesView)sereis6.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                    ((LineSeriesView)sereis6.View).LineStyle.DashStyle = DashStyle.Dash;
                }
               
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>Area or Spline Area
            //
            //
            if ((Type == "Area" || Type == "Spline Area") && View == true)
            {
                Series sereis7;
                Series sereis8;
                Series sereis9;
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height = 26,
                    Text = ChartName[5]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                sereis7 = new Series
                {
                    Name = "Alınan"
                };
                sereis8 = new Series
                {
                    Name = "Anlaşma"
                };
                sereis9 = new Series
                {
                    Name = "Kalan"
                };
                if (Type == "Area")
                {
                    sereis7.ChangeView(ViewType.Area); sereis8.ChangeView(ViewType.Area); sereis9.ChangeView(ViewType.Area);
                    ((AreaSeriesView)sereis7.View).Transparency = 80;
                    ((AreaSeriesView)sereis8.View).Transparency = 40;
                    ((AreaSeriesView)sereis9.View).Transparency = 60;
                }
                else if (Type == "Spline Area")
                {
                    sereis7.ChangeView(ViewType.SplineArea); sereis8.ChangeView(ViewType.SplineArea); sereis9.ChangeView(ViewType.SplineArea);
                    ((SplineAreaSeriesView)sereis7.View).LineTensionPercent = 80;
                    ((SplineAreaSeriesView)sereis8.View).LineTensionPercent = 40;
                    ((SplineAreaSeriesView)sereis9.View).LineTensionPercent = 60;
                }
                for (int i = 0; i < Year.Count; i++)
                {
                    sereis7.Points.Add(new SeriesPoint(Year[i], YearGPoint[i]));
                    chartControl1.Series.Add(sereis7);
                    sereis8.Points.Add(new SeriesPoint(Year[i], YearPricePoint[i]));
                    chartControl1.Series.Add(sereis8);
                    sereis9.Points.Add(new SeriesPoint(Year[i], YearRPoint[i]));
                    chartControl1.Series.Add(sereis9);
                    sereis7.ArgumentScaleType = ScaleType.Qualitative;
                    sereis8.ArgumentScaleType = ScaleType.Qualitative;
                    sereis9.ArgumentScaleType = ScaleType.Qualitative;
                    XYDiagram diagram3 = chartControl1.Diagram as XYDiagram;
                    diagram3.AxisY.Label.TextPattern = "{V:C2}";
                    diagram3.AxisX.Label.TextPattern = "{S}";
                    sereis7.CrosshairLabelPattern = "{S} : {V:C2}";
                    sereis8.CrosshairLabelPattern = "{S} : {V:C2}";
                    sereis9.CrosshairLabelPattern = "{S} : {V:C2}";
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                }
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
            //
            //
            //Type=>pie or doughnut
            //
            //
            if ((Type == "Pie" || Type == "Doughnut") && View == true)
            {
                Series series10;
                DevExpress.XtraEditors.PanelControl panel = new DevExpress.XtraEditors.PanelControl()
                {
                    Height = 422,
                    Width = 900,
                };
                ChartControl chartControl1 = new ChartControl()
                {
                    Dock = DockStyle.Fill,
                };

                DevExpress.XtraEditors.LabelControl Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Dock = DockStyle.Top,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                    AutoSizeInLayoutControl = false,
                    Height=26,
                    Text = ChartName[5]
                };
                panel.Controls.Add(Label);
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                for (int i = 0; i < Year.Count; i++)
                {
                    series10 = new Series
                    {
                        Name = Year[i]
                    };
                    series10.Points.Add(new SeriesPoint("Alınan", YearGPoint[i]));
                    series10.Points.Add(new SeriesPoint("Toplam", YearPricePoint[i]));
                    series10.Points.Add(new SeriesPoint("Kalan", YearRPoint[i]));
                    chartControl1.Series.Add(series10);
                    series10.ArgumentScaleType = ScaleType.Qualitative;
                    if (YearGPoint[i] == 0 && YearPricePoint[i] == 0 && YearRPoint[i] == 0)
                        series10.Visible = false;
                    if (Type == "Pie")
                    {
                        series10.ChangeView(ViewType.Pie); series10.Label.TextPattern = "{S} {V:C2} - {A} ({VP:p0})"; ((PieSeriesLabel)series10.Label).Position = PieSeriesLabelPosition.TwoColumns;
                        ((PieSeriesLabel)series10.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                        PieSeriesView myView1 = (PieSeriesView)series10.View;
                        myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                            DataFilterCondition.GreaterThanOrEqual, 9));
                        myView1.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                            DataFilterCondition.NotEqual, "Others"));
                        myView1.ExplodeMode = PieExplodeMode.UseFilters;
                        myView1.ExplodedDistancePercentage = 15;
                        myView1.RuntimeExploding = true;
                        chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                        myView1.TotalLabel.Visible = true;
                        myView1.TotalLabel.TextPattern = "{S}";
                    }
                    if (Type == "Doughnut")
                    {
                        series10.ChangeView(ViewType.Doughnut); series10.Label.TextPattern = "{A} {V:C2} - {S} ({VP:p0})";
                        ((DoughnutSeriesLabel)series10.Label).Position = PieSeriesLabelPosition.TwoColumns;
                        ((DoughnutSeriesLabel)series10.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                        ((DoughnutSeriesLabel)series10.Label).ResolveOverlappingMinIndent = 5;
                        series10.SeriesPointsSorting = SortingMode.Ascending;
                        series10.SeriesPointsSortingKey = SeriesPointKey.Argument;
                        DoughnutSeriesView myView1 = (DoughnutSeriesView)series10.View;
                        chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                        myView1.TotalLabel.Visible = true;
                        myView1.TotalLabel.TextPattern = "{S}";
                    }
                }
                chartControl1.AutoLayout = true;
                
                panel.Controls.Add(chartControl1);
                chartControl1.BringToFront();
                flowLayoutPanel1.Controls.Add(panel);
            }
        }
        public void ShowDefaultChartControl()
        {
            flowLayoutPanel1.Controls.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.ChartOption", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            AppointmentCountFS(dt.Rows[0]["Typee"].ToString(), Convert.ToBoolean(dt.Rows[0]["Showi"]));
            AppointmentMonth(dt.Rows[1]["Typee"].ToString(), Convert.ToBoolean(dt.Rows[1]["Showi"]));
            AppointmentYear(dt.Rows[2]["Typee"].ToString(), Convert.ToBoolean(dt.Rows[2]["Showi"]));
            GivenandRemaining(dt.Rows[3]["Typee"].ToString(), Convert.ToBoolean(dt.Rows[3]["Showi"]));
            MonthRGPrice(dt.Rows[4]["Typee"].ToString(), Convert.ToBoolean(dt.Rows[4]["Showi"]));
            YearRGPrice(dt.Rows[5]["Typee"].ToString(), Convert.ToBoolean(dt.Rows[5]["Showi"]));
        }
        private void CreateAppo()
        {
            schedulerControl1.SelectedAppointments.Clear();
            Appointment apt = null;
            if (this.schedulerControl1.SelectedAppointments.Count > 0)
            {
                apt = this.schedulerControl1.SelectedAppointments[0];
            }
            else
            {
                apt = this.schedulerControl1.DataStorage.CreateAppointment(AppointmentType.Normal);
                apt.Start = this.schedulerControl1.SelectedInterval.Start;
                apt.End = this.schedulerControl1.SelectedInterval.End;
            }
            this.schedulerControl1.ShowEditAppointmentForm(apt);
        }
        private void LoadAppo()
        {
            schedulerControl1.SelectedAppointments.Clear();
            Appointment apt = null;
            string SelectingDate=string.Empty;
            if (gridView1.RowCount>0)
                 SelectingDate= gridView1.GetFocusedRowCellValue("StartDate").ToString();
            string SelectingSubject = gridView1.GetFocusedRowCellValue("Subject").ToString();
            string SelectingPhoneNumber = gridView1.GetFocusedRowCellValue("PhoneNumber").ToString();
            int iD=-1 ;
            for (int i=0;i< schedulerStorage1.Appointments.Count; i++)
            {
                if (schedulerStorage1.Appointments[i].Start.ToString() == SelectingDate && schedulerStorage1.Appointments[i].Subject.ToString()==SelectingSubject && schedulerStorage1.Appointments[i].CustomFields["PhoneNumber"].ToString()==SelectingPhoneNumber)
                {
                    iD = i;
                }
            }
            if(iD!=-1)
                apt = schedulerControl1.DataStorage.Appointments[iD];
            if (apt != null)
                schedulerControl1.SelectedAppointments.Add(apt);
            if (this.schedulerControl1.SelectedAppointments.Count > 0)
            {
                apt = this.schedulerControl1.SelectedAppointments[0];
            }
            else
            {
                apt = this.schedulerControl1.DataStorage.CreateAppointment(AppointmentType.Normal);
                apt.Start = this.schedulerControl1.SelectedInterval.Start;
                apt.End = this.schedulerControl1.SelectedInterval.End;
            }
            this.schedulerControl1.ShowEditAppointmentForm(apt);
        }
        private void CreateAppointment_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CreateAppo();
        }
        private void SchedulerStorage1_AppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {
            appointmentsTableAdapter.Update(weddingAppDataSet);
            weddingAppDataSet.AcceptChanges();
        }
        public void UpdateTableAdapter()
        {
            appointmentsTableAdapter.Update(weddingAppDataSet);
            weddingAppDataSet.AcceptChanges();
        }
        private void SchedulerControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAllDayEvent);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringAppointment);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringEvent);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoThisDay);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoDate);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.SwitchToTimelineView);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.SwitchViewMenu);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.DeleteAppointment);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.EditSeries);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu);

            SchedulerMenuItem item1 = e.Menu.GetMenuItemById(SchedulerMenuItemId.NewAppointment);
            if (item1 != null)
            {
                item1.Caption = NewAppo;
            }
            SchedulerMenuItem item2 = e.Menu.GetMenuItemById(SchedulerMenuItemId.GotoToday);
            if (item2 != null)
            {
                item2.Caption = GoToToday;
            }
            SchedulerMenuItem item3 = e.Menu.GetMenuItemById(SchedulerMenuItemId.OpenAppointment);
            if (item3 != null)
                item3.Caption = Open;
        }
        private void SchedulerControl1_CustomizeAppointmentFlyout(object sender, CustomizeAppointmentFlyoutEventArgs e)
        {
            e.ShowSubject = true;
            e.ShowReminder = false;
            e.ShowLocation = false;
            e.ShowEndDate = false;
            e.ShowStartDate = false;
            e.ShowStatus = false;
            string Remain = string.Format("{0:c2}", Convert.ToDecimal(e.Appointment.CustomFields["RemainingPrice"].ToString()));
            e.Subject += "\n\n" +PhoneNumbers+ e.Appointment.CustomFields["PhoneNumber"].ToString()+"\n";
            e.Subject += "\n" +Mails+ e.Appointment.CustomFields["Mail"].ToString()+"\n";
            e.Subject += "\n" + PersonCount + e.Appointment.Location+"\n";
            if ((bool)e.Appointment.CustomFields["Food"] == true)
                e.Subject += "\n"+Food+"\n";
            if (e.Appointment.CustomFields["RemainingPrice"].ToString() != "")
            {
                if (e.Appointment.CustomFields["RemainingPrice"].ToString()=="0")
                    e.Subject += "\n" + Prices;
                else
                    e.Subject += "\n" + Rema + Remain;
            }
        }
        public void GriID()
        {
            var iD = gridView1.GetFocusedRowCellValue("UniqueID");
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.Information where ID='" + iD + "' Order by RemainingPrice asc , StartDate desc ", con);
            DataSet dt = new DataSet();
            sda.Fill(dt, "dbo.Information");
            gridControl2.DataSource = dt.Tables[0];
        }
        List lis;
        private void DeleteAppointment_CLick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lis = new List(this);
            try { lis.ShowDelete = true; lis.labelControl1.Text = DeleteList; lis.ShowDialog(); }
            catch { lis.Dispose(); }
        }
        private void UpdateAppointment_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lis = new List(this);
            try { lis.labelControl1.Text = UpdateList; lis.ShowDialog(); }
            finally { lis.Dispose();  }
        }

        public void DataRefres()
        {
            gridControl1.DataSource = appointmentsBindingSource;
            listBoxControl1.DataSource = appointmentsBindingSource;
            gridControl2.DataSource = null;
            SqlDataAdapter sad = new SqlDataAdapter("Select * from dbo.Information where ID='" + gridView1.GetFocusedRowCellValue("UniqueID") + "' order by RemainingPrice asc , StartDate desc", con);
            DataSet sd = new DataSet();
            sad.Fill(sd, "dbo.Information");
            gridControl2.DataSource = sd.Tables[0];
            appointmentsTableAdapter.Adapter.Update(weddingAppDataSet);
            weddingAppDataSet.AcceptChanges();

        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GriID();
        }

        private void TextEdit1_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.Appointments where Subject Like '%" + textEdit1.Text + "%'", con);
            DataSet dt = new DataSet();
            sda.Fill(dt, "dbo.Appointments");
            listBoxControl1.DataSource = dt.Tables["dbo.Appointments"];
            gridControl1.DataSource = dt.Tables["dbo.Appointments"];
            GriID();
            if (textEdit1.Text == "" || textEdit1.Text == null)
            {
                textEdit1.Properties.NullValuePrompt = "Aranacak İsmi Giriniz...";
                textEdit1.Properties.NullValuePromptShowForEmptyValue = true;
            }
        }
        private void AppointmentPrevview_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
        private void InformationPrevview_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl2.ShowRibbonPrintPreview();
        }
        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            LoadAppo();
        }
        private void ListBoxControl1_DoubleClick(object sender, EventArgs e)
        {
            LoadAppo();
        }
        private int HallCount()
        {
            int ID;
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.HallCustom", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("Update dbo.HallCustom set ID='"+(i+1)+"' where UniqueID='" + dt.Rows[i]["UniqueID"] + "'", con);
                try { con.Open();cmd.ExecuteNonQuery(); }
                catch(Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { con.Close();}
            }
            SqlDataAdapter sad = new SqlDataAdapter("Select Top(1) ID from dbo.HallCustom order by ID desc", con);
            DataTable td = new DataTable();
            sad.Fill(td);
            ID = Convert.ToInt32(td.Rows[0]["ID"])+1;
            return ID;
        }
        public void HallCustom()
        {
            schedulerControl1.Storage.Appointments.Labels.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.HallCustom", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            IAppointmentLabelStorage labelStorage = schedulerControl1.Storage.Appointments.Labels;
            labelStorage.Clear();
            IAppointmentLabel lbl = labelStorage.CreateNewLabel(0, Hall);
            lbl.SetColor(Color.White);
            labelStorage.Add(lbl);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                string[] Coor = dt.Rows[i]["Color"].ToString().Split('=',',',']');
                Color a = Color.FromArgb(Convert.ToInt32(Coor[3]), Convert.ToInt32(Coor[5]), Convert.ToInt32(Coor[7]));
                string Name = dt.Rows[i]["Name"].ToString()+"   ("+dt.Rows[i]["PersonCount"]+")";
                object Mennu = dt.Rows[i]["ID"].ToString();
                schedulerStorage1.Appointments.Labels.BeginUpdate();
                IAppointmentLabel label = labelStorage.CreateNewLabel(i + 1, Name);
                label.SetColor(a);
                labelStorage.Add(label);
                schedulerControl1.DataStorage.RefreshData();
                schedulerStorage1.Appointments.Labels.EndUpdate();
            }
        }
        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HY = new History(this);
            try { HY.ShowDialog(); }
            finally { HY.Dispose(); }
        }
        
        DisplaySettings DS;
        private void BarButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DS = new DisplaySettings(this);
            try
            {
                DS.ShowDialog();
            }
            finally { DS.Dispose(); }
        }
        Help HP;
        private void BarButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HP = new Help(this);
            try { HP.ShowDialog(); }
            finally { HP.Dispose(); }
        }
    }
}
