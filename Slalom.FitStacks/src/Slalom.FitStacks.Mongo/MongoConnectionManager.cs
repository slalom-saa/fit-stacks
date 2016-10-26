using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Slalom.FitStacks.Mongo
{
    public class MongoConnectionManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoConnectionManager" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="mappings">The mappings.</param>
        public MongoConnectionManager(IConfiguration configuration, MongoMappingsManager mappings)
        {
            this._configuration = configuration;

            mappings.EnsureInitialized();
        }

        private IConfiguration _configuration;

        /// <summary>
        /// Gets the collection with the specified database and name.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="collection">The collection name.</param>
        /// <param name="connection">The connection string.</param>
        /// <returns>Returns the collection with the specified database and name.</returns>
        public IMongoCollection<T> GetCollection<T>(string collection, string connection = null)
        {
            return this.GetDatabase(connection).GetCollection<T>(collection);
        }

        /// <summary>
        /// Gets the database with the specified name.
        /// </summary>
        /// <param name="connection">The database name.</param>
        /// <returns>Returns the database with the specified name.</returns>
        public IMongoDatabase GetDatabase(string connection)
        {
            var client = !string.IsNullOrWhiteSpace(this._configuration["Mongo:Connection"]) ? new MongoClient(this._configuration["Mongo:Connection"])
                             : new MongoClient();

            return client.GetDatabase(connection ?? "local");
        }
    }
}