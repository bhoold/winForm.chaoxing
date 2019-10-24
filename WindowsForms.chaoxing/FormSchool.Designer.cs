namespace WindowsForms.chaoxing
{
    partial class FormSchool
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
            this.gridViewRegion = new System.Windows.Forms.DataGridView();
            this.gridViewSchool = new System.Windows.Forms.DataGridView();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSchool)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewRegion
            // 
            this.gridViewRegion.AllowUserToAddRows = false;
            this.gridViewRegion.AllowUserToDeleteRows = false;
            this.gridViewRegion.AllowUserToResizeColumns = false;
            this.gridViewRegion.AllowUserToResizeRows = false;
            this.gridViewRegion.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridViewRegion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewRegion.Location = new System.Drawing.Point(12, 49);
            this.gridViewRegion.MultiSelect = false;
            this.gridViewRegion.Name = "gridViewRegion";
            this.gridViewRegion.ReadOnly = true;
            this.gridViewRegion.RowHeadersVisible = false;
            this.gridViewRegion.RowTemplate.Height = 23;
            this.gridViewRegion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewRegion.Size = new System.Drawing.Size(190, 436);
            this.gridViewRegion.TabIndex = 0;
            this.gridViewRegion.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewRegion_CellClick);
            // 
            // gridViewSchool
            // 
            this.gridViewSchool.AllowUserToAddRows = false;
            this.gridViewSchool.AllowUserToDeleteRows = false;
            this.gridViewSchool.AllowUserToResizeRows = false;
            this.gridViewSchool.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridViewSchool.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewSchool.Location = new System.Drawing.Point(228, 49);
            this.gridViewSchool.MultiSelect = false;
            this.gridViewSchool.Name = "gridViewSchool";
            this.gridViewSchool.ReadOnly = true;
            this.gridViewSchool.RowHeadersVisible = false;
            this.gridViewSchool.RowTemplate.Height = 23;
            this.gridViewSchool.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewSchool.Size = new System.Drawing.Size(571, 436);
            this.gridViewSchool.TabIndex = 0;
            this.gridViewSchool.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewSchool_CellClick);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(12, 12);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(190, 21);
            this.textBoxSearch.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(226, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(724, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FormSchool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 497);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.gridViewSchool);
            this.Controls.Add(this.gridViewRegion);
            this.Name = "FormSchool";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "学校";
            this.Load += new System.EventHandler(this.FormSchool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSchool)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridViewRegion;
        private System.Windows.Forms.DataGridView gridViewSchool;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnOk;
    }
}