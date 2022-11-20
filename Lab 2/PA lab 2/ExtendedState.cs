using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA_lab_2
{
    public class ExtendedState : State
    {
        public ExtendedState(int[,] matrix, int path_cost, ExtendedState? parent) : base(matrix)
        {
            if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
            {
                throw new ArgumentOutOfRangeException();
            }
            Matrix = matrix;
            Parent = parent;
            PathCost = GetPathCost(parent, path_cost);
            FunctionValue = GetFunctionValue(matrix, PathCost);
        }

        public int FunctionValue { get; set; }
        public ExtendedState? Parent { get; set; }
        public int PathCost { get; set; }


        private static int GetPathCost(ExtendedState? parent, int path_cost)
        {
            if (parent == null)
            {
                return path_cost;
            }
            else
            {
                return parent.PathCost + path_cost;
            }
        }

        private static int GetFunctionValue(int[,] matrix, int ready_path_cost)
        {
            int value = 0;
            int k = 1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrix[i, j] != k && matrix[i, j] != 0)
                    {
                        value++;
                    }
                    k++;
                }
            }
            return value + ready_path_cost;
        }

        public  List<ExtendedState> GetChildsEx()
        {

            int[,] parent_matrix = this.Matrix;
            List<ExtendedState> childs = new List<ExtendedState>();
            var cords = GetZeroCord();
            int i = cords.Item1;
            int j = cords.Item2;
            int newI;
            int newJ;
            int tmp;
            ExtendedState child;
            for (int k = 0; k < 4; k++)
            {
                newI = i + offsetI[k];
                newJ = j + offsetJ[k];
                if (newI >= 3 || newI < 0 || newJ >= 3 || newJ < 0)
                {
                    continue;
                }

                int[,] childMatrix = (int[,])parent_matrix.Clone(); 
                tmp = childMatrix[newI, newJ];
                childMatrix[newI, newJ] = 0;
                childMatrix[i, j] = tmp;
                child = new ExtendedState(childMatrix, 1, this);
                childs.Add(child);
            }
            return childs;
        }
    }
}
