using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private  IUsersRepository usersRepository;
    public UsersController(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    [HttpGet("{id}")]
    public async Task<User> Get([FromRoute] int id)
    {
        var user = await usersRepository.GetUserById(id);
        return user;
    }
}
