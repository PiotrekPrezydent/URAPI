using URAPI;

namespace CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (var c in Client.GetCollages().Result)
            {
                Console.WriteLine(c.Name + " #########################################");
                foreach (var m in c.GetMajors().Result)
                {
                    Console.WriteLine(m.Name);
                    foreach(var y in m.GetSchedules().Result)
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