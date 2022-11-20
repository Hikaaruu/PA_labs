namespace PA_lab_2
{
    public class RBFS
    {
        public static readonly State finalState = new State(new int[,] { {1,2,3},
                                                                   {4,5,6},
                                                                   {7,8,0}  });


        private static ExtendedState? finalResult = null;

        public static void Start(int[,] matrix)
        {
            int iterations = 0;
            int states_count = 1;

            int angles = 0;
            (ExtendedState? node, int f ) = Search(new ExtendedState(matrix, 0, null), int.MaxValue, ref iterations, ref angles, ref states_count);
            if (node is null)
            {
                Console.WriteLine("not solved");
            }
            else
            {
                int state_in_memory = 1;
                PrintResult(FindSolution(node, ref state_in_memory), iterations, states_count, angles, state_in_memory);
            }
        }


        public static List<ExtendedState> FindSolution(ExtendedState node, ref int state_in_memory)
        {
            List<ExtendedState> result= new List<ExtendedState>();
            result.Add(node);
            while (node.Parent != null)
            {
                node = node.Parent;
                List<ExtendedState> list = node.GetChildsEx();
                state_in_memory += list.Count;
                result.Add(node);
            }
            result.Reverse();
            return result;
        }

        private static void PrintResult(List<ExtendedState> result, int iterations, int states_count, int angles, int state_in_memory)
        {
            foreach (var item in result)
            {
                item.Print();
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine($"--------- {iterations} iterations, {angles} angles, {states_count} total states count, {state_in_memory} states in memory");
        }

        public static (ExtendedState?, int) Search(ExtendedState node, int f_limit, ref int iterations, ref int angles, ref int state_count)
        {
            iterations++;

            if (node == finalState)
            {
                return (node, node.FunctionValue);
            }

            List<ExtendedState> successors = node.GetChildsEx();

            state_count += successors.Count;

            if (successors.Count==0)
            {
                return (null, int.MaxValue);
            }

            while (successors.Count>=1)
            {
                successors.Sort(delegate (ExtendedState c1, ExtendedState c2) { return c1.FunctionValue.CompareTo(c2.FunctionValue); });
                ExtendedState bestNode = successors.First();

                if (bestNode.FunctionValue>f_limit)
                {
                    angles++;
                    return (null, bestNode.FunctionValue);
                }

                int alternative = successors[1].FunctionValue;

                (ExtendedState? result, bestNode.FunctionValue) = Search(bestNode, Math.Min(f_limit, alternative), ref iterations, ref angles, ref state_count);

                if (result != null)
                {
                    finalResult= result;
                    break;
                }
            }

            return (finalResult, 0);

        }
    }
}
