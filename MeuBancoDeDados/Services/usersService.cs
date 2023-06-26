using MeuBancoDeDados.Context.Entities;
using MeuBancoDeDados.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeuNamespace;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Services
{
    public class usersService : Iusers
    {
        private readonly MeuDbContext _context;

        public usersService(MeuDbContext context)
        {
            _context = context;
        }

        public List<users> FindAll()
        {
            return  _context.users.ToList();
        }

        public async Task<users> CreateUser(users newUser)
        {
            _context.users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }


    }

}