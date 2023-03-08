using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CarListApp.Tests
{
    [TestClass]
    public class CarListViewModelTests
    {
        [TestMethod]
        public void TestTitle()
        {
            // Arrange
            var viewModel = new CarListViewModel(null);

            // Act
            var expectedTitle = "Liste de voiture (Examen POO)";
            var actualTitle = viewModel.Title;

            // Assert
            Assert.AreEqual(expectedTitle, actualTitle);
        }

    }
}
