using MySqlConnector;
using TheElm.MySql;

namespace MySqlExtensions.Tests {
    public class Tests {
        [SetUp]
        public void Setup() {}
        
        [Test]
        public void Test1() {
            List<MySqlParameter[]> parameters = [];
            for ( int i = 0; i < 10; i++ ) {
                parameters.Add([
                    new MySqlParameter("one", "thing"),
                    new MySqlParameter("two", "thing"),
                    new MySqlParameter("three", "thing"),
                    new MySqlParameter("four", "thing")
                ]);
            }
            
            MySqlConnection connection = new();
            MySqlCommand command = connection.Insert("test_table", parameters);
            
            Console.WriteLine(command.CommandText);
        }
    }
}
