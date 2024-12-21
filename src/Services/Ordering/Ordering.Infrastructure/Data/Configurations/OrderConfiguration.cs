using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasConversion(
                    orderId => orderId.Value,
                    dbId => OrderId.Of(dbId)
                );

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany<OrderItem>()
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(
                o => o.OrderName, _ =>
                {
                    _.Property(n => n.Value)
                     .HasColumnName(nameof(Order.OrderName))
                     .HasMaxLength(100)
                     .IsRequired();
                }
            );

            builder.ComplexProperty(
                o => o.ShippingAddress, _ =>
                {
                    _.Property(a => a.FirstName)
                     .HasMaxLength(50)
                     .IsRequired();

                    _.Property(a => a.LastName)
                     .HasMaxLength(50)
                     .IsRequired();

                    _.Property(a => a.EmailAddress)
                     .HasMaxLength(50)
                     .IsRequired();

                    _.Property(a => a.AddressLine)
                     .HasMaxLength(180)
                     .IsRequired();

                    _.Property(a => a.Country)
                     .HasMaxLength(50);

                    _.Property(a => a.State)
                     .HasMaxLength(50);

                    _.Property(a => a.ZipCode)
                     .HasMaxLength(5)
                     .IsRequired();
                }
            );

            builder.ComplexProperty(
                o => o.BillingAddress, _ =>
                {
                    _.Property(a => a.FirstName)
                     .HasMaxLength(50)
                     .IsRequired();

                    _.Property(a => a.LastName)
                     .HasMaxLength(50)
                     .IsRequired();

                    _.Property(a => a.EmailAddress)
                     .HasMaxLength(50)
                     .IsRequired();

                    _.Property(a => a.AddressLine)
                     .HasMaxLength(180)
                     .IsRequired();

                    _.Property(a => a.Country)
                     .HasMaxLength(50);

                    _.Property(a => a.State)
                     .HasMaxLength(50);

                    _.Property(a => a.ZipCode)
                     .HasMaxLength(5)
                     .IsRequired();
                }
            );

            builder.ComplexProperty(
                o => o.Payment, _ =>
                {
                    _.Property(p => p.CardName)
                     .HasMaxLength(50);

                    _.Property(p => p.CardNumber)
                     .HasMaxLength(24)
                     .IsRequired();

                    _.Property(p => p.Expiration)
                     .HasMaxLength(10);

                    _.Property(p => p.CVV)
                     .HasMaxLength(3);

                    _.Property(p => p.PaymentMethod);
                }
            );

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus)
                );

            builder.Property(o => o.TotalPrice);
        }
    }
}
