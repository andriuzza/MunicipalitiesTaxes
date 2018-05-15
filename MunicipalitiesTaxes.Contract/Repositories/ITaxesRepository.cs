using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalitiesTaxes.Contract.Dto;

namespace MunicipalitiesTaxes.Contract.Repositories
{
    public interface ITaxesRepository
    {
        Task<IEnumerable<TaxDto>> GetTaxByCityAsync(string cityName, DateTime taxDate);
        Task<TaxDto> AddTaxAsync(TaxDto taxDto);
        Task<TaxDto>UpadateTaxAsync(TaxDto taxDto);
        Task<bool> AddRangeTaxes(List<TaxDto> items);
        Task<IEnumerable<TaxDto>> GetAllTaxesAsync();
    }
}
