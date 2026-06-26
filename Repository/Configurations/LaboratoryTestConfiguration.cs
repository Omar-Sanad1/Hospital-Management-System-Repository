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
    public class LaboratoryTestConfiguration : IEntityTypeConfiguration<LaboratoryTest>
    {
        public void Configure(EntityTypeBuilder<LaboratoryTest> builder)
        {
            //////////////////////////////////////////////////////////////////////////////////
            builder.HasOne(l => l.Doctor)
                   .WithMany(l => l.LaboratoryTests)
                   .HasForeignKey(l => l.DoctorID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Patient)
                   .WithMany(l => l.LaboratoryTests)
                   .HasForeignKey(l => l.PatientID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
