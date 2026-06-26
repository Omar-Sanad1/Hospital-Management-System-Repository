using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            ///////////////////////////////////////////////////////////////////////
            builder.HasMany(d => d.Doctors)
                   .WithOne(d => d.Department)
                   .HasForeignKey(d => d.DepartmentID)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
