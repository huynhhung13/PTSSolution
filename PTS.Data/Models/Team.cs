using System;
using System.Collections.Generic;

namespace PTS.Data.Models
{
    public partial class Team
    {
        public Team()
        {
            Tasks = new HashSet<Task>();
            Users = new HashSet<Person>();
        }

        public int TeamId { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int? TeamLeaderId { get; set; }
        public bool IsExternal { get; set; }

        public virtual Person? TeamLeader { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

        public virtual ICollection<Person> Users { get; set; }
    }
}
