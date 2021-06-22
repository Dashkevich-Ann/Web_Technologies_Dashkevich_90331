using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLabsV06.DAL.Data;
using WebLabsV06.DAL.Entities;

namespace Dashkevich_90331.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context,
                                      UserManager<ApplicationUser> userManager,
                                      RoleManager<UserRole> roleManager)
        {
            // создать БД, если она еще не создана
            context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new UserRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                // создать роль admin
                await roleManager.CreateAsync(roleAdmin);
            }

            // проверка наличия пользователей
            if (!context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new ApplicationUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru"
                };
                await userManager.CreateAsync(user, "123456");
                // создать пользователя admin@mail.ru
                var admin = new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    UserName = "admin@mail.ru"
                };
                await userManager.CreateAsync(admin, "123456");
                // назначить роль admin
                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");
            }



            //проверка наличия групп объектов
            if (!context.FeedGroups.Any())
            {
                context.FeedGroups.AddRange(
                new List<FeedGroup>
                {
                new FeedGroup {GroupName="Сухой корм для кошек"},
                new FeedGroup {GroupName="Сухой корм для собак"},
                new FeedGroup {GroupName="Лакомства"},
                new FeedGroup {GroupName="Мокрый корм для кошек"},
                new FeedGroup {GroupName="Консервы для кошек"},
                new FeedGroup {GroupName="Консервы для собак"}
                });
                await context.SaveChangesAsync();
            }
            // проверка наличия объектов
            if (!context.Feeds.Any())
            {
                context.Feeds.AddRange(
                new List<Feed>
                {
                new Feed { FeedName="Sanabelle", Description="Для кожи и шерсти", Weight =2.2, FeedGroupId=1, Image="Cat_001.png" },
                new Feed { FeedName="Royal Canin", Description="Контроль веса", Weight =1.4, FeedGroupId=1, Image="Cat_002.png" },
                new Feed { FeedName="Dreamies", Description="Подушечки с говядиной", Weight =0.03, FeedGroupId=3, Image="Goody_001.jpg" },
                new Feed { FeedName="Integra", Description="Консерва для собак", Weight =0.15, FeedGroupId=6, Image="CannedFood_001.jpg" },
                new Feed { FeedName="Happy Dog", Description="Для молодых собак", Weight =14.0, FeedGroupId=2, Image="Dog_001.jpg" }
                });
                await context.SaveChangesAsync();
            }


        }
    }
}
