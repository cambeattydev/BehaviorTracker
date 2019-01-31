using BehaviorTracker.Repository.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviorTracker.Repository.DatabaseMappings
{
    public class BehaviorTrackerRoleGroupRoleConfiguration : IEntityTypeConfiguration<BehaviorTrackerRoleGroupRole>
    {
        public void Configure(EntityTypeBuilder<BehaviorTrackerRoleGroupRole> builder)
        {
            builder.HasKey(roleGroupRole => roleGroupRole.BehaviorTrackerRoleGroupRoleKey);

            builder.HasOne(roleGroupRole => roleGroupRole.BehaviorTrackerRole)
                .WithMany(role => role.BehaviorTrackerRoleGroupRoles)
                .HasForeignKey(roleGroupRole => roleGroupRole.BehaviorTrackerRoleKey);

            builder.HasOne(roleGroupRole => roleGroupRole.BehaviorTrackerRoleGroup)
                .WithMany(roleGroup => roleGroup.BehaviorTrackerRoleGroupRoles)
                .HasForeignKey(roleGroupRole => roleGroupRole.BehaviorTrackerRoleGroupKey);
        }
    }
}