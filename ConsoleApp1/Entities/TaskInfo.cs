using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1.Entities
{
    public class TaskInfo
    {
        public TaskInfo(DateTime deadline=default, string description = null)
        {
            Description = string.IsNullOrEmpty(description) ? "Not set" : description;
            TaskGroup = null;
            Deadline = deadline == default ? DateTime.Today : deadline;
            SubTask = new List<Task>();
        }
        
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public List<Task> SubTask { get; set; }
        public TaskGroup TaskGroup { get; set; }
        
    }
}