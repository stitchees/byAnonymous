using Microsoft.EntityFrameworkCore;
using System;

namespace GrupoAzureWebIII.Models
{
    public class MensajeDbContext : DbContext
    {

        public MensajeDbContext(DbContextOptions<MensajeDbContext> options) : base(options) { }

        public DbSet<Mensaje> Mensaje { get; set; }

    }
}