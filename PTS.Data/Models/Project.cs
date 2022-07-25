using System;
using System.Collections.Generic;

namespace PTS.Data.Models
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public Guid ProjectId { get; set; }
        public string Name { get; set; } = null!;
        public string? ExpectedStartDate { get; set; }
        public string? ExpectedEndDate { get; set; }
        public string? ActualStartDate { get; set; }
        public string? ActualEndDate { get; set; }
        public bool Completed { get; set; }
        public int CustomerId { get; set; }
        public int AdministratorId { get; set; }

        public virtual Person? Administrator { get; set; } = null!;
        public virtual Customer? Customer { get; set; } = null!;
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
