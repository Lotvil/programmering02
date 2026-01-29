using System.Reflection;
using System.Text.RegularExpressions;

namespace CarDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            //Skapar variabler
            List<Car> carList = new List<Car>();
            char menuSelection;

            //Lägger till bilar
            StartCars(carList);

            //Program loopen
            do
            {   //Utförandet efter ett menu val
                switch (menuSelection = Menu(carList))
                {
                    case '0':
                        Console.WriteLine("\n\nTack för att du använt denna applikation!");
                        System.Threading.Thread.Sleep(1000); // 1000 ms = 1 sekunder
                        break;
                    case '1':
                        PrintList(carList);
                        break;
                    case '2':
                        carList = AddCar(carList);
                        break;
                    case '3':
                        RemoveCar(carList);
                        break;
                    case '4':
                        EmptyList(carList);
                        break;
                    default:
                        break;
                }
            } while (menuSelection != '0');
        }

        /// <summary>
        /// Metod som visar programmets meny och returnerar användarens val som en char.
        /// </summary>
        /// <param name="carList"></param>
        /// <returns>Menyvalet (char)</returns>
        public static char Menu(List<Car> carList)
        {
            string menu = "\n\n###############################" +
                  "\n##                           ##" +
                  "\n## Programmeny               ##" +
                  "\n## Antal bilar: " + carList.Count + " st.        ##" +
                  "\n##                           ##" +
                  "\n###############################" +
                  "\n1. Skriv ut listan" +
                  "\n2. Lägg till bil" +
                  "\n3. Ta bort bil" +
                  "\n4. Töm hela listan" +
                  "\n0. Avsluta" +
                  "\nAnge ditt val: ";

            Console.Write(menu);

            return Console.ReadKey().KeyChar;
        }

        /// <summary>
        /// Metod som skapar och lägger till en bil i listan
        /// </summary>
        /// <param name="carList"></param>
        /// <returns>Listan med den adderade bilen</returns>
        public static List<Car> AddCar(List<Car> carList)
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
            carList.Add(new Car(regNr, make, model, year, forSale));
            //Returnera listan
            return carList;
        }

        /// <summary>
        /// Skriver ut listan på alla bilar
        /// </summary>
        /// <param name="carList"></param>
        public static void PrintList(List<Car> carList)
        {
            int i = 1;

            Console.WriteLine("\n\nNr\tRegNr\tMärke\tModel\tÅrsmodell\tTill salu?");

            foreach (Car c in carList)
            {
                Console.WriteLine($"{i++}\t{c.ToStringList()}");
            }
        }

        /// <summary>
        /// Tar bort en bil
        /// </summary>
        /// <param name="carList"></param>
        /// <returns>Listan på alla bilar</returns>
        public static List<Car> RemoveCar(List<Car> carList)
        {
            Console.WriteLine("\n\nBilarna som finns:");

            PrintList(carList);

            Console.WriteLine("Välj en bil att ta bort (0 går tillbaka):");
            int removeIndex = Convert.ToInt16(Console.ReadLine());

            if(removeIndex != 0)
            {
                carList.RemoveAt(removeIndex - 1);
            }

            return carList;
        }

        /// <summary>
        /// Tömmer listan
        /// </summary>
        /// <param name="carList"></param>
        /// <returns>Den tomma listan</returns>
        public static List<Car> EmptyList(List<Car> carList)
        {
            carList.Clear();

            return carList;
        }

        /// <summary>
        /// Metoden lägger in fyra biler och är till för testning
        /// </summary>
        /// <param name="carList"></param>
        /// <returns>Den uppdaterade nya listan</returns>
        public static List<Car> StartCars(List<Car> carList)
        {
            //Lägger till bilen i listan
            carList.Add(new Car("ABC123", "Volvo", "v90", 1987, true));
            carList.Add(new Car("DEF456", "Toyota", "Corolla", 2015, false));
            carList.Add(new Car("GHI789", "BMW", "320i", 2020, true));
            carList.Add(new Car("JKL321", "Audi", "A4", 2018, false));
            //Returnera listan
            return carList;
        }
    }
}
