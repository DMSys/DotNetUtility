namespace RSSFeeds
{
    partial class UCTools
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnDeleteOldNews = new System.Windows.Forms.Button();
            this.btnDeleteInvalidEntries = new System.Windows.Forms.Button();
            this.btn_Optimization = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(626, 199);
            this.listBox1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_Optimization);
            this.splitContainer1.Panel1.Controls.Add(this.btnDeleteOldNews);
            this.splitContainer1.Panel1.Controls.Add(this.btnDeleteInvalidEntries);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Size = new System.Drawing.Size(626, 385);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 2;
            // 
            // btnDeleteOldNews
            // 
            this.btnDeleteOldNews.Location = new System.Drawing.Point(3, 3);
            this.btnDeleteOldNews.Name = "btnDeleteOldNews";
            this.btnDeleteOldNews.Size = new System.Drawing.Size(162, 23);
            this.btnDeleteOldNews.TabIndex = 10;
            this.btnDeleteOldNews.Text = "Изтрива старите новини";
            this.btnDeleteOldNews.UseVisualStyleBackColor = true;
            this.btnDeleteOldNews.Click += new System.EventHandler(this.btnDeleteOldNews_Click);
            // 
            // btnDeleteInvalidEntries
            // 
            this.btnDeleteInvalidEntries.Location = new System.Drawing.Point(3, 32);
            this.btnDeleteInvalidEntries.Name = "btnDeleteInvalidEntries";
            this.btnDeleteInvalidEntries.Size = new System.Drawing.Size(162, 23);
            this.btnDeleteInvalidEntries.TabIndex = 9;
            this.btnDeleteInvalidEntries.Text = "Изтрива невалидни записи";
            this.btnDeleteInvalidEntries.UseVisualStyleBackColor = true;
            this.btnDeleteInvalidEntries.Click += new System.EventHandler(this.btnDeleteInvalidEntries_Click);
            // 
            // btn_Optimization
            // 
            this.btn_Optimization.Location = new System.Drawing.Point(3, 61);
            this.btn_Optimization.Name = "btn_Optimization";
            this.btn_Optimization.Size = new System.Drawing.Size(162, 23);
            this.btn_Optimization.TabIndex = 11;
            this.btn_Optimization.Text = "Optimization";
            this.btn_Optimization.UseVisualStyleBackColor = true;
            this.btn_Optimization.Click += new System.EventHandler(this.btn_Optimization_Click);
            // 
            // UCTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCTools";
            this.Size = new System.Drawing.Size(626, 385);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnDeleteInvalidEntries;
        private System.Windows.Forms.Button btnDeleteOldNews;
        private System.Windows.Forms.Button btn_Optimization;
    }
}
