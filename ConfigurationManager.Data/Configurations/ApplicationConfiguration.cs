using ConfigurationManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ConfigurationManager.Data.Configurations
{
    public class ApplicationConfiguration: IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder
                .ToTable("Application");
        }
    }
}
