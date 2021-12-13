using Microsoft.EntityFrameworkCore;
using SignalRChatApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRChatApp.Data
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminEmail = "admin@gmail.com";
            string adminUsername = "admin";
            string adminPassword = "12345";

            string adminRoleName = "admin";
            string userRoleName = "user";

            Role adminRole = new Role() { Id = 1, Name = adminRoleName };
            Role userRole = new Role() { Id = 2, Name = userRoleName };

            User admin = new User() {Id=1,Email = adminEmail, Username = adminUsername, Password = adminPassword, RoleId = 1 };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { admin });
            base.OnModelCreating(modelBuilder);  
        }
    }
}
