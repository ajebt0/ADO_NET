namespace ADO_NET_Lesson08.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"ID: {Id}, Имя: {FirstName}, Фамилия: {LastName}";
    }
}
