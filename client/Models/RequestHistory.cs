using System;

namespace RequestFlowClient.Models
{
    public class RequestHistory
    {
        public long Id { get; set; }
        public Request Request { get; set; }
        public User ChangedBy { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public User OldAssignee { get; set; }
        public User NewAssignee { get; set; }
        public string Comment { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
