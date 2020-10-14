namespace TaiwanLotterySpider
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_GetHttp = new System.Windows.Forms.Button();
            this.Text_Year = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Text_Purpose = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Com_Select = new System.Windows.Forms.ComboBox();
            this.Btn_Export = new System.Windows.Forms.Button();
            this.Chk_IncludeDate = new System.Windows.Forms.CheckBox();
            this.List_ShowLog = new System.Windows.Forms.ListBox();
            this.Chk_AutoExport = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Btn_GetHttp
            // 
            this.Btn_GetHttp.Location = new System.Drawing.Point(12, 22);
            this.Btn_GetHttp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_GetHttp.Name = "Btn_GetHttp";
            this.Btn_GetHttp.Size = new System.Drawing.Size(75, 22);
            this.Btn_GetHttp.TabIndex = 0;
            this.Btn_GetHttp.Text = "Start!";
            this.Btn_GetHttp.UseVisualStyleBackColor = true;
            this.Btn_GetHttp.Click += new System.EventHandler(this.Btn_GetHttp_Click);
            // 
            // Text_Year
            // 
            this.Text_Year.Location = new System.Drawing.Point(12, 150);
            this.Text_Year.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Text_Year.Name = "Text_Year";
            this.Text_Year.Size = new System.Drawing.Size(100, 25);
            this.Text_Year.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "起始年份";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "目標年份";
            // 
            // Text_Purpose
            // 
            this.Text_Purpose.Location = new System.Drawing.Point(11, 201);
            this.Text_Purpose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Text_Purpose.Name = "Text_Purpose";
            this.Text_Purpose.Size = new System.Drawing.Size(100, 25);
            this.Text_Purpose.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "處理進度";
            // 
            // Com_Select
            // 
            this.Com_Select.FormattingEnabled = true;
            this.Com_Select.Items.AddRange(new object[] {
            "大樂透",
            "威力彩",
            "今彩539"});
            this.Com_Select.Location = new System.Drawing.Point(12, 73);
            this.Com_Select.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Com_Select.Name = "Com_Select";
            this.Com_Select.Size = new System.Drawing.Size(103, 23);
            this.Com_Select.TabIndex = 11;
            this.Com_Select.Text = "大樂透";
            // 
            // Btn_Export
            // 
            this.Btn_Export.Enabled = false;
            this.Btn_Export.Location = new System.Drawing.Point(476, 35);
            this.Btn_Export.Name = "Btn_Export";
            this.Btn_Export.Size = new System.Drawing.Size(75, 23);
            this.Btn_Export.TabIndex = 12;
            this.Btn_Export.Text = "匯出";
            this.Btn_Export.UseVisualStyleBackColor = true;
            // 
            // Chk_IncludeDate
            // 
            this.Chk_IncludeDate.AutoSize = true;
            this.Chk_IncludeDate.Location = new System.Drawing.Point(476, 65);
            this.Chk_IncludeDate.Name = "Chk_IncludeDate";
            this.Chk_IncludeDate.Size = new System.Drawing.Size(89, 19);
            this.Chk_IncludeDate.TabIndex = 13;
            this.Chk_IncludeDate.Text = "包含日期";
            this.Chk_IncludeDate.UseVisualStyleBackColor = true;
            // 
            // List_ShowLog
            // 
            this.List_ShowLog.FormattingEnabled = true;
            this.List_ShowLog.ItemHeight = 15;
            this.List_ShowLog.Location = new System.Drawing.Point(137, 38);
            this.List_ShowLog.Name = "List_ShowLog";
            this.List_ShowLog.Size = new System.Drawing.Size(333, 199);
            this.List_ShowLog.TabIndex = 14;
            // 
            // Chk_AutoExport
            // 
            this.Chk_AutoExport.AutoSize = true;
            this.Chk_AutoExport.Location = new System.Drawing.Point(12, 49);
            this.Chk_AutoExport.Name = "Chk_AutoExport";
            this.Chk_AutoExport.Size = new System.Drawing.Size(89, 19);
            this.Chk_AutoExport.TabIndex = 15;
            this.Chk_AutoExport.Text = "自動匯出";
            this.Chk_AutoExport.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 245);
            this.Controls.Add(this.Chk_AutoExport);
            this.Controls.Add(this.List_ShowLog);
            this.Controls.Add(this.Chk_IncludeDate);
            this.Controls.Add(this.Btn_Export);
            this.Controls.Add(this.Com_Select);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Text_Purpose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Text_Year);
            this.Controls.Add(this.Btn_GetHttp);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "TaiwanLotterySpider";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_GetHttp;
        private System.Windows.Forms.TextBox Text_Year;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Text_Purpose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Com_Select;
        private System.Windows.Forms.Button Btn_Export;
        private System.Windows.Forms.CheckBox Chk_IncludeDate;
        private System.Windows.Forms.ListBox List_ShowLog;
        private System.Windows.Forms.CheckBox Chk_AutoExport;
    }
}

