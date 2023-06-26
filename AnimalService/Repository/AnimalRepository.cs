using AnimalService.Context;
using AnimalService.Contracts;
using AnimalService.Models;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;

namespace AnimalService.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly DapperContext _context;

        public AnimalRepository(DapperContext context)
        { 
            _context=context;
        }

        public async Task<Animal> CreateAnimal(Animal animal)
        {
            var query = "INSERT INTO Animal (AnimalId,Name,Breed,BirthDate,Sex,Price,Status) OUTPUT INSERTED.AnimalId VALUES (NEWID(),@Name,@Breed,@BirthDate,@Sex,@Price,@Status) ";
            var parameters = new DynamicParameters();
            parameters.Add("Name", animal.Name);
            parameters.Add("Breed", animal.Breed);
            parameters.Add("BirthDate", animal.BirthDate);
            parameters.Add("Sex", animal.Sex);
            parameters.Add("Price", animal.Price);
            parameters.Add("Status", animal.Status);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdAnimal = new Animal
                {
                    AnimalId = id,
                    Name = animal.Name,
                    Breed = animal.Breed,
                    BirthDate = animal.BirthDate,
                    Sex = animal.Sex,
                    Price = animal.Price,
                    Status = animal.Status
                };
                return createdAnimal;
            }
        }

        public async Task DeleteAnimal(Guid id)
        {
            var query = "DELETE FROM Animal WHERE AnimalId = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Animal> GetAnimal(Guid id)
        {
            var query = "SELECT * FROM Animal WHERE AnimalId = @Id";
            using (var connection = _context.CreateConnection())
            {
                var animal = await connection.QuerySingleOrDefaultAsync<Animal>(query, new { id });
                return animal;
            }
        }

        public async Task<IEnumerable<Animal>> GetAnimals()
        {
            string query = "SELECT * FROM Animal";
            using (var connection=_context.CreateConnection())
            {
                var animals= await connection.QueryAsync<Animal>(query);
                return animals.ToList();
            }
        }

        public async Task<IEnumerable<Animal>> GetAnimalsFiltered(Guid? id, string? name, bool? sex, bool? status)
        {
            StringBuilder query = new StringBuilder("SELECT * FROM Animal WHERE ");
            var parameters = new DynamicParameters();
            if (id!=null)
            {
                query.Append("AnimalId = @AnimalId OR ");
                parameters.Add("AnimalId", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
            }
            if(name!=null)
            {
                query.Append("Name=@Name OR ");
                parameters.Add("Name", name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            }
            if(sex!=null) 
            {
                query.Append("Sex=@Sex OR ");
                parameters.Add("Sex", sex, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            }
            if(status!=null) 
            {
                query.Append("Status=@Status OR ");
                parameters.Add("Status", status, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            }
            query.Remove(query.Length - 3, 2);            
            using (var connection = _context.CreateConnection())
            {
                var animals = await connection.QueryAsync<Animal>(query.ToString(), parameters);
                return animals.ToList();
            }
        }

        public async Task UpdateAnimal(Guid id, Animal animal)
        {
            var query = "UPDATE Animal SET AnimalId=@AnimalId,Name=@Name,Breed=@Breed,BirthDate=@BirthDate,Sex=@Sex,Price=@Price,Status=@Status";
            var parameters = new DynamicParameters();
            parameters.Add("AnimalId", animal.AnimalId);
            parameters.Add("Name", animal.Name);
            parameters.Add("Breed", animal.Breed);
            parameters.Add("BirthDate", animal.BirthDate);
            parameters.Add("Sex", animal.Sex);
            parameters.Add("Price", animal.Price);
            parameters.Add("Status", animal.Status);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}