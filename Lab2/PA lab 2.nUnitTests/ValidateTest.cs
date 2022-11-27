namespace PA_lab_2.nUnitTests
{
    public class ValidateTest
    {

        private string? MyStr { get; set; }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            bool result = false;
            List<int>? list = Program.Validate(null, out result);
            Assert.That(result, Is.False);
        }

        [Test]
        public void Test2()
        {
            bool result = false;
            List<int>? list = Program.Validate("1234567", out result);
            Assert.That(result, Is.False);
        }

        [Test]
        public void Test3()
        {
            bool result = false;
            List<int>? list = Program.Validate("123456789", out result);
            Assert.That(result, Is.False);
        }

        [Test]
        public void Test4()
        {
            bool result = false;
            List<int>? list = Program.Validate("12345678w", out result);
            Assert.That(result, Is.False);
        }

        [Test]
        public void Test5()
        {
            bool result = false;
            List<int>? list = Program.Validate("132450678", out result);
            Assert.That(result, Is.True);
        }
    }
}