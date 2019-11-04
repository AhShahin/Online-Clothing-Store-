using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Item_details> Item_Details { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
            .Entity<User>()
            .Property(e => e.Gender)
            .HasConversion(
            v => v.ToString(),
            v => (Gender)Enum.Parse(typeof(Gender), v));

            builder
            .Entity<Item_details>()
            .Property(e => e.Size)
            .HasConversion(
            v => v.ToString(),
            v => (Size)Enum.Parse(typeof(Size), v));

            builder.Entity<OrderItem>(orderItem =>
            {
                orderItem.HasKey(oi => new { oi.OrderId, oi.ItemId });

                orderItem.HasOne(oi => oi.Item)
                    .WithMany(i => i.OrderItems)
                    .HasForeignKey(oi => oi.ItemId)
                    .IsRequired();

                orderItem.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .IsRequired();
            });

            builder.Entity<UserItem>(UserItem =>
            {
                UserItem.HasKey(ui => new { ui.UserId, ui.ItemId });

                UserItem.HasOne(ui => ui.Item)
                    .WithMany(i => i.UserItems)
                    .HasForeignKey(ui => ui.ItemId)
                    .IsRequired();

                UserItem.HasOne(ui => ui.User)
                    .WithMany(u => u.UserItems)
                    .HasForeignKey(ui => ui.UserId)
                    .IsRequired();
            });
        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(
                    entity,
                    validationContext,
                    validateAllProperties: true);
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(
                    entity,
                    validationContext,
                    validateAllProperties: true);
            }

            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }
}
