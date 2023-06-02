using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ecommerce.Infrastructure.Persistence;

public  class EcommerceDbContextData{

    protected EcommerceDbContextData()
    {
    }
    public static async Task LoadDataAsync(EcommerceDbContext context, 
    UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
    ILoggerFactory loggerFactory){
       
       try
       {
            if (!roleManager.Roles.Any()){
                await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));    
                await roleManager.CreateAsync(new IdentityRole(Role.USER));
            }

            if (!userManager.Users.Any()){
                var user = new User{
                    UserName = "Germani568",
                    Email = "germani.jesus.uicab@gmail.com",
                    EmailConfirmed = true,
                    LastName = "Jesus",
                    FirstName = "Germani",
                    PhoneNumber = "809-000-0000",
                    AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/avatar-1.webp?alt=media&token=58da3007-ff21-494d-a85c-25ffa758ff6d"
                };

                await userManager.CreateAsync(user, "$$##P@ssw0rd34");
                await userManager.AddToRoleAsync(user, Role.ADMIN);
            } 

             if (!userManager.Users.Any()){
                var user = new User{
                    UserName = "GermaniCustomer",
                    Email = "germani.jesus.uicab@gmail.com",
                    EmailConfirmed = true,
                    LastName = "Jesus",
                    FirstName = "Germani",
                    PhoneNumber = "809-000-0000",
                    AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/avatar-1.webp?alt=media&token=58da3007-ff21-494d-a85c-25ffa758ff6d"
                };

                await userManager.CreateAsync(user, "$$##P@ssw0rdCustomer");
                await userManager.AddToRoleAsync(user, Role.USER);
            } 
           

            if (!context.Categories!.Any()){
                var categoriesData = File.ReadAllText("../Infrastructure/Data/category.json");
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesData);

                await context.Categories!.AddRangeAsync(categories!);

                await context.SaveChangesAsync();
            } 

             if (!context.Products!.Any()){
                var productData = File.ReadAllText("../Infrastructure/Data/product.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productData);

                await context.Products!.AddRangeAsync(products!);

                await context.SaveChangesAsync();
            } 

            if (!context.Images!.Any()){
                var imageData = File.ReadAllText("../Infrastructure/Data/image.json");
                var images = JsonConvert.DeserializeObject<List<Image>>(imageData);

                await context.Images!.AddRangeAsync(images!);

                await context.SaveChangesAsync();
            } 

            if (!context.Countries!.Any()){
                var CountriesData = File.ReadAllText("../Infrastructure/Data/Countries.json");
                var Countries = JsonConvert.DeserializeObject<List<Country>>(CountriesData);

                await context.Countries!.AddRangeAsync(Countries!);

                await context.SaveChangesAsync();
            } 

            if (!context.Reviews!.Any()){
                var ReviewsData = File.ReadAllText("../Infrastructure/Data/review.json");
                var Reviews = JsonConvert.DeserializeObject<List<Review>>(ReviewsData);

                await context.Reviews!.AddRangeAsync(Reviews!);

                await context.SaveChangesAsync();
            } 
            
       }
       catch (Exception e)
       {
            var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
            logger.LogError(e, "An error occurred seeding the DB.");
       }
    }
   
}