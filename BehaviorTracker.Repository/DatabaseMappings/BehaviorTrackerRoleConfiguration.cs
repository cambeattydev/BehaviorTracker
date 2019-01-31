using BehaviorTracker.Repository.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviorTracker.Repository.DatabaseMappings
{
    public class BehaviorTrackerRoleConfiguration : IEntityTypeConfiguration<BehaviorTrackerRole>
    {
        public void Configure(EntityTypeBuilder<BehaviorTrackerRole> builder)
        {
            builder.HasKey(role => role.BehaviorTrackerRoleKey);
            builder.Property(role => role.RoleName).IsRequired();
        }
    }
}