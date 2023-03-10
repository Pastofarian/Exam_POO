using CarListApp.Maui.Models;
using SQLite;
using System.Text;

namespace CarListApp.Maui.Services
{
    public class CarDatabaseService
    {
        SQLiteConnection conn;
        string _dbPath;
        public string StatusMessage;
        int result = 0;
        public CarDatabaseService(string dbPath)
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
                Init(); // Appelle la méthode "Init" pour init. la connexion à la db
                return conn.Table<Car>().ToList(); // Retourne une liste de toutes les voitures de la table "Car"
            }
            catch (Exception)
            {
                StatusMessage = "Echec de la récupérartion des données.";
            }

            return new List<Car>(); // Retourne une liste vide si n'a pas marché
        }


        public Car GetCar(int id)
        {
            try
            {
                Init();
                return conn.Table<Car>().FirstOrDefault(q => q.Id == id); //Retourne la première voiture avec l'id spécifié
            }
            catch (Exception)
            {
                StatusMessage = "Echec de la récupérartion des données.";
            }

            return null;
        }

        public int DeleteCar(int id)
        {
            try
            {
                Init();
                return conn.Table<Car>().Delete(q => q.Id == id); // Supprime la première voiture avec l'id spécifié
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
                    throw new Exception("Cette voiture n'existe pas."); // exception si la voiture est nulle

                result = conn.Insert(car); // Ajoute une nouvelle voiture dans la db
                StatusMessage = result == 0 ? "Echec de l'insertion" : "Insertion réussie";
            }
            catch (Exception)
            {
                StatusMessage = "Echec de l'ajout de données.";
            }
        }

        public void UpdateCar(Car car)
        {
            try
            {
                Init();

                if (car == null)
                    throw new Exception("Cette voiture n'existe pas.");

                result = conn.Update(car); // Met à jour la voiture dans la db
                StatusMessage = result == 0 ? "Echec de la mise à jour" : "Mise à jour réussie";
            }
            catch (Exception)
            {
                StatusMessage = "Echec de la mise à jour des données.";
            }
        }
    }
}