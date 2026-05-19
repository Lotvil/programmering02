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
            this.lsvCars = new System.Windows.Forms.ListView();
            this.regNr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.make = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.model = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.year = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.color = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txbRegNr = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lsvCars
            // 
            this.lsvCars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.regNr,
            this.type,
            this.make,
            this.model,
            this.year,
            this.color});
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
            // color
            // 
            this.color.Text = "color";
            this.color.Width = 108;
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
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(50, 90);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(109, 61);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txbRegNr
            // 
            this.txbRegNr.Location = new System.Drawing.Point(131, 46);
            this.txbRegNr.Name = "txbRegNr";
            this.txbRegNr.Size = new System.Drawing.Size(100, 22);
            this.txbRegNr.TabIndex = 15;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(47, 46);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(47, 16);
            this.lbl.TabIndex = 14;
            this.lbl.Text = "RegNr";
            // 
            // type
            // 
            this.type.Text = "type";
            this.type.Width = 110;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 487);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txbRegNr);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lsvCars);
            this.Name = "Form1";
            this.Text = "CarGUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lsvCars;
        private System.Windows.Forms.ColumnHeader regNr;
        private System.Windows.Forms.ColumnHeader make;
        private System.Windows.Forms.ColumnHeader model;
        private System.Windows.Forms.ColumnHeader year;
        private System.Windows.Forms.ColumnHeader color;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txbRegNr;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.ColumnHeader type;
    }
}

