using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalitiesTaxes.Contract.Dto;

namespace MunicipalitiesTaxes.Contract.Services
{
    public interface ITaxesService
    {
        Task<TaxDto> GetTax(SearchTaskDto search);
        Task<TaxDto> AddTax(TaxDto taxDto);
        Task<TaxDto> UpdateTax(TaxDto taxDto);
        Task<bool> AddRangeTaxes(List<TaxDto> items);
        Task<IEnumerable<TaxDto>> GetAllTaxesAsync();
    }
}
