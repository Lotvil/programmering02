using System;

namespace VehicleApplikation1a
{

    class MainClass
    {
        //Deklarera klassvariebler
        public static List<Vehicle> vehicleList = new List<Vehicle>();
    }
    public abstract class Vehicle
    {
        //
        // Medlemsvariabler
        //
        private String regNr;    //registreringsnummer
        private String make;     //bilmärke
        private String model;    //bilmodell
        private int year;        //årsmodell
        private bool forSale;    //är bilen till salu?

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="regNr"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="forSale"></param>

        public Vehicle(string regNr, string make, string model, int year, bool forSale)
        {
            this.RegNr = regNr;
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.ForSale = forSale;
        }

        //
        //Properties
        //

        public String RegNr
        {
            get { return regNr; }
            set { regNr = value; }
        }

        public String Make
        {
            get { return make; }
            set { make = value; }
        }
        public String Model
        {
            get { return model; }
            set { model = value; }
        }
        public int Year
        {
            get { return year; }
            set
            {
                if (value < 1800)
                {
                    year = -1;
                }
                else
                {
                    year = value;
                }
            }
        }
        public bool ForSale
        {
            get { return forSale; }
            set { forSale = value; }
        }

        /// <summary>
        /// Omvandlar årtalet till en snygg utskrift, skriver ut "felaktigt årtal" om årtalet är omöjligt.
        /// </summary>
        /// <returns>En string med ett år eller "felaktigt årtal"</returns>

        public String YearToString()
        {
            if (this.year == -1)
            {
                return "felaktigt årtal";
            }
            else
            {
                return Convert.ToString(this.year);
            }
        }

        /// <summary>
        /// Omvandlar forSale variablen till en snygg utskrift
        /// </summary>
        /// <returns>En string i form av en mening ifall forSale är true eller false</returns>
        public String ForSaleToString()
        {
            if (this.forSale)
            {
                return "\nBilen är till salu";
            }
            else
            {
                return "\nBilen är inte till salu";
            }
        }

        /// <summary>
        /// Returnerar ett läsbart format av alla variabler till en viss bil
        /// </summary>
        /// <returns>En string med information om en bil</returns>
        public override string ToString()
        {
            return String.Format("\nBilinformation\nReg: {0}, {1} {2} [{3}]\n{4}",
                                     this.RegNr, this.Make, this.Model, this.YearToString(), this.ForSaleToString());
        }

        /// <summary>
        /// Formaterar en snygg lista för all fordonsinformation om ett fordon, kan läggas ovanpå sig självt för en lista.
        /// </summary>
        /// <returns>En string i form av en rad i en lista</returns>
        public virtual String ToStringList()
        {
            return String.Format($"{this.RegNr}\t\t{this.Make}\t\t{this.Model}\t\t{this.YearToString()}\t\t{this.ForSaleToString2()}\t\t");
        }

        /// <summary>
        /// Omvandlar forSale till "JA" eller "NEJ" om true eller false
        /// </summary>
        /// <returns>En string med "JA" eller "NEJ"</returns>
        public String ForSaleToString2()
        {
            if (this.forSale)
            {
                return "JA";
            }
            else
            {
                return "NEJ";
            }
        }
    }
}
