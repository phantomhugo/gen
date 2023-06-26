namespace AnimalService.Models
{
    public class Animal
    {
        public Guid AnimalId { get; set; }
        public required string Name { get; set; }
        public required string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Sex { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}
