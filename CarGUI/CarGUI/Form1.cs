using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarGUI
{
    public partial class Form1 : Form
    {
        Database dbObject = new Database();

        public Form1()
        {
            InitializeComponent();
            fillListViewFromDatabase();
            checkDeleteButton();
        }

        
        private void fillListViewFromDatabase()
        {
            string q_select = "SELECT * FROM car;";
            SQLiteCommand dbCommand = new SQLiteCommand(q_select, dbObject.dbConn);
            dbObject.OpenConnection();

            SQLiteDataReader res = dbCommand.ExecuteReader();

            if(res.HasRows)
            {
                while (res.Read())
                {
                    ListViewItem item = new ListViewItem(res["regNr"].ToString());
                    item.SubItems.Add(res["make"].ToString());
                    item.SubItems.Add(res["model"].ToString());
                    item.SubItems.Add(res["year"].ToString());
                    item.SubItems.Add(Convert.ToInt16(res["forSale"]) == 1 ? "Yes" : "No");
                    lsvCars.Items.Add(item);
                }
            }
            dbObject.CloseConnection();
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

            // Lägg till bilen i db
            string q_insert = "INSERT INTO car ('regNr', 'make', 'model', 'year', 'forSale') " +
                              "VALUES (@regNr, @make, @model, @year, @forSale);";
            SQLiteCommand dbCommand = new SQLiteCommand(q_insert, dbObject.dbConn);
            dbObject.OpenConnection();

            // Koppla parametrar
            dbCommand.Parameters.AddWithValue("@regNr", txtRegNr.Text);
            dbCommand.Parameters.AddWithValue("@make", txtMake.Text);
            dbCommand.Parameters.AddWithValue("@model", txtModel.Text);
            dbCommand.Parameters.AddWithValue("@year", Convert.ToInt16(txtYear.Text));
            dbCommand.Parameters.AddWithValue("@forSale", cbxForSale.Checked ? 1 : 0);

            int result = dbCommand.ExecuteNonQuery();
            dbObject.CloseConnection();

            // Meddela att det fungerar
            MessageBox.Show("Bilen med regnr: " + txtRegNr.Text + " har lagts till.", "Tillagd");

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

            // Ta bort bilen från db
            string q_delete = "DELETE FROM car WHERE regNr = @regNr;";
            SQLiteCommand dbCommand = new SQLiteCommand(q_delete, dbObject.dbConn);
            dbObject.OpenConnection();
            dbCommand.Parameters.AddWithValue("@regNr", index.Text);
            
            int result = dbCommand.ExecuteNonQuery();
            dbObject.CloseConnection();

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

            // Ta bort bilen från db
            string q_delete = "DELETE FROM car;";
            SQLiteCommand dbCommand = new SQLiteCommand(q_delete, dbObject.dbConn);
            dbObject.OpenConnection();

            int result = dbCommand.ExecuteNonQuery();
            dbObject.CloseConnection();

            MessageBox.Show("Alla bilar(" + result +"st) har tagits bort.", "Rensat");
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
