using MeuBancoDeDados.Context.Entities;
using MeuBancoDeDados.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using SeuNamespace;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeuBancoDeDados.Services
{
    public class usersService : Iusers
    {
        private readonly MeuDbContext _context;
        private readonly HttpContext _httpcontext;

        public usersService(MeuDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpcontext = httpContextAccessor.HttpContext;
        }

        private string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public List<users> FindAll()
        {
            return _context.users.ToList();
        }

        public async Task<users> CreateUser(users newUser)
        {
            // Validar os dados do usuário conforme necessário...

            string emailRegex = @"^\w+@[a-zA-Z_]+?";

            // Verificar se o email é válido e se a senha atende aos critérios mínimos
            if (string.IsNullOrWhiteSpace(newUser.Email) || !Regex.IsMatch(newUser.Email, emailRegex) || newUser.Senha.Length < 8)
            {
                throw new EmailAndSenhaInvalidoException("Email ou senha inválidos.");
            }

            // Verificar se já existe um usuário com o mesmo email
            if (_context.users.Any(u => u.Email == newUser.Email))
            {
                throw new EmailAndSenhaInvalidoException("Usuário já existe.");
            }

            // Calcula o hash MD5 da senha
            string hashedPassword = CalculateMD5Hash(newUser.Senha);

            // Substitui a senha original pela senha hash
            newUser.Senha = hashedPassword;

            _context.users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
    }

    public class EmailAndSenhaInvalidoException : Exception
    {
        public EmailAndSenhaInvalidoException(string message) : base(message)
        {
        }
    }
}
