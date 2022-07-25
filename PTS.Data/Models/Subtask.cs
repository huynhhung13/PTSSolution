using System;
using System.Collections.Generic;

namespace PTS.Data.Models
{
    public partial class Subtask
    {
        public int SubtaskId { get; set; }
        public string? Name { get; set; }
        public int StatusId { get; set; }
        public byte? PercentCompleted { get; set; }
        public Guid TaskId { get; set; }
        public int? TeamMemberId { get; set; }

        public virtual Status? Status { get; set; }
        public virtual Task? Task { get; set; }
        public virtual Person? TeamMember { get; set; }
    }
}
