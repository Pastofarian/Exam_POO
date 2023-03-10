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

            Assert.NotEmpty(viewModel.Cars); //Echec si Empty
        }
        [Fact]
        public void TestNumberOfCars()
        {
            var viewModel = new CarListViewModel(null);
            viewModel.GetCarList().Wait();

            var actualNumberOfCars = viewModel.Cars.Count;
            var expectedNumberOfCars = 8; // A changer en fonction du nombre de voitures

            Assert.Equal(expectedNumberOfCars, actualNumberOfCars);
        }

    }
}
