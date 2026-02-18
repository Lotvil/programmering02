using System;

namespace VehicleDemo
{
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

        //
        // Defaultkonstruktor
        //
        public Vehicle()
        {
        }


        public Vehicle(string regNr, string make, string model, int year, bool forSale)
        {
            this.RegNr = regNr;
            this.Make = make;
            this.Model = model;
            this.Year = year;
            this.ForSale = forSale;
        }

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
                if (value < 1900)
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

        public override String ToString()
        {
            return String.Format("\nBilinformation\nReg: {0}, {1} {2} [{3}]{4}",
                                     this.RegNr, this.Make, this.Model, this.YearToString(), this.ForSaleToString());
        }
    }
}
