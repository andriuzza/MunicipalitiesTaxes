using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalitiesTaxes.Contract.Dto;
using MunicipalitiesTaxes.Contract.Repositories;
using MunicipalitiesTaxes.Contract.Services;

namespace MunicipalitiesTaxes.Service.TaxesService
{
    public class TaxesService : ITaxesService
    {
        private readonly ITaxesRepository _textRepository;

        public TaxesService(ITaxesRepository textRepository)
        {
            _textRepository = textRepository;
        }
        public async Task<TaxDto> GetTax(SearchTaskDto search)
        {
            if (string.IsNullOrEmpty(search.MunicipalityName))
            {
                return null;
            }

            var taxes = await _textRepository
                .GetTaxByCityAsync(search.MunicipalityName);

            if (taxes == null)
            {
                return null;
            }

            var orderedEnumerable = taxes.OrderByDescending(x => x.TaxType);

            foreach (var tax in orderedEnumerable)
            {
                var result = GetTaxInformation(tax, search);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public async Task<TaxDto> AddTax(TaxDto taxDto)
        {
           return await _textRepository.AddTaxAsync(taxDto);

        }

        public async Task<IEnumerable<TaxDto>> GetAllTaxesAsync()
        {
           return await _textRepository.GetAllTaxesAsync();
        }


        public async Task<TaxDto> UpdateTax(TaxDto taxDto)
        {
            return await _textRepository.UpadateTaxAsync(taxDto);
        }

        public async Task<bool> AddRangeTaxes(List<TaxDto> items)
        {
           return await _textRepository.AddRangeTaxes(items);
        }

        private TaxDto GetTaxInformation(TaxDto tax, SearchTaskDto search)
        {
            if (tax.Date == search.Date &&
                tax.TaxType == TaxType.Daily)
            {
                return tax;
            }

            if (tax.Date <= search.Date && tax.Date.AddDays(7) >= search.Date
                && tax.TaxType == TaxType.Weekly)
            {
                return tax;
            }

            if (tax.Date <= search.Date && tax.Date.AddMonths(1) >= search.Date
                && tax.TaxType == TaxType.Monthly)
            {
                return tax;
            }

            if (tax.Date <= search.Date && tax.Date.AddYears(1) >= search.Date
                && tax.TaxType == TaxType.Yearly)
            {
                return tax;
            }

            return null;
        }
    }
}
