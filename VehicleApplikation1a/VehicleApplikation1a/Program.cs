using System.Reflection;
using System.Text.RegularExpressions;
using Vehicleapplikation1a;
using VehicleApplikation1a;

namespace VehicleApplikation1a
{
    internal class Program
    {

        public static List<Vehicle> vehicleList = new List<Vehicle>();
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            //Skapar variabler
            List<Vehicle> carList = new List<Vehicle>();
            char menuSelection;

            //Lägger till bilar
            StartCars(carList);
            StartVehicle();

            //Program loopen
            do
            {   //Utförandet efter ett menu val
                switch (menuSelection = Menu())
                {
                    case '0':
                        Console.WriteLine("\n\nTack för att du använt denna applikation!");
                        System.Threading.Thread.Sleep(1000); // 1000 ms = 1 sekunder
                        break;
                    case '1':
                        PrintList();
                        break;
                    case '2':
                        AddCar();
                        break;
                    case '3':
                        AddLorry();
                        break;
                    case '4':
                        RemoveVehicle();
                        break;
                    case '5':
                        EmptyList();
                        break;
                    default:
                        break;
                }
            } while (menuSelection != '0');
        }

        /// <summary>
        /// Visar en meny och låter användaren göra ett val
        /// </summary>
        /// <returns>Användarens val som en char</returns>
        public static char Menu()
        {
            string menu = "\n\n###############################" +
                  "\n##                           ##" +
                  "\n## Programmeny               ##" +
                  "\n## Antal bilar: " + vehicleList.Count + " st.        ##" +
                  "\n##                           ##" +
                  "\n###############################" +
                  "\n1. Skriv ut listan" +
                  "\n2. Lägg till bil" +
                  "\n3. Lägg till Lastbil" +
                  "\n4. Ta bort fordon" +
                  "\n5. Töm hela listan" +
                  "\n0. Avsluta" +
                  "\nAnge ditt val: ";

            Console.Write(menu);

            return Console.ReadKey().KeyChar;
        }

        /// <summary>
        /// Låter användaren lägga till en bil i listan
        /// </summary>
        public static void AddCar()
        {
            Console.WriteLine("\n\nAnge information om bilen");
            //Frågar användaren om bilens information
            Console.Write("Ange registreringsnummer: ");
            String regNr = Console.ReadLine();
            Console.Write("Ange bilmärke: ");
            String make = Console.ReadLine();
            Console.Write("Ange bilmodell: ");
            String model = Console.ReadLine();
            Console.Write("Ange årsmodell: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.Write("Är bilen till salu? (J/N): ");
            char ch = Convert.ToChar(Console.ReadKey().KeyChar);

            bool forSale = false;
            if (Char.ToUpper(ch) == 'J')
            {
                forSale = true;
            }

            //Lägger till bilen i listan
            vehicleList.Add(new Lorry(regNr, make, model, year, forSale, 15000));
            //Returnera listan
        }
        /// <summary>
        /// Låter användaren lägga till en lastbil i listan
        /// </summary>
        public static void AddLorry()
        {
            Console.WriteLine("\n\nAnge information om bilen");
            //Frågar användaren om bilens information
            Console.Write("Ange registreringsnummer: ");
            String regNr = Console.ReadLine();
            Console.Write("Ange bilmärke: ");
            String make = Console.ReadLine();
            Console.Write("Ange bilmodell: ");
            String model = Console.ReadLine();
            Console.Write("Ange årsmodell: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ange maxlast: ");
            int load = Convert.ToInt32(Console.ReadLine());
            Console.Write("Är bilen till salu? (J/N): ");
            char ch = Convert.ToChar(Console.ReadKey().KeyChar);

            bool forSale = false;
            if (Char.ToUpper(ch) == 'J')
            {
                forSale = true;
            }

            //Lägger till bilen i listan
            vehicleList.Add(new Lorry(regNr, make, model, year, forSale, load));
            //Returnera listan
        }

        /// <summary>
        /// Skriver ut en formaterad lista på alla fordon i listan
        /// </summary>
        public static void PrintList()
        {
            int i = 1;

            Console.WriteLine("\n\nNr\tRegNr\tMärke\tModel\tÅrsmodell\tTill salu?\tMaxLast");

            foreach (Vehicle c in vehicleList)
            {
                Console.WriteLine($"{i++}\t{c.ToStringList()}");
            }
        }

        /// <summary>
        /// Tar bort ett fordon från listan baserat på användarens val av index i listan
        /// </summary>
        public static void RemoveVehicle()
        {
            Console.WriteLine("\n\nBilarna som finns:");

            PrintList();

            Console.WriteLine("Välj en bil att ta bort (0 går tillbaka):");
            int removeIndex = Convert.ToInt16(Console.ReadLine());

            if (removeIndex != 0)
            {
                vehicleList.RemoveAt(removeIndex - 1);
            }
        }

        /// <summary>
        /// Tar bort alla fordon från listan
        /// </summary>
        public static void EmptyList()
        {
            vehicleList.Clear();
        }

        /// <summary>
        /// Metoden lägger in fyra biler och är till för testning
        /// </summary>
        /// <param name="carList"></param>
        /// <returns>Den uppdaterade nya listan</returns>
        public static List<Vehicle> StartCars(List<Vehicle> carList)
        {
            //Lägger till bilen i listan
            carList.Add(new Car("ABC123", "Volvo", "v90", 1987, true));
            carList.Add(new Car("DEF456", "Toyota", "Corolla", 2015, false));
            carList.Add(new Car("GHI789", "BMW", "320i", 2020, true));
            carList.Add(new Car("JKL321", "Audi", "A4", 2018, false));
            //Returnera listan
            return carList;
        }
        /// <summary>
        /// Lägger till fem fordon i listan, både bilar och lastbilar, och är till för testning
        /// </summary>
        public static void StartVehicle()
        {
            //Lägger till bilen i listan
            vehicleList.Add(new Car("ABC123", "Volvo", "v90", 1987, true));
            vehicleList.Add(new Car("DEF456", "Toyota", "Corolla", 2015, false));
            vehicleList.Add(new Car("GHI789", "BMW", "320i", 2020, true));
            vehicleList.Add(new Lorry("JKL321", "Audi", "A4", 2018, false, 15000));
            vehicleList.Add(new Lorry("MNO654", "Scania", "R500", 2019, true, 30000));
        }
    }
}
