namespace CollegeDB_Repository_Project.Models
{
    /// <summary>Модель студента.</summary>
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? GroupId { get; set; }

        public Student()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        public override string ToString()
        {
            return string.Format("ID: {0}; {1} {2}; ID группы: {3}",
                StudentId, LastName, FirstName,
                GroupId.HasValue ? GroupId.Value.ToString() : "не назначена");
        }
    }
}
