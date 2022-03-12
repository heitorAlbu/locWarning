using LocWarning.Data;
using LocWarning.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LocWarning.Service
{
    public class LocationService
    {
        public readonly IMongoCollection<Location> locations;

        public LocationService(IOptions<LocWarningDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            locations = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<Location>(options.Value.LocWarningColletionName);
        }


        public async Task<List<Location>> Get() =>
            await locations.Find(e => true).ToListAsync();

        public async Task<Location> Get(string id) =>
            await locations.Find(e => e.Id == id).FirstOrDefaultAsync();

        public async Task Create(Location location) =>
            await locations.InsertOneAsync(location);

        public async Task Update(string id, Location updateLocation) =>
            await locations.ReplaceOneAsync(e => e.Id == id, updateLocation);

        public async Task Remove(string id) =>
            await locations.DeleteOneAsync(e => e.Id == id);

    }
}
