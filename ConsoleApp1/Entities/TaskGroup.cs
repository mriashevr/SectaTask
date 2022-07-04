using System.Collections.Generic;

namespace ConsoleApp1.Entities
{
    public class TaskGroup
    {
        public TaskGroup(string name)
        {
            Name = name;
            Tasks = new List<Task>();
        }
        
        public string Name { get; }
        public List<Task> Tasks { get; set; }

    }
}