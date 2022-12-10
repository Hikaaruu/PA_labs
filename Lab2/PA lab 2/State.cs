using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PA_lab_2
{
    public class State : ICloneable
    {
        public int[,] Matrix { get; set; }

        public State(int[,] matrix)
        {
            if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
            {
                throw new ArgumentOutOfRangeException();
            }
            Matrix = matrix;
        }

        static protected readonly int[] offsetI = { -1, 0, 0, 1 };

        static protected readonly int[] offsetJ = { 0, -1, 1, 0 };

        public static bool operator ==(State? a, State? b)
        {
            if ((a is null && b is not null) || (a is not null && b is null))
            {
                return false;
            }
            if (a is null && b is null)
            {
                return true;
            }
            bool result = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (a[i, j] != b[i, j])
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public static bool operator !=(State? a, State? b)
        {
            if (a == b)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int this[int x, int y]
        {
            get
            {
                return Matrix[x, y];
            }
            protected set
            {
                Matrix[x, y] = value;
            }
        }

        public bool IsSolvable()
        {
            int[] linearForm = new int[9];
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    linearForm[k++] = Matrix[i, j];
                }
            }

            int inv_count = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = i + 1; j < 9; j++)
                {
                    if (linearForm[i] > 0 && linearForm[j] > 0 && linearForm[i] > linearForm[j])
                        inv_count++;
                }
            }
                

                    // Value 0 is used for empty space
                    
            return (inv_count % 2 == 0);
        }

        public void Print()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(Matrix[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }

        public List<State> GetChilds()
        {
            int[,] parent_matrix = this.Matrix;
            List<State> childs = new List<State>();
            var cords = GetZeroCord();
            int i = cords.Item1;
            int j = cords.Item2;
            int newI;
            int newJ;
            int tmp;
            State child;
            for (int k = 0; k < 4; k++)
            {
                newI = i + offsetI[k];
                newJ = j + offsetJ[k];
                if (newI >= 3 || newI < 0 || newJ >= 3 || newJ < 0)
                {
                    continue;
                }

                child = new State((int[,])parent_matrix.Clone());
                tmp = child[newI, newJ];
                child[newI, newJ] = 0;
                child[i, j] = tmp;
                childs.Add(child);
            }
            return childs;
        }


        public (int, int) GetZeroCord()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Matrix[i, j] == 0)
                    {
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
        }

        public object Clone()
        {
            State result = new State((int[,])Matrix.Clone());
            return result;
        }
    }
}
