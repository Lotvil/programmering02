using System;
using VehicleApplikation1a;

namespace Vehicleapplikation1a
{
    public class Car : Vehicle
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="regNr"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="forSale"></param>
        public Car(String regNr, String make, String model, int year, bool forSale) : base(regNr, make, model, year, forSale)
        {
        }
    }
}
