using MongoDB.Driver;
using Users.Infrastructure.Entities;
using Users.Infrastructure.Interfaces;

namespace Users.Infrastructure
{
    public class Context : IContext
    {
        private readonly IMongoDatabase _database;

        public Context(string stringConnection, string dbName)
        {
            MongoClient cliente = new(stringConnection);
            _database = cliente.GetDatabase(dbName);
        }

        public IMongoCollection<UserMongo> Users => _database.GetCollection<UserMongo>("Users");

        public IMongoCollection<WorkTeamMongo> WorkTeams => _database.GetCollection<WorkTeamMongo>("WorkTeams");
    }
}
