using SQLite;

namespace CarListApp.Maui.Models
{
    public abstract class BaseEntity
    {
        [PrimaryKey, AutoIncrement] //SQLite
        public int Id
        {
            get; set;
        }
    }
}
