namespace minimalAPI.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public long Year { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
