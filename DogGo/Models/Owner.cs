using System.ComponentModel;

namespace DogGo.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [DisplayName("Neighborhood Name")]
        public string NieghborhoodName { get; set; }
        public int NeighborhoodId { get; set; }
        public Neighborhood neighborhood { get; set; }
        public string Phone { get; set; }
        public List<Dog> Dogs { get; set; }
    }
}
