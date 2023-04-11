using System.ComponentModel;

namespace DogGo.Models
{
    public class Neighborhood
    {
        public int Id { get; set; }
        [DisplayName("Neighborhood Name")]
        public string Name { get; set; }
    }
}