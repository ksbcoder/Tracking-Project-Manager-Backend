using MongoDB.Driver;
using Users.Infrastructure.Entities;

namespace Users.Infrastructure.Interfaces
{
    public interface IContext
    {
        public IMongoCollection<UserMongo> Users { get; }
        public IMongoCollection<WorkTeamMongo> WorkTeams { get; }
    }
}
