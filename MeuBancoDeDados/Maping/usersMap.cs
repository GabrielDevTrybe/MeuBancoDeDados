using MeuBancoDeDados.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuBancoDeDados.Maping
{
    public class usersMap : IEntityTypeConfiguration<users>
    {
        public void Configure(EntityTypeBuilder<users> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasColumnType("varchar(50)");
            builder.Property(x => x.Sobrenome).HasColumnType("varchar(50)");
            builder.Property(x => x.Email).HasColumnType("varchar(50)");
            builder.Property(x => x.Senha).HasColumnType("varchar(50)");

        }


    }
}