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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            /////////////////////////////////////////////////////////////////
            builder.HasOne(p => p.Bill)
                   .WithMany(p => p.Payments)
                   .HasForeignKey(p => p.BillID)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
