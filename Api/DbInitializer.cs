namespace Api;

class DbInitializer
{
    public static async Task SeedAsync(CrmDbContext dbContext)
    {
        if (!dbContext.Users.Any())
        {
            dbContext.Users.AddRange(
                new Models.User
                {
                    Name = "Cengiz",
                    Surname = "Atilla",
                    Email = "cengizatilla@gmail.com",
                    Password = "123456",
                    Phone = "5551231234"
                },
                new Models.User
                {
                    Name = "Mert",
                    Surname = "Söğüt",
                    Email = "mert@gmail.com",
                    Password = "123456",
                    Phone = "123123123"
                }
            );

            await dbContext.SaveChangesAsync();
        }
    }
}