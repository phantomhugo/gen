using Microsoft.Data.SqlClient;
using System.Data;

namespace AnimalService.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            if(_configuration != null && _configuration.GetConnectionString("SqlConnection") != null)
            {
                _connectionString = _configuration.GetConnectionString("SqlConnection");
            }
            if(_connectionString==null)
            {
                _connectionString = "";
            }
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
