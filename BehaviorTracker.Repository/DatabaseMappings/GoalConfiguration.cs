using BehaviorTracker.Repository.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviorTracker.Repository.DatabaseMappings
{
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.HasKey(goal => goal.GoalKey);
            builder.Property(goal => goal.GoalKey).ValueGeneratedOnAdd();

            builder.Property(goal => goal.GoalDescription).IsRequired();

            builder.HasOne(goal => goal.BehaviorTrackerUser)
                .WithMany(user => user.Goals)
                .HasForeignKey(goal => goal.BehaviorTrackerUserKey);

            builder.HasMany(goal => goal.GoalAnswers)
                .WithOne(goalAnswer => goalAnswer.Goal)
                .HasForeignKey(goalAnswer => goalAnswer.GoalKey);

            builder.HasMany(goal => goal.AvailableAnswers)
                .WithOne(availableAnswer => availableAnswer.Goal)
                .HasForeignKey(availableAnswer => availableAnswer.GoalKey);
        }
    }
}