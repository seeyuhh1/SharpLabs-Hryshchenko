using Task5;

namespace Practical1Test
{
    internal class Task5_StudentGroups_Tests
    {

        [Test]
        public void GetAverage_ValidArray_ReturnsCorrectAverage()
        {
            int[] marks = { 80, 90, 70, 60, 100 };
            double result = Program.GetAverage(marks);
            Assert.AreEqual(80.0, result, 0.001);
        }

        [Test]
        public void GetMin_ValidArray_ReturnsMinimum()
        {
            int[] marks = { 80, 90, 70, 60, 100 };
            int result = Program.GetMin(marks);
            Assert.AreEqual(60, result);
        }

        [Test]
        public void GetMax_ValidArray_ReturnsMaximum()
        {
            int[] marks = { 80, 90, 70, 60, 100 };
            int result = Program.GetMax(marks);
            Assert.AreEqual(100, result);
        }
    }
}
