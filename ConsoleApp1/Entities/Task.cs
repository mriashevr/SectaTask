using System;

namespace ConsoleApp1.Entities
{
    public class Task
    {

        public Task(string name, string description = null, DateTime deadline = default)
        {
            Name = name;
            Id = Guid.NewGuid();
            Complete = false;
            TaskInfo = new TaskInfo(deadline, description);
        }

        public Task AddParentTask(Task parentTask)
        {
            if (parentTask.Id != Id)
                parentTask.TaskInfo.SubTask.Add(this);
            return this;
        }

        public int GetAmountSubTasks()
        {
            var taskInfo = this.TaskInfo;
            return taskInfo != null ? taskInfo.SubTask.Count : 0;
        }

        public void TaskComplete()
        {
            this.Complete = true;
        }

        public void ChangeDeadLine(DateTime dateTime)
        {
            this.TaskInfo.Deadline = dateTime;
        }
        
        public string Name { get; set; }
        public TaskInfo TaskInfo { get; set; }
        public bool Complete { get; set; }
        public Guid Id { get; set; }
    }
}