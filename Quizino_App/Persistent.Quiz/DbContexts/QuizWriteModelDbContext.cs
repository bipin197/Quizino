using Domain.Quiz.Interfaces;
using Domain.Quiz.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistent.Quiz.DbContexts
{
    public class QuizWriteModelDbContext : DbContext
    {

        public DbSet<QuizWriteModel> Quizes { get; set; }

        public QuizWriteModelDbContext(DbContextOptions<QuizWriteModelDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<QuizWriteModel>(entity =>
            {
                entity.ToTable("Quiz", "public");
                entity.Ignore(x => x.Id);
                entity.Ignore(x => x.IsNew);
                entity.Property(e => e.Id)
                        .HasColumnName("quiz_id").HasDefaultValueSql("nextval('quiz_quiz_id_seq'::regclass)")
                        .HasMaxLength(250);
                entity.HasKey(e => e.Id)
                        .HasName("quiz_id");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasMaxLength(250);
                entity.Property(e => e.Finish)
                    .HasColumnName("finish")
                    .HasMaxLength(250);
                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(250);
                entity.Property(e => e.QuesHash)
                    .HasColumnName("ques_hash")
                    .HasMaxLength(250);
                entity.Property(e => e.MaxTime)
                    .HasColumnName("max_time")
                    .HasMaxLength(250);
                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasMaxLength(250);
                entity.Property(e => e.UserCreated)
                    .HasColumnName("user_created")
                    .HasMaxLength(250);
                entity.Property(e => e.IsChallenge)
                    .HasColumnName("Is_Challenge")
                    .HasMaxLength(250);

            });
            base.OnModelCreating(modelBuilder);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
