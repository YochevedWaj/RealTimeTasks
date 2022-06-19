using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealTimeTasks.Data
{
    public class TasksRepository
    {
        private readonly string _connectionString;

        public TasksRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<IncompletedTask> GetAllIncompleted()
        {
            var ctx = new TasksDataContext(_connectionString);
            return ctx.TaskItems
                .Include(t => t.User)
                .Where(t => !t.IsCompleted)
                .Select(t => new IncompletedTask
                {
                    ID = t.ID,
                    Title = t.Title,
                    UserID = t.UserID,
                    UserName = t.UserID.HasValue ? $"{t.User.FirstName} {t.User.LastName}" : null
                }).ToList();
        }
        public void AddTask(TaskItem task)
        {
            var ctx = new TasksDataContext(_connectionString);
            ctx.TaskItems.Add(task);
            ctx.SaveChanges();
        }

        public void DoTask(int userID, int taskID)
        {
            var ctx = new TasksDataContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"UPDATE TaskItems SET UserID = {userID} WHERE ID = {taskID}");
        }

        public void CompleteTask(int taskID)
        {
            var ctx = new TasksDataContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"UPDATE TaskItems SET IsCompleted = 1 WHERE ID = {taskID}");
        }

        public bool IsMyTask(int userID, int taskID)
        {
            var ctx = new TasksDataContext(_connectionString);
            var task = ctx.TaskItems.First(t => t.ID == taskID);
            return task.UserID == userID;
        }
    }

}
