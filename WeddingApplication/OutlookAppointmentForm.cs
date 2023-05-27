using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Internal;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Native;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.iCalendar;
using DevExpress.XtraScheduler.Localization;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraScheduler.Printing;
using DevExpress.XtraScheduler.Printing.Native;
using DevExpress.XtraScheduler.UI;
using DevExpress.XtraScheduler.Commands;
using DevExpress.XtraScheduler.Services;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Globalization;

namespace WeddingApplication
{
    /// <summary>
    /// Summary description for AppointmentRibbonForm.
    /// </summary>
    public partial class OutlookAppointmentForm : DevExpress.XtraEditors.XtraForm, IDXManagerPopupMenu
    {
        #region Fields
        bool openRecurrenceForm;
        readonly ISchedulerStorage storage;
        readonly SchedulerControl control;
        Icon recurringIcon;
        Icon normalIcon;
        readonly AppointmentFormController controller;
        IDXMenuManager menuManager;
        bool supressCancelCore;
        string _PhoneNumber = string.Empty;
        string _Mail = string.Empty;
        string _Price = string.Empty;
        string _GivenPrice = string.Empty;
        string _RemainingPrice = string.Empty;
        bool _Food = false;
        string _Hall = string.Empty;
        bool _MailCheck = false;
        #endregion
        [EditorBrowsable(EditorBrowsableState.Never)]
        public OutlookAppointmentForm()
        {
            InitializeComponent();
        }
        DisplaySettings DS;
        public OutlookAppointmentForm(DevExpress.XtraScheduler.SchedulerControl control, Appointment apt,Form1 frm)
            : this(control, apt, false,frm)
        {
        }
        public OutlookAppointmentForm(DevExpress.XtraScheduler.SchedulerControl control, Appointment apt, bool openRecurrenceForm, Form1 frm)
        {
            Guard.ArgumentNotNull(control, "control");
            Guard.ArgumentNotNull(control.DataStorage, "control.DataStorage");
            Guard.ArgumentNotNull(apt, "apt");
            frm1 = frm;
            this.openRecurrenceForm = openRecurrenceForm;
            this.controller = CreateController(control, apt);
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            LoadIcons();

            this.control = control;
            this.storage = control.DataStorage;
            

            this.riAppointmentResource.SchedulerControl = control;
            this.riAppointmentResource.Storage = storage;
            this.riAppointmentStatus.Storage = storage;

            this.riAppointmentLabel.Storage = storage;

            BindControllerToControls();

            LookAndFeel.ParentLookAndFeel = control.LookAndFeel;
            this.defaultBarAndDockingController.Controller.LookAndFeel.ParentLookAndFeel = LookAndFeel;

            this.supressCancelCore = false;
        }
        #region Properties
        [Browsable(false)]
        public IDXMenuManager MenuManager { get { return menuManager; } private set { menuManager = value; } }
        protected internal AppointmentFormController Controller { get { return controller; } }
        protected internal SchedulerControl Control { get { return control; } }
        protected internal ISchedulerStorage Storage { get { return storage; } }
        protected internal bool IsNewAppointment { get { return controller != null ? controller.IsNewAppointment : true; } }
        protected internal Icon RecurringIcon { get { return recurringIcon; } }
        protected internal Icon NormalIcon { get { return normalIcon; } }
        protected internal bool OpenRecurrenceForm { get { return openRecurrenceForm; } }
        [DXDescription("DevExpress.XtraScheduler.UI.AppointmentRibbonForm,ReadOnly")]
        [DXCategory(CategoryName.Behavior)]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return Controller.ReadOnly; }
            set
            {
                if (Controller.ReadOnly == value)
                    return;
                Controller.ReadOnly = value;
            }
        }
        protected override FormShowMode ShowMode { get { return DevExpress.XtraEditors.FormShowMode.AfterInitialization; } }
        #endregion
        
        private void Gp_TextChanged(object sender, EventArgs e)
        {
            if (GivenPrice.Text == "")
                GivenPrice.Text = "0";
            if (!string.IsNullOrEmpty(GivenPrice.Text))
            {
                decimal sayi = Convert.ToDecimal(GivenPrice.Text);
                GivenPrice.Text = sayi.ToString("#,##0");
                GivenPrice.SelectionStart = GivenPrice.Text.Length;
            }
        }
        private void Price_TextChanged(object sender, EventArgs e)
        {
            if (Price.Text == "")
                Price.Text = "0";
            if (!string.IsNullOrEmpty(Price.Text))
            {
                decimal sayi = Convert.ToDecimal(Price.Text);
                Price.Text = sayi.ToString("#,##0");
                Price.SelectionStart = Price.Text.Length;
            }
        }
        string Messages = string.Empty;
        string Internet = string.Empty;
        private void Translate()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select top(1) * from dbo.DisplaySettings where Name='Language'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0]["Description"].ToString() == "Turkish")
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("tr-TR");
                WeddingApplication.Properties.Resources.Culture = (new CultureInfo("tr-TR"));
                Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
                lblSubject.Text = "Ad Soyad :";
                lblPhoneNumber.Text = "Telefon No:";
                lblMail.Text = "Mail:";
                lblLocation.Text = "Kişi Sayısı:";
                lblStartTime.Text = "Başlangıç Tarihi:";
                chkAllDay.Text = "Bütün Gün:";
                lblPrice.Text = "Anlaşma Fiyat:";
                lblGivenPrice.Text = "Alınan Fiyat:";
                lblRemaininigPrice.Text = "Kalan Fiyat:";
                lblSumGivenPrice.Text = "Toplam Alınan F. :";
                lblEndTime.Text = "Bitiş Tarihi:";
                lblDescription.Text = "Açıklama:";
                MailCheck.Text = "İletişim";
                barLabel.Caption = "Salon";
                btnSaveAndClose.Caption = "Kaydet ve Kapat";
                CheckFood.Text = "Yemek ?";
                Messages = "Mail bilgilendirme ayarlarını kontrol ediniz.";
                Internet = "İnternet kontrolü yapınız";
            }
            if (dt.Rows[0]["Description"].ToString() == "English")
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                WeddingApplication.Properties.Resources.Culture = (new CultureInfo("en"));
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                lblSubject.Text = "Name:";
                lblPhoneNumber.Text = "Phone No:";
                lblMail.Text = "Mail:";
                lblLocation.Text = "Person Count:";
                lblStartTime.Text = "Start Date:";
                chkAllDay.Text = "All Day:";
                lblPrice.Text = "Deal Price:";
                lblGivenPrice.Text = "Price Received:";
                lblRemaininigPrice.Text = "Remaining Price:";
                lblSumGivenPrice.Text = "Total Received Pr.:";
                lblEndTime.Text = "End Date:";
                lblDescription.Text = "Description:";
                MailCheck.Text = "Communication";
                barLabel.Caption = "Hall";
                btnSaveAndClose.Caption = "Save and Close";
                CheckFood.Text = "Food ?";
                Messages = "Check your mail notification settings";
                Internet = "check the internet";
            }
        }
        public virtual void LoadFormData(Appointment appointment)
        {
            Translate();
            if (appointment.CustomFields["PhoneNumber"]!= null)
                _PhoneNumber = appointment.CustomFields["PhoneNumber"].ToString();
            if (appointment.CustomFields["Mail"] != null)
                _Mail = appointment.CustomFields["Mail"].ToString();
            if (appointment.CustomFields["Price"] != null)
                _Price = appointment.CustomFields["Price"].ToString();
            if (appointment.CustomFields["RemainingPrice"] != null)
                _RemainingPrice = appointment.CustomFields["RemainingPrice"].ToString();
            if (appointment.CustomFields["Food"] != null)
                _Food = Convert.ToBoolean(appointment.CustomFields["Food"]);
            if (appointment.CustomFields["GivenPrice"] != null && (int)appointment.CustomFields["GivenPrice"] < (int)appointment.CustomFields["Price"])
                _GivenPrice = appointment.CustomFields["GivenPrice"].ToString();
            if (appointment.CustomFields["GivenPrice"] != null && (int)appointment.CustomFields["GivenPrice"] > (int)appointment.CustomFields["Price"])
            {
                _RemainingPrice = "Hesap Ödendi";
                _GivenPrice = appointment.CustomFields["Price"].ToString();
            }
            if (appointment.CustomFields["MailCheck"] != null)
                _MailCheck = Convert.ToBoolean(appointment.CustomFields["MailCheck"]);
            if (appointment.CustomFields["Hall"] != null)
                _Hall = appointment.CustomFields["Hall"].ToString();
            PhoneNumber.Text = _PhoneNumber;
            Mail.Text = _Mail;
            Price.Text = _Price;
            SumGivenPrice.Text = _GivenPrice;
            RemainingPrice.Text = _RemainingPrice;
            CheckFood.Checked = _Food;
            SumGivenPrice.Text = string.Format("{0:C2}", Convert.ToDecimal(appointment.CustomFields["GivenPrice"]));
            RemainingPrice.Text = string.Format("{0:C2}", Convert.ToDecimal(appointment.CustomFields["RemainingPrice"]));
            MailCheck.Checked = _MailCheck;
            tbLocation.Enabled = false;
            string[] PersonCount = barLabel.EditValue.ToString().Split('(', ')');
            
            if (appointment.CustomFields["Hall"]!=null &&( barLabel.EditValue.ToString() != "Hepsi" && barLabel.EditValue.ToString() != "All"))
                if (tbLocation.Text != PersonCount[1])
                    checkBox1.Checked = true;
        }
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-LR9KKE2U\\SQLEXPRESS;Initial Catalog=WeddingApp;Integrated Security=True");
        private string SubjectUpdate()
        {
            return "Kayıt Güncelleme";
        }
        private string SubjectInsert()
        {
            return "Kayıt Ekleme";
        }
        private string BodyUpdateAdmin(string GIVEN, string REMAİNİNG)
        {

            return DateTime.Now.ToShortDateString()+" tarihinde "+ tbSubject.Text+" isimli "+ PhoneNumber.Text +" telefon numaralı kişi "+ GIVEN +" TL ödedi Kalan Tutar "+REMAİNİNG+" TL";
        }
        private string BodyInsertAdmin(string remainiingPRice)
        {
            return DateTime.Now.ToShortDateString() + " tarihinde " + tbSubject.Text + " isimli " + PhoneNumber.Text + " telefon numaralı kişi kaydı Oluşturuldu Kalan Tutar " + remainiingPRice;
        }
        private string BodyUpdateCustomer(string Given,string Remaining)
        {
            return "Sayın " + tbSubject.Text.ToUpper() + " " + DateTime.Now.ToShortDateString() + " tarihinde ödediğiniz " + Given + " TL işleme alınmıştır. Kalan borcunuz " + Remaining + " TL. İyi Günler...";
        }
        private string BodyInsertCustomer()
        {
            return "Sayın " + tbSubject.Text.ToUpper() + " " + DateTime.Now.ToShortDateString() + " tarihinde oluşturduğunuz " + edtStartDate.Text + " tarihli randevunuz işleme alımıştır";
        }
        List Lt;
        public void Insert()
        {
            int count = 0;
            SqlDataAdapter uniqu = new SqlDataAdapter("Select * from dbo.Appointments where Subject='" + tbSubject.Text + "' and PhoneNumber='" + PhoneNumber.Text + "'", con);
            DataTable getir = new DataTable();
            uniqu.Fill(getir);
            count += getir.Rows.Count;
            int sumprice = 0;
            int price = 0;
            int givenprice = 0;
            string p = string.Empty;
            string g = string.Empty;
            if (SumGivenPrice.Text == "₺0,00" || SumGivenPrice.Text == "₺0.00" || SumGivenPrice.Text == "0" || SumGivenPrice.Text == "" || SumGivenPrice.Text == "0,00")
            {
                sumprice = 0;
            }
            else
            {
                sumprice = Convert.ToInt32(getir.Rows[0]["GivenPrice"]);
            }
            if (GivenPrice.Text != "")
            {
                string[] gp = GivenPrice.Text.Split('.',',');
                for (int i = 0; i < gp.Length; i++)
                {
                    g += gp[i];
                    
                }
                givenprice = Convert.ToInt32(g);
            }
            if (Price.Text != "")
            {
                string[] u = Price.Text.Split('.',',');
                for (int i = 0; i < u.Length; i++)
                {
                    p += u[i];
                    
                }
                price = Convert.ToInt32(p);
            }
            if (count > 0)
            {
                SqlDataAdapter sad = new SqlDataAdapter("Select * from dbo.Information where ID='" + (int)getir.Rows[0]["UniqueID"] + "' order by RemainingPrice asc", con);
                DataTable dt = new DataTable();
                sad.Fill(dt);
                DS = new DisplaySettings(this.frm1);
                Lt = new List(frm1);
                //try
                //{
                    if (dt.Rows.Count == 0)
                    {
                        Lt.HistoryInsert("Kayıt Eklendi", tbSubject.Text, PhoneNumber.Text, getir.Rows[0]["RemainingPrice"].ToString());
                    }
                    else
                    {
                        Lt.HistoryInsert("Kayıt Güncellendi", tbSubject.Text, PhoneNumber.Text, getir.Rows[0]["RemainingPrice"].ToString());
                    }
                    SqlDataAdapter sd = new SqlDataAdapter("Select TandF,Description from dbo.DisplaySettings where Name='Mail'", con);DataTable dtq = new DataTable(); sd.Fill(dtq);

                    if (!string.IsNullOrEmpty(GivenPrice.Text) && MailCheck.Checked==true && (bool)dtq.Rows[0]["TandF"]==true && !string.IsNullOrEmpty(dtq.Rows[0]["Description"].ToString()))
                    {
                        string Remaining;
                        string Given;
                        decimal sayi = Convert.ToDecimal(getir.Rows[0]["RemainingPrice"]);
                        Remaining = (string.Format("{0:c2}", sayi.ToString("#,##0")));
                        decimal givenSay = Convert.ToDecimal(GivenPrice.Text.Trim('.',','));
                        Given = string.Format("{0:c2}", givenSay.ToString("#,##0"));

                        if (dt.Rows.Count == 0)
                        {
                            DS.SendEmail(DS.MailAdmin(), SubjectInsert(), BodyInsertAdmin(Remaining));

                            DS.SendEmail(Mail.Text, SubjectInsert(), BodyInsertCustomer());
                        }
                        else
                        {
                            DS.SendEmail(Mail.Text, SubjectUpdate(), BodyUpdateCustomer(Given, Remaining));

                            DS.SendEmail(DS.MailAdmin(), SubjectUpdate(), BodyUpdateAdmin(Given, Remaining));
                        }
                    }
                //}
                //catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                int id = 0;
                id += Convert.ToInt32(getir.Rows[0]["UniqueID"]);
                SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.Information where ID='" + id + "'", con);
                DataTable dt1 = new DataTable();
                sda.Fill(dt1);
                if (GivenPrice.Text != "" || dt1.Rows.Count == 0)
                {
                    //try
                    //{
                        SqlCommand cmd = new SqlCommand("Insert Into dbo.Information(ID,StartDate,Subject,Location,Description,Hall,PhoneNumber,Mail,Price,GivenPrice,RemainingPrice) values (@ID,@StartDate,@Subject,@Location,@Description,@Hall,@PhoneNumber,@Mail,@Price,@GivenPrice,@RemainingPrice)", con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Subject", tbSubject.Text.ToString());
                        cmd.Parameters.AddWithValue("@Location", getir.Rows[0]["Location"].ToString());
                        cmd.Parameters.AddWithValue("@Description", tbDescription.Text.ToString());
                        cmd.Parameters.AddWithValue("@Hall", getir.Rows[0]["Hall"].ToString());
                        cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber.Text.ToString());
                        cmd.Parameters.AddWithValue("@Mail", Mail.Text.ToString());
                        cmd.Parameters.AddWithValue("@Price", Convert.ToInt32(price));
                        cmd.Parameters.AddWithValue("@GivenPrice", Convert.ToInt32(givenprice));
                        cmd.Parameters.AddWithValue("@RemainingPrice", Convert.ToInt32(getir.Rows[0]["RemainingPrice"]));
                        cmd.ExecuteNonQuery();
                        con.Close();
                    //}
                    //catch (Exception es)
                    //{
                    //    DevExpress.XtraEditors.XtraMessageBox.Show("Hata Oluştu : " + es.ToString());
                    //}
                }
                else
                {
                    string Locations = getir.Rows[0]["Location"].ToString();
                    SqlCommand cmd = new SqlCommand("Update dbo.Information set Subject='" + tbSubject.Text + "', Description='" + tbDescription.Text + "',PhoneNumber='" + this.PhoneNumber.Text + "',Mail='" + Mail.Text + "',Location='" + Locations + "' ,Hall='" + getir.Rows[0]["Hall"] + "' where ID='" + id + "'", con);
                    //try
                    //{
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //    DevExpress.XtraEditors.XtraMessageBox.Show("Hata : " + ex.Message);
                    //}
                }
            }
        }
        public virtual bool SaveFormData(Appointment appointment)
        {
            int a = 1;
            int sumprice = 0;
            int price = 0;
            int givenprice = 0;
            string p = string.Empty;
            string g = string.Empty;
            if (appointment.CustomFields["GivenPrice"] == null)
            {
                sumprice = 0;
            }
            else
            {
                sumprice = Convert.ToInt32(appointment.CustomFields["GivenPrice"]);
            }
            if (GivenPrice.Text != "")
            {
                string[] gp = GivenPrice.Text.Split(',', '.', ' ', '₺');
                for (int i = 0; i < gp.Length; i++)
                {
                    g += gp[i];
                    givenprice = Convert.ToInt32(g);
                }
            }
            if (Price.Text != "")
            {
                string[] u = Price.Text.ToString().Split(',', '.', ' ', '₺');
                for (int i = 0; i < u.Length; i++)
                {
                    p += u[i];
                    price = Convert.ToInt32(p);
                }
            }
            appointment.CustomFields["PhoneNumber"] = this.PhoneNumber.Text;
            appointment.CustomFields["Mail"] = Mail.Text;
            if (Price.Text == "")
            {
                Price.Text = "0";
            }
            appointment.CustomFields["GivenPrice"] = Convert.ToInt32(sumprice) + Convert.ToInt32(givenprice);
            if (Convert.ToInt32(price) < (Convert.ToInt32(sumprice) + Convert.ToInt32(givenprice)))
                a = 0;
            appointment.CustomFields["RemainingPrice"] = (Convert.ToInt32(price) - Convert.ToInt32(givenprice) - Convert.ToInt32(sumprice)) * a;
            if (CheckFood.Checked != true)
                CheckFood.Checked = false;
            appointment.CustomFields["MailCheck"] = MailCheck.Checked;
            appointment.CustomFields["Price"] = Convert.ToInt32(price);
            appointment.CustomFields["Food"] = CheckFood.Checked;
            SqlDataAdapter sda = new SqlDataAdapter("Select * from dbo.HallCustom where Name='" + barLabel.EditValue.ToString() + "'", con);
            DataTable dp = new DataTable();
            sda.Fill(dp);
            appointment.CustomFields["Hall"] = barLabel.EditValue.ToString();
            string Personcount=string.Empty;
            if (barLabel.EditValue.ToString() != "Hepsi" && barLabel.EditValue.ToString() != "All")
            {
                
                string[] PersonCount = barLabel.EditValue.ToString().Split('(', ')');
                Personcount = PersonCount[1];
            }
            if (checkBox1.Checked == false && !string.IsNullOrEmpty(Personcount))
                    tbLocation.Text = Personcount;
            if (string.IsNullOrEmpty(Mail.Text))
                _Mail = ".";
            return true;
        }
        public virtual bool IsAppointmentChanged(Appointment appointment)
        {
            if (_MailCheck==(bool)appointment.CustomFields["MailCheck"]  && _Hall==appointment.CustomFields["Hall"].ToString() && _PhoneNumber == appointment.CustomFields["PhoneNumber"].ToString() && _Mail == appointment.CustomFields["Mail"].ToString() && _Price == appointment.CustomFields["Price"].ToString() && _GivenPrice == appointment.CustomFields["GivenPrice"].ToString() && _RemainingPrice == appointment.CustomFields["RemainingPrice"].ToString() && _Food == (bool)appointment.CustomFields["Food"])
                return false;
            else
                return true;
        }
        public virtual void SetMenuManager(DevExpress.Utils.Menu.IDXMenuManager menuManager)
        {
            MenuManagerUtils.SetMenuManager(Controls, menuManager);
            this.menuManager = menuManager;
        }
        
        protected virtual void BindControllerToControls()
        {
            this.DataBindings.Add("Text", Controller, "Caption");
            BindControllerToIcon();
            BindProperties(this.tbSubject, "Text", "Subject");
            BindProperties(this.tbLocation, "Text", "Location");
            BindProperties(this.tbDescription, "Text", "Description");
            BindProperties(this.edtStartDate, "EditValue", "DisplayStartDate");
            BindProperties(this.edtStartDate, "Enabled", "IsDateTimeEditable");
            BindProperties(this.edtStartTime, "EditValue", "DisplayStartTime");
            BindProperties(this.edtStartTime, "Enabled", "IsTimeEnabled");
            BindProperties(this.edtEndDate, "EditValue", "DisplayEndDate", DataSourceUpdateMode.Never);
            BindProperties(this.edtEndDate, "Enabled", "IsDateTimeEditable", DataSourceUpdateMode.Never);
            BindProperties(this.edtEndTime, "EditValue", "DisplayEndTime", DataSourceUpdateMode.Never);
            BindProperties(this.edtEndTime, "Enabled", "IsTimeEnabled", DataSourceUpdateMode.Never);
            BindProperties(this.chkAllDay, "Checked", "AllDay");
            BindProperties(this.chkAllDay, "Enabled", "IsDateTimeEditable");

            BindProperties(this.barLabel, "EditValue", "Label");


            BindToBoolPropertyAndInvert(this.ribbonControl1, "Enabled", "ReadOnly");
        }

        protected virtual void BindControllerToIcon()
        {
            Binding binding = new Binding("Icon", Controller, "AppointmentType");
            binding.Format += AppointmentTypeToIconConverter;
            DataBindings.Add(binding);
        }
        protected virtual void ObjectToStringConverter(object o, ConvertEventArgs e)
        {
            e.Value = e.Value.ToString();
        }
        protected virtual void AppointmentTypeToIconConverter(object o, ConvertEventArgs e)
        {
            AppointmentType type = (AppointmentType)e.Value;
            if (type == AppointmentType.Pattern)
                e.Value = RecurringIcon;
            else
                e.Value = NormalIcon;
        }
        protected virtual void BindProperties(Control target, string targetProperty, string sourceProperty)
        {
            BindProperties(target, targetProperty, sourceProperty, DataSourceUpdateMode.OnPropertyChanged);
        }
        protected virtual void BindProperties(Control target, string targetProperty, string sourceProperty, DataSourceUpdateMode updateMode)
        {
            target.DataBindings.Add(targetProperty, Controller, sourceProperty, true, updateMode);
            BindToIsReadOnly(target, updateMode);
        }
        protected virtual void BindProperties(Control target, string targetProperty, string sourceProperty, ConvertEventHandler objectToStringConverter)
        {
            Binding binding = new Binding(targetProperty, Controller, sourceProperty, true);
            binding.Format += objectToStringConverter;
            target.DataBindings.Add(binding);
        }
        protected virtual void BindToBoolPropertyAndInvert(Control target, string targetProperty, string sourceProperty)
        {
            target.DataBindings.Add(new BoolInvertBinding(targetProperty, Controller, sourceProperty));
            BindToIsReadOnly(target);
        }
        protected virtual void BindToIsReadOnly(Control control)
        {
            BindToIsReadOnly(control, DataSourceUpdateMode.OnPropertyChanged);
        }
        protected virtual void BindToIsReadOnly(Control control, DataSourceUpdateMode updateMode)
        {
            if ((!(control is BaseEdit)) || control.DataBindings["ReadOnly"] != null)
                return;
            control.DataBindings.Add("ReadOnly", Controller, "ReadOnly", true, updateMode);
        }

        protected virtual void BindProperties(DevExpress.XtraBars.BarItem target, string targetProperty, string sourceProperty)
        {
            BindProperties(target, targetProperty, sourceProperty, DataSourceUpdateMode.OnPropertyChanged);
        }
        protected virtual void BindProperties(DevExpress.XtraBars.BarItem target, string targetProperty, string sourceProperty, DataSourceUpdateMode updateMode)
        {
            target.DataBindings.Add(targetProperty, Controller, sourceProperty, true, updateMode);
        }
        protected virtual void BindProperties(DevExpress.XtraBars.BarItem target, string targetProperty, string sourceProperty, ConvertEventHandler objectToStringConverter)
        {
            Binding binding = new Binding(targetProperty, Controller, sourceProperty, true);
            binding.Format += objectToStringConverter;
            target.DataBindings.Add(binding);
        }
        protected virtual void BindToBoolPropertyAndInvert(DevExpress.XtraBars.BarItem target, string targetProperty, string sourceProperty)
        {
            target.DataBindings.Add(new BoolInvertBinding(targetProperty, Controller, sourceProperty));
        }
        protected virtual void BindBoolToVisibility(DevExpress.XtraBars.BarItem target, string targetProperty, string sourceProperty)
        {
            target.DataBindings.Add(new BoolToVisibilityBinding(targetProperty, Controller, sourceProperty, false));
        }
        protected virtual void BindBoolToVisibility(DevExpress.XtraBars.BarItem target, string targetProperty, string sourceProperty, bool invert)
        {
            target.DataBindings.Add(new BoolToVisibilityBinding(targetProperty, Controller, sourceProperty, invert));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Controller == null)
                return;
            SubscribeControlsEvents();
            LoadFormData(Controller.EditedAppointmentCopy);
        }

        protected virtual AppointmentFormController CreateController(SchedulerControl control, Appointment apt)
        {
            return new AppointmentFormController(control, apt);
        }
        protected internal virtual void LoadIcons()
        {
            Assembly asm = typeof(SchedulerControl).Assembly;
            this.recurringIcon = ResourceImageHelper.CreateIconFromResources(SchedulerIconNames.RecurringAppointment, asm);
            this.normalIcon = ResourceImageHelper.CreateIconFromResources(SchedulerIconNames.Appointment, asm);
        }
        protected internal virtual void SubscribeControlsEvents()
        {
            this.edtEndDate.Validating += new CancelEventHandler(OnEdtEndDateValidating);
            this.edtEndDate.InvalidValue += new InvalidValueExceptionEventHandler(OnEdtEndDateInvalidValue);
            this.edtEndTime.Validating += new CancelEventHandler(OnEdtEndTimeValidating);
            this.edtEndTime.InvalidValue += new InvalidValueExceptionEventHandler(OnEdtEndTimeInvalidValue);
            this.edtStartDate.Validating += new CancelEventHandler(OnEdtStartDateValidating);
            this.edtStartDate.InvalidValue += new InvalidValueExceptionEventHandler(OnEdtStartDateInvalidValue);
            this.edtStartTime.Validating += new CancelEventHandler(OnEdtStartTimeValidating);
            this.edtStartTime.InvalidValue += new InvalidValueExceptionEventHandler(OnEdtStartTimeInvalidValue);
        }

        protected internal virtual void UnsubscribeControlsEvents()
        {
            this.edtEndDate.Validating -= new CancelEventHandler(OnEdtEndDateValidating);
            this.edtEndDate.InvalidValue -= new InvalidValueExceptionEventHandler(OnEdtEndDateInvalidValue);
            this.edtEndTime.Validating -= new CancelEventHandler(OnEdtEndTimeValidating);
            this.edtEndTime.InvalidValue -= new InvalidValueExceptionEventHandler(OnEdtEndTimeInvalidValue);
            this.edtStartDate.Validating -= new CancelEventHandler(OnEdtStartDateValidating);
            this.edtStartDate.InvalidValue -= new InvalidValueExceptionEventHandler(OnEdtStartDateInvalidValue);
            this.edtStartTime.Validating -= new CancelEventHandler(OnEdtStartTimeValidating);
            this.edtStartTime.InvalidValue -= new InvalidValueExceptionEventHandler(OnEdtStartTimeInvalidValue);
        }

        protected internal virtual void OnEdtStartTimeInvalidValue(object sender, InvalidValueExceptionEventArgs e)
        {
            e.ErrorText = SchedulerLocalizer.GetString(SchedulerStringId.Msg_DateOutsideLimitInterval);
        }
        protected internal virtual void OnEdtStartTimeValidating(object sender, CancelEventArgs e)
        {
            e.Cancel = !Controller.ValidateLimitInterval(edtStartDate.DateTime.Date, edtStartTime.Time.TimeOfDay, edtEndDate.DateTime.Date, edtEndTime.Time.TimeOfDay);
        }
        protected internal virtual void OnEdtStartDateInvalidValue(object sender, InvalidValueExceptionEventArgs e)
        {
            e.ErrorText = SchedulerLocalizer.GetString(SchedulerStringId.Msg_DateOutsideLimitInterval);
        }
        protected internal virtual void OnEdtStartDateValidating(object sender, CancelEventArgs e)
        {
            e.Cancel = !Controller.ValidateLimitInterval(edtStartDate.DateTime.Date, edtStartTime.Time.TimeOfDay, edtEndDate.DateTime.Date, edtEndTime.Time.TimeOfDay);
        }
        protected internal virtual void OnEdtEndDateValidating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValidInterval();
            if (!e.Cancel)
                this.edtEndDate.DataBindings["EditValue"].WriteValue();
        }
        protected internal virtual void OnEdtEndDateInvalidValue(object sender, InvalidValueExceptionEventArgs e)
        {
            if (!AppointmentFormControllerBase.ValidateInterval(edtStartDate.DateTime.Date, edtStartTime.Time.TimeOfDay, edtEndDate.DateTime.Date, edtEndTime.Time.TimeOfDay))
                e.ErrorText = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate);
            else
                e.ErrorText = SchedulerLocalizer.GetString(SchedulerStringId.Msg_DateOutsideLimitInterval);
        }
        protected internal virtual void OnEdtEndTimeValidating(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsValidInterval();
            if (!e.Cancel)
                this.edtEndTime.DataBindings["EditValue"].WriteValue();
        }
        protected internal virtual void OnEdtEndTimeInvalidValue(object sender, InvalidValueExceptionEventArgs e)
        {
            if (!AppointmentFormControllerBase.ValidateInterval(edtStartDate.DateTime.Date, edtStartTime.Time.TimeOfDay, edtEndDate.DateTime.Date, edtEndTime.Time.TimeOfDay))
                e.ErrorText = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate);
            else
                e.ErrorText = SchedulerLocalizer.GetString(SchedulerStringId.Msg_DateOutsideLimitInterval);
        }
        protected internal virtual bool IsValidInterval()
        {
            return AppointmentFormControllerBase.ValidateInterval(edtStartDate.DateTime.Date, edtStartTime.Time.TimeOfDay, edtEndDate.DateTime.Date, edtEndTime.Time.TimeOfDay) &&
                Controller.ValidateLimitInterval(edtStartDate.DateTime.Date, edtStartTime.Time.TimeOfDay, edtEndDate.DateTime.Date, edtEndTime.Time.TimeOfDay);
        }
        protected internal virtual void OnOkButton()
        {
            Save(true);
        }
        protected virtual void OnSaveButton()
        {
            Save(false);
        }
        private void Save(bool closeAfterSave)
        {
            if (!ValidateDateAndTime())
                return;
            if (!SaveFormData(Controller.EditedAppointmentCopy))
                return;
            if (!Controller.IsConflictResolved())
            {
                ShowMessageBox(SchedulerLocalizer.GetString(SchedulerStringId.Msg_Conflict), Controller.GetMessageBoxCaption(SchedulerStringId.Msg_Conflict), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (IsAppointmentChanged(Controller.EditedAppointmentCopy) || Controller.IsAppointmentChanged() || Controller.IsNewAppointment)
                Controller.ApplyChanges();
            if (closeAfterSave)
            {
                this.supressCancelCore = true;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
        private bool ValidateDateAndTime()
        {
            this.edtEndDate.DoValidate();
            this.edtEndTime.DoValidate();
            this.edtStartDate.DoValidate();
            this.edtStartTime.DoValidate();

            return String.IsNullOrEmpty(this.edtEndTime.ErrorText) && String.IsNullOrEmpty(this.edtEndDate.ErrorText) && String.IsNullOrEmpty(this.edtStartDate.ErrorText) && String.IsNullOrEmpty(this.edtStartTime.ErrorText);
        }
        protected virtual void OnSaveAsButton()
        {
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                Filter = "iCalendar files (*.ics)|*.ics",
                FilterIndex = 1
            };
            if (fileDialog.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                using (Stream stream = fileDialog.OpenFile())
                    ExportAppointment(stream);
            }
            catch
            {
                ShowMessageBox("Error: could not export appointments", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ExportAppointment(Stream stream)
        {
            if (stream == null)
                return;

            AppointmentBaseCollection aptsToExport = new AppointmentBaseCollection
            {
                Controller.EditedAppointmentCopy
            };
            iCalendarExporter exporter = new iCalendarExporter(this.storage, aptsToExport)
            {
                ProductIdentifier = "-//Developer Express Inc."
            };
            exporter.Export(stream);
        }
        protected internal virtual DialogResult ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return XtraMessageBox.Show(this, text, caption, buttons, icon);
        }
        protected internal virtual void OnDeleteButton()
        {
            if (IsNewAppointment)
                return;

            Controller.DeleteAppointment();

            DialogResult = DialogResult.Abort;
            Close();
        }
        public void DeleteButton()
        {
            OnDeleteButton();
        }
        protected internal virtual void OnRecurrenceButton()
        {
            if (!Controller.ShouldShowRecurrenceButton)
                return;

            Appointment patternCopy = Controller.PrepareToRecurrenceEdit();

            DialogResult result;
            using (Form form = CreateAppointmentRecurrenceForm(patternCopy, Control.OptionsView.FirstDayOfWeek))
            {
                result = ShowRecurrenceForm(form);
            }

            if (result == DialogResult.Abort)
            {
                Controller.RemoveRecurrence();
            }
            else if (result == DialogResult.OK)
            {
                Controller.ApplyRecurrence(patternCopy);
            }
            
        }
        protected virtual void OnCloseButton()
        {
            this.Close();
        }

        private bool CancelCore()
        {
            bool result = true;

            if (DialogResult != System.Windows.Forms.DialogResult.Abort && Controller != null && Controller.IsAppointmentChanged() && !this.supressCancelCore)
            {
                DialogResult dialogResult = ShowMessageBox(SchedulerLocalizer.GetString(SchedulerStringId.Msg_SaveBeforeClose), Controller.GetMessageBoxCaption(SchedulerStringId.Msg_SaveBeforeClose), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                    result = false;
                else if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                    Save(true);
            }

            return result;
        }

        protected virtual DialogResult ShowRecurrenceForm(Form form)
        {
            return FormTouchUIAdapter.ShowDialog(form, this);
        }
        protected internal virtual Form CreateAppointmentRecurrenceForm(Appointment patternCopy, FirstDayOfWeek firstDayOfWeek)
        {
            AppointmentRecurrenceForm form = new AppointmentRecurrenceForm(patternCopy, firstDayOfWeek, Controller);
            form.SetMenuManager(MenuManager);
            form.LookAndFeel.ParentLookAndFeel = LookAndFeel;
            form.ShowExceptionsRemoveMsgBox = controller.AreExceptionsPresent();
            return form;
        }
        internal void OnAppointmentFormActivated(object sender, EventArgs e)
        {
            if (openRecurrenceForm)
            {
                openRecurrenceForm = false;
                OnRecurrenceButton();
            }
        }

        protected internal virtual void OnNextButton()
        {
            if (CancelCore())
            {
                this.supressCancelCore = true;
                OpenNextAppointmentCommand command = new OpenNextAppointmentCommand(Control);
                command.Execute();
                this.Close();
            }
        }

        protected internal virtual void OnPreviousButton()
        {
            if (CancelCore())
            {
                this.supressCancelCore = true;
                OpenPrevAppointmentCommand command = new OpenPrevAppointmentCommand(Control);
                command.Execute();
                this.Close();
            }
        }

        protected internal virtual void OnTimeZonesButton()
        {
            Controller.TimeZoneVisible = !Controller.TimeZoneVisible;
        }
        Form1 frm1;
        private void BtnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnOkButton();
            Insert();
            frm1.DefaultValues();
            frm1.AppointmentCount();
            frm1.PriceRGivn();
            frm1.ShowDefaultChartControl();
            frm1.GriID();
        }
        private void BarButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnDeleteButton();
        }

        private void BarRecurrence_ItemClick(object sender, ItemClickEventArgs e)
        {
            OnRecurrenceButton();
        }

        private void BvbSave_ItemClick(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            OnSaveButton();
        }

        private void BvbSaveAs_ItemClick(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            OnSaveAsButton();
        }

        private void BvbClose_ItemClick(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            OnCloseButton();
        }

        private void BtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            OnSaveButton();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = !CancelCore();
            base.OnClosing(e);
        }

        private void BtnNext_ItemClick(object sender, ItemClickEventArgs e)
        {
            OnNextButton();
        }

        private void BtnPrevious_ItemClick(object sender, ItemClickEventArgs e)
        {
            OnPreviousButton();
        }

        private void BtnTimeZones_ItemClick(object sender, ItemClickEventArgs e)
        {
            OnTimeZonesButton();
        }
        
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
                if (LookAndFeel != null)
                    LookAndFeel.ParentLookAndFeel = null;
                this.defaultBarAndDockingController.Controller.LookAndFeel.ParentLookAndFeel = null;
                this.defaultBarAndDockingController.Dispose();
            }
            base.Dispose(disposing);
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                tbLocation.Enabled = true;
            if (checkBox1.Checked == false)
                tbLocation.Enabled = false;
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
        private bool Communication()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select TandF from dbo.DisplaySettings where Name='Mail'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if ((bool)dt.Rows[0]["TandF"] == true)
                return true;
            else
                return false;
        }
        private void MailCheck_CheckedChanged(object sender, EventArgs e)
        {
            bool Controls = InternetKontrol();
            if (Communication() == false)
            {
                MailCheck.Checked = false; DevExpress.XtraEditors.XtraMessageBox.Show(Messages);
                if (Controls == false)
                    DevExpress.XtraEditors.XtraMessageBox.Show(Internet); MailCheck.Checked = false;
            }
        }

        private void Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            
        }
    }
}