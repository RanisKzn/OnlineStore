using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.DataSeeders
{
    public static class DataSeeder
    {
        public static ModelBuilder SeedData(ModelBuilder modelBuilder)
        {
            var managerRole = new Role { Id = new Guid("197abfaa-4ae4-431f-9082-04e5e70799ab"), Name = "Manager" };
            var customerRole = new Role { Id = new Guid("f2058115-0e96-4deb-9fec-79dbab43899d"), Name = "Customer" };

            modelBuilder.Entity<Role>().HasData(managerRole);
            modelBuilder.Entity<Role>().HasData(customerRole);

            var managerUser = new User { Id = new Guid("6e2694c8-e6f0-41d4-8faf-e084ab629ee4"), Username = "Manager", Password = "1231", RoleId = managerRole.Id};
            var customerUser = new User { Id = new Guid("afa77327-08a9-40ca-9ab3-39e0ff7ab47d"), Username = "Customer", Password = "1231", RoleId = customerRole.Id};
            var customerUser2 = new User { Id = new Guid("afa77327-08a9-40ca-9ab3-39e0ff7ab48d"), Username = "Customer", Password = "1231", RoleId = customerRole.Id };

            modelBuilder.Entity<User>().HasData(managerUser);
            modelBuilder.Entity<User>().HasData(customerUser);
            modelBuilder.Entity<User>().HasData(customerUser2);

            modelBuilder.Entity<Customer>().HasData(new Customer { Id = new Guid("0da4f0d6-f312-4f0f-9b40-eaff18d8d99d"), Name = "Customer 1", Code = "0001-2021", Address = "Address 1", UserId = customerUser.Id });
            modelBuilder.Entity<Customer>().HasData(new Customer { Id = new Guid("0da4f0d6-f312-4f0f-9b40-eaff18d8d98d"), Name = "Customer 2", Code = "0002-2021", Address = "Address 2", UserId = customerUser2.Id });

            var electronicCategory = new Category { Id = new Guid("550e8400-e29b-41d4-a716-446655440000"), Name = "Электроника" };
            var furnitureCategory = new Category { Id = new Guid("550e8400-e29b-41d4-a716-446655440001"), Name = "Фурнитруа" };
            var clothingCategory = new Category { Id = new Guid("550e8400-e29b-41d4-a716-446655440002"), Name = "Одежда" };

            modelBuilder.Entity<Category>().HasData(electronicCategory);
            modelBuilder.Entity<Category>().HasData(furnitureCategory);
            modelBuilder.Entity<Category>().HasData(clothingCategory);

            modelBuilder.Entity<Item>().HasData(new Item { Id = new Guid("a77e5635-475c-4351-9f8d-8e1d9cfc9ec5"), Code = "01-0001-A001", Name = "Item 1", Price = 10.0, CategoryId = electronicCategory.Id });
            modelBuilder.Entity<Item>().HasData(new Item { Id = new Guid("11ccb803-2819-4a3c-bf27-0a45e1f943ad"), Code = "02-0002-B002", Name = "Item 2", Price = 20.0, CategoryId = furnitureCategory.Id});

            var newStatus = new Status { Id = new Guid("550e8400-e29b-41d4-a716-446655440003"), Name = "Новый" };
            var inProgressStatus = new Status { Id = new Guid("550e8400-e29b-41d4-a716-446655440004"), Name = "Выполняется" };
            var completedStatus = new Status { Id = new Guid("550e8400-e29b-41d4-a716-446655440005"), Name = "Выаолнен" };

            modelBuilder.Entity<Status>().HasData(newStatus);
            modelBuilder.Entity<Status>().HasData(inProgressStatus);
            modelBuilder.Entity<Status>().HasData(completedStatus);

            return modelBuilder;
        }
    }
}
