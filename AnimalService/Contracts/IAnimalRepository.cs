using AnimalService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimalService.Contracts
{
    public interface IAnimalRepository
    {        
        public Task<Animal> CreateAnimal(Animal animal);
        public Task DeleteAnimal(Guid id);
        public Task<IEnumerable<Animal>> GetAnimals();
        public Task<IEnumerable<Animal>> GetAnimalsFiltered(Guid? id, string? name, bool? sex, bool? status);
        public Task<Animal> GetAnimal(Guid id);

        public Task UpdateAnimal(Guid id, Animal animal);
        
    }
}
