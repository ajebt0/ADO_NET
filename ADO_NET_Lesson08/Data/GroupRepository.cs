using Microsoft.Data.SqlClient;
using ADO_NET_Lesson08.Models;

namespace ADO_NET_Lesson08.Data;

/// <summary>
/// Репозиторий для чтения, добавления и удаления групп в базе CollegeDB.
/// </summary>
public class GroupRepository
{
    private readonly string _connectionString;

    public GroupRepository(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Строка подключения не должна быть пустой.", nameof(connectionString));

        _connectionString = connectionString;
    }

    public List<Group> GetAll()
    {
        var groups = new List<Group>();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = "SELECT Id, Name FROM dbo.Groups ORDER BY Id;";

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            groups.Add(new Group
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name"))
            });
        }

        return groups;
    }

    public void Add(Group group)
    {
        ArgumentNullException.ThrowIfNull(group);

        if (string.IsNullOrWhiteSpace(group.Name))
            throw new ArgumentException("Название группы не должно быть пустым.", nameof(group));

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = "INSERT INTO dbo.Groups (Name) VALUES (@Name);";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", group.Name.Trim());
        command.ExecuteNonQuery();
    }

    public bool Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "ID должен быть положительным числом.");

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string query = "DELETE FROM dbo.Groups WHERE Id = @Id;";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        return command.ExecuteNonQuery() > 0;
    }
}
