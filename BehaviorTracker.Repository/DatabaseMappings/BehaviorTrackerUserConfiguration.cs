using BehaviorTracker.Repository.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviorTracker.Repository.DatabaseMappings
{
    public class BehaviorTrackerUserConfiguration : IEntityTypeConfiguration<BehaviorTrackerUser>
    {
        public void Configure(EntityTypeBuilder<BehaviorTrackerUser> builder)
        {
            builder.HasKey(user => user.BehaviorTrackerUserKey);
            builder.Property(user => user.BehaviorTrackerUserKey).ValueGeneratedOnAdd();
            builder.Property(user => user.FirstName).IsRequired();
            builder.Property(user => user.LastName).IsRequired();
            builder.Property(user => user.Email).IsRequired();

            builder.HasOne(user => user.BehaviorTrackerUserRoleGroup)
                .WithOne(userRoleGroup => userRoleGroup.BehaviorTrackerUser)
                .HasForeignKey<BehaviorTrackerUserRoleGroup>(userRoleGroup => userRoleGroup.BehaviorTrackerUserKey);

            builder.HasMany(user => user.BehaviorTrackerUserManagers)
                .WithOne(userManager => userManager.BehaviorTrackerUser)
                .HasForeignKey(user => user.BehaviorTrackerUserKey);

            builder.HasMany(user => user.MangedBehaviorTrackerUsers)
                .WithOne(userManager => userManager.ManagerBehaviorTrackerUser)
                .HasForeignKey(user => user.BehaviorTrackerUserKey);

            builder.HasMany(user => user.Goals)
                .WithOne(goal => goal.BehaviorTrackerUser)
                .HasForeignKey(goal => goal.BehaviorTrackerUserKey);


            builder.HasIndex(user => user.Email);
        }
    }
}