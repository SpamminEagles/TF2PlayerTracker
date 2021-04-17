using MongoDB.Driver;
using System.Security;

namespace PlayerTracker.AppServer.Services
{
    public class MongoDbContext
    {
        public MongoClient Client { get; private set; }
        public IMongoDatabase PluginDb { get; set; }
        public string DbName { get; }

        private string ConnectionString;

        public MongoDbContext(SecureString connString, string dbanme)
        {
            this.ConnectionString = connString.ToString();
            this.DbName = dbanme;
        }

        public MongoDbContext(string connString, string dbanme)
        {
            this.ConnectionString = connString;
            this.DbName = dbanme;
        }

        public void Open()
        {
            Client = new MongoClient(ConnectionString.ToString());
            ConnectionString = null; // ereasing the secrets if not necessary

            this.PluginDb = Client.GetDatabase(DbName);
        }

    }
}
