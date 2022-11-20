using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA_lab_2
{
    public class LDFS
    {
        static readonly State finalState = new State(new int[,] { {1,2,3},
                                                                   {4,5,6},
                                                                   {7,8,0}  });

        static bool solved = false;
        static int iterator = 0;
        public static Stack<State> stack = new Stack<State>();
        private static List<State> visited = new List<State>();
        private static int max_depth;


        public static void PrintPath(ref int state_in_mem)
        {
            int i = 1;
            int count = stack.Count;

            foreach (var item in stack.Reverse())
            {
                item.Print();
                Console.WriteLine();
                if (i!=count)
                {
                    List<State> list = item.GetChilds();
                    state_in_mem += list.Count;
                }
                i++;
            }
        }

        public static void Solve(State root, int depth_limit)
        {
            int iterations = 0;
            int angles = 0;
            int states = 1;
            stack.Push(root);
            State? result = DFS(root, 0, ref iterations, depth_limit, ref angles, ref states);
            if (result is null)
            {
                Console.WriteLine("result not found");
            }
            else
            {
                int state_in_mem = 1;
                PrintPath(ref state_in_mem);
                Console.WriteLine($"--------- {max_depth} depth, {iterations} iterations, {angles} angles, {states} total states count, {state_in_mem} states in memory");
            }
           
        }

        public static State? DFS(State root, int depth, ref int iterations, int depth_limit, ref int angles, ref int state_count)
        {

            iterations++;



            // check if matrix is final matrix.
            if (root == finalState)
            {

                max_depth = depth;
                return root;
            }

            List<State> childs = root.GetChilds();

            if (depth >= depth_limit)
            {
                angles++;
                return null;
            }

            state_count += childs.Count;

            foreach (var child in childs)
            {
                stack.Push(child);
                // recursive call to dfs.
                State? result = DFS(child, depth + 1, ref iterations, depth_limit, ref angles, ref state_count);

                if (result is not null)
                {
                    return result;
                }
                stack.Pop();
            }
            return null;

        }
    }
}
