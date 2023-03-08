using Xunit;

namespace CarListApp.Tests
{
    public class CarListViewModelTests
    {
        [Fact]
        public void TestTitle()
        {
            var viewModel = new CarListViewModel(null);

            var expectedTitle = "Liste de voiture (Examen POO)";
            var actualTitle = viewModel.Title;

            Assert.Equal(expectedTitle, actualTitle);
        }

        [Fact]
        public void TestCarsListNotEmpty()
        {
            var viewModel = new CarListViewModel(null);

            viewModel.GetCarList().Wait(); // Pour être sûr que la liste est chargée

            Assert.NotEmpty(viewModel.Cars);
        }
        [Fact]
        public void TestNumberOfCars()
        {
            // Arrange
            var viewModel = new CarListViewModel(null);
            viewModel.GetCarList().Wait();

            // Act
            var actualNumberOfCars = viewModel.Cars.Count;
            var expectedNumberOfCars = 8; // A changer en fonction du nombre de voitures

            // Assert
            Assert.Equal(expectedNumberOfCars, actualNumberOfCars);
        }

    }
}
