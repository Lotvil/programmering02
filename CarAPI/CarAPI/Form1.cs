using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CarAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            // Skapar labels som skall läggas i tabellen
            Label l1 = new Label();
            l1.Text = "RegNr";
            Label l2 = new Label();
            l2.Text = "Typ";
            Label l3 = new Label();
            l3.Text = "Märke";
            Label l4 = new Label();
            l4.Text = "Modell";
            Label l5 = new Label();
            l5.Text = "Årsmodell";
            Label l6 = new Label();
            l6.Text = "Färg";

            // Fetmarkera
            l1.Font = new Font(l1.Font, FontStyle.Bold);
            l2.Font = new Font(l2.Font, FontStyle.Bold);
            l3.Font = new Font(l3.Font, FontStyle.Bold);
            l4.Font = new Font(l4.Font, FontStyle.Bold);
            l5.Font = new Font(l5.Font, FontStyle.Bold);
            l6.Font = new Font(l6.Font, FontStyle.Bold);

            // Lägg in labels i tabellen
            int row = 0;
            tlpCar.Controls.Add(l1, 0, row++);
            tlpCar.Controls.Add(l2, 0, row++);
            tlpCar.Controls.Add(l3, 0, row++);
            tlpCar.Controls.Add(l4, 0, row++);
            tlpCar.Controls.Add(l5, 0, row++);
            tlpCar.Controls.Add(l6, 0, row++);


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Om regNr är inmatat
            if (txbRegNr.Text.Length > 0)
            {
                // Hämta regNr
                string regNr = txbRegNr.Text.ToUpper();

                // Nollställ textboxen
                txbRegNr.Text = "";

                // Anropa metoden printData
                printData(regNr);
            }
            else
            {
                MessageBox.Show("Du måste ange ett registreringsnummer.", "Felaktig inmatning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void printData(string regNr)
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
                string vehicleYear = jo.SelectToken("vehicle.vehicle_year")?.ToString() ?? "";
                string color = jo.SelectToken("vehicle.color")?.ToString() ?? "";

                // Justerad
                String car = String.Format($"{"Registreringnr":20} {regNr:10}");
                car += String.Format($"{"\nTyp":20} {type}");
                car += String.Format($"{"\nMärke":20} {make}");
                car += String.Format($"{"\nModell":20} {model}");
                car += String.Format($"{"\nÅrsmodell":20} {vehicleYear}");
                car += String.Format($"{"\nFärg":20} {color}");
                
                txtCarInfo.Text = car;
                
                // Koppla kolumn 2
                Label l1 = new Label();
                Label l2 = new Label();
                Label l3 = new Label();
                Label l4 = new Label();
                Label l5 = new Label();
                Label l6 = new Label();

                l1.Text = regNr;
                l2.Text = type;
                l3.Text = make;
                l4.Text = model;
                l5.Text = vehicleYear;
                l6.Text = color;

                ClearColumn(1);

                int row = 0;
                tlpCar.Controls.Add(l1, 1, row++);
                tlpCar.Controls.Add(l2, 1, row++);
                tlpCar.Controls.Add(l3, 1, row++);
                tlpCar.Controls.Add(l4, 1, row++);
                tlpCar.Controls.Add(l5, 1, row++);
                tlpCar.Controls.Add(l6, 1, row++);
            }
            catch (Exception e)
            {
                //Töm textrutan
                txtCarInfo.Text = "";

                //Skriv ut felmeddelande
                MessageBox.Show($"Din sökning på registreringsnummer {regNr} gav ingen träff.\nMeddelande: {e.Message}", "Ingen träff i databasen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearColumn(int column)
        {
            for (int i = tlpCar.Controls.Count - 1; i >= 0; i--)
            {
                Control control = tlpCar.Controls[i];

                if (tlpCar.GetColumn(control) == column)
                {
                    tlpCar.Controls.Remove(control);
                    control.Dispose();
                }
            }
        }
    }
}
