using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
                    item.SubItems.Add(res["type"].ToString());
                    item.SubItems.Add(res["make"].ToString());
                    item.SubItems.Add(res["model"].ToString());
                    item.SubItems.Add(res["year"].ToString());
                    item.SubItems.Add(res["color"].ToString());
                    lsvCars.Items.Add(item);
                }
            }
            dbObject.CloseConnection();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Om regNr är inmatat
            if (txbRegNr.Text.Length > 0)
            {
                // Hämta regNr
                string regNr = txbRegNr.Text.ToUpper();

                // Nollställ textboxen
                txbRegNr.Text = "";

                // Anropa metoden addData
                addData(regNr);
            }
            else
            {
                MessageBox.Show("Du måste ange ett registreringsnummer.", "Felaktig inmatning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void addData(string regNr)
        {
            // Variabler
            string token = "jtBt2fWF8vS0v1B5EcZT9111j9YExcUfiLHq5-4xSWY";
            string call = $"https://data.biluppgifter.se/api/v1/lookup/vehicle/regno/{regNr}";

            try
            {
                // Skapar ett objekt med API-anropet till HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(call);
                request.Method = "GET";
                request.Headers["Authorization"] = $"Bearer {token}";

                // Skapar ett svarsobjekt
                WebResponse response = request.GetResponse();

                // Läser av strömmen från svaret
                StreamReader reader = new StreamReader(response.GetResponseStream());

                // Läser in innehållet till en JSON-sträng
                String car_JSON = reader.ReadToEnd();


                // Skapar ett JObject.
                JObject jo = JObject.Parse(car_JSON);

                // Skriver ut hela objektet
                //txtJSON.Text = jo.ToString();

                // Hämtar data från JSON-objektet och formaterar det till en sträng, om den är tom så sätts den till en tom sträng
                string type = jo.SelectToken("vehicle.type")?.ToString() ?? "";
                string make = jo.SelectToken("vehicle.make")?.ToString() ?? "";
                string model = jo.SelectToken("vehicle.model")?.ToString() ?? "";
                string year = jo.SelectToken("vehicle.vehicle_year")?.ToString() ?? "";
                string color = jo.SelectToken("vehicle.color")?.ToString() ?? "";

                // Lägg till ett listItem i list View
                ListViewItem item = new ListViewItem(regNr);
                item.SubItems.Add(type);
                item.SubItems.Add(make);
                item.SubItems.Add(model);
                item.SubItems.Add(year);
                item.SubItems.Add(color);

                // Koppla item till listView
                lsvCars.Items.Add(item);

                // Lägg till bilen i db
                string q_insert = "INSERT INTO car ('regNr', 'type','make', 'model', 'year', 'color') " +
                                  "VALUES (@regNr, @type, @make, @model, @year, @color);";
                SQLiteCommand dbCommand = new SQLiteCommand(q_insert, dbObject.dbConn);
                dbObject.OpenConnection();

                // Koppla parametrar
                dbCommand.Parameters.AddWithValue("@regNr", regNr);
                dbCommand.Parameters.AddWithValue("@type", type);
                dbCommand.Parameters.AddWithValue("@make", make);
                dbCommand.Parameters.AddWithValue("@model", model);
                dbCommand.Parameters.AddWithValue("@year", Convert.ToInt16(year));
                dbCommand.Parameters.AddWithValue("@color", color);

                int result = dbCommand.ExecuteNonQuery();
                dbObject.CloseConnection();

                // Meddela att det fungerar
                MessageBox.Show("Bilen med regnr: " + regNr + " har lagts till.", "Tillagd");

                checkDeleteButton();

            }
            catch (Exception e)
            {
                //Skriv ut felmeddelande
                MessageBox.Show($"Din sökning på registreringsnummer {regNr} gav ingen träff.\nMeddelande: {e.Message}", "Ingen träff i databasen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
