using System;

namespace minimalAPI.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public decimal Salary { get; set; }

        public string Position { get; set; } = null!;

        public int PersonId { get; set; }

        public virtual Person Person { get; set; } = null!;

        public virtual Teacher? Teacher { get; set; }
    }
}
