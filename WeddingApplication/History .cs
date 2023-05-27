using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeddingApplication
{
    public partial class History : DevExpress.XtraEditors.XtraForm
    {
        Form1 f1;
        SqlCommand cmd;

        public History(Form1 frm1)
        {
            InitializeComponent();
            f1 = frm1;
        }
        private void Translate()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) * from dbo.DisplaySettings where Name='Language'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0]["Description"].ToString() == "Turkish")
            {
                radioAll.Text = "Tümü";
                radioAdd.Text = "Eklenen Kayıtlar";
                radioDelete.Text = "Silinen Kayıtlar";
                radioUpdate.Text = "Güncellenen Kayıtlar";
                radioPrice.Text = "Ücreti Alınmadan Silinen Kayıtlar";
               
                cmd = new SqlCommand("Update dbo.History set Process='Kayıt Güncellendi' where Process='Record Update'", f1.con);
                try { f1.con.Open();cmd.ExecuteNonQuery(); }catch(Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { f1.con.Close(); }
                cmd = new SqlCommand("Update dbo.History set Process='Kayıt Eklendi' where Process='Record Added'", f1.con);
                try { f1.con.Open(); cmd.ExecuteNonQuery(); }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { f1.con.Close(); }
                cmd = new SqlCommand("Update dbo.History set Process='Kayıt Silindi' where Process='Record Delete'", f1.con);
                try { f1.con.Open(); cmd.ExecuteNonQuery(); }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { f1.con.Close(); }
            }
            else if (dt.Rows[0]["Description"].ToString() == "English")
            {
                radioAll.Text = "All";
                radioAdd.Text = "Added Records";
                radioDelete.Text = "Deleted Records";
                radioUpdate.Text = "Updated Records";
                radioPrice.Text = "Records deleted Before Payment is Complete";

                cmd = new SqlCommand("Update dbo.History set Process='Record Update' where Process='Kayıt Güncellendi'", f1.con);
                try { f1.con.Open(); cmd.ExecuteNonQuery(); }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { f1.con.Close(); }
                cmd = new SqlCommand("Update dbo.History set Process='Record Added' where Process='Kayıt Eklendi'", f1.con);
                try { f1.con.Open(); cmd.ExecuteNonQuery(); }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { f1.con.Close(); }
                cmd = new SqlCommand("Update dbo.History set Process='Record Delete' where Process='Kayıt Silindi'", f1.con);
                try { f1.con.Open(); cmd.ExecuteNonQuery(); }
                catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
                finally { f1.con.Close(); }
            }
        }
        private void History_Load(object sender, EventArgs e)
        {
            Translate();
            DeleteHistory();
            List();
            
        }
        private void DeleteHistory()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select top(1) Description from dbo.DisplaySettings where Name='History'", f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int da=-30;
            if (!string.IsNullOrEmpty(dt.Rows[0]["Description"].ToString()))
                da = Int32.Parse(dt.Rows[0]["Description"].ToString());
            string das = Convert.ToDateTime(System.DateTime.Now.AddDays(da)).ToString("yyyy-MM-dd");
            SqlCommand cmd = new SqlCommand("Delete from dbo.History where DateTime <'" +das + "'", f1.con);
            try
            {
                f1.con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message); }
            finally
            {
                f1.con.Close();
            }
        }
        private void List()
        {
            flowLayoutPanel1.Controls.Clear();
            string AllSql="Select * from dbo.History order by DateTime desc, RemainingPrice asc, Process desc" ;
            if (radioAll.Checked == true)
            {
                AllSql = "Select * from dbo.History order by DateTime desc , RemainingPrice asc, Process desc";
            }
            if (radioAdd.Checked == true)
            {
                AllSql = "Select * from dbo.History where Process='" + "Kayıt Eklendi" + "' or Process='"+"Record Added"+"' order by DateTime desc , RemainingPrice asc";
            }
            if (radioDelete.Checked == true)
            {
                AllSql = "Select * from dbo.History where Process='" + "Kayıt Silindi" + "'or Process='Record Delete' order by DateTime desc , RemainingPrice asc";
            }
            if (radioUpdate.Checked == true)
            {
                AllSql = "Select * from dbo.History where Process='" + "Kayıt Güncellendi" + "'or Process='"+"Record Update"+"' order by DateTime desc , RemainingPrice asc";
            }
            if (radioPrice.Checked == true)
            {
                AllSql = "Select * from dbo.History where (Process='" + "Kayıt Silindi" + "' or Process='Record Delete') and RemainingPrice>0 order by DateTime desc , RemainingPrice asc";
            }
            SqlDataAdapter sda = new SqlDataAdapter(AllSql, f1.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                List<string> Values = new List<string> { dt.Rows[i]["DateTime"].ToString(), dt.Rows[i]["Process"].ToString(), dt.Rows[i]["Name"].ToString(), dt.Rows[i]["PhoneNumber"].ToString(), dt.Rows[i]["RemainingPrice"].ToString() };
                Color cl = Color.FromArgb(0, 120, 255);
                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
                {
                    
                    BackColor =cl,
                    Location = new System.Drawing.Point(13, 13),
                    Name = "flowLayoutPanel2",
                    Size = new System.Drawing.Size(954, 35),
                    TabIndex = 5
                };
                for (int b = 0; b < 5; b++)
                {
                    DevExpress.XtraEditors.LabelControl labelControl = new DevExpress.XtraEditors.LabelControl();
                    labelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    labelControl.Appearance.Options.UseFont = true;
                    labelControl.Appearance.Options.UseTextOptions = true;
                    labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    labelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    labelControl.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    labelControl.AppearanceDisabled.Options.UseFont = true;
                    labelControl.AppearanceDisabled.Options.UseTextOptions = true;
                    labelControl.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    labelControl.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    labelControl.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    labelControl.AppearanceHovered.Options.UseFont = true;
                    labelControl.AppearanceHovered.Options.UseTextOptions = true;
                    labelControl.AppearanceHovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    labelControl.AppearanceHovered.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    labelControl.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    labelControl.AppearancePressed.Options.UseFont = true;
                    labelControl.AppearancePressed.Options.UseTextOptions = true;
                    labelControl.AppearancePressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    labelControl.AppearancePressed.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    labelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                    labelControl.Location = new System.Drawing.Point(3, 3);
                    labelControl.Name = "labelControl1";
                    labelControl.Size = new System.Drawing.Size(184, 27);
                    labelControl.TabIndex = 0;

                    labelControl.Text =Values[b];
                    if (b == 4)
                    {
                        labelControl.Text = string.Format("{0:C2}", Convert.ToDecimal(Values[b]));
                    }
                    flowLayoutPanel.Controls.Add(labelControl);
                }
                flowLayoutPanel1.Controls.Add(flowLayoutPanel);
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            List();
        }
    }
}
