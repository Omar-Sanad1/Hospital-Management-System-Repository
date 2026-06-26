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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            //////////////////////////////////////////////////////////////////
            builder.HasOne(d => d.User)
                   .WithOne(d => d.Doctor)
                   .HasForeignKey<Doctor>(d => d.UserID)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
