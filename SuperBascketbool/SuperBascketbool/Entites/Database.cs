using System.Data;
using MySql.Data.MySqlClient;

namespace SuperBascketbool.Entites;

public class Database
{
    private MySqlConnection _connection =
        new MySqlConnection(@"server=localhost, database=pro1_15;port=3306;User Id=user01;password=user_01pro");

    public void OpenConnection()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }
    }
    
    public void CloseConnection()
    {
        if (_connection.State == ConnectionState.Open)
        {
            _connection.Close();
        }
    }

    public MySqlConnection GetConnection()
    {
        return _connection;
    }
}