using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts
{
    public class QuestionDbContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }

        public QuestionDbContext(DbContextOptions<QuestionDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("question", "public");
                entity.Ignore(x => x.Id);
                entity.Ignore(x => x.IsNew);
                entity.Property(e => e.QuestionId)
                        .HasColumnName("ques_id").HasDefaultValueSql("nextval('question_ques_id_seq'::regclass)")
                        .HasMaxLength(250);
                entity.HasKey(e => e.QuestionId)
                        .HasName("ques_id");

                entity.Property(e => e.Text)
                    .HasColumnName("ques_text")
                    .HasMaxLength(250);

                entity.Property(e => e.OptionA)
                    .HasColumnName("ques_optiona")
                    .HasMaxLength(50);

                entity.Property(e => e.OptionB)
                    .HasColumnName("ques_optionb")
                    .HasMaxLength(50);

                entity.Property(e => e.OptionC)
                     .HasColumnName("ques_optionc")
                     .HasMaxLength(50);

                entity.Property(e => e.OptionD)
                     .HasColumnName("ques_optiond")
                     .HasMaxLength(50);

                entity.Property(e => e.ApplicableCategories)
                       .HasColumnName("ques_applicable_cat")
                       .HasMaxLength(250);
                entity.Property(e => e.Answer)
                       .HasColumnName("ques_answer");

                entity.Property(e => e.HashCode)
                       .HasColumnName("ques_hash")
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
