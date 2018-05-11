using MunicipalitiesTaxes.Contract.Dto;
using MunicipalitiesTaxes.Repositories.Models;

namespace MunicipalitiesTaxes.Repositories.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MunicipalitiesTaxes.Repositories.DbContext.MunicipalitiesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MunicipalitiesTaxes.Repositories.DbContext.MunicipalitiesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var municipality1 = new Municipality()
            {
                Id = Guid.NewGuid(),
                Name = "Vilnius"
            };
            var municipality2 = new Municipality()
            {
                Id = Guid.NewGuid(),
                Name = "Kaunas"
            };
            var municipality3 = new Municipality()
            {
                Id = Guid.NewGuid(),
                Name = "Klaipėda"
            };

            context.Municipalities.Add(municipality1);
            context.SaveChanges();

            context.Municipalities.Add(municipality2);
            context.SaveChanges();

            context.Municipalities.Add(municipality3);
            context.SaveChanges();


            context.Taxes.Add(new Tax()
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(2018, 01, 01),
                TaxDecimal = (decimal) 0.515,
                TaxType = TaxType.Yearly,
                MunicipalityId = municipality1.Id
            });

            context.Taxes.Add(new Tax()
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(2018,05,01),
                TaxDecimal = (decimal)0.34,
                TaxType = TaxType.Monthly,
                MunicipalityId = municipality2.Id
            });

            context.Taxes.Add(new Tax()
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(2018, 08, 01),
                TaxDecimal = (decimal)0.3335,
                TaxType = TaxType.Daily,
                MunicipalityId = municipality1.Id
            });
        }
    }
}
