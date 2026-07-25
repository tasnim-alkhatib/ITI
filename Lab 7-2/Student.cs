using System.Text;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public StringBuilder Notes { get; set; }
    public Student(int id, string name, StringBuilder notes)
    {
        Id = id;
        Name = name;
        Notes = notes;
    }
    public Student DeepCopy() => new Student(this.Id, this.Name, new StringBuilder(this.Notes.ToString()));

}