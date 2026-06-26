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
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            //////////////////////////////////////////////////////////////////
            builder.HasOne(b => b.Patient)
                   .WithMany(b => b.Bills)
                   .HasForeignKey(b => b.PatientID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
