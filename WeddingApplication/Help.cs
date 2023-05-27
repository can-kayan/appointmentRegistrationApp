using DevExpress.LookAndFeel;
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
using WeddingApplication.Properties;

namespace WeddingApplication
{
    public partial class Help : DevExpress.XtraEditors.XtraForm
    {
        Form1 f1;
        string Links=string.Empty;
        public Help(Form1 frm1)
        {
            f1 = frm1;
            InitializeComponent();
        }
        string[] Contents = new string[9];
        private void Help_Load(object sender, EventArgs e)
        {
            Translate();
            ObjectValuePage();
        }
        private void Translate()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) Description from dbo.DisplaySettings where Name ='Language'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0]["Description"].ToString() == "Turkish")
            {
                Contents[0] = "\n\nTüm kayıtlar ve seçilen kaydın ne zaman ve hangi işlemi yaptığı burada görülebilir.\n\nSeçilen kayda çift tıklayarak o kaydı güncelleyebilirsiniz.\n\nArama bölümüne Adınızı/Soyadınızı girerek aramanızı kolaylaştırabilirsiniz.\n\nSeçilen kayıt, işlemleri gösteren tablonun altında kalan fiyatı gösterir.";
                Contents[1] = "\n\nYeni kayıt ekleyebilir, mevcut kayıtları güncelleyebilirsiniz.\n\nKayıt bilgilerini görüntüleyebilirsiniz.";
                Contents[2] = "\n\nToplam Randevu Sayısı, Aylık Randevu Sayısı, Yapılacak Randevu Sayısı gibi analizleri gösterir.\n\nFiyatın aylara, yıllara göre dağılımı ve Gelen-Kalan oranı.";
                Contents[3] = "\n\nKayıt bilgileri bu alana girilir.\n\nGüncelleme ve ekleme işlemi bu sayfadan yapılır.\n\nSalon seçimi yaptıktan sonra salonun kapasitesi kadar kişi sayısını otomatik olarak girer.\n\nFarklı bir kişi sayısı girmek için Kişi sayısı yanındaki butonu seçerek kendiniz farklı bir kişi sayısı girebilirsiniz.\n\nİletişim butonunu seçerseniz girdiğiniz G-mail adresine bilgilendirme maili gidecektir.\n\nNot:Mail göndermek için internet bağlantısı gereklidir.Aksi taktirde gönderim sağlanamayacaktır.";
                Contents[4] = "\n\nTema rengi,Uygulama dili ve geçmiş ayarları bu sayfadan yapılır.\n\nGeçmiş ayarı;seçtiğiniz güne göre geçmiş kendini temizler.\n\nGeçmiş sıfırlamak için 'Şimdi' butonuna tıklamanız yeterli olacaktır.\n\nNot:Kaydetme sonrası Uygulama kendini yeniden başlatacak. ";
                Contents[5] = "\n\nBilgilendirme ayarı açılırsa yapılan işlemleri(Ekleme,Güncelleme,Silme) bu sayfada ki G-mail adresine gönderir.\n\nSol üstteki link'e tıklayıp, gerekli adımları izleyerek şifre elde edebilirsiniz.\n\nŞifre alırken gönderim seçeneğini 'Posta' olarak girmelisiniz.\n\nNot:Bu işlem için internet bağlantısı gereklidir.Aksi taktirde gönderim sağlanamayacaktır.";
                Contents[6] = "\n\nKayıt Listesindeki tabloların hangi bilgileri göstereceğini bu sayfadan ayarlayabilirsiniz.\n\nNot:Kaydetme sonrası Uygulama kendini yeniden başlatacak.";
                Contents[7] = "\n\nAnaliz Sayfasındaki grafiklerin tipini ayarlayabilirsiniz.\n\nAnaliz sayfasındaki grafikleri gösterebilir ya da gizleyebilirsiniz.\n\nNot:Kaydetme sonrası Uygulama kendini yeniden başlatacak.";
                Contents[8] = "\n\nSalon bilgisi ekleyebilir ya da silebilirisiniz\n\nRenk seçeneği ile kayıtları kolayca ayırt edebilirsiniz\n\nKişi Sayısı bilgisine göre kayıt ekleme işleminde salon seçiminden sonra uygulama kişi sayısını burada girdiğiniz sayısı girer.\n(İsterseniz kolayca kişi sayısını kendiniz girebilirsiniz.İlgili sayfanın yardım bölümüne bakınız.)\n\nKayıtlı salonu silmek için tablodaki bilgilere bakarak silmek istediğiniz salonun ismine ya da bilgilerine çift tıklamanız yeterli olacaktır.\n\nNot:Kaydetme sonrası Uygulama kendini yeniden başlatacak.";
                Links = "https://support.google.com/accounts/answer/185833?hl=tr";
                pictureEdit1.EditValue = global::WeddingApplication.Properties.Resources.KayitListesi;
                pictureEdit2.EditValue = global::WeddingApplication.Properties.Resources.Takvim;
                pictureEdit3.EditValue = global::WeddingApplication.Properties.Resources.Analiz;
                pictureEdit4.EditValue = global::WeddingApplication.Properties.Resources.EkleVeGuncelleKayıt;
                pictureEdit5.EditValue = global::WeddingApplication.Properties.Resources.SistemAyarlari;
                pictureEdit6.EditValue = global::WeddingApplication.Properties.Resources.BilgilendirmeAyari;
                pictureEdit7.EditValue = global::WeddingApplication.Properties.Resources.TabloAyari;
                pictureEdit8.EditValue = global::WeddingApplication.Properties.Resources.AnalizAyari;
                pictureEdit9.EditValue = global::WeddingApplication.Properties.Resources.SalonAyari;
            }
            else if (dt.Rows[0]["Description"].ToString() == "English")
            {
                Contents[0] = "\n\nAll records and when and what action the selected record took can be seen here.\n\nYou can update that record by double-clicking on the selected record.\n\nYou can facilitate your search by entering your Name/Surname in the search box on the side. .\n\nThe selected record displays the remaining price at the bottom of the table showing the transactions.";
                Contents[1] = "\n\nYou can add new records, update existing records.\n\nYou can view the registration information.";
                Contents[2] = "\n\nIt shows analysis such as Total Number of Appointments, Number of Monthly Appointments, Number of Appointments To Be Made.\n\nDistribution of price by months, years and Received-Remaining ratio.";
                Contents[3] = "\n\nRegistration information is entered in this field.\n\nUpdating and adding is done on this page.\n\nAfter choosing the hall, it automatically enters the number of people as much as the capacity of the hall.\n\nTo enter a different number of people, select the button next to the number of people and choose a different one yourself. You can enter the number of people.\n\nIf you select the contact button, a notification mail will be sent to the G-mail address you entered.\n\nNote: Internet connection is required to send mail. Otherwise, delivery will not be possible.";
                Contents[4] = "Theme color, Application language and history settings are made on this page.\n\nHistory setting; the history will clear itself according to the day you selected.\n\nTo reset the history, it will be enough to click the 'Now' button.\n\nNote: After saving, the application will restart itself .";
                Contents[5] = "If the notification setting is turned on, it will send the actions (Add, Update, Delete) to the G-mail address on this page.\n\nYou can obtain a password by clicking the link at the top left and following the necessary steps.\n\nYou must enter the sending option as 'Mail' when receiving a password. .\n\nNote: An internet connection is required for this operation. Otherwise, delivery will not be possible.";
                Contents[6] = "You can set what information the tables in the Recording List will display from this page.\n\nNote: After saving, the application will restart itself.";
                Contents[7] = "You can set the type of charts on the Analysis Page.\n\nYou can show or hide the charts on the Analysis page.\n\nNote: The application will restart itself after saving.";
                Contents[8] = "You can add or delete hall information\n\nYou can easily distinguish the registrations with the color option\n\nAfter selecting the Hall in the registration process according to the number of people, the application enters the number of people you entered here.\n(If you wish, you can easily enter the number of people yourself. See the relevant section.) \n\nTo delete the registered hall, simply double-click on the name or information of the hall you entered.\n\nNote: After saving, the application will restart itself.";
                Links = "https://support.google.com/accounts/answer/185833?hl=en";
                pictureEdit1.EditValue = global::WeddingApplication.Properties.Resources.AppointmentList;
                pictureEdit2.EditValue = global::WeddingApplication.Properties.Resources.Calendar;
                pictureEdit3.EditValue = global::WeddingApplication.Properties.Resources.Analysis;
                pictureEdit4.EditValue = global::WeddingApplication.Properties.Resources.CreateAndUpdateAppo;
                pictureEdit5.EditValue = global::WeddingApplication.Properties.Resources.SystemSetting;
                pictureEdit6.EditValue = global::WeddingApplication.Properties.Resources.CommunicationSetting;
                pictureEdit7.EditValue = global::WeddingApplication.Properties.Resources.TableSetting;
                pictureEdit8.EditValue = global::WeddingApplication.Properties.Resources.AnalysisSetting;
                pictureEdit9.EditValue = global::WeddingApplication.Properties.Resources.HallSetting;
            }
        }
        void ObjectValuePage()
        {
            DevExpress.XtraEditors.LabelControl Label=new DevExpress.XtraEditors.LabelControl();
            for (int i = 0; i < navigationFrame1.Pages.Count; i++)
            {
                Label = new DevExpress.XtraEditors.LabelControl()
                {
                    Name = i.ToString(),
                    Text = Contents[i],
                    AutoSizeInLayoutControl = false,
                    Font = new System.Drawing.Font("Tahoma", 11.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162))),
                    Dock = DockStyle.Fill,
                    
                    AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                };
                Label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                Label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                if (Label.Name == "0")
                    panel3.Controls.Add(Label);
                if (Label.Name == "1")
                    panel4.Controls.Add(Label);
                if (Label.Name == "2")
                    panel5.Controls.Add(Label);
                if (Label.Name == "3")
                    panel6.Controls.Add(Label);
                if (Label.Name == "4")
                    panel7.Controls.Add(Label);
                if (Label.Name == "5")
                { panel8.Controls.Add(Label);
                    DevExpress.XtraEditors.HyperLinkEdit Linls = new DevExpress.XtraEditors.HyperLinkEdit()
                    {
                        Text = Links,
                        
                    };
                    Label.Controls.Add(Linls);
                }
                if (Label.Name == "6")
                    panel9.Controls.Add(Label);
                if (Label.Name == "7")
                    panel10.Controls.Add(Label);
                if (Label.Name == "8")
                    panel11.Controls.Add(Label);
            }
           
        }
        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            navigationFrame1.SelectNextPage();
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            navigationFrame1.SelectPrevPage();
        }
    }
}
