namespace DDD_Project.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public ICollection<Phone> Phones { get; set; } = [];
    }
}