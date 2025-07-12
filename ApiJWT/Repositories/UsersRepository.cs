using ApiJWT.Models;
using ApiJWT.Utils;
using Microsoft.EntityFrameworkCore;

namespace ApiJWT.Repositories;

public interface IUsersRepository
{
    Task<User> GetUserById(int id);
    Task<User> GetUserByEmail(string email);
    Task<User> CreateUser(SignUpRequest request);
}

public class UsersRepository : IUsersRepository
{
    private readonly CrmDbContext dbContext;

    public UsersRepository(CrmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<User> CreateUser(SignUpRequest request)
    {
        var user = new User()
        {
            Email = request.Email,
            Password = PasswordHasher.HashPassword(request.Password),
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
            throw new UserCreationFailedException();
        }

        return user;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email) ?? throw new UserNotFoundException();
    }

    public async Task<User> GetUserById(int id)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id) ?? throw new UserNotFoundException();
    }
}

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("Kullanıcı bulunamadı.")
    {

    }
}

public class UserCreationFailedException : Exception
{
    public UserCreationFailedException() : base("Kullanıcı oluşturulamadı.")
    {

    }
}