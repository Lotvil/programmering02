namespace u06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            //u06
            int a = 1;

            for (int i = 1; i <= 10; i += 2)
            {
                Console.Write(i);
                a = a * i;
            }

            Console.WriteLine("\n" + a);

            //u07
            Console.WriteLine("\n");
            int råttor = 100;
            int månader = 0;

            while (råttor < 1000000)
            {
                månader += 1;
                råttor *= 2;
                Console.WriteLine("\n" + råttor);
            }
            Console.WriteLine("\n");
            Console.WriteLine(råttor);
            Console.WriteLine(månader);

            //u08
            Console.WriteLine("\n");
            char c = '0';
            do
            {
                c = Console.ReadKey().KeyChar;

                switch (c)
                {
                    case '1':     // Om val har värdet 1 så fortsätter koden här
                        Console.WriteLine("Val 1");
                        break;    // break avslutar varje "case" och måste finnas med
                    case '2':
                        Console.WriteLine("Val 2");
                        break;
                    case '3':     // Flera case kan listas efter varandra
                    case '4':     // Alla dessa case kommer köra samma instruktion
                    case '5':
                        Console.WriteLine("Val 3, 4 eller 5");
                        break;
                    default:    // Default-caset kommer ta hand om alla andra val
                        Console.WriteLine("Alla andra val");
                        break;
                }
            } while (c != '6');
            Console.WriteLine("\nSlut på program");
        }
    }
}
