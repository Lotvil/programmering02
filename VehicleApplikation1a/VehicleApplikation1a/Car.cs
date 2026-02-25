using System;
using VehicleApplikation1a;

namespace Vehicleapplikation1a
{
    public class Car : Vehicle
    {
        public Car(String regNr, String make, String model, int year, bool forSale) : base(regNr, make, model, year, forSale)
        {
        }
    }
}
