using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CastBank.Models;

namespace CastBank.Data
{
    public class CastBankContext : DbContext
    {
        public CastBankContext (DbContextOptions<CastBankContext> options)
            : base(options)
        {
        }

        public DbSet<CastBank.Models.Empresa> Empresa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<CastBank.Models.Emprestimo> Emprestimo { get; set; }
    }
}
