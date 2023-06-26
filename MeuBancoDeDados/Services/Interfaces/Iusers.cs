using MeuBancoDeDados.Context.Entities;


namespace MeuBancoDeDados.Services.Interfaces
{
    public interface Iusers
    {
        public List<users> FindAll();
        public Task<users> CreateUser(users newUser);
    }
}
