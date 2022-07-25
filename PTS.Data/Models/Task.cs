using System;
using System.Collections.Generic;

namespace PTS.Data.Models
{
    public partial class Task
    {
        public Task()
        {
            Subtasks = new HashSet<Subtask>();
            Predecessors = new HashSet<Task>();
            Tasks = new HashSet<Task>();
        }

        public Guid TaskId { get; set; }
        public string Name { get; set; } = null!;
        public string? ExpectedDateStarted { get; set; }
        public string? ExpectedDateCompleted { get; set; }
        public string? ActualDateStarted { get; set; }
        public string? ActualDateCompleted { get; set; }
        public Guid ProjectId { get; set; }
        public int TeamId { get; set; }
        public int StatusId { get; set; }
        public byte? PercentCompleted { get; set; }

        public virtual Project? Project { get; set; } = null!;
        public virtual Status? Status { get; set; } = null!;
        public virtual Team? Team { get; set; } = null!;
        public virtual ICollection<Subtask> Subtasks { get; set; }

        public virtual ICollection<Task> Predecessors { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
