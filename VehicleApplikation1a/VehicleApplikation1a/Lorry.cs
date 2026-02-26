using System;
using VehicleApplikation1a;

namespace Vehicleapplikation1a
{
    public class Lorry : Vehicle
    {
        //
        // Medlemsvariabler
        //
        private int load;
        
        //
        //Properties
        //

        public int Load
        {
            get { return load; }
            set
            {
                if (value < 0)
                {
                    load = -1;
                }
                else
                {
                    load = value;
                }
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="regNr"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="forSale"></param>
        /// <param name="load"></param>
        public Lorry(String regNr, String make, String model, int year, bool forSale, int load) : base(regNr, make, model, year, forSale)
        {
            this.Load = load;
        }

        /// <summary>
        /// Lägger till load till den vanliga ToString() i föräldern
        /// </summary>
        /// <returns>En string med maxLast tillagt</returns>
        public new String ToString() 
        {
            String s = base.ToString();
            s += String.Format("\nMaxlast: {0} kg", this.load);
            return s;
        }

        /// <summary>
        /// Lägger till load till den vanliga ToStringList() i föräldern
        /// </summary>
        /// <returns>En string med maxLast tillagt</returns>
        public override String ToStringList()
        {
            String s = base.ToStringList();
            s += String.Format("{0} kg", this.load);
            return s;
        }
    }
}
