using lab;

namespace TestProject;

[TestClass]
public class UnitTest1
{
    // Это тест для тестового метода в Program.cs.
    // Тест можно удалить из проекта.
    [TestMethod]
    public void TestTestMethod()
    {
        // Arrange
        int numberToPass = 8;
        int expected = 64;
        
        // Act
        int actual = Program.GetSquare(numberToPass);

        // Assert
        Assert.AreEqual(expected, actual);
    }
}