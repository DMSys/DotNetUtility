namespace RSSFeeds
{
    partial class frmRss
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRss));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbRSSFeeds = new System.Windows.Forms.TabPage();
            this.tbCategories = new System.Windows.Forms.TabPage();
            this.tbTools = new System.Windows.Forms.TabPage();
            this.tbTest = new System.Windows.Forms.TabPage();
            this.tpUpdateData = new System.Windows.Forms.TabPage();
            this.ucNews = new RSSFeeds.UCRSSFeeds();
            this.ucCat = new RSSFeeds.UCCategories();
            this.ucTools1 = new RSSFeeds.UCTools();
            this.ucTest1 = new RSSFeeds.UCTest();
            this.ucUpdateD = new RSSFeeds.UCUpdateData();
            this.tabControl1.SuspendLayout();
            this.tbRSSFeeds.SuspendLayout();
            this.tbCategories.SuspendLayout();
            this.tbTools.SuspendLayout();
            this.tbTest.SuspendLayout();
            this.tpUpdateData.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbRSSFeeds);
            this.tabControl1.Controls.Add(this.tbCategories);
            this.tabControl1.Controls.Add(this.tpUpdateData);
            this.tabControl1.Controls.Add(this.tbTools);
            this.tabControl1.Controls.Add(this.tbTest);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(754, 410);
            this.tabControl1.TabIndex = 8;
            // 
            // tbRSSFeeds
            // 
            this.tbRSSFeeds.Controls.Add(this.ucNews);
            this.tbRSSFeeds.Location = new System.Drawing.Point(4, 22);
            this.tbRSSFeeds.Name = "tbRSSFeeds";
            this.tbRSSFeeds.Padding = new System.Windows.Forms.Padding(3);
            this.tbRSSFeeds.Size = new System.Drawing.Size(746, 384);
            this.tbRSSFeeds.TabIndex = 0;
            this.tbRSSFeeds.Text = "Новини";
            this.tbRSSFeeds.UseVisualStyleBackColor = true;
            // 
            // tbCategories
            // 
            this.tbCategories.Controls.Add(this.ucCat);
            this.tbCategories.Location = new System.Drawing.Point(4, 22);
            this.tbCategories.Name = "tbCategories";
            this.tbCategories.Size = new System.Drawing.Size(746, 384);
            this.tbCategories.TabIndex = 2;
            this.tbCategories.Text = "Категории";
            this.tbCategories.UseVisualStyleBackColor = true;
            // 
            // tbTools
            // 
            this.tbTools.Controls.Add(this.ucTools1);
            this.tbTools.Location = new System.Drawing.Point(4, 22);
            this.tbTools.Name = "tbTools";
            this.tbTools.Size = new System.Drawing.Size(746, 384);
            this.tbTools.TabIndex = 3;
            this.tbTools.Text = "Tools";
            this.tbTools.UseVisualStyleBackColor = true;
            // 
            // tbTest
            // 
            this.tbTest.Controls.Add(this.ucTest1);
            this.tbTest.Location = new System.Drawing.Point(4, 22);
            this.tbTest.Name = "tbTest";
            this.tbTest.Padding = new System.Windows.Forms.Padding(3);
            this.tbTest.Size = new System.Drawing.Size(746, 384);
            this.tbTest.TabIndex = 1;
            this.tbTest.Text = "Test";
            this.tbTest.UseVisualStyleBackColor = true;
            // 
            // tpUpdateData
            // 
            this.tpUpdateData.Controls.Add(this.ucUpdateD);
            this.tpUpdateData.Location = new System.Drawing.Point(4, 22);
            this.tpUpdateData.Name = "tpUpdateData";
            this.tpUpdateData.Size = new System.Drawing.Size(746, 384);
            this.tpUpdateData.TabIndex = 4;
            this.tpUpdateData.Text = "Update";
            this.tpUpdateData.UseVisualStyleBackColor = true;
            // 
            // ucNews
            // 
            this.ucNews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucNews.Location = new System.Drawing.Point(3, 3);
            this.ucNews.Name = "ucNews";
            this.ucNews.Size = new System.Drawing.Size(740, 378);
            this.ucNews.TabIndex = 0;
            // 
            // ucCat
            // 
            this.ucCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCat.Location = new System.Drawing.Point(0, 0);
            this.ucCat.Name = "ucCat";
            this.ucCat.Size = new System.Drawing.Size(746, 384);
            this.ucCat.TabIndex = 0;
            // 
            // ucTools1
            // 
            this.ucTools1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTools1.Location = new System.Drawing.Point(0, 0);
            this.ucTools1.Name = "ucTools1";
            this.ucTools1.Size = new System.Drawing.Size(746, 384);
            this.ucTools1.TabIndex = 0;
            // 
            // ucTest1
            // 
            this.ucTest1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTest1.Location = new System.Drawing.Point(3, 3);
            this.ucTest1.Name = "ucTest1";
            this.ucTest1.Size = new System.Drawing.Size(740, 378);
            this.ucTest1.TabIndex = 0;
            // 
            // ucUpdateD
            // 
            this.ucUpdateD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUpdateD.Location = new System.Drawing.Point(0, 0);
            this.ucUpdateD.Name = "ucUpdateD";
            this.ucUpdateD.Size = new System.Drawing.Size(746, 384);
            this.ucUpdateD.TabIndex = 0;
            // 
            // frmRss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 410);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRss";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSS News";
            this.tabControl1.ResumeLayout(false);
            this.tbRSSFeeds.ResumeLayout(false);
            this.tbCategories.ResumeLayout(false);
            this.tbTools.ResumeLayout(false);
            this.tbTest.ResumeLayout(false);
            this.tpUpdateData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbRSSFeeds;
        private System.Windows.Forms.TabPage tbTest;
        private UCRSSFeeds ucNews;
        private UCTest ucTest1;
        private System.Windows.Forms.TabPage tbCategories;
        private UCCategories ucCat;
        private System.Windows.Forms.TabPage tbTools;
        private UCTools ucTools1;
        private System.Windows.Forms.TabPage tpUpdateData;
        private UCUpdateData ucUpdateD;
    }
}

