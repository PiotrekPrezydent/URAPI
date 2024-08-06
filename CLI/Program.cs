using URAPI;

namespace CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (var c in Client.GetCollages())
            {
                Console.WriteLine(c.Name + " #########################################");
                foreach (var m in c.GetMajors())
                {
                    Console.WriteLine(m.Name);
                    m.GetYearOfStudies();
                }
                Console.WriteLine("\n\n\n\n\n");
            }

        }
    }
}
