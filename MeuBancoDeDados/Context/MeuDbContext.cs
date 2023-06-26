using MeuBancoDeDados.Context.Entities;
using MeuBancoDeDados.Maping;
using MeuBancoDeDados.Services;

using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace SeuNamespace
{
    public class MeuDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public MeuDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Defina a propriedade DbSet para representar sua entidade "systems"
        public DbSet<users> users { get; set; }
        //public DbSet<qSystemResume> qSystemResume { get; set; }

        //public DbSet<indicators> indicators { get; set; }

        //public DbSet<qIndicators> qIndicators { get; set; }

        ////public DbSet<projects> projects { get; set; }

        //public DbSet<businessentities> businessentities { get; set; }
        //public DbSet<NewList> NewList { get; set; }

        //public DbSet<Events> Events { get; set; }

        //public DbSet<EventTickets> EventTickets { get; set; }
        //public DbSet<CountEventTickets> CountEventTickets { get; set; }

        //public DbSet<UserSurvey> UserSurvey { get; set; }

        //public DbSet<UserSurveyAnswer> UserSurveyAnswer { get; set; }

        //public DbSet<ResponseClass> ResponseClass { get; set; }
        //public DbSet<PrjNotificationConfig> PrjNotificationConfig { get; set; }






        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost; Port=3306; Database=saleswebmvcappdb; User=root; Password=senha_mysql;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(new MySqlConnection(connectionString)));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new usersMap());
            //modelBuilder.ApplyConfiguration(new indicatorsMap());
            //modelBuilder.ApplyConfiguration(new qIndicatorsMap());
            //modelBuilder.ApplyConfiguration(new projectsMap());
            //modelBuilder.ApplyConfiguration(new businessentitiesMap());
            //modelBuilder.ApplyConfiguration(new newListMap());
            //modelBuilder.ApplyConfiguration(new MapingEvents());
            //modelBuilder.ApplyConfiguration(new MapingEventsTickets());
            //modelBuilder.ApplyConfiguration(new MapingUserSurvey());
            //modelBuilder.ApplyConfiguration(new MapingUserSurveyAnswer());
            //modelBuilder.ApplyConfiguration(new MapingPrjNotificationConfig());


            // Outras configurações do modelo...



            base.OnModelCreating(modelBuilder);
        }
    }

}
