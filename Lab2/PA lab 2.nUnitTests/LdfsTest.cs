namespace PA_lab_2.nUnitTests
{
    public class LdfsTest
    {
        private int iterations = 0;
        private int angles = 0;
        private int states = 0;

        private static bool CheckPath(List<State> list)
        {
            for (int i = 0; i < list.Count-1; i++)
            {
                List<State> childs = list[i].GetChilds();
                bool found = false;
                foreach (var child in childs)
                {
                    if (child == list[i+1])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    return false;
                }
            }

            if (list.Last()==RBFS.finalState)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            LDFS.stack = new Stack<State>();  

            int[,] matrix = new int[,] { {0,5,3},
                                         {2,1,7},
                                         {4,8,6}  };

            State? result = LDFS.DFS(new State(matrix), 0, ref iterations, 12, ref angles, ref states);
            List<State>? list = LDFS.stack.Reverse().ToList();
            Assert.IsTrue(CheckPath(list));
        }

        [Test]
        public void Test2()
        {
            LDFS.stack = new Stack<State>();

            int[,] matrix = new int[,] { {2,3,6},
                                         {1,5,8},
                                         {0,4,7}  };

            State? result = LDFS.DFS(new State(matrix), 0, ref iterations, 10, ref angles, ref states);
            List<State>? list = LDFS.stack.Reverse().ToList();
            Assert.IsTrue(CheckPath(list));
        }

        [Test]
        public void Test3()
        {
            LDFS.stack = new Stack<State>();

            int[,] matrix = new int[,] { {1,3,0},
                                         {8,2,5},
                                         {4,7,6}  };

            State? result = LDFS.DFS(new State(matrix), 0, ref iterations, 7, ref angles, ref states);
            Assert.IsTrue(result is null);
        }

        [Test]
        public void Test4()
        {
            LDFS.stack = new Stack<State>();

            int[,] matrix = new int[,] { {1,3,0},
                                         {4,5,6},
                                         {7,8,2}  };

            State? result = LDFS.DFS(new State(matrix), 0, ref iterations, 10, ref angles, ref states);
            List<State>? list = LDFS.stack.Reverse().ToList();
            Assert.IsTrue(CheckPath(list));
        }

        [Test]
        public void Test5()
        {
            LDFS.stack = new Stack<State>();

            int[,] matrix = new int[,] { {1,6,0},
                                         {7,3,2},
                                         {5,4,8}  };

            State? result = LDFS.DFS(new State(matrix), 0, ref iterations, 17, ref angles, ref states);
            List<State>? list = LDFS.stack.Reverse().ToList();
            Assert.IsTrue(CheckPath(list));
        }


    }
}