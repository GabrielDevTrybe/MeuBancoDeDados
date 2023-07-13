using MeuBancoDeDados.Context.Entities;
using MeuBancoDeDados.Services;
using MeuBancoDeDados.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var users = _usersService.FindAll();
            return users; // Retorna o status 200 com os dados obtidos
        }

        [HttpPost]
        public async Task<ActionResult<users>> CreateUser(users newUser)
        {
            try
            {
                var createdUser = await _usersService.CreateUser(newUser);
                return CreatedAtAction(nameof(CreateUser), createdUser);
            }
            catch (EmailAndSenhaInvalidoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
