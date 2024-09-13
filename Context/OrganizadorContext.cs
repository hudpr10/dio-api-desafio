using dio_api_desafio.Entities;
using Microsoft.EntityFrameworkCore;

namespace dio_api_desafio.Context
{
    public class OrganizadorContext : DbContext
    {   
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}