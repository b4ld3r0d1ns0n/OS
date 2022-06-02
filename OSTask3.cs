using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OSTask3
{
    class Program
    {
        static public bool tumbler = true;
        static public int count = 0;

        static void MainMenu()
        {

            bool tumbler = true;
            while (tumbler)
            {
                Console.WriteLine("Меню");
                Console.WriteLine("1 Запустить задание");
                Console.WriteLine("0 Выйти из программы");
                Console.Write("Выберите пункт меню: ");
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:

                        Channel<int> channel = Channel.CreateBounded<int>(200);
                        var sends = new CancellationTokenSource();

                        Task[] channels = new Task[5];
                        for (int i = 0; i < 5; i++)
                        {
                            if (i < 3)
                            {
                                channels[i] = Task.Run(() => { new Producer(channel.Writer, sends.Token); }, sends.Token);
                            }
                            else
                            {
                                channels[i] = Task.Run(() => { new Consumer(channel.Reader, sends.Token); }, sends.Token);
                            }
                        }
                        new Thread(() =>
                        {
                            bool tumbler2 = true;
                            while (tumbler2 is true)
                            {
                                if (Console.ReadKey(true).Key == ConsoleKey.Q)
                                {
                                    sends.Cancel();
                                    tumbler2 = false;
                                }
                            }
                        })
                        { IsBackground = true }.Start();
                        Task.WaitAll(channels);
                        break;

                    case 0:
                        tumbler = false;
                        break;

                    default:
                        Console.WriteLine("Такого пункта не существует");
                        break;
                }
            }
        }
        class Producer
    {
        private ChannelWriter<int> Writer;
        public Producer(ChannelWriter<int> _writer, CancellationToken tunnel)
        {
            Writer = _writer;
            Task.WaitAll(Run(tunnel));
        }

        private async Task Run(CancellationToken tunnel)
        {
            var r = new Random();
            while (await Writer.WaitToWriteAsync())
            {
                if (tunnel.IsCancellationRequested)
                {
                    Console.WriteLine("Производитель остановлен");
                    return;
                }
                if (Program.tumbler && Program.count <= 100 && Program.count > -1)
                {
                    var product = r.Next(1, 101);
                    await Writer.WriteAsync(product);
                    Program.count += 1;
                    Console.WriteLine($"Записанные данные: {product}" + $" Очередь: {Program.count}");
                }
            }
        }
    }

    class Consumer
    {
        private ChannelReader<int> Reader;

        public Consumer(ChannelReader<int> _reader, CancellationToken tunnel)
        {
            Reader = _reader;
            Task.WaitAll(Run(tunnel));
        }

        private async Task Run(CancellationToken tunnel)
        {
            while (await Reader.WaitToReadAsync())
            {

                if (Reader.Count >= 0)
                {
                    var product = await Reader.ReadAsync();
                    Program.count -= 1;
                    if (Program.count == -1)
                    {
                        Program.count = 0;
                    }

                    Console.WriteLine($"Полученные данные: {product}" + $" Очередь: {Program.count}");


                }
                if (Reader.Count >= 100)
                {
                    Program.tumbler = false;
                }
                else if (Reader.Count <= 80)
                {
                    Program.tumbler = true;
                }

                if (tunnel.IsCancellationRequested)
                {
                    if (Reader.Count == 0)
                    {
                        Console.WriteLine("Потребитель остановлен");
                        return;
                    }
                }
            }
        }
    }

        static void Main(string[] args)
        {
            MainMenu();
        }
    }
}
