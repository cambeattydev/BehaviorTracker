using BehaviorTracker.Repository.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviorTracker.Repository.DatabaseMappings
{
    public class GoalAnswerConfiguration : IEntityTypeConfiguration<GoalAnswer>
    {
        public void Configure(EntityTypeBuilder<GoalAnswer> builder)
        {
            builder.HasKey(goalAnswer => goalAnswer.GoalAnswerKey);
            builder.Property(goalAnswer => goalAnswer.Answer).IsRequired();

            builder.HasOne(goalAnswer => goalAnswer.Goal)
                .WithMany(goal => goal.GoalAnswers)
                .HasForeignKey(goalAnswer => goalAnswer.GoalKey);
        }
    }
}