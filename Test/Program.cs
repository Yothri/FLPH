using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new Client())
            {
                client.Connect();

                while(true)
                {
                    string inp = Console.ReadLine();
                }
            }
        }
    }
}
