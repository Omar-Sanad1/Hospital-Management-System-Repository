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
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            /////////////////////////////////////////////////////////////////////////
            builder.HasOne(p => p.Doctor)
                   .WithMany(p => p.Prescriptions)
                   .HasForeignKey(p => p.DoctorID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Patient)
                   .WithMany(p => p.Prescriptions)
                   .HasForeignKey(p => p.PatientID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
