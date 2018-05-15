using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalitiesTaxes.Contract.Dto;
using MunicipalitiesTaxes.Contract.Repositories;
using MunicipalitiesTaxes.Repositories.DbContext;
using MunicipalitiesTaxes.Repositories.Models;

namespace MunicipalitiesTaxes.Repositories.TaxesRepository
{
    public class TaxesRepository : ITaxesRepository
    {
        private readonly MunicipalitiesContext _dbContext;

        public TaxesRepository(MunicipalitiesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TaxDto>> GetAllTaxesAsync()
        {
            var result = await _dbContext.Taxes.Select(x=> new TaxDto()
            {
                Date = x.Date,
                TaxDecimal = x.TaxDecimal,
                MunicipalityId = x.MunicipalityId,
                TaxType = x.TaxType,
                Id = x.Id
            }).ToListAsync();

            return (IEnumerable <TaxDto>) result;
        }

        private DateTime GetTheLastMonday(DateTime dt)
        {
            int diff = (7 + (dt.DayOfWeek - (DayOfWeek.Monday)) % 7);
            return dt.AddDays(-1 * diff).Date.AddDays(-7);
        }
        public async Task<IEnumerable<TaxDto>> GetTaxByCityAsync(string cityName, DateTime taxDate)
        {
            var weekStart = GetTheLastMonday(taxDate);

            var taxesByCity = from municipality in _dbContext.Municipalities
                join tax in _dbContext.Taxes on municipality.Id equals tax.MunicipalityId
                let year = tax.Date.Year
                let month = tax.Date.Month
                let day = tax.Date.Day
                
                   where (municipality.Name.Equals(cityName))
                 && ( 
                    (tax.Date == taxDate.Date && tax.TaxType == TaxType.Daily) ||

                    (year == taxDate.Year && month == taxDate.Month
                        && day == 1 && tax.TaxType == TaxType.Monthly) ||

                        (weekStart == tax.Date && tax.TaxType == TaxType.Monthly) || 
                        
                        (taxDate.Year == year && tax.TaxType == TaxType.Yearly)
                )
                
                select new TaxDto()
                {
                    Date = tax.Date,
                    TaxDecimal = tax.TaxDecimal,
                    MunicipalityId = tax.MunicipalityId,
                    TaxType = tax.TaxType,
                    Id = tax.Id
                };

            var texesList = taxesByCity.AsEnumerable();

            return texesList;
        }

        public async Task<TaxDto> AddTaxAsync(TaxDto taxDto)
        {
            var result = _dbContext.Taxes.Add(new Tax()
            {
                Id = Guid.NewGuid(),
                MunicipalityId = taxDto.MunicipalityId,
                TaxDecimal = taxDto.TaxDecimal,
                TaxType = taxDto.TaxType,
                Date = taxDto.Date
            });

            await _dbContext.SaveChangesAsync();

            taxDto.Id = result.Id;

            return taxDto;
        }

        public async Task<TaxDto> UpadateTaxAsync(TaxDto taxDto)
        {
            var tax = await _dbContext.Taxes
                .FirstOrDefaultAsync(x => x.Id == taxDto.Id);

            if (tax != null)
            {
                tax.MunicipalityId = taxDto.MunicipalityId;
                tax.TaxDecimal = taxDto.TaxDecimal;
                tax.TaxType = taxDto.TaxType;
                tax.Date = taxDto.Date;
            }

            await _dbContext.SaveChangesAsync();

            return taxDto;
        }

        public async Task<bool> AddRangeTaxes(List<TaxDto> items)
        {
            var taxes = items.Select(x => new Tax()
            {
                Id = Guid.NewGuid(),
                MunicipalityId = x.MunicipalityId,
                TaxDecimal = x.TaxDecimal,
                TaxType = x.TaxType,
                Date = x.Date

            }).AsEnumerable();

             _dbContext.Taxes.AddRange(taxes);
            var isDone = await _dbContext.SaveChangesAsync();
            return isDone != 0;
        }
    }
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date.AddDays(-7);
        }
    }
}
