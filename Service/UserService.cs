using MongoDB.Driver;
using CRUDOperation.Model;

namespace CRUDOperation.Service
{
    public class UserService 
    {
        private readonly IMongoCollection<User> _users;

        public UserService(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.CollectionName);

        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User user) =>
            _users.ReplaceOne(u => u.Id == id, user);

        public void Delete(string id) =>
            _users.DeleteOne(u => u.Id == id);
    }
}
