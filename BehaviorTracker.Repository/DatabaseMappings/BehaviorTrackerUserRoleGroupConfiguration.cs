using BehaviorTracker.Repository.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviorTracker.Repository.DatabaseMappings
{
    public class BehaviorTrackerUserRoleGroupConfiguration : IEntityTypeConfiguration<BehaviorTrackerUserRoleGroup>
    {
        public void Configure(EntityTypeBuilder<BehaviorTrackerUserRoleGroup> builder)
        {
            builder.HasKey(userRoleGroup => userRoleGroup.BehaviorTrackerUserRoleGroupKey);
            builder.Property(userRoleGroup => userRoleGroup.BehaviorTrackerUserRoleGroupKey).ValueGeneratedOnAdd();

            builder.HasOne(userRoleGroup => userRoleGroup.BehaviorTrackerUser)
                .WithOne(user => user.BehaviorTrackerUserRoleGroup);

            builder.HasOne(userRoleGroup => userRoleGroup.BehaviorTrackerRoleGroup)
                .WithMany(roleGroup => roleGroup.BehaviorTrackerUserRoleGroups)
                .HasForeignKey(roleGroup => roleGroup.BehaviorTrackerRoleGroupKey);
        }
    }
}