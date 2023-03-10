using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.Models
{
    [Table("cars")] //SQLite
    public class Car : BaseEntity
    {
        public string Make
        {
            get; set;
        } //Marque
        public string Model
        {
            get; set;
        }

        [MaxLength(12)]
        [Unique]
        public string Vin //Vehicule Identification Number
        {
            get; set;
        }
    }
}
