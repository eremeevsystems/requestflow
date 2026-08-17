using System;

namespace RequestFlowClient.Models
{
    public class Request
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public User CreatedBy { get; set; }
        public User AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
