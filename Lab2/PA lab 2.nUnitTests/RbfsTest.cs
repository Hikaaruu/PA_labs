namespace PA_lab_2.nUnitTests
{
    public class RbfsTest
    {
        private ExtendedState? ES { get; set; }
        private int? MyInt { get; set; }
        private int iterations = 0;
        private int angles = 0;
        private int states_count = 0;
        private int state_in_memory = 0;

        private static bool CheckPath(List<ExtendedState> list)
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
            int[,] matrix = new int[,] { {0,2,3},
                                         {1,5,6},
                                         {4,7,8}  };

            (ES, MyInt) = RBFS.Search(new ExtendedState(matrix, 0, null), int.MaxValue, ref iterations, ref angles, ref states_count);
            List<ExtendedState> list = RBFS.FindSolution(ES, ref state_in_memory);

            Assert.IsTrue(CheckPath(list));
        }

        [Test]
        public void Test2()
        {
            int[,] matrix = new int[,] { {1,3,7},
                                         {5,0,2},
                                         {4,8,6}  };

            (ES, MyInt) = RBFS.Search(new ExtendedState(matrix, 0, null), int.MaxValue, ref iterations, ref angles, ref states_count);
            List<ExtendedState> list = RBFS.FindSolution(ES, ref state_in_memory);

            Assert.IsTrue(CheckPath(list));
        }

        [Test]
        public void Test3()
        {
            int[,] matrix = new int[,] { {1,5,2},
                                         {8,7,3},
                                         {0,4,6}  };

            (ES, MyInt) = RBFS.Search(new ExtendedState(matrix, 0, null), int.MaxValue, ref iterations, ref angles, ref states_count);
            List<ExtendedState> list = RBFS.FindSolution(ES, ref state_in_memory);

            Assert.IsTrue(CheckPath(list));
        }

        [Test]
        public void Test4()
        {
            int[,] matrix = new int[,] { {4,1,3},
                                         {7,0,2},
                                         {8,6,5}  };

            (ES, MyInt) = RBFS.Search(new ExtendedState(matrix, 0, null), int.MaxValue, ref iterations, ref angles, ref states_count);
            List<ExtendedState> list = RBFS.FindSolution(ES, ref state_in_memory);

            Assert.IsTrue(CheckPath(list));
        }

        [Test]
        public void Test5()
        {
            int[,] matrix = new int[,] { {2,3,0},
                                         {1,5,7},
                                         {4,8,6}  };

            (ES, MyInt) = RBFS.Search(new ExtendedState(matrix, 0, null), int.MaxValue, ref iterations, ref angles, ref states_count);
            List<ExtendedState> list = RBFS.FindSolution(ES, ref state_in_memory);

            Assert.IsTrue(CheckPath(list));
        }

    }
}