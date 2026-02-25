using System;
using VehicleApplikation1a;

namespace Vehicleapplikation1a
{
    public class Lorry : Vehicle
    {

        private int load;
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
        public Lorry(String regNr, String make, String model, int year, bool forSale, int load) : base(regNr, make, model, year, forSale)
        {
            this.Load = load;
        }

        public new String ToString() 
        {
            String s = base.ToString();
            s += String.Format("\nMaxlast: {0} kg", this.load);
            return s;
        }

        public override String ToStringList()
        {
            String s = base.ToStringList();
            s += String.Format("{0} kg", this.load);
            return s;
        }
    }
}
