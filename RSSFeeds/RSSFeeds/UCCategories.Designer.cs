namespace RSSFeeds
{
    partial class UCCategories
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvCatOUT = new System.Windows.Forms.DataGridView();
            this.gcCategoryID_OUT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcCategory_OUT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcSource_OUT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsCatOUT = new System.Windows.Forms.BindingSource(this.components);
            this.dSet = new System.Data.DataSet();
            this.dtTNCategory = new System.Data.DataTable();
            this.tcNCategoryID = new System.Data.DataColumn();
            this.tcNCategory = new System.Data.DataColumn();
            this.dtTNCategoriesSub = new System.Data.DataTable();
            this.tcNCategorySubID = new System.Data.DataColumn();
            this.tcNCategorySub = new System.Data.DataColumn();
            this.dtCatIN = new System.Data.DataTable();
            this.tcCategoryID_IN = new System.Data.DataColumn();
            this.tcCategory_IN = new System.Data.DataColumn();
            this.tcSource_IN = new System.Data.DataColumn();
            this.dtCatOUT = new System.Data.DataTable();
            this.tcCategoryID_OUT = new System.Data.DataColumn();
            this.tcCategory_OUT = new System.Data.DataColumn();
            this.tcSource_OUT = new System.Data.DataColumn();
            this.dgvCatIN = new System.Windows.Forms.DataGridView();
            this.gcCategoryID_IN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcCategory_IN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gcSource_IN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsCatIN = new System.Windows.Forms.BindingSource(this.components);
            this.btnOUT = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbTNCategoriesSub = new System.Windows.Forms.ComboBox();
            this.bsTNCategoriesSub = new System.Windows.Forms.BindingSource(this.components);
            this.btnIN = new System.Windows.Forms.Button();
            this.cbTNCategories = new System.Windows.Forms.ComboBox();
            this.bsTNCategories = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatOUT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCatOUT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTNCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTNCategoriesSub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCatIN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCatOUT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatIN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCatIN)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsTNCategoriesSub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTNCategories)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 88);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvCatOUT);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvCatIN);
            this.splitContainer1.Size = new System.Drawing.Size(602, 293);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgvCatOUT
            // 
            this.dgvCatOUT.AllowUserToAddRows = false;
            this.dgvCatOUT.AllowUserToDeleteRows = false;
            this.dgvCatOUT.AutoGenerateColumns = false;
            this.dgvCatOUT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCatOUT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gcCategoryID_OUT,
            this.gcCategory_OUT,
            this.gcSource_OUT});
            this.dgvCatOUT.DataSource = this.bsCatOUT;
            this.dgvCatOUT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCatOUT.Location = new System.Drawing.Point(0, 0);
            this.dgvCatOUT.MultiSelect = false;
            this.dgvCatOUT.Name = "dgvCatOUT";
            this.dgvCatOUT.ReadOnly = true;
            this.dgvCatOUT.RowHeadersWidth = 18;
            this.dgvCatOUT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCatOUT.Size = new System.Drawing.Size(274, 293);
            this.dgvCatOUT.TabIndex = 0;
            this.dgvCatOUT.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCatOUT_CellMouseDoubleClick);
            // 
            // gcCategoryID_OUT
            // 
            this.gcCategoryID_OUT.DataPropertyName = "news_category_id";
            this.gcCategoryID_OUT.HeaderText = "id";
            this.gcCategoryID_OUT.Name = "gcCategoryID_OUT";
            this.gcCategoryID_OUT.ReadOnly = true;
            this.gcCategoryID_OUT.Width = 50;
            // 
            // gcCategory_OUT
            // 
            this.gcCategory_OUT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.gcCategory_OUT.DataPropertyName = "news_category";
            this.gcCategory_OUT.HeaderText = "Category";
            this.gcCategory_OUT.Name = "gcCategory_OUT";
            this.gcCategory_OUT.ReadOnly = true;
            // 
            // gcSource_OUT
            // 
            this.gcSource_OUT.DataPropertyName = "n_source";
            this.gcSource_OUT.HeaderText = "Source";
            this.gcSource_OUT.Name = "gcSource_OUT";
            this.gcSource_OUT.ReadOnly = true;
            // 
            // bsCatOUT
            // 
            this.bsCatOUT.DataMember = "CatOUT";
            this.bsCatOUT.DataSource = this.dSet;
            // 
            // dSet
            // 
            this.dSet.DataSetName = "NewDataSet";
            this.dSet.Tables.AddRange(new System.Data.DataTable[] {
            this.dtTNCategory,
            this.dtTNCategoriesSub,
            this.dtCatIN,
            this.dtCatOUT});
            // 
            // dtTNCategory
            // 
            this.dtTNCategory.Columns.AddRange(new System.Data.DataColumn[] {
            this.tcNCategoryID,
            this.tcNCategory});
            this.dtTNCategory.TableName = "TNCategory";
            // 
            // tcNCategoryID
            // 
            this.tcNCategoryID.Caption = "n_category_id";
            this.tcNCategoryID.ColumnName = "n_category_id";
            this.tcNCategoryID.DataType = typeof(long);
            // 
            // tcNCategory
            // 
            this.tcNCategory.Caption = "n_category";
            this.tcNCategory.ColumnName = "n_category";
            // 
            // dtTNCategoriesSub
            // 
            this.dtTNCategoriesSub.Columns.AddRange(new System.Data.DataColumn[] {
            this.tcNCategorySubID,
            this.tcNCategorySub});
            this.dtTNCategoriesSub.TableName = "TNCategoriesSub";
            // 
            // tcNCategorySubID
            // 
            this.tcNCategorySubID.Caption = "n_category_sub_id";
            this.tcNCategorySubID.ColumnName = "n_category_sub_id";
            this.tcNCategorySubID.DataType = typeof(long);
            // 
            // tcNCategorySub
            // 
            this.tcNCategorySub.Caption = "n_category_sub";
            this.tcNCategorySub.ColumnName = "n_category_sub";
            // 
            // dtCatIN
            // 
            this.dtCatIN.Columns.AddRange(new System.Data.DataColumn[] {
            this.tcCategoryID_IN,
            this.tcCategory_IN,
            this.tcSource_IN});
            this.dtCatIN.TableName = "CatIN";
            // 
            // tcCategoryID_IN
            // 
            this.tcCategoryID_IN.Caption = "news_category_id";
            this.tcCategoryID_IN.ColumnName = "news_category_id";
            this.tcCategoryID_IN.DataType = typeof(long);
            // 
            // tcCategory_IN
            // 
            this.tcCategory_IN.Caption = "news_category";
            this.tcCategory_IN.ColumnName = "news_category";
            // 
            // tcSource_IN
            // 
            this.tcSource_IN.Caption = "source";
            this.tcSource_IN.ColumnName = "n_source";
            // 
            // dtCatOUT
            // 
            this.dtCatOUT.Columns.AddRange(new System.Data.DataColumn[] {
            this.tcCategoryID_OUT,
            this.tcCategory_OUT,
            this.tcSource_OUT});
            this.dtCatOUT.TableName = "CatOUT";
            // 
            // tcCategoryID_OUT
            // 
            this.tcCategoryID_OUT.Caption = "news_category_id";
            this.tcCategoryID_OUT.ColumnName = "news_category_id";
            this.tcCategoryID_OUT.DataType = typeof(long);
            // 
            // tcCategory_OUT
            // 
            this.tcCategory_OUT.Caption = "news_category";
            this.tcCategory_OUT.ColumnName = "news_category";
            // 
            // tcSource_OUT
            // 
            this.tcSource_OUT.Caption = "source";
            this.tcSource_OUT.ColumnName = "n_source";
            // 
            // dgvCatIN
            // 
            this.dgvCatIN.AllowUserToAddRows = false;
            this.dgvCatIN.AllowUserToDeleteRows = false;
            this.dgvCatIN.AutoGenerateColumns = false;
            this.dgvCatIN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCatIN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gcCategoryID_IN,
            this.gcCategory_IN,
            this.gcSource_IN});
            this.dgvCatIN.DataSource = this.bsCatIN;
            this.dgvCatIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCatIN.Location = new System.Drawing.Point(0, 0);
            this.dgvCatIN.MultiSelect = false;
            this.dgvCatIN.Name = "dgvCatIN";
            this.dgvCatIN.ReadOnly = true;
            this.dgvCatIN.RowHeadersWidth = 18;
            this.dgvCatIN.RowTemplate.Height = 20;
            this.dgvCatIN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCatIN.Size = new System.Drawing.Size(324, 293);
            this.dgvCatIN.TabIndex = 0;
            this.dgvCatIN.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCatIN_CellMouseDoubleClick);
            // 
            // gcCategoryID_IN
            // 
            this.gcCategoryID_IN.DataPropertyName = "news_category_id";
            this.gcCategoryID_IN.HeaderText = "id";
            this.gcCategoryID_IN.Name = "gcCategoryID_IN";
            this.gcCategoryID_IN.ReadOnly = true;
            this.gcCategoryID_IN.Width = 50;
            // 
            // gcCategory_IN
            // 
            this.gcCategory_IN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.gcCategory_IN.DataPropertyName = "news_category";
            this.gcCategory_IN.HeaderText = "Category";
            this.gcCategory_IN.Name = "gcCategory_IN";
            this.gcCategory_IN.ReadOnly = true;
            // 
            // gcSource_IN
            // 
            this.gcSource_IN.DataPropertyName = "n_source";
            this.gcSource_IN.HeaderText = "Source";
            this.gcSource_IN.Name = "gcSource_IN";
            this.gcSource_IN.ReadOnly = true;
            // 
            // bsCatIN
            // 
            this.bsCatIN.DataMember = "CatIN";
            this.bsCatIN.DataSource = this.dSet;
            // 
            // btnOUT
            // 
            this.btnOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOUT.Location = new System.Drawing.Point(563, 57);
            this.btnOUT.Name = "btnOUT";
            this.btnOUT.Size = new System.Drawing.Size(36, 23);
            this.btnOUT.TabIndex = 3;
            this.btnOUT.Text = "<<";
            this.btnOUT.UseVisualStyleBackColor = true;
            this.btnOUT.Click += new System.EventHandler(this.btnOUT_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cbTNCategoriesSub);
            this.panel4.Controls.Add(this.btnIN);
            this.panel4.Controls.Add(this.cbTNCategories);
            this.panel4.Controls.Add(this.btnOUT);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(602, 88);
            this.panel4.TabIndex = 0;
            // 
            // cbTNCategoriesSub
            // 
            this.cbTNCategoriesSub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTNCategoriesSub.DataSource = this.bsTNCategoriesSub;
            this.cbTNCategoriesSub.DisplayMember = "n_category_sub";
            this.cbTNCategoriesSub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTNCategoriesSub.FormattingEnabled = true;
            this.cbTNCategoriesSub.Location = new System.Drawing.Point(3, 30);
            this.cbTNCategoriesSub.Name = "cbTNCategoriesSub";
            this.cbTNCategoriesSub.Size = new System.Drawing.Size(596, 21);
            this.cbTNCategoriesSub.TabIndex = 1;
            this.cbTNCategoriesSub.ValueMember = "n_category_sub_id";
            this.cbTNCategoriesSub.SelectedIndexChanged += new System.EventHandler(this.cbTNCategoriesSub_SelectedIndexChanged);
            // 
            // bsTNCategoriesSub
            // 
            this.bsTNCategoriesSub.DataMember = "TNCategoriesSub";
            this.bsTNCategoriesSub.DataSource = this.dSet;
            // 
            // btnIN
            // 
            this.btnIN.Location = new System.Drawing.Point(3, 57);
            this.btnIN.Name = "btnIN";
            this.btnIN.Size = new System.Drawing.Size(36, 23);
            this.btnIN.TabIndex = 2;
            this.btnIN.Text = ">>";
            this.btnIN.UseVisualStyleBackColor = true;
            this.btnIN.Click += new System.EventHandler(this.btnIN_Click);
            // 
            // cbTNCategories
            // 
            this.cbTNCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTNCategories.DataSource = this.bsTNCategories;
            this.cbTNCategories.DisplayMember = "n_category";
            this.cbTNCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTNCategories.FormattingEnabled = true;
            this.cbTNCategories.Location = new System.Drawing.Point(3, 3);
            this.cbTNCategories.Name = "cbTNCategories";
            this.cbTNCategories.Size = new System.Drawing.Size(596, 21);
            this.cbTNCategories.TabIndex = 0;
            this.cbTNCategories.ValueMember = "n_category_id";
            this.cbTNCategories.SelectedIndexChanged += new System.EventHandler(this.cbTNCategories_SelectedIndexChanged);
            // 
            // bsTNCategories
            // 
            this.bsTNCategories.DataMember = "TNCategory";
            this.bsTNCategories.DataSource = this.dSet;
            // 
            // UCCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel4);
            this.Name = "UCCategories";
            this.Size = new System.Drawing.Size(602, 381);
            this.Load += new System.EventHandler(this.UCCategories_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatOUT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCatOUT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTNCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTNCategoriesSub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCatIN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCatOUT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatIN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCatIN)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsTNCategoriesSub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTNCategories)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvCatOUT;
        private System.Windows.Forms.DataGridView dgvCatIN;
        private System.Windows.Forms.Button btnOUT;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnIN;
        private System.Windows.Forms.ComboBox cbTNCategories;
        private System.Data.DataSet dSet;
        private System.Windows.Forms.BindingSource bsTNCategories;
        private System.Data.DataTable dtTNCategory;
        private System.Data.DataColumn tcNCategoryID;
        private System.Data.DataColumn tcNCategory;
        private System.Windows.Forms.ComboBox cbTNCategoriesSub;
        private System.Data.DataTable dtTNCategoriesSub;
        private System.Windows.Forms.BindingSource bsTNCategoriesSub;
        private System.Data.DataColumn tcNCategorySubID;
        private System.Data.DataColumn tcNCategorySub;
        private System.Data.DataTable dtCatIN;
        private System.Data.DataColumn tcCategoryID_IN;
        private System.Data.DataColumn tcCategory_IN;
        private System.Data.DataTable dtCatOUT;
        private System.Data.DataColumn tcCategoryID_OUT;
        private System.Data.DataColumn tcCategory_OUT;
        private System.Windows.Forms.BindingSource bsCatIN;
        private System.Windows.Forms.BindingSource bsCatOUT;
        private System.Data.DataColumn tcSource_IN;
        private System.Windows.Forms.DataGridViewTextBoxColumn gcCategoryID_OUT;
        private System.Windows.Forms.DataGridViewTextBoxColumn gcCategory_OUT;
        private System.Windows.Forms.DataGridViewTextBoxColumn gcSource_OUT;
        private System.Data.DataColumn tcSource_OUT;
        private System.Windows.Forms.DataGridViewTextBoxColumn gcCategoryID_IN;
        private System.Windows.Forms.DataGridViewTextBoxColumn gcCategory_IN;
        private System.Windows.Forms.DataGridViewTextBoxColumn gcSource_IN;
    }
}
