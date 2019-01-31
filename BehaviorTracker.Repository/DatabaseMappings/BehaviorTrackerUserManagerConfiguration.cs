using BehaviorTracker.Repository.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviorTracker.Repository.DatabaseMappings
{
    public class BehaviorTrackerUserManagerConfiguration : IEntityTypeConfiguration<BehaviorTrackerUserManager>
    {
        public void Configure(EntityTypeBuilder<BehaviorTrackerUserManager> builder)
        {
            builder
                .HasOne(userManager => userManager.ManagerBehaviorTrackerUser)
                .WithMany(user => user.MangedBehaviorTrackerUsers)
                .HasForeignKey(userManager => userManager.ManagerBehaviorTrackerUserKey);

            builder
                .HasOne(userManager => userManager.BehaviorTrackerUser)
                .WithMany(user => user.BehaviorTrackerUserManagers)
                .HasForeignKey(userManager => userManager.BehaviorTrackerUserKey);

            builder.HasKey(userManager => userManager.BehaviorTrackerUserManagerKey);
        }
    }
}