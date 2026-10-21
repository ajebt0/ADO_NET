using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using CollegeDB_Repository_Project.Models;

namespace CollegeDB_Repository_Project.Data
{
    /// <summary>Репозиторий для работы со студентами через Dapper.</summary>
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Строка подключения не может быть пустой.", nameof(connectionString));
            _connectionString = connectionString;
        }

        public List<Student> GetAll()
        {
            const string sql = @"
SELECT StudentId, FirstName, LastName, GroupId
FROM dbo.Students
ORDER BY StudentId;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
                return connection.Query<Student>(sql).AsList();
        }

        public Student GetById(int id)
        {
            const string sql = @"
SELECT StudentId, FirstName, LastName, GroupId
FROM dbo.Students
WHERE StudentId = @StudentId;";

            using (IDbConnection connection = new SqlConnection(_connectionString))
                return connection.QuerySingleOrDefault<Student>(sql, new { StudentId = id });
        }

        /// <summary>
        /// Создаёт студента и записывает сгенерированный базой StudentId в модель.
        /// </summary>
        public Student CreateStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (string.IsNullOrWhiteSpace(student.FirstName))
                throw new ArgumentException("Имя студента обязательно.", nameof(student));
            if (string.IsNullOrWhiteSpace(student.LastName))
                throw new ArgumentException("Фамилия студента обязательна.", nameof(student));

            const string sql = @"
INSERT INTO dbo.Students (FirstName, LastName, GroupId)
VALUES (@FirstName, @LastName, @GroupId);
SELECT CAST(SCOPE_IDENTITY() AS int);";

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                student.StudentId = connection.QuerySingle<int>(sql, student);
            }

            return student;
        }
    }
}
