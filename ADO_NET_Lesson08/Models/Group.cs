namespace ADO_NET_Lesson08.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"ID: {Id}, Название группы: {Name}";
    }
}
