using CarListApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Services
{
    public class CarServices
    {
        SQLiteConnection conn;
        string _dbPath;
        public string StatusMessage;

        public CarServices(string dbPath)
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
                StatusMessage = "Impossible de télécharger les données.";
            }
            return new List<Car>();

            //return new List<Car>()
            //{
            //    new Car
            //    {
            //        Id = 1, Make = "Honda", Model = "Fit", Vin = "123"
            //    },
            //    new Car
            //    {
            //        Id = 1, Make = "Toyota", Model = "Prado", Vin = "123"
            //    },
            //    new Car
            //    {
            //    Id = 1, Make = "Honda", Model = "Civic", Vin = "123"
            //    },
            //    new Car
            //    {
            //    Id = 1, Make = "Audi", Model = "A5", Vin = "123"
            //    },
            //    new Car
            //    {
            //    Id = 1, Make = "BMW", Model = "M3", Vin = "123"
            //    },
            //    new Car
            //    {
            //    Id = 1, Make = "Nissan", Model = "Note", Vin = "123"
            //    },
            //    new Car
            //    {
            //    Id = 1, Make = "Ferrari", Model = "spider", Vin = "123"
            //    },
            //};
        }
    }
}
