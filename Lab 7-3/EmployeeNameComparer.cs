public class EmployeeNameComparer : IComparer<Employee>
{
    public int Compare(Employee? emp1, Employee? emp2)
    {
        return String.Compare(emp1?.Name, emp2?.Name);
    }
}