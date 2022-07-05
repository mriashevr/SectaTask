using System;
using System.Collections.Generic;

namespace ConsoleApp1.Entities
{
    public class TaskGroup
    {
        public TaskGroup(string name)
        {
            Name = name;
            Id = new Guid();
            Tasks = new List<Task>();
        }

        public void AddTask(Task task)
        {
            this.Tasks.Add(task);
            task.TaskInfo.TaskGroup = this;
            if (task.TaskInfo.SubTask != null)
            {
                foreach (var subTask in this.Tasks)
                {
                    this.Tasks.Add(subTask);
                    subTask.TaskInfo.TaskGroup = this;
                }
            }
        }

        public List<Task> GetAllTasks()
        {
            List<Task> tasks = null;
            foreach (var task in this.Tasks)
            {
                tasks.Add(task);
            }

            return tasks;
        }

        public void DeleteTask(Task task)
        {
            this.Tasks.Remove(task);
            if (task.TaskInfo.SubTask != null)
            {
                foreach (var subTask in this.Tasks)
                {
                    this.Tasks.Remove(subTask);
                    subTask.TaskInfo.TaskGroup = null;
                }
            }
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }

    }
}