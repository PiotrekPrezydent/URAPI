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
                        if (s.ScheduleType != "pdf")
                            continue;
                        var ms = await s.GetPDFAsImageStream(0);
                        Console.WriteLine(ms ==null);
                    }
                    Console.WriteLine("\n\n");
                }
                Console.WriteLine("\n\n\n\n\n");
            }

        }
    }
}