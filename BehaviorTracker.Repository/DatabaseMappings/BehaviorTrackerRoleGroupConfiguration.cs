using BehaviorTracker.Repository.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviorTracker.Repository.DatabaseMappings
{
    public class BehaviorTrackerRoleGroupConfiguration : IEntityTypeConfiguration<BehaviorTrackerRoleGroup>
    {
        public void Configure(EntityTypeBuilder<BehaviorTrackerRoleGroup> builder)
        {
            builder.HasKey(roleGroup => roleGroup.BehaviorTrackerRoleGroupKey);
            builder.Property(roleGroup => roleGroup.RoleGroupName).IsRequired();

            builder.HasMany(roleGroup => roleGroup.BehaviorTrackerRoleGroupRoles)
                .WithOne(roleGroupRole => roleGroupRole.BehaviorTrackerRoleGroup)
                .HasForeignKey(roleGroupRole => roleGroupRole.BehaviorTrackerRoleGroupKey);
        }
    }
}