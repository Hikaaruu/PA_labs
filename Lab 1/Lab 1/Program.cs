
using System.Diagnostics;
using System.Xml.Linq;

namespace Lab_1
{
    internal class Program
    {
        public const int sizeInMB = 1048576;
        const string path = "file.bin";

        private static void CreateFile(string path, long mb)
        {
            byte[] data = new byte[sizeInMB * mb];
            Random rng = new Random();
            rng.NextBytes(data);
            File.WriteAllBytes(path, data);
        }

        private static void ReadFromFile(string path, long count)
        {
            BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open));
            for (int i = 0; i < count; i++)
            {
                if (br.BaseStream.Position == br.BaseStream.Length)
                {
                    break;
                }
                else
                {
                    Console.Write($"{br.ReadByte()} ");
                }
            }

            br.Close();
        }

        private static void MainProgram()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            File.Delete(path);
            CreateFile(path, 1024);
            ReadFromFile(path, 1000);
            sw.Stop();
            Console.WriteLine("time to generate and output file : {0}", sw.Elapsed);
            sw.Reset();
            sw.Start();
            MergeSort ms = new MergeSort(path, "b.bin", "c.bin");
            ms.DefaultStart();
            sw.Stop();
            Console.WriteLine("time to sort : {0}", sw.Elapsed);
            ReadFromFile(path, 100000);
        }

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            TimeSpan optimizedTime;
            File.Delete(path);
            File.Delete("b.bin");
            File.Delete("c.bin");

            CreateFile(path, 100);


            sw.Start();
            MergeSort ms = new MergeSort(path, "b.bin", "c.bin");
            ms.OptimizedStart();
            sw.Stop();
            optimizedTime = sw.Elapsed;
            ReadFromFile(path, 100);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("optimized sort time :  {0}", optimizedTime.ToString());

        }
    }
}