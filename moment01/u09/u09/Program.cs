using System.Xml.Linq;

namespace u09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();



            Console.WriteLine("Hur många tärningsslag vill du göra?");
            int kast = Convert.ToInt32(Console.ReadLine());

            Random r = new Random();
            List<int> tärningar = new List<int>();

            for (int i = 0; i < kast; i++)
            {
                int tärning = r.Next(1, 7);
                tärningar.Add(tärning);
            }
            for (int i = 1; i < 7; i++)
            {
                Console.WriteLine($"Du slog {tärningar.Count(t => t == i)} kast med sidan {i}");
            }
        }
    }
}
