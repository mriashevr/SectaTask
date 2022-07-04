

using System;
using ConsoleApp1.Entities;
using ConsoleApp1.Servise;
using NUnit.Framework;

namespace TaskManagerTest
{
    public class Tests
    {
        private TaskManager _taskManager;
        
        [SetUp]
        public void Setup()
        {
            _taskManager = new TaskManager();
            
        }

        [Test]
        public void TryToAddSubTask_CheckAmountOfSubTasks()
        {
            var mainTask = new Task("I'm super", "Parent Task");
            var childTask = new Task("I'm duper", "Child Task", DateTime.Today);
            childTask.AddParentTask(mainTask);
            Assert.AreEqual(mainTask.GetAmountSubTasks(), 1);
        }
        
        [Test]
        public void TryToDelete_CheckSubTaskDelete()
        {
            var mainTask = new Task("I'm super", "Parent Task");
            var childTask = new Task("I'm duper", "Child Task", DateTime.Today);
            childTask.AddParentTask(mainTask);

            _taskManager.DeleteTask(mainTask);
            
            Assert.False(_taskManager.ListAllTasks().Contains(childTask));
        }
        
        [Test]
        public void TryToDelete_CheckGroupTaskDelete()
        {
            var mainTask = _taskManager.CreateTask("I'm super", "Parent Task");
            var taskGroup = _taskManager.CreateGroup("cool tasks");
            taskGroup.AddTask(mainTask);

            _taskManager.DeleteTask(mainTask);
            
            Assert.False(taskGroup.Tasks.Contains(mainTask));
        }
    }
}