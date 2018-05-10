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

        public async Task<IEnumerable<TaxDto>> GetTax()
        {
            return null;
        }

        public async Task<IEnumerable<TaxDto>> GetTaxByCityAsync(string cityName)
        {
            var result = await _dbContext.Municipalities
                .Where(x => x.Name.Equals(cityName))
                .ToListAsync() as IEnumerable<TaxDto>;

            return result;
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
}
