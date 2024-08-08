using ApplicationCore.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seeds;
public class DataSeeding
{
    public static void Run(IServiceProvider serviceProvider)
    {
        using (MainDbContext dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<MainDbContext>())
        {
            Task.WaitAll(
                InsertTypeSales(dbContext.TypeSales),
                InsertTypeBida(dbContext.TypeBida));

            dbContext.SaveChanges();
        }

    }
    private static async Task InsertTypeSales(DbSet<TypeSales> typeSales)
    {
        List<TypeSales> items = new()
        {
            new TypeSales
            {
                Name = "Bida",
                StoreCode="STO1",
                StaffCode="EMP1",
                Status="Active",
                Code="TS01"

            },
            new TypeSales
            {
                Name = "Cafe",
                StoreCode="STO1",
                StaffCode="EMP1",
                Status="Active",
                Code="TS02"

            },
            new TypeSales
            {
                Name = "Food",
                StoreCode="STO1",
                StaffCode="EMP1",
                Status="Active",
                Code="TS03"

            },
            new TypeSales
            {
                Name = "Restaurand",
                StoreCode="STO1",
                StaffCode="EMP1",
                Status="Active",
                Code="TS04"

            }
        };
        foreach (TypeSales item in items)
        {
            TypeSales? checkUpdate = typeSales.FirstOrDefault(c => c.Code == item.Code);
            if (checkUpdate is null)
            {
                typeSales.Add(item);
            }
            else
            {

                checkUpdate.Name = item.Name;
                checkUpdate.StaffCode = item.StaffCode;
                checkUpdate.StoreCode = item.StoreCode;
                typeSales.Update(checkUpdate);

            }
        }
    }

    private static async Task InsertTypeBida(DbSet<TypeBida> typeSales)
    {
        List<TypeBida> items = new()
        {
            new TypeBida
            {
                Name = "Phang",
                StoreCode="STO1",
                StaffCode="EMP1",
                Status="Active",
                Code="TB01"

            },
            new TypeBida
            {
                Name = "3Bang",
                StoreCode="STO1",
                StaffCode="EMP1",
                Status="Active",
                Code="TB02"

            },
            new TypeBida
            {
                Name = "Lo",
                StoreCode="STO1",
                StaffCode="EMP1",
                Status="Active",
                Code="TB03"

            }

        };
        foreach (TypeBida item in items)
        {
            TypeBida? checkUpdate = typeSales.FirstOrDefault(c => c.Code == item.Code);
            if (checkUpdate is null)
            {
                typeSales.Add(item);
            }
            else
            {

                checkUpdate.Name = item.Name;
                checkUpdate.StaffCode = item.StaffCode;
                checkUpdate.StoreCode = item.StoreCode;
                typeSales.Update(checkUpdate);

            }
        }
    }
}
