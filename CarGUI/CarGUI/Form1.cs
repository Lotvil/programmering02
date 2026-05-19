using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            checkDeleteButton();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kollar om någon textruta är tom
            if(string.IsNullOrEmpty(txtRegNr.Text) || string.IsNullOrEmpty(txtMake.Text) || 
                string.IsNullOrEmpty(txtModel.Text) || string.IsNullOrEmpty(txtYear.Text))
            {
                // Visar ett meddelande om att alla fält måste fyllas i (msgBox)
                MessageBox.Show("Vänligen fyll i alla fält.", "Felaktig inmatning");
                return;
            }

            // Lägg till ett listItem i list View
            ListViewItem item = new ListViewItem(txtRegNr.Text);
            item.SubItems.Add(txtMake.Text);
            item.SubItems.Add(txtModel.Text);
            item.SubItems.Add(txtYear.Text);
            item.SubItems.Add(cbxForSale.Checked ? "Yes" : "No");

            // Koppla item till listView
            lsvCars.Items.Add(item);

            // Rensa alla formulär

            txtRegNr.Clear();
            txtMake.Clear();
            txtModel.Clear();
            txtYear.Clear();
            cbxForSale.Checked = false;

            // Sätt fokus på första textrutan
            txtRegNr.Focus();

            checkDeleteButton();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var index = lsvCars.SelectedItems[0];
            lsvCars.Items.Remove(index);
            MessageBox.Show("Bilen med regnr: " + index.Text +" har tagits bort.", "Borttagen");
            checkDeleteButton();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // loopa igenom alla items i listView och ta bort dem

            int count = lsvCars.Items.Count;

            foreach (ListViewItem item in lsvCars.Items)
            {
                lsvCars.Items.Remove(item);
            }
            MessageBox.Show("Alla bilar(" + count +"st) har tagits bort.", "Rensat");
            checkDeleteButton();
        }

        public void checkDeleteButton()
        {
            if (lsvCars.Items.Count > 0)
            {
                btnRemove.Enabled = true;
                btnClear.Enabled = true;
            }
            else
            {
                btnRemove.Enabled = false;
                btnClear.Enabled = false;
            }
        }
    }
}
