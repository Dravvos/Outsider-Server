using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Outsider.EnderecoAPI.Model.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var mongoConnectionString = configuration["MongoDBConnection"];
            var client = new MongoClient(mongoConnectionString);     
            
            _database = client.GetDatabase(configuration["MongoDBDatabase"]);
        }

        public IMongoCollection<ENDERECODTC_NOSQL> Enderecos =>
            _database.GetCollection<ENDERECODTC_NOSQL>("Endereco");
    }
}
