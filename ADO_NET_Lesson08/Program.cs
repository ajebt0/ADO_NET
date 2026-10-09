using System;
using System.Text;
using Microsoft.Data.SqlClient;

internal class Program
{
    private const string ConnectionString =
    @"Server=COMP7A2\SQLEXPRESS01;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

```
private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Работа с группами ===");
            Console.WriteLine("1. Показать все группы");
            Console.WriteLine("2. Добавить группу");
            Console.WriteLine("3. Удалить группу");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowGroups();
                    break;

                case "2":
                    AddGroup();
                    break;

                case "3":
                    DeleteGroup();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный пункт меню.");
                    break;
            }
        }
    }

    private static void ShowGroups()
    {
        try
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            const string sql = "SELECT Id, Name FROM dbo.Groups ORDER BY Id";

            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine();
            Console.WriteLine("Список групп:");

            bool hasGroups = false;

            while (reader.Read())
            {
                hasGroups = true;
                Console.WriteLine($"ID: {reader["Id"]}, Название: {reader["Name"]}");
            }

            if (!hasGroups)
            {
                Console.WriteLine("Групп пока нет.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка базы данных: {ex.Message}");

            if (ex.Message.Contains("Invalid object name", StringComparison.OrdinalIgnoreCase) ||
                ex.Message.Contains("Недопустимое имя объекта", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(
                    "Таблица dbo.Groups не найдена в CollegeDB. " +
                    "Проверь, что таблица создана именно в этой базе данных.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void AddGroup()
    {
        Console.Write("Введите название группы: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Название не может быть пустым.");
            return;
        }

        try
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            const string sql = "INSERT INTO dbo.Groups (Name) VALUES (@Name)";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", name.Trim());

            command.ExecuteNonQuery();

            Console.WriteLine("Группа успешно добавлена.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка базы данных: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void DeleteGroup()
    {
        Console.Write("Введите ID группы для удаления: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int id) || id <= 0)
        {
            Console.WriteLine("Введите корректный положительный ID.");
            return;
        }

        try
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            const string sql = "DELETE FROM dbo.Groups WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Группа успешно удалена.");
            }
            else
            {
                Console.WriteLine("Группа с таким ID не найдена.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка базы данных: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
```

}
