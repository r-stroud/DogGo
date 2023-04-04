namespace DogGo.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int NieghborhoodId { get; set; }
        public Neighborhood neighborhood { get; set; }
        public string Phone { get; set; }
    }
}
