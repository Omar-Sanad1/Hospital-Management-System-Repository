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
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            //////////////////////////////////////////////////////////////////////////
            builder.HasOne(a => a.Patient)
                   .WithMany(a => a.Appointments)
                   .HasForeignKey(a => a.PatientID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor)
                   .WithMany(a => a.Appointments)
                   .HasForeignKey(a => a.DoctorID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
