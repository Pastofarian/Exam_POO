using CarListApp.Maui.Models;
using SQLite;

namespace CarListApp.Maui.Services
{
    public class CarService
    {
        SQLiteConnection conn;
        string _dbPath;
        public string StatusMessage;
        int result = 0;
        public CarService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Car>();
        }

        public List<Car> GetCars()
        {
            try
            {
                Init();
                return conn.Table<Car>().ToList();
            }
            catch (Exception)
            {
                StatusMessage = "Echec de la récupération des données.";
            }

            return new List<Car>();
        }

        public Car GetCar(int id)
        {
            try
            {
                Init();
                return conn.Table<Car>().FirstOrDefault(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Echec de la récupération des données.";
            }

            return null;
        }

        public int DeleteCar(int id)
        {
            try
            {
                Init();
                return conn.Table<Car>().Delete(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Echec de la suppression des données.";
            }

            return 0;
        }

        public void AddCar(Car car)
        {
            try
            {
                Init();

                if (car == null)
                    throw new Exception("Enregistrement de la voiture invalide");

                result = conn.Insert(car);
                StatusMessage = result == 0 ? "Échec de l'insertion" : "Insertion réussie";
            }
            catch (Exception ex)
            {
                StatusMessage = "Echec de l'insertion des données.";
            }
        }

        public void UpdateCar(Car car)
        {
            try
            {
                Init();

                if (car == null)
                    throw new Exception("Enregistrement de la voiture invalide");

                result = conn.Update(car);
                StatusMessage = result == 0 ? "Échec de la mise à jour" : "Mise à jour réussie";
            }
            catch (Exception ex)
            {
                StatusMessage = "Echec de la mise à jour des données.";
            }
        }
    }
}
