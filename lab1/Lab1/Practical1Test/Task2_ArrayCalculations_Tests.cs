using Task2;

namespace Practical1Test
{
    internal class Task2_ArrayCalculations_Tests
    {

        [Test]
        public void GenerateRandomArray_ValidParameters_ReturnsCorrectSize()
        {
            int[] result = Program.GenerateRandomArray(5, 1, 10);
            Assert.AreEqual(5, result.Length);
        }

        [Test]
        public void GenerateRandomArray_ValidParameters_ReturnsValuesInRange()
        {
            int[] result = Program.GenerateRandomArray(100, 1, 10);
            foreach (int value in result)
            {
                Assert.GreaterOrEqual(value, 1);
                Assert.LessOrEqual(value, 10);
            }
        }

        

        

        [Test]
        public void GetSum_ValidArray_ReturnsCorrectSum()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            int result = Program.GetSum(array);
            Assert.AreEqual(15, result);
        }

        [Test]
        public void GetAverage_ValidArray_ReturnsCorrectAverage()
        {
            int[] array = { 2, 4, 6, 8 };
            double result = Program.GetAverage(array);
            Assert.AreEqual(5.0, result, 0.001);
        }

        [Test]
        public void GetMin_ValidArray_ReturnsMinimum()
        {
            int[] array = { 5, 2, 8, 1, 9 };
            int result = Program.GetMin(array);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetMax_ValidArray_ReturnsMaximum()
        {
            int[] array = { 5, 2, 8, 1, 9 };
            int result = Program.GetMax(array);
            Assert.AreEqual(9, result);
        }
    }
}
