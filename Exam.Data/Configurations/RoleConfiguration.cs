﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        private const string administratorRoleId = "0bdba52e-e8ca-4481-84fe-ccd0142c33b6";
        private const string techRoleId = "9bf1gh11-joa8-24n3-343x-akdi20fjs932";
        private const string customerRoleId = "2bc2f661-4a73-4105-9f9f-93009a35ca26";

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = administratorRoleId,
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }, 
                new IdentityRole
                {
                    Id = techRoleId,
                    Name = "Tech",
                    NormalizedName = "TECH"
                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
            );
        }
    }
}
