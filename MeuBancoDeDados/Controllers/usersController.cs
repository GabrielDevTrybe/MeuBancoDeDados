using MeuBancoDeDados.Context.Entities;
using MeuBancoDeDados.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuBancoDeDados.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class usersController : ControllerBase
    {
        private readonly Iusers _usersService;

        public usersController(Iusers systemsService)
        {
            _usersService = systemsService;
        }

        [HttpGet]
        public List<users> GetAllUsers()
        {
            var users =  _usersService.FindAll();
            return (users); // Retorna o status 200 com os dados obtidos
        }

        [HttpPost]

        public async Task<ActionResult<users>> CreateUser(users newUser)
        {
            var createdUser = await _usersService.CreateUser(newUser);
            return CreatedAtAction(nameof(CreateUser), createdUser);
        }

    }
}