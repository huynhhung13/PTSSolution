using System;
using System.Collections.Generic;

namespace PTS.Data.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Projects = new HashSet<Project>();
        }

        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? TelephoneNo { get; set; }
        public string? Company { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
