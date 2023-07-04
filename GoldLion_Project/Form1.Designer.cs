namespace GoldLion_Project
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.RemoteFolderBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OpenSoPathBox = new System.Windows.Forms.TextBox();
            this.start_btn = new System.Windows.Forms.Button();
            this.clear_log_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.reset_btn = new System.Windows.Forms.Button();
            this.groupbox2 = new System.Windows.Forms.GroupBox();
            this.orderListView = new System.Windows.Forms.ListView();
            this.orderFolderName = new System.Windows.Forms.ColumnHeader();
            this.orderFileCol = new System.Windows.Forms.ColumnHeader();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.reportListView = new System.Windows.Forms.ListView();
            this.reportFolderCol = new System.Windows.Forms.ColumnHeader();
            this.reportFileCol = new System.Windows.Forms.ColumnHeader();
            this.slt_orderForm_btn = new System.Windows.Forms.Button();
            this.slt_report_btn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupbox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "订单根目录->";
            // 
            // RemoteFolderBox
            // 
            this.RemoteFolderBox.Location = new System.Drawing.Point(93, 13);
            this.RemoteFolderBox.Name = "RemoteFolderBox";
            this.RemoteFolderBox.ReadOnly = true;
            this.RemoteFolderBox.Size = new System.Drawing.Size(661, 22);
            this.RemoteFolderBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Open SO Report 目录->";
            // 
            // OpenSoPathBox
            // 
            this.OpenSoPathBox.Location = new System.Drawing.Point(157, 48);
            this.OpenSoPathBox.Name = "OpenSoPathBox";
            this.OpenSoPathBox.ReadOnly = true;
            this.OpenSoPathBox.Size = new System.Drawing.Size(597, 22);
            this.OpenSoPathBox.TabIndex = 10;
            // 
            // start_btn
            // 
            this.start_btn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start_btn.Location = new System.Drawing.Point(20, 433);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(98, 29);
            this.start_btn.TabIndex = 1;
            this.start_btn.Text = "开始运行";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // clear_log_btn
            // 
            this.clear_log_btn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clear_log_btn.Location = new System.Drawing.Point(133, 434);
            this.clear_log_btn.Name = "clear_log_btn";
            this.clear_log_btn.Size = new System.Drawing.Size(98, 29);
            this.clear_log_btn.TabIndex = 2;
            this.clear_log_btn.Text = "清空Log";
            this.clear_log_btn.UseVisualStyleBackColor = true;
            this.clear_log_btn.Click += new System.EventHandler(this.clear_log_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 477);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 254);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // logBox
            // 
            this.logBox.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logBox.Location = new System.Drawing.Point(15, 21);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(736, 220);
            this.logBox.TabIndex = 6;
            // 
            // reset_btn
            // 
            this.reset_btn.Location = new System.Drawing.Point(247, 435);
            this.reset_btn.Name = "reset_btn";
            this.reset_btn.Size = new System.Drawing.Size(90, 28);
            this.reset_btn.TabIndex = 3;
            this.reset_btn.Text = "重置";
            this.reset_btn.UseVisualStyleBackColor = true;
            this.reset_btn.Click += new System.EventHandler(this.reset_btn_Click);
            // 
            // groupbox2
            // 
            this.groupbox2.Controls.Add(this.orderListView);
            this.groupbox2.Location = new System.Drawing.Point(3, 121);
            this.groupbox2.Name = "groupbox2";
            this.groupbox2.Size = new System.Drawing.Size(366, 298);
            this.groupbox2.TabIndex = 11;
            this.groupbox2.TabStop = false;
            this.groupbox2.Text = "Order Form 文件 [可多选]";
            // 
            // orderListView
            // 
            this.orderListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.orderFolderName, this.orderFileCol });
            this.orderListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.orderListView.Location = new System.Drawing.Point(9, 21);
            this.orderListView.Name = "orderListView";
            this.orderListView.Size = new System.Drawing.Size(347, 263);
            this.orderListView.TabIndex = 7;
            this.orderListView.UseCompatibleStateImageBehavior = false;
            this.orderListView.View = System.Windows.Forms.View.Details;
            // 
            // orderFolderName
            // 
            this.orderFolderName.Text = "文件夹名";
            this.orderFolderName.Width = 99;
            // 
            // orderFileCol
            // 
            this.orderFileCol.Text = "文件名";
            this.orderFileCol.Width = 228;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.reportListView);
            this.groupBox3.Location = new System.Drawing.Point(385, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(384, 298);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Open SO Report [可多选]";
            // 
            // reportListView
            // 
            this.reportListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.reportFolderCol, this.reportFileCol });
            this.reportListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.reportListView.Location = new System.Drawing.Point(6, 21);
            this.reportListView.Name = "reportListView";
            this.reportListView.Size = new System.Drawing.Size(362, 263);
            this.reportListView.TabIndex = 8;
            this.reportListView.UseCompatibleStateImageBehavior = false;
            this.reportListView.View = System.Windows.Forms.View.Details;
            // 
            // reportFolderCol
            // 
            this.reportFolderCol.Text = "文件夹名";
            this.reportFolderCol.Width = 102;
            // 
            // reportFileCol
            // 
            this.reportFileCol.Text = "文件名";
            this.reportFileCol.Width = 250;
            // 
            // slt_orderForm_btn
            // 
            this.slt_orderForm_btn.Location = new System.Drawing.Point(12, 87);
            this.slt_orderForm_btn.Name = "slt_orderForm_btn";
            this.slt_orderForm_btn.Size = new System.Drawing.Size(118, 28);
            this.slt_orderForm_btn.TabIndex = 4;
            this.slt_orderForm_btn.Text = "选择Order Form";
            this.slt_orderForm_btn.UseVisualStyleBackColor = true;
            this.slt_orderForm_btn.Click += new System.EventHandler(this.slt_orderForm_btn_Click);
            // 
            // slt_report_btn
            // 
            this.slt_report_btn.Location = new System.Drawing.Point(385, 87);
            this.slt_report_btn.Name = "slt_report_btn";
            this.slt_report_btn.Size = new System.Drawing.Size(140, 28);
            this.slt_report_btn.TabIndex = 5;
            this.slt_report_btn.Text = "选择Open SO Report";
            this.slt_report_btn.UseVisualStyleBackColor = true;
            this.slt_report_btn.Click += new System.EventHandler(this.slt_report_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 745);
            this.Controls.Add(this.slt_report_btn);
            this.Controls.Add(this.slt_orderForm_btn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupbox2);
            this.Controls.Add(this.reset_btn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.clear_log_btn);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.OpenSoPathBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RemoteFolderBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "GoldLion 订单整理";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupbox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ColumnHeader reportFileCol;

        private System.Windows.Forms.ColumnHeader orderFileCol;

        private System.Windows.Forms.ColumnHeader reportFolderCol;

        private System.Windows.Forms.ColumnHeader orderFolderName;

        private System.Windows.Forms.ListView orderListView;
        private System.Windows.Forms.ListView reportListView;

        private System.Windows.Forms.Button slt_orderForm_btn;
        private System.Windows.Forms.Button slt_report_btn;

        private System.Windows.Forms.GroupBox groupbox2;
        private System.Windows.Forms.GroupBox groupBox3;

        private System.Windows.Forms.Button reset_btn;

        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Button clear_log_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox logBox;

        private System.Windows.Forms.TextBox OpenSoPathBox;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.TextBox RemoteFolderBox;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}