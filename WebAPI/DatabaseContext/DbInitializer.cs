using WebAPI.Entities;

namespace WebAPI.DatabaseContext
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<WebApiDbContext>();

                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {
                            FirstName = "Jan",
                            LastName = "Pierwszy"
                        },
                        new User()
                        {
                            FirstName = "Jan",
                            LastName = "Drugi"
                        },
                         new User()
                        {
                            FirstName = "Jan",
                            LastName = "Trzeci"
                        },
                    });
                    context.SaveChanges();
                }
            }

        }

    }
}