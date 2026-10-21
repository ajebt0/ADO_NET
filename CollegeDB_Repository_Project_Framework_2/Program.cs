using System;
using System.Data.SqlClient;
using CollegeDB_Repository_Project.Data;
using CollegeDB_Repository_Project.Models;

namespace CollegeDB_Repository_Project
{
    internal static class Program
    {
        private static void Main()
        {
            const string connectionString =
                @"Server=(localdb)\MSSQLLocalDB;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;";

            var studentRepository = new StudentRepository(connectionString);

            Console.WriteLine("Добавление студента в CollegeDB");
            Console.Write("Имя: ");
            string firstName = Console.ReadLine();
            Console.Write("Фамилия: ");
            string lastName = Console.ReadLine();
            Console.Write("ID группы (Enter, если не назначена): ");
            string groupText = Console.ReadLine();

            int? groupId = null;
            if (!string.IsNullOrWhiteSpace(groupText))
            {
                int parsedGroupId;
                if (!int.TryParse(groupText, out parsedGroupId) || parsedGroupId <= 0)
                {
                    Console.WriteLine("ID группы должен быть положительным целым числом.");
                    return;
                }
                groupId = parsedGroupId;
            }

            // Создаём и заполняем модель Student, затем передаём её в CreateStudent(Student).
            var student = new Student
            {
                FirstName = firstName == null ? string.Empty : firstName.Trim(),
                LastName = lastName == null ? string.Empty : lastName.Trim(),
                GroupId = groupId
            };

            try
            {
                Student createdStudent = studentRepository.CreateStudent(student);
                Console.WriteLine("Студент успешно добавлен:");
                Console.WriteLine(createdStudent);
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine("Ошибка SQL Server. Проверьте подключение, таблицу Students и ID группы.");
                Console.Error.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Ошибка: " + ex.Message);
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
