using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using System;

namespace WebAPI.DatabaseContext

{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
