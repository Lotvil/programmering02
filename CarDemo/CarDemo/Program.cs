namespace CarDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            Console.WriteLine("Ange information om bilen");   // Skriver ut till konsollen

            Console.Write("Registreringsnummer: ");          // Registreringsnummer
            string regNr = Console.ReadLine();

            Console.Write("Bilmärke: ");                      // Bilmärke
            string make = Console.ReadLine();

            Console.Write("Modell: ");                        // Modell
            string model = Console.ReadLine();

            Console.Write("Årsmodell: ");                     // Årsmodell
            int year = Convert.ToInt16(Console.ReadLine());   // Konverteras till int

            Console.Write("Till salu (J/N): ");               // Till salu?
            char ch = Convert.ToChar(Console.Read());         // Konverteras till char

            bool forSale = false;
            if (Char.ToUpper(ch) == 'J')
            {
                forSale = true;
            }

            // Skapa ny bil med inmatade värden
            Car c = new Car(regNr, make, model, year, forSale);

            // Skriv ut info om bilen c
            Console.WriteLine("\n" + c.ToString());
        }
    }
}
