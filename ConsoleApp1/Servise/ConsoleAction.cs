using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using ConsoleApp1.Entities;

namespace ConsoleApp1.Servise
{
    public class ConsoleAction : IAction
    {
        private TaskManager _taskManager;

        public ConsoleAction(TaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public void StartListening()
        {
            Console.WriteLine("Get started by writing command");
            var command = Console.ReadLine();
            while (!String.IsNullOrEmpty(command) && command != "/quit")
            {
                switch (command)
                {
                    case "/create-task":
                        CreateTask();
                        break;
                    case "/create-subtask":
                        AddSubTask();
                        break;
                    case "/delete-task":
                        Delete();
                        break;
                    case "/mark-task-as-completed":
                        CompleteTask();
                        break;
                    case "/show-all-completed-tasks":
                        ListCompleteTasks();
                        break;
                    case "/change-deadline":
                        ChangeDeadline();
                        break;
                    case "/show-all-tasks-for-today":
                        GetTodayTasks();
                        break;
                    case "/create-task-group":
                        CreateGroup();
                        break;
                    case "/delete-task-group":
                        DeleteGroup();
                        break;
                    case "/add-task-to-group":
                        AddTaskGroup();
                        break;
                    case "/delete-task-from-group":
                        DeleteTaskGroup();
                        break;
                    case "/show-all-groups-and-tasks":
                        GetListGroupTasks();
                        break;
                    case "/show-subtasks-of-task":
                        GetSubtasks();
                        break;
                    default:
                        Console.WriteLine("Error");
                        break;

                }

                command = Console.ReadLine();
            }
        }

        private Task CreateTask()
        {
            Console.WriteLine("Enter Task Name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the date in format YYYY-MM-DD \n Or leave blank for today");
            var date = Console.ReadLine();
            var dateResult = string.IsNullOrEmpty(date) ? default : DateTime.Parse(date);
            Console.WriteLine("Enter description (optional)");
            var desc = Console.ReadLine();
            var task = _taskManager.CreateTask(name, desc, dateResult);
            Console.WriteLine($"Task created {task.Id}");
            return task;
        }

        private Task FindNeededTask()
        {
            foreach (var item in _taskManager.ListAllTasks())
            {
                Console.WriteLine($"{item.Id} | {item.Name}");
            }
            
            Guid parse;
            parse = Guid.Parse(Console.ReadLine());
            Task task = null;
            foreach (var task1 in _taskManager.ListAllTasks().Where(task1 => task1.Id == parse))
            {
                task = task1;
            }

            return task;
        }
        
        private TaskGroup FindNeededGroup()
        {
            foreach (var item in _taskManager.ListAllTasksGroups())
            {
                Console.WriteLine($"{item.Id} | {item.Name}");
            }
            
            Guid parse;
            parse = Guid.Parse(Console.ReadLine());
            TaskGroup task = null;
            foreach (var task1 in _taskManager.ListAllTasksGroups().Where(task1 => task1.Id == parse))
            {
                task = task1;
            }

            return task;
        }

        private void AddSubTask()
        {
            var task = CreateTask();
            Console.WriteLine("Enter GUID of parent task");
            foreach (var item in _taskManager.ListAllTasks().Where(item => task.Id != item.Id))
            {
                Console.WriteLine($"{item.Id} | {item.Name}");
            }
            
            Guid parse;
            parse = Guid.Parse(Console.ReadLine());
            Task parenttask = null;
            foreach (var task1 in _taskManager.ListAllTasks().Where(task1 => task1.Id == parse))
            {
                parenttask = task1;
            }

            if (parenttask == null)
            {
                Console.WriteLine("Invalid Task");
            }
            else
            {
                task.AddParentTask(parenttask);
                Console.WriteLine($"{parenttask.Name} -> {task.Name}");
            }
            
        }

        private void Delete()
        {
            Console.WriteLine("Enter GUID of task that will be deleted");
            var deletedtask = FindNeededTask();

            _taskManager.DeleteTask(deletedtask);
        }

        private Task CompleteTask()
        {
            var completedtask = FindNeededTask();
            completedtask.TaskComplete();
            Console.WriteLine("This task has been marked as completed");
            return completedtask;
        }

        private void ListCompleteTasks()
        {
            var completedTasks = _taskManager.GetCompletedTasks();
            foreach (var task in completedTasks)
            {
                Console.WriteLine($"{task.Id} | {task.Name}");
            }
        }

        private void ChangeDeadline()
        {
            Console.WriteLine("Enter GUID of task to change its' deadline");
            var taskdeadline = FindNeededTask();
            Console.WriteLine("Enter new date in format YYYY-MM-DD");
            var date = Console.ReadLine();
            var dateResult = string.IsNullOrEmpty(date) ? default : DateTime.Parse(date);
            taskdeadline.ChangeDeadLine(dateResult);
            Console.WriteLine("The date has been changed");
        }

        private void GetTodayTasks()
        {
            var todaytask = _taskManager.GetTodayTasks();
            foreach (var task in todaytask)
            {
                Console.WriteLine($"{task.Id} | {task.Name}");
            }
        }

        private void CreateGroup()
        {
            Console.WriteLine("Enter Group id");
            var name = Console.ReadLine();
            var group = _taskManager.CreateGroup(name);
            Console.WriteLine($"Task created {group.Id}");
        }

        private void DeleteGroup()
        {
            Console.WriteLine("Enter Group id");
            var group = FindNeededGroup();
            _taskManager.DeleteTaskGroup(group);
            Console.WriteLine("Group has been deleted");
        }

        private void AddTaskGroup()
        {
            Console.WriteLine("Enter Group GUID");
            var group = FindNeededGroup();
            Console.WriteLine("Enter GUID of task");
            var task = FindNeededTask();
            group.AddTask(task);
            Console.WriteLine("Task was added to group");
        }
        
        private void DeleteTaskGroup()
        {
            Console.WriteLine("Enter Group GUID");
            var group = FindNeededGroup();
            Console.WriteLine("Enter GUID of task");
            var task = FindNeededTask();
            group.DeleteTask(task);
            Console.WriteLine("Task was deleted from group");
        }

        private void GetListGroupTasks()
        {
            var groups = _taskManager.ListAllTasksGroups();
            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Id} | {group.Name}");
                foreach (var task in group.GetAllTasks())
                {
                    Console.WriteLine($"{task.Id} | {task.Name}");
                }
            }
        }

        private void GetSubtasks()
        {
            Console.WriteLine("Enter GUID of task");
            var task = FindNeededTask();
            Console.WriteLine($"{task.Id} | {task.Name}");
            foreach (var sTask in task.TaskInfo.SubTask)
            {
                Console.WriteLine($"{sTask.Id} | {sTask.Name}");
            }
        }
    }
}