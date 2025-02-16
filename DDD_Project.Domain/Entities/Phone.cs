namespace DDD_Project.Domain.Entities
{
    public class Phone
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Year { get; set; }

        public required int CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
