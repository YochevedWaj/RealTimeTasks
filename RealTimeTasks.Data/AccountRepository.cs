﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTasks.Data
{
    public class AccountRepository
    {
        private string _connectionString;
        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var ctx = new TasksDataContext(_connectionString);
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValidPassword)
            {
                return null;
            }

            return user;

        }
        public User GetByEmail(string email)
        {
            using var ctx = new TasksDataContext(_connectionString);
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }

        public int GetUserId(string email)
        {
            using var ctx = new TasksDataContext(_connectionString);
            return ctx.Users.Where(u => u.Email == email).Select(u => u.ID).FirstOrDefault();
        }

    }

    
}
