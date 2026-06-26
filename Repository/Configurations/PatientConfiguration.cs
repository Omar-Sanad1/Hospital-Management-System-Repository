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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            /////////////////////////////////////////////////////////////////
            builder.HasOne(p => p.User)
                   .WithOne(p => p.Patient)
                   .HasForeignKey<Patient>(p => p.UserID)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
