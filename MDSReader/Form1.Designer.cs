namespace MDSReader
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbRateUnit = new System.Windows.Forms.Label();
            this.tbxTestRate = new System.Windows.Forms.TextBox();
            this.lbTestRate = new System.Windows.Forms.Label();
            this.lbFpath = new System.Windows.Forms.Label();
            this.fpathSubmitBtn = new System.Windows.Forms.Button();
            this.tbxFilePah = new System.Windows.Forms.TextBox();
            this.TestPanel = new System.Windows.Forms.Panel();
            this.outTxtBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOpenFile);
            this.panel1.Controls.Add(this.lbRateUnit);
            this.panel1.Controls.Add(this.tbxTestRate);
            this.panel1.Controls.Add(this.lbTestRate);
            this.panel1.Controls.Add(this.lbFpath);
            this.panel1.Controls.Add(this.fpathSubmitBtn);
            this.panel1.Controls.Add(this.tbxFilePah);
            this.panel1.Location = new System.Drawing.Point(12, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(618, 82);
            this.panel1.TabIndex = 0;
            // 
            // lbRateUnit
            // 
            this.lbRateUnit.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRateUnit.Location = new System.Drawing.Point(191, 39);
            this.lbRateUnit.Name = "lbRateUnit";
            this.lbRateUnit.Size = new System.Drawing.Size(22, 25);
            this.lbRateUnit.TabIndex = 5;
            this.lbRateUnit.Text = "ms";
            this.lbRateUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxTestRate
            // 
            this.tbxTestRate.Location = new System.Drawing.Point(85, 39);
            this.tbxTestRate.Multiline = true;
            this.tbxTestRate.Name = "tbxTestRate";
            this.tbxTestRate.Size = new System.Drawing.Size(100, 25);
            this.tbxTestRate.TabIndex = 4;
            this.tbxTestRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxTestRate_KeyPress);
            // 
            // lbTestRate
            // 
            this.lbTestRate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTestRate.Location = new System.Drawing.Point(3, 39);
            this.lbTestRate.Name = "lbTestRate";
            this.lbTestRate.Size = new System.Drawing.Size(76, 25);
            this.lbTestRate.TabIndex = 3;
            this.lbTestRate.Text = "Test Rate:";
            this.lbTestRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFpath
            // 
            this.lbFpath.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFpath.Location = new System.Drawing.Point(3, 4);
            this.lbFpath.Name = "lbFpath";
            this.lbFpath.Size = new System.Drawing.Size(76, 25);
            this.lbFpath.TabIndex = 2;
            this.lbFpath.Text = "File Path:";
            this.lbFpath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fpathSubmitBtn
            // 
            this.fpathSubmitBtn.Location = new System.Drawing.Point(530, 41);
            this.fpathSubmitBtn.Name = "fpathSubmitBtn";
            this.fpathSubmitBtn.Size = new System.Drawing.Size(75, 23);
            this.fpathSubmitBtn.TabIndex = 1;
            this.fpathSubmitBtn.Text = "Submit";
            this.fpathSubmitBtn.UseVisualStyleBackColor = true;
            this.fpathSubmitBtn.Click += new System.EventHandler(this.fpathSubmitBtn_Click);
            // 
            // tbxFilePah
            // 
            this.tbxFilePah.Location = new System.Drawing.Point(85, 3);
            this.tbxFilePah.Multiline = true;
            this.tbxFilePah.Name = "tbxFilePah";
            this.tbxFilePah.Size = new System.Drawing.Size(430, 25);
            this.tbxFilePah.TabIndex = 0;
            // 
            // TestPanel
            // 
            this.TestPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TestPanel.Location = new System.Drawing.Point(12, 123);
            this.TestPanel.Name = "TestPanel";
            this.TestPanel.Size = new System.Drawing.Size(314, 599);
            this.TestPanel.TabIndex = 1;
            // 
            // outTxtBox
            // 
            this.outTxtBox.Location = new System.Drawing.Point(341, 123);
            this.outTxtBox.Multiline = true;
            this.outTxtBox.Name = "outTxtBox";
            this.outTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outTxtBox.Size = new System.Drawing.Size(289, 599);
            this.outTxtBox.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(530, 4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 6;
            this.btnOpenFile.Text = "Open";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 734);
            this.Controls.Add(this.outTxtBox);
            this.Controls.Add(this.TestPanel);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel TestPanel;
        private System.Windows.Forms.Label lbFpath;
        private System.Windows.Forms.Button fpathSubmitBtn;
        private System.Windows.Forms.TextBox tbxFilePah;
        private System.Windows.Forms.Label lbTestRate;
        private System.Windows.Forms.Label lbRateUnit;
        private System.Windows.Forms.TextBox tbxTestRate;
        private System.Windows.Forms.TextBox outTxtBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOpenFile;

    }
}

