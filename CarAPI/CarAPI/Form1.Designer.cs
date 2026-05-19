namespace CarAPI
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
            this.lbl = new System.Windows.Forms.Label();
            this.txbRegNr = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtCarInfo = new System.Windows.Forms.RichTextBox();
            this.tlpCar = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(54, 36);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(47, 16);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "RegNr";
            // 
            // txbRegNr
            // 
            this.txbRegNr.Location = new System.Drawing.Point(138, 36);
            this.txbRegNr.Name = "txbRegNr";
            this.txbRegNr.Size = new System.Drawing.Size(100, 22);
            this.txbRegNr.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(57, 75);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 50);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Sök";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtCarInfo
            // 
            this.txtCarInfo.Location = new System.Drawing.Point(459, 12);
            this.txtCarInfo.Name = "txtCarInfo";
            this.txtCarInfo.Size = new System.Drawing.Size(329, 426);
            this.txtCarInfo.TabIndex = 3;
            this.txtCarInfo.Text = "";
            // 
            // tlpCar
            // 
            this.tlpCar.ColumnCount = 2;
            this.tlpCar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCar.Location = new System.Drawing.Point(57, 166);
            this.tlpCar.Name = "tlpCar";
            this.tlpCar.RowCount = 6;
            this.tlpCar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpCar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpCar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpCar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpCar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpCar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpCar.Size = new System.Drawing.Size(247, 250);
            this.tlpCar.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlpCar);
            this.Controls.Add(this.txtCarInfo);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txbRegNr);
            this.Controls.Add(this.lbl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.TextBox txbRegNr;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RichTextBox txtCarInfo;
        private System.Windows.Forms.TableLayoutPanel tlpCar;
    }
}

