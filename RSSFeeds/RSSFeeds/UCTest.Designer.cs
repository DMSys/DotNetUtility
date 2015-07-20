namespace RSSFeeds
{
    partial class UCTest
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
            this.btnDownLoadURL = new System.Windows.Forms.Button();
            this.tbRSSFeedsURL = new System.Windows.Forms.TextBox();
            this.btnTestOpen = new System.Windows.Forms.Button();
            this.btnParseHTML = new System.Windows.Forms.Button();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnParseHTML);
            this.splitContainer1.Panel1.Controls.Add(this.btnDownLoadURL);
            this.splitContainer1.Panel1.Controls.Add(this.tbRSSFeedsURL);
            this.splitContainer1.Panel1.Controls.Add(this.btnTestOpen);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Size = new System.Drawing.Size(626, 385);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 2;
            // 
            // btnDownLoadURL
            // 
            this.btnDownLoadURL.Location = new System.Drawing.Point(3, 58);
            this.btnDownLoadURL.Name = "btnDownLoadURL";
            this.btnDownLoadURL.Size = new System.Drawing.Size(134, 23);
            this.btnDownLoadURL.TabIndex = 9;
            this.btnDownLoadURL.Text = "DownLoadURL";
            this.btnDownLoadURL.UseVisualStyleBackColor = true;
            this.btnDownLoadURL.Click += new System.EventHandler(this.btnDownLoadURL_Click);
            // 
            // tbRSSFeedsURL
            // 
            this.tbRSSFeedsURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRSSFeedsURL.Location = new System.Drawing.Point(3, 3);
            this.tbRSSFeedsURL.Name = "tbRSSFeedsURL";
            this.tbRSSFeedsURL.Size = new System.Drawing.Size(620, 20);
            this.tbRSSFeedsURL.TabIndex = 8;
            this.tbRSSFeedsURL.Text = "http://www.investor.bg/rss.php?last";
            // 
            // btnTestOpen
            // 
            this.btnTestOpen.Location = new System.Drawing.Point(3, 29);
            this.btnTestOpen.Name = "btnTestOpen";
            this.btnTestOpen.Size = new System.Drawing.Size(134, 23);
            this.btnTestOpen.TabIndex = 7;
            this.btnTestOpen.Text = "Load RSS News";
            this.btnTestOpen.UseVisualStyleBackColor = true;
            this.btnTestOpen.Click += new System.EventHandler(this.btnTestOpen_Click);
            // 
            // btnParseHTML
            // 
            this.btnParseHTML.Location = new System.Drawing.Point(3, 87);
            this.btnParseHTML.Name = "btnParseHTML";
            this.btnParseHTML.Size = new System.Drawing.Size(134, 23);
            this.btnParseHTML.TabIndex = 10;
            this.btnParseHTML.Text = "Parse HTML";
            this.btnParseHTML.UseVisualStyleBackColor = true;
            this.btnParseHTML.Click += new System.EventHandler(this.btnParseHTML_Click);
            // 
            // UCTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCTest";
            this.Size = new System.Drawing.Size(626, 385);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnTestOpen;
        private System.Windows.Forms.TextBox tbRSSFeedsURL;
        private System.Windows.Forms.Button btnDownLoadURL;
        private System.Windows.Forms.Button btnParseHTML;
    }
}
