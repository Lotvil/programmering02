namespace u03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            Console.WriteLine("Hur många pennor vill du köpa?");
            int pennor = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Vad kostar pennorna styck?");
            double kostnadPenna = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Hur mycket äpplen vill du köpa i vikt(kg)?");
            double äpple = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Hur mycket kostar äpplen/kg?");
            double äppleKostnad = Convert.ToDouble(Console.ReadLine());

            double kostnadPennor = pennor * kostnadPenna;
            double kostnadÄpplen = äpple * äppleKostnad;
            double totalKostnad = kostnadPennor + kostnadÄpplen;

            Console.WriteLine($"Jag handlade {pennor} pennor för {kostnadPennor} och 1 äpple för {kostnadÄpplen:#.00} vilket blev totalt {totalKostnad:#.00}kr");
        }
    }
}
