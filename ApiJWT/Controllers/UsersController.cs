using ApiJWT.Models;
using ApiJWT.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiJWT.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private IUsersRepository usersRepository;
    public UsersController(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
        throw new NotImplementedException();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get([FromRoute] int id)
    {
        try
        {
            var user = await usersRepository.GetUserById(id);
            return user;
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}
