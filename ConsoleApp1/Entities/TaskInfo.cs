using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1.Entities
{
    public class TaskInfo
    {
        public TaskInfo()
        {
            Discription = null;
            Deadline = DateTime.Today;
            SubTask = new List<Task>();
        }
        
        public string Discription { get; set; }
        public DateTime Deadline { get; set; }
        public List<Task> SubTask { get; set; }
        
    }
}