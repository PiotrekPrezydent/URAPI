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
                    foreach(var y in m.GetYearOfStudies())
                    {
                        Console.WriteLine(y.Name);
                    }
                    Console.WriteLine("\n\n");
                }
                Console.WriteLine("\n\n\n\n\n");
            }

        }
    }
}
