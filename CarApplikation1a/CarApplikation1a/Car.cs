using System;

namespace CarDemo
{
    public class Car
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
        public Car()
        {
        }


        public Car(string regNr, string make, string model, int year, bool forSale)
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

        public override string ToString()
        {
            return String.Format("\nBilinformation\nReg: {0}, {1} {2} [{3}]\n{4}",
                                     this.RegNr, this.Make, this.Model, this.YearToString(), this.ForSaleToString());
        }
        public String ToStringList()
        {
            return String.Format($"{this.RegNr}\t{this.Make}\t{this.Model}\t{this.YearToString()}\t{this.ForSaleToString2()}");
        }
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
