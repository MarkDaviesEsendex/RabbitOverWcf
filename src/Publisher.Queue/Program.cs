using System;
using System.Threading.Tasks;
using EasyNetQ;

namespace Publisher.Queue
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "Publisher Example";

            Console.WriteLine("Press '1' to publish the file scan");
            Console.WriteLine("Press '2' to simulate a long running file scan");
            Console.WriteLine("Press any other key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key == ConsoleKey.D1)
                {
                    PublishFileScan(1000);
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    PublishFileScan(30000);
                }
                else
                {
                    return;
                }
            }
        }

        private static void PublishFileScan(int sleepTime)
        {
            var fileId = Guid.NewGuid();
            const string fileName = "File.jpg";

            var bus = RabbitHutch.CreateBus("host=localhost");
            bus.Publish(new Message { Id = fileId, FileName = fileName, SleepTime = 10}, configuration => configuration.WithQueueName("FileScanning/HandleFileScanRequest"));
        }
    }

    public class Message
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public int SleepTime { get; set; }
    }
}
