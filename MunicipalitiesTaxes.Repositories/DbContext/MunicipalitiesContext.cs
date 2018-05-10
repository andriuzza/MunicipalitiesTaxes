using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MunicipalitiesTaxes.Contract.Dto;
using MunicipalitiesTaxes.Repositories.ContextConfigurations;
using MunicipalitiesTaxes.Repositories.Models;

namespace MunicipalitiesTaxes.Repositories.DbContext
{
    public class MunicipalitiesContext : System.Data.Entity.DbContext
    {

        public MunicipalitiesContext() : base("MunicipalitiesContext")
        {
        }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new TaxesConfiguration());
            modelBuilder.Configurations.Add(new MunicipalitiesConfiguration());
        }
    }
}
