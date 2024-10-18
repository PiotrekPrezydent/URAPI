using System.Drawing;
using URAPI;

namespace CLI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            foreach (var c in Client.GetCollages().Result)
            {
                foreach (var m in c.GetMajors().Result)
                {
                    Console.WriteLine(m.Name);
                    foreach(var s in m.GetSchedules().Result)
                    {
                        Image? i = await s.ScheduleAsImage();
                        if (i == null)
                            Console.WriteLine("null");
                        else
                            Console.WriteLine("git");
                    }
                    Console.WriteLine("\n\n");
                }
                Console.WriteLine("\n\n\n\n\n");
            }

        }
    }
}