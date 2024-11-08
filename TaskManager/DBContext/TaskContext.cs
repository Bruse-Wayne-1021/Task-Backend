using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskManager.Models;

namespace TaskManager.DBContext
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
            
        }

        public DbSet<TaskItems> Tasks { get; set; } 
        public DbSet<User> Users { get; set; }

      
        public DbSet<UserRegistration> UserRegistration { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                 .HasOne(o => o.Address)
                 .WithOne(o => o.user)
                 .HasForeignKey<Address>(o => o.Userid)
                  .OnDelete( DeleteBehavior.Cascade);



            modelBuilder.Entity<User>()
                .HasMany(o => o.TaskItems)
                .WithOne(A => A.Assigee)
                .HasForeignKey(L => L.AssigneeId);


            modelBuilder.Entity<TaskItems>()
                .HasMany(c => c.CheckList)
                .WithOne(t => t.TaskItems)
                .HasForeignKey(t => t.TaskId);

            //modelBuilder.Entity<UserRegistration>()
            //    .Property(p => p.Role)
            // .HasConversion(
            //    v => v.ToString(),
            //    v => (UserRegistration)Enum.Parse(typeof(), v));



            base.OnModelCreating(modelBuilder);
        }
    }
}
