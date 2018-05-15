using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                .GetTaxByCityAsync(search.MunicipalityName, search.Date);

            return taxes.OrderByDescending(x => x.TaxType).First(); /*Fixed code there and in the repository */
        }

        public async Task<TaxDto> AddTax(TaxDto taxDto)
        {
            if (taxDto.TaxDecimal == 0 ||
                Enum.IsDefined(typeof(TaxType), (int) taxDto.TaxType) == false)
            {
                throw new Exception("Wrong properties, try again");
            }
           return await _textRepository.AddTaxAsync(taxDto);
        }

        public async Task<IEnumerable<TaxDto>> GetAllTaxesAsync()
        {
           return await _textRepository.GetAllTaxesAsync();
        }

        public async Task<TaxDto> UpdateTax(TaxDto taxDto)
        {
            if (taxDto.TaxDecimal == 0 ||
                Enum.IsDefined(typeof(TaxType), (int)taxDto.TaxType) == false
            )
            {
                throw new Exception("Wrong properties, try again");
            }

            return await _textRepository.UpadateTaxAsync(taxDto);
        }

        public async Task<bool> AddRangeTaxes(List<TaxDto> items)
        {
            if (items == null)
            {
                throw new Exception("Empty sortedTaxes");
            }
            return await _textRepository.AddRangeTaxes(items);
        }

    }
}
