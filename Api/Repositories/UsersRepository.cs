using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public interface IUsersRepository
{
    Task<User> GetUserById(int i);
    Task<User> CreateUser(SignUpRequest request);
}

public class UsersRepository : IUsersRepository
{
    private readonly CrmDbContext dbContext;

    public UsersRepository(CrmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<User> GetUserById(int id)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("asdasda");
    }
    public async Task<User> CreateUser(SignUpRequest request)
    {
        var user = new User()
        {
            Email = request.Email,
            Password = request.Password,
            Name = request.Name,
            Surname = request.Surname,
            Phone = request.Phone,
        };

        try
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception();
        }

        return user;
    }
}
