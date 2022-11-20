namespace PA_lab_2.nUnitTests
{
    public class IsSolvableTests
    {

        private State? MyState { get; set; }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            int[,] f = new int[,] { {8,1,2},
                                    {0,4,3},
                                    {7,6,5}  };
            MyState = new State(f);
            bool isSolvable = MyState.IsSolvable();
            Assert.IsFalse(isSolvable);
        }

        [Test]
        public void Test2()
        {
            int[,] f = new int[,] { {1,8,2},
                                    {0,4,3},
                                    {7,6,5}  };
            MyState = new State(f);
            bool isSolvable = MyState.IsSolvable();
            Assert.IsTrue(isSolvable);
        }

        [Test]
        public void Test3()
        {
            int[,] f = new int[,] { {1,2,3},
                                    {4,5,6},
                                    {0,8,7}  };
            MyState = new State(f);
            bool isSolvable = MyState.IsSolvable();
            Assert.IsFalse(isSolvable);
        }

        [Test]
        public void Test4()
        {
            int[,] f = new int[,] { {5,2,8},
                                    {4,1,7},
                                    {0,3,6}  };
            MyState = new State(f);
            bool isSolvable = MyState.IsSolvable();
            Assert.IsTrue(isSolvable);
        }

    }
}