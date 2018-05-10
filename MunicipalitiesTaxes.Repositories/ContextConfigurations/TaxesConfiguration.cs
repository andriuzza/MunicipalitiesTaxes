using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalitiesTaxes.Contract.Dto;
using MunicipalitiesTaxes.Repositories.Models;

namespace MunicipalitiesTaxes.Repositories.ContextConfigurations
{
    public class TaxesConfiguration : EntityTypeConfiguration<Tax>
    {
        public TaxesConfiguration()
        {
            HasKey(x => x.Id);

            Property(c => c.Date)
                .IsRequired();

            Property(c => c.TaxDecimal)
                .IsRequired();

            Property(c => c.TaxType)
                .IsRequired();

            HasRequired(x => x.Municipality)
                .WithMany(x => x.Taxes)
                .HasForeignKey(x => x.MunicipalityId);
        }
    }
}
