using LiteDB;
using Vaelastrasz.Server.Services;

namespace Vaelastrasz.Server.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            using var db = new LiteDatabase("Filename=C:/Projects/github.com/sventhiel/Vaelastrasz/Vaelastrasz.Server/Database/Vaelastrasz.db;Connection=Shared");

            var userService = new UserService(new LiteDB.ConnectionString("Filename=C:/Projects/github.com/sventhiel/Vaelastrasz/Vaelastrasz.Server/Database/Vaelastrasz.db;Connection=Shared"));

            //    var users = db.GetCollection<User>("users");
            //var user = users.FindById(null);

            var user = userService.FindById(null);

            Assert.IsNotNull(user);
        }
    }
}