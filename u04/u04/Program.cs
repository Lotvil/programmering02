namespace u04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            DateTime dt = DateTime.Now;
            int hour = dt.Hour;

            string u;

            if(hour >= 8)
            {
                u = "Skoldagen pågår";
            }
            else if (hour > 17)
            {
                u = "Skoldagen har slutat";
            }
            else
            {
                u = "Skoldagen har inte börjat";
            }
            Console.WriteLine(u);
        }
    }
}
