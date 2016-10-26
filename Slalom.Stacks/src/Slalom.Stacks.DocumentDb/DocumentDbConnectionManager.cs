using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Slalom.Stacks.DocumentDb
{
    public class DocumentDbConnectionManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentDbConnectionManager" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="mappings">The mappings.</param>
        public DocumentDbConnectionManager(IConfiguration configuration, DocumentDbMappingsManager mappings, DocumentDbOptions options)
        {
            _configuration = configuration;
            _options = options;

            mappings.EnsureInitialized();
        }

        private IConfiguration _configuration;
        private readonly DocumentDbOptions _options;

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
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(_options.Host, _options.Port),
                UseSsl = true,
                SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 }
            };

            var identity = new MongoInternalIdentity(_options.Database, _options.UserName);
            var evidence = new PasswordEvidence(_options.Password);

            settings.Credentials = new List<MongoCredential>
            {
                new MongoCredential("SCRAM-SHA-1", identity, evidence)
            };
            var client = new MongoClient(settings);
            return client.GetDatabase(_options.Database);

            //MongoClientSettings settings = new MongoClientSettings();
            //settings.Server = new MongoServerAddress("patolus-dev.documents.azure.com", 10250);
            //settings.UseSsl = true;
            //settings.SslSettings = new SslSettings();
            //settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            //MongoIdentity identity = new MongoInternalIdentity("treatment", "patolus-dev");
            //MongoIdentityEvidence evidence = new PasswordEvidence("dyBrKINSHcrRcu7bYr4vtfHSkCV0THi6qcWxeMb65oHvOo2xZqyPdtVzrgDuWqvp3Tl5vrhVQNc1geTF566t4g==");

            //settings.Credentials = new List<MongoCredential>()
            //{
            //    new MongoCredential("SCRAM-SHA-1", identity, evidence)
            //};
            //MongoClient client = new MongoClient(settings);
            //var database = client.GetDatabase("treatment");

            //return database;
        }
    }
}
