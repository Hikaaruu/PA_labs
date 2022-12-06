
using System.Diagnostics;
using System.Transactions;

namespace PA_lab_2
{
    public class Program
    {
        public static List<int>? Validate(string? str,out bool success)
        {
            success = true;
            if (str is null || str.Length != 9)
            {
                success = false;
                return null;
            }
            List<int> checkList = new List<int>();
            foreach (var sym in str)
            {
                int a = (int)char.GetNumericValue(sym);
                if (a == -1 || a == 9)
                {
                    success = false;
                    return null;
                }
                if (checkList.Contains(a))
                {
                    success = false;
                    return null;
                }
                checkList.Add(a);
            }
            if (!checkList.Contains(0))
            {
                success = false;
                return null;
            }
            return checkList;
        }

        static int[,]? ConvertData(string? str)
        {
            bool success = false;
            List<int>? list = Validate(str,out success);
            if (!success || list is null)
            {
                Console.WriteLine("Wrong Format");
                return null;
            }

            int[,] f = new int[3, 3];
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    f[i, j] = list[k];
                    k++;
                }
            }
            return f;
        }

        static void Start(int[,] f, int limit)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("RBFS : ");
            Console.WriteLine();
            RBFS.Start(f);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("---------time : " + sw.Elapsed.ToString());
            Console.WriteLine();
            Console.WriteLine();
            sw.Restart();
            Console.WriteLine("LDFS : ");
            Console.WriteLine();
            LDFS.Solve(new State(f), limit);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("---------time : " + sw.Elapsed.ToString());
        }

        static readonly Task timeAndRamControl = new Task(() =>
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            cancelTokenSource.Cancel();
            Process proc = Process.GetCurrentProcess();

            while (true)
            {
                TimeSpan ts = sw.Elapsed;
                if (ts.Minutes >= 30)
                {

                    Console.WriteLine("program stopped due to timeout");
                    token.ThrowIfCancellationRequested();
                }
                if (proc.PrivateMemorySize64 / (1024 * 1024) >= 1000)
                {
                    Console.WriteLine("program stopped due ram limit exceeded ");
                    token.ThrowIfCancellationRequested();
                }

                Thread.Sleep(5000);
            }

        });

        static void Main(string[] args)
        {

            
            timeAndRamControl.Start(); 

            //int[,] f = new int[,] { {8,1,2},
            //                        {0,4,3},
            //                        {7,6,5}  };

            Console.WriteLine("Enter state in liniar form (for example 230157486):");
            string? str = Console.ReadLine();
            int[,]? f = ConvertData(str);


            Console.WriteLine();
            if (f is not null)
            {
                bool solvable = new State(f).IsSolvable();
                if (!solvable)
                {
                    Console.WriteLine("Given state is not solvable");
                }
                else
                {
                    Console.WriteLine("enter depth limit for LDFS");
                    int limit;
                    bool success = int.TryParse(Console.ReadLine(), out limit);
                    Console.WriteLine();
                    if (!success || limit<1)
                    {
                        Console.WriteLine("Incorect depth format");
                    }
                    else
                    {
                        Start(f, limit);
                    }
                    
                }
            }





        }
    }
}