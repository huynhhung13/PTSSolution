using System;
using System.Collections.Generic;

namespace PTS.Data.Models
{
    public partial class Status
    {
        public Status()
        {
            Subtasks = new HashSet<Subtask>();
            Tasks = new HashSet<Task>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Subtask> Subtasks { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
