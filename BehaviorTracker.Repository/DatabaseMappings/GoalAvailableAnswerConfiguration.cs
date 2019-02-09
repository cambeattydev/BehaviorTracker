using BehaviorTracker.Repository.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviorTracker.Repository.DatabaseMappings
{
    public class GoalAvailableAnswerConfiguration : IEntityTypeConfiguration<GoalAvailableAnswer>
    {
        public void Configure(EntityTypeBuilder<GoalAvailableAnswer> builder)
        {
            builder.HasKey(goalAvailableAnswer => goalAvailableAnswer.GoalAvailableAnswerKey);
            builder.Property(goalAvailableAnswer => goalAvailableAnswer.GoalAvailableAnswerKey).ValueGeneratedOnAdd();

            builder.Property(goalAvailableAnswer => goalAvailableAnswer.OptionValue).IsRequired();

            builder.HasOne(goalAvailableAnswer => goalAvailableAnswer.Goal)
                .WithMany(goal => goal.AvailableAnswers)
                .HasForeignKey(goalAvailableAnswer => goalAvailableAnswer.GoalKey);

            builder.HasIndex(goalAvailableAnswer => goalAvailableAnswer.GoalKey);
        }
    }
}