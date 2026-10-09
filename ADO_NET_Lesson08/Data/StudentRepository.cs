using Microsoft.Data.SqlClient;
using ADO_NET_Lesson08.Models;

namespace ADO_NET_Lesson08.Data;

/// <summary>
/// Пример репозитория для чтения студентов. Названия столбцов
/// FirstName и LastName должны соответствовать вашей таблице Students.
/// </summary>
public class StudentRepository
{
    private readonly string _connectionString;

    public StudentRepository(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Строка подключения не должна быть пустой.", nameof(connectionString));

        _connectionString = connectionString;
    }

    public List<Student> GetAll()
    {
        var students = new List<Student>();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = "SELECT Id, FirstName, LastName FROM dbo.Students ORDER BY Id;";

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            students.Add(new Student
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName"))
            });
        }

        return students;
    }
}
