using EasyGram.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EasyGram.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskResult> TaskResults { get; set; }
        public DbSet<TaskTest> TaskTests { get; set; }
        public DbSet<ExamTask> ExamTasks { get; set; }
        public DbSet<ExamTaskTest> ExamTaskTests { get; set; }


        public DbSet<Exam> Exams { get; set; }

        public DbSet<Material> Materials { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            builder.Entity<Topic>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200); // Конфигурация сущности Topic
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Order).IsRequired();
            });

            builder.Entity<Question>()
                .HasOne(q => q.Topic)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TopicId);

            builder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            builder.Entity<UserProgress>()
                .HasOne(up => up.Question)
                .WithMany(q => q.UserProgresses)
                .HasForeignKey(up => up.QuestionId);

            builder.Entity<ExamTask>()
            .HasOne(et => et.Exam)
            .WithMany(e => e.ExamTasks)
            .HasForeignKey(et => et.ExamId);

            builder.Entity<ExamResult>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);
        }
    }
}
