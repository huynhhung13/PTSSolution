using System;
using System.Collections.Generic;

namespace PTS.Data.Models
{
    public partial class Person
    {
        public Person()
        {
            Projects = new HashSet<Project>();
            Subtasks = new HashSet<Subtask>();
            Teams = new HashSet<Team>();
            TeamsNavigation = new HashSet<Team>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? TelephoneNo { get; set; }
        public bool IsAdministrator { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Subtask> Subtasks { get; set; }
        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Team> TeamsNavigation { get; set; }
    }
}
