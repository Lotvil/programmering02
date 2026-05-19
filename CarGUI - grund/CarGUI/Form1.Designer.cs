namespace CarGUI
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
            this.lblRegNr = new System.Windows.Forms.Label();
            this.lblMake = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblForSale = new System.Windows.Forms.Label();
            this.txtRegNr = new System.Windows.Forms.TextBox();
            this.txtMake = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.cbxForSale = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lsvCars = new System.Windows.Forms.ListView();
            this.regNr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.make = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.model = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.year = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.forSale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblRegNr
            // 
            this.lblRegNr.AutoSize = true;
            this.lblRegNr.Location = new System.Drawing.Point(56, 36);
            this.lblRegNr.Name = "lblRegNr";
            this.lblRegNr.Size = new System.Drawing.Size(41, 16);
            this.lblRegNr.TabIndex = 0;
            this.lblRegNr.Text = "regNr";
            // 
            // lblMake
            // 
            this.lblMake.AutoSize = true;
            this.lblMake.Location = new System.Drawing.Point(56, 64);
            this.lblMake.Name = "lblMake";
            this.lblMake.Size = new System.Drawing.Size(41, 16);
            this.lblMake.TabIndex = 1;
            this.lblMake.Text = "make";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(56, 92);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(45, 16);
            this.lblModel.TabIndex = 2;
            this.lblModel.Text = "model";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(56, 120);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(34, 16);
            this.lblYear.TabIndex = 3;
            this.lblYear.Text = "year";
            // 
            // lblForSale
            // 
            this.lblForSale.AutoSize = true;
            this.lblForSale.Location = new System.Drawing.Point(56, 146);
            this.lblForSale.Name = "lblForSale";
            this.lblForSale.Size = new System.Drawing.Size(50, 16);
            this.lblForSale.TabIndex = 4;
            this.lblForSale.Text = "forSale";
            // 
            // txtRegNr
            // 
            this.txtRegNr.Location = new System.Drawing.Point(125, 29);
            this.txtRegNr.Name = "txtRegNr";
            this.txtRegNr.Size = new System.Drawing.Size(121, 22);
            this.txtRegNr.TabIndex = 5;
            // 
            // txtMake
            // 
            this.txtMake.Location = new System.Drawing.Point(125, 57);
            this.txtMake.Name = "txtMake";
            this.txtMake.Size = new System.Drawing.Size(121, 22);
            this.txtMake.TabIndex = 6;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(125, 85);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(121, 22);
            this.txtModel.TabIndex = 7;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(125, 113);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(121, 22);
            this.txtYear.TabIndex = 8;
            // 
            // cbxForSale
            // 
            this.cbxForSale.AutoSize = true;
            this.cbxForSale.Location = new System.Drawing.Point(125, 141);
            this.cbxForSale.Name = "cbxForSale";
            this.cbxForSale.Size = new System.Drawing.Size(18, 17);
            this.cbxForSale.TabIndex = 9;
            this.cbxForSale.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(276, 29);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 50);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lsvCars
            // 
            this.lsvCars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.regNr,
            this.make,
            this.model,
            this.year,
            this.forSale});
            this.lsvCars.HideSelection = false;
            this.lsvCars.Location = new System.Drawing.Point(12, 196);
            this.lsvCars.Name = "lsvCars";
            this.lsvCars.Size = new System.Drawing.Size(725, 260);
            this.lsvCars.TabIndex = 11;
            this.lsvCars.UseCompatibleStateImageBehavior = false;
            this.lsvCars.View = System.Windows.Forms.View.Details;
            // 
            // regNr
            // 
            this.regNr.Text = "regNr";
            this.regNr.Width = 111;
            // 
            // make
            // 
            this.make.Text = "make";
            this.make.Width = 118;
            // 
            // model
            // 
            this.model.Text = "model";
            this.model.Width = 112;
            // 
            // year
            // 
            this.year.Text = "year";
            this.year.Width = 104;
            // 
            // forSale
            // 
            this.forSale.Text = "forSale";
            this.forSale.Width = 147;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(662, 146);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 44);
            this.btnRemove.TabIndex = 12;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(661, 29);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 50);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 487);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lsvCars);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbxForSale);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtMake);
            this.Controls.Add(this.txtRegNr);
            this.Controls.Add(this.lblForSale);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.lblMake);
            this.Controls.Add(this.lblRegNr);
            this.Name = "Form1";
            this.Text = "CarGUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRegNr;
        private System.Windows.Forms.Label lblMake;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblForSale;
        private System.Windows.Forms.TextBox txtRegNr;
        private System.Windows.Forms.TextBox txtMake;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.CheckBox cbxForSale;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListView lsvCars;
        private System.Windows.Forms.ColumnHeader regNr;
        private System.Windows.Forms.ColumnHeader make;
        private System.Windows.Forms.ColumnHeader model;
        private System.Windows.Forms.ColumnHeader year;
        private System.Windows.Forms.ColumnHeader forSale;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClear;
    }
}

