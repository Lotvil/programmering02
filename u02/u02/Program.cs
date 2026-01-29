namespace u02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            int pennor = 3;
            int kostnadPenna = 4;
            double äpple = 0.2;
            int äppleKostnad = 19;

            double kostnadPennor = pennor * kostnadPenna;
            double kostnadÄpplen = äpple * äppleKostnad;
            double totalKostnad = kostnadPennor + kostnadÄpplen;

            Console.WriteLine($"Jag handlade {pennor} pennor för {kostnadPennor} och 1 äpple för {kostnadÄpplen:#.00} vilket blev totalt {totalKostnad:#.00}kr");
        }
    }
}
