using CarListApp.Maui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarListApp.Maui.Services
{
    public class CarApiService
    {
        HttpClient _httpClient; // Client HTTP pour accéder à l'API
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8010" : "http://localhost:8010"; // URL de l'API
        public string StatusMessage;

        public CarApiService()
        {
            _httpClient = new() { BaseAddress = new Uri(BaseAddress) }; // nouvelle instance du client HTTP avec l'url de base
        }

        public async Task<List<Car>> GetCars()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("/cars"); // Envoie une query HTTP GET à l'API pour récupérer /cars
                return JsonConvert.DeserializeObject<List<Car>>(response); // Désérialise le retour JSON en une liste de voitures
            }
            catch (Exception)
            {
                StatusMessage = "Echec de la récupération des données.";
            }

            return null;
        }

        public async Task<Car> GetCar(int id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync("/cars/" + id); // query avec l'id spécifié
                return JsonConvert.DeserializeObject<Car>(response); // Désérialise le retour JSON en une voiture
            }
            catch (Exception)
            {
                StatusMessage = "Echec de la récupération des données.";
            }

            return null;
        }

        public async Task AddCar(Car car)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/cars/", car); // query Post pour une nouvelle voiture
                response.EnsureSuccessStatusCode();
                StatusMessage = "Insertion réussie";
            }
            catch (Exception)
            {
                StatusMessage = "Echec de l'ajout de données.";
            }
        }

        public async Task DeleteCar(int id)
        {
            try
            {

                var response = await _httpClient.DeleteAsync("/cars/" + id); // query delete avec id spécifique
                response.EnsureSuccessStatusCode();
                StatusMessage = "Suppression réussie";
            }
            catch (Exception)
            {
                StatusMessage = "Echec de la suppression des données.";
            }
        }

        public async Task UpdateCar(int id, Car car)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/cars/" + id, car); // query updte avec id spécifique
                response.EnsureSuccessStatusCode();
                StatusMessage = "Mise à jour réussie";
            }
            catch (Exception)
            {
                StatusMessage = "Echec de la mise à jour des données.";
            }
        }
    }
}