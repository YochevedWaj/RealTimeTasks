using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using RealTimeTasks.Data;
using RealTimeTasks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeTasks.Web
{
    public class TasksHub : Hub
    {
        private string _connectionString;

        public TasksHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [Authorize]
        public void UserLogedIn()
        {
            var repo = new TasksRepository(_connectionString);
            Clients.Caller.SendAsync("RenderTasks", repo.GetAllIncompleted() );
        }
        [Authorize]
        public void AddTask(TaskItem task)
        {
            var repo = new TasksRepository(_connectionString);
            repo.AddTask(task);
            SendTasks();
        }

        [Authorize]
        public void DoTask(int taskID)
        {
            var accountRepo = new AccountRepository(_connectionString);
            var userID = accountRepo.GetUserId(Context.User.Identity.Name);
            var tasksRepo = new TasksRepository(_connectionString);
            tasksRepo.DoTask(userID, taskID);
            SendTasks();
        }

        [Authorize]
        public void CompleteTask(int taskID)
        {
            var tasksRepo = new TasksRepository(_connectionString);
            var accountRepo = new AccountRepository(_connectionString);
            var userID = accountRepo.GetUserId(Context.User.Identity.Name);
            if (!tasksRepo.IsMyTask(userID, taskID))
            {
                return;
            }
            tasksRepo.CompleteTask(taskID);
            SendTasks();
        }
        private void SendTasks()
        {
            var tasksRepo = new TasksRepository(_connectionString);
            Clients.All.SendAsync("RenderTasks", tasksRepo.GetAllIncompleted());
        }
    }
}
