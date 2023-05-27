namespace WeddingApplication
{
    partial class List
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(List));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new System.Windows.Forms.Label();
            this.teNullText = new DevExpress.XtraEditors.TextEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.colUniqueID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAllDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLabel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResourceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResourceIDs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReminderInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecurrenceInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTimeZoneId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomField1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhoneNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGivenPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemainingPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFood = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHall = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teNullText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.teNullText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(904, 47);
            this.panel1.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl1.Appearance.Image")));
            this.labelControl1.Appearance.Options.UseImage = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelControl1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl1.Location = new System.Drawing.Point(867, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 47);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.MouseLeave += new System.EventHandler(this.SimpleButton1_MouseLeave);
            this.labelControl1.MouseHover += new System.EventHandler(this.SimpleButton1_MouseHover);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblName.Location = new System.Drawing.Point(10, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(75, 18);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Ad Soyad : ";
            // 
            // teNullText
            // 
            this.teNullText.Location = new System.Drawing.Point(86, 13);
            this.teNullText.Name = "teNullText";
            this.teNullText.Properties.Appearance.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.teNullText.Properties.Appearance.Options.UseFont = true;
            this.teNullText.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.teNullText.Properties.NullText = "Aranacak İsmi Giriniz...";
            this.teNullText.Size = new System.Drawing.Size(187, 22);
            this.teNullText.TabIndex = 0;
            this.teNullText.TextChanged += new System.EventHandler(this.TextEdit1_TextChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 47);
            this.gridControl1.MainView = this.cardView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(904, 326);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cardView1});
            // 
            // cardView1
            // 
            this.cardView1.CardCaptionFormat = "{6}";
            this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUniqueID,
            this.colType,
            this.colStartDate,
            this.colEndDate,
            this.colAllDay,
            this.colSubject,
            this.colLocation,
            this.colDescription,
            this.colStatus,
            this.colLabel,
            this.colResourceID,
            this.colResourceIDs,
            this.colReminderInfo,
            this.colRecurrenceInfo,
            this.colTimeZoneId,
            this.colCustomField1,
            this.colPhoneNumber,
            this.colMail,
            this.colPrice,
            this.colGivenPrice,
            this.colRemainingPrice,
            this.colFood,
            this.colHall});
            this.cardView1.FocusedCardTopFieldIndex = 0;
            this.cardView1.GridControl = this.gridControl1;
            this.cardView1.Name = "cardView1";
            this.cardView1.OptionsBehavior.Editable = false;
            this.cardView1.OptionsPrint.PrintEmptyFields = false;
            this.cardView1.OptionsView.ShowCardExpandButton = false;
            this.cardView1.OptionsView.ShowQuickCustomizeButton = false;
            this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.cardView1.DoubleClick += new System.EventHandler(this.CardView1_DoubleClick);
            // 
            // colUniqueID
            // 
            this.colUniqueID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colUniqueID.AppearanceCell.Options.UseFont = true;
            this.colUniqueID.AppearanceCell.Options.UseTextOptions = true;
            this.colUniqueID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUniqueID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUniqueID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colUniqueID.AppearanceHeader.Options.UseFont = true;
            this.colUniqueID.FieldName = "UniqueID";
            this.colUniqueID.Name = "colUniqueID";
            // 
            // colType
            // 
            this.colType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colType.AppearanceCell.Options.UseFont = true;
            this.colType.AppearanceCell.Options.UseTextOptions = true;
            this.colType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colType.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colType.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colType.AppearanceHeader.Options.UseFont = true;
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            // 
            // colStartDate
            // 
            this.colStartDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colStartDate.AppearanceCell.Options.UseFont = true;
            this.colStartDate.AppearanceCell.Options.UseTextOptions = true;
            this.colStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStartDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colStartDate.AppearanceHeader.Options.UseFont = true;
            this.colStartDate.Caption = "R. Tarihi";
            this.colStartDate.FieldName = "StartDate";
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.Visible = true;
            this.colStartDate.VisibleIndex = 0;
            // 
            // colEndDate
            // 
            this.colEndDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colEndDate.AppearanceCell.Options.UseFont = true;
            this.colEndDate.AppearanceCell.Options.UseTextOptions = true;
            this.colEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEndDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colEndDate.AppearanceHeader.Options.UseFont = true;
            this.colEndDate.FieldName = "EndDate";
            this.colEndDate.Name = "colEndDate";
            // 
            // colAllDay
            // 
            this.colAllDay.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colAllDay.AppearanceCell.Options.UseFont = true;
            this.colAllDay.AppearanceCell.Options.UseTextOptions = true;
            this.colAllDay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAllDay.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAllDay.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colAllDay.AppearanceHeader.Options.UseFont = true;
            this.colAllDay.FieldName = "AllDay";
            this.colAllDay.Name = "colAllDay";
            // 
            // colSubject
            // 
            this.colSubject.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colSubject.AppearanceCell.Options.UseFont = true;
            this.colSubject.AppearanceCell.Options.UseTextOptions = true;
            this.colSubject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSubject.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSubject.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colSubject.AppearanceHeader.Options.UseFont = true;
            this.colSubject.Caption = "Adı Soyadı";
            this.colSubject.FieldName = "Subject";
            this.colSubject.Name = "colSubject";
            this.colSubject.Visible = true;
            this.colSubject.VisibleIndex = 1;
            // 
            // colLocation
            // 
            this.colLocation.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colLocation.AppearanceCell.Options.UseFont = true;
            this.colLocation.AppearanceCell.Options.UseTextOptions = true;
            this.colLocation.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLocation.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLocation.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colLocation.AppearanceHeader.Options.UseFont = true;
            this.colLocation.FieldName = "Location";
            this.colLocation.Name = "colLocation";
            // 
            // colDescription
            // 
            this.colDescription.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colDescription.AppearanceCell.Options.UseFont = true;
            this.colDescription.AppearanceCell.Options.UseTextOptions = true;
            this.colDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescription.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDescription.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colDescription.AppearanceHeader.Options.UseFont = true;
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colStatus.AppearanceCell.Options.UseFont = true;
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            // 
            // colLabel
            // 
            this.colLabel.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colLabel.AppearanceCell.Options.UseFont = true;
            this.colLabel.AppearanceCell.Options.UseTextOptions = true;
            this.colLabel.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLabel.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLabel.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colLabel.AppearanceHeader.Options.UseFont = true;
            this.colLabel.FieldName = "Label";
            this.colLabel.Name = "colLabel";
            // 
            // colResourceID
            // 
            this.colResourceID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colResourceID.AppearanceCell.Options.UseFont = true;
            this.colResourceID.AppearanceCell.Options.UseTextOptions = true;
            this.colResourceID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colResourceID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colResourceID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colResourceID.AppearanceHeader.Options.UseFont = true;
            this.colResourceID.FieldName = "ResourceID";
            this.colResourceID.Name = "colResourceID";
            // 
            // colResourceIDs
            // 
            this.colResourceIDs.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colResourceIDs.AppearanceCell.Options.UseFont = true;
            this.colResourceIDs.AppearanceCell.Options.UseTextOptions = true;
            this.colResourceIDs.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colResourceIDs.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colResourceIDs.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colResourceIDs.AppearanceHeader.Options.UseFont = true;
            this.colResourceIDs.FieldName = "ResourceIDs";
            this.colResourceIDs.Name = "colResourceIDs";
            // 
            // colReminderInfo
            // 
            this.colReminderInfo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colReminderInfo.AppearanceCell.Options.UseFont = true;
            this.colReminderInfo.AppearanceCell.Options.UseTextOptions = true;
            this.colReminderInfo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReminderInfo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colReminderInfo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colReminderInfo.AppearanceHeader.Options.UseFont = true;
            this.colReminderInfo.FieldName = "ReminderInfo";
            this.colReminderInfo.Name = "colReminderInfo";
            // 
            // colRecurrenceInfo
            // 
            this.colRecurrenceInfo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colRecurrenceInfo.AppearanceCell.Options.UseFont = true;
            this.colRecurrenceInfo.AppearanceCell.Options.UseTextOptions = true;
            this.colRecurrenceInfo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRecurrenceInfo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRecurrenceInfo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colRecurrenceInfo.AppearanceHeader.Options.UseFont = true;
            this.colRecurrenceInfo.FieldName = "RecurrenceInfo";
            this.colRecurrenceInfo.Name = "colRecurrenceInfo";
            // 
            // colTimeZoneId
            // 
            this.colTimeZoneId.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colTimeZoneId.AppearanceCell.Options.UseFont = true;
            this.colTimeZoneId.AppearanceCell.Options.UseTextOptions = true;
            this.colTimeZoneId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTimeZoneId.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTimeZoneId.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colTimeZoneId.AppearanceHeader.Options.UseFont = true;
            this.colTimeZoneId.FieldName = "TimeZoneId";
            this.colTimeZoneId.Name = "colTimeZoneId";
            // 
            // colCustomField1
            // 
            this.colCustomField1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colCustomField1.AppearanceCell.Options.UseFont = true;
            this.colCustomField1.AppearanceCell.Options.UseTextOptions = true;
            this.colCustomField1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCustomField1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCustomField1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colCustomField1.AppearanceHeader.Options.UseFont = true;
            this.colCustomField1.FieldName = "CustomField1";
            this.colCustomField1.Name = "colCustomField1";
            // 
            // colPhoneNumber
            // 
            this.colPhoneNumber.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colPhoneNumber.AppearanceCell.Options.UseFont = true;
            this.colPhoneNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colPhoneNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPhoneNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPhoneNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colPhoneNumber.AppearanceHeader.Options.UseFont = true;
            this.colPhoneNumber.Caption = "Telefon Numarası";
            this.colPhoneNumber.FieldName = "PhoneNumber";
            this.colPhoneNumber.Name = "colPhoneNumber";
            this.colPhoneNumber.Visible = true;
            this.colPhoneNumber.VisibleIndex = 2;
            // 
            // colMail
            // 
            this.colMail.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colMail.AppearanceCell.Options.UseFont = true;
            this.colMail.AppearanceCell.Options.UseTextOptions = true;
            this.colMail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMail.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colMail.AppearanceHeader.Options.UseFont = true;
            this.colMail.Caption = "Mail";
            this.colMail.FieldName = "Mail";
            this.colMail.Name = "colMail";
            this.colMail.Visible = true;
            this.colMail.VisibleIndex = 3;
            // 
            // colPrice
            // 
            this.colPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colPrice.AppearanceCell.Options.UseFont = true;
            this.colPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colPrice.AppearanceHeader.Options.UseFont = true;
            this.colPrice.Caption = "Anlaşma Tutarı";
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 4;
            // 
            // colGivenPrice
            // 
            this.colGivenPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colGivenPrice.AppearanceCell.Options.UseFont = true;
            this.colGivenPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colGivenPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGivenPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colGivenPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colGivenPrice.AppearanceHeader.Options.UseFont = true;
            this.colGivenPrice.Caption = "Alınan Tutar";
            this.colGivenPrice.FieldName = "GivenPrice";
            this.colGivenPrice.Name = "colGivenPrice";
            this.colGivenPrice.Visible = true;
            this.colGivenPrice.VisibleIndex = 5;
            // 
            // colRemainingPrice
            // 
            this.colRemainingPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colRemainingPrice.AppearanceCell.Options.UseFont = true;
            this.colRemainingPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colRemainingPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRemainingPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRemainingPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colRemainingPrice.AppearanceHeader.Options.UseFont = true;
            this.colRemainingPrice.Caption = "Kalan Tutar";
            this.colRemainingPrice.FieldName = "RemainingPrice";
            this.colRemainingPrice.Name = "colRemainingPrice";
            this.colRemainingPrice.Visible = true;
            this.colRemainingPrice.VisibleIndex = 6;
            // 
            // colFood
            // 
            this.colFood.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colFood.AppearanceCell.Options.UseFont = true;
            this.colFood.AppearanceCell.Options.UseTextOptions = true;
            this.colFood.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFood.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFood.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colFood.AppearanceHeader.Options.UseFont = true;
            this.colFood.FieldName = "Food";
            this.colFood.Name = "colFood";
            // 
            // colHall
            // 
            this.colHall.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colHall.AppearanceCell.Options.UseFont = true;
            this.colHall.AppearanceCell.Options.UseTextOptions = true;
            this.colHall.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHall.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHall.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.colHall.AppearanceHeader.Options.UseFont = true;
            this.colHall.FieldName = "Hall";
            this.colHall.Name = "colHall";
            // 
            // List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 373);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "List";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.List_FormClosing);
            this.Load += new System.EventHandler(this.List_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teNullText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraEditors.TextEdit teNullText;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Card.CardView cardView1;
        private DevExpress.XtraGrid.Columns.GridColumn colUniqueID;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAllDay;
        private DevExpress.XtraGrid.Columns.GridColumn colSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colLabel;
        private DevExpress.XtraGrid.Columns.GridColumn colResourceID;
        private DevExpress.XtraGrid.Columns.GridColumn colResourceIDs;
        private DevExpress.XtraGrid.Columns.GridColumn colReminderInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colRecurrenceInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colTimeZoneId;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomField1;
        private DevExpress.XtraGrid.Columns.GridColumn colPhoneNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colMail;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colGivenPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colRemainingPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colFood;
        private DevExpress.XtraGrid.Columns.GridColumn colHall;
        public DevExpress.XtraEditors.LabelControl labelControl1;
    }
}