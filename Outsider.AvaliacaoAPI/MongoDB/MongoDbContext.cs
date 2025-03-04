using MongoDB.Driver;

namespace Outsider.AvaliacaoAPI.MongoDB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var mongoConnectionString = configuration["MongoDBConnection"];
            var client = new MongoClient(mongoConnectionString);
            _database = client.GetDatabase("MongoDBDatabase");
        }

        public IMongoCollection<AVALIACAODTC_NOSQL> Avaliacoes =>
            _database.GetCollection<AVALIACAODTC_NOSQL>("Avaliacoes");
    }
}
