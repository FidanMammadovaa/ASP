namespace minimalAPI.Entities
{
    public class Teacher
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public virtual Group Group { get; set; } = null!;
    }
}
