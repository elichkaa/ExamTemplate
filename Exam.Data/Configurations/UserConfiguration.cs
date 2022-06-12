using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Data.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        private const string administratorUserId = "11e87c16-bc89-4393-a2b0-eb4e2debbd08";
        private const string administratorRoleId = "0bdba52e-e8ca-4481-84fe-ccd0142c33b6";
        private const string clientRoleId = "2bc2f661-4a73-4105-9f9f-93009a35ca26";

        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            IdentityUserRole<string> adminRole = new IdentityUserRole<string>
            {
                RoleId = administratorRoleId,
                UserId = administratorUserId
            };
            IdentityUserRole<string> clientRole = new IdentityUserRole<string>
            {
                RoleId = clientRoleId,
                UserId = administratorUserId
            };

            builder.HasData(new[] { adminRole, clientRole });
        }
    }
}
