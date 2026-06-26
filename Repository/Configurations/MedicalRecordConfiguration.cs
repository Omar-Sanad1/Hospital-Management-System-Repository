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
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            ///////////////////////////////////////////////////////////////
            builder.HasOne(m => m.Doctor)
                   .WithMany(m => m.MedicalRecords)
                   .HasForeignKey(m => m.DoctorID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Patient)
                   .WithMany(m => m.MedicalRecords)
                   .HasForeignKey(m => m.PatientID)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
