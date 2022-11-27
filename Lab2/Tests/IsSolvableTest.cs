
namespace PA_lab_2.nUnitTests
{
    public class Tests
    {
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

            State f = new State(f);
        }
    }
}