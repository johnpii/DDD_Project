namespace DDD_Project.API.ViewModels
{
    public class CreatePhoneViewModel
    {
        public required string Name { get; set; }
        public required string Year { get; set; }
        public required int CountryId { get; set; }
    }
}