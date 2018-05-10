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
                .GetTaxByCityAsync(search.MunicipalityName);

            if (taxes == null)
            {
                return null;
            }

            var sortedTaxes = taxes.OrderByDescending(x => x.TaxType).ToArray();

            for (var i = 0; i < sortedTaxes.Length; i++)
            {
                try
                {
                    while (sortedTaxes[i].TaxType == TaxType.Daily)
                    {
                        if (CheckTaxesDaily(sortedTaxes[i], search))
                        {
                            return sortedTaxes[i];
                        }
                        i++;
                    }

                    while (sortedTaxes[i].TaxType == TaxType.Weekly)
                    {
                        if (CheckTaxesWeekly(sortedTaxes[i], search))
                        {
                            return sortedTaxes[i];
                        }
                        ;
                        i++;
                    }

                    while (sortedTaxes[i].TaxType == TaxType.Monthly)
                    {
                        if (CheckTaxesMonthly(sortedTaxes[i], search))
                        {
                            return sortedTaxes[i];
                        }
                        i++;
                    }

                    while (sortedTaxes[i].TaxType == TaxType.Yearly)
                    {
                        if (CheckTaxesYearly(sortedTaxes[i], search))
                        {
                            return sortedTaxes[i];
                        }
                        ;
                        i++;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    return null;
                }

            }

            return null;
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

        private bool CheckTaxesDaily(TaxDto tax, SearchTaskDto search)
        {
            return (tax.Date == search.Date &&
                    tax.TaxType == TaxType.Daily);

        }

        private bool CheckTaxesWeekly(TaxDto tax, SearchTaskDto search)
        {
            return tax.Date <= search.Date && tax.Date.AddDays(7) >= search.Date
                   && tax.TaxType == TaxType.Weekly;
        }

        private bool CheckTaxesMonthly(TaxDto tax, SearchTaskDto search)
        {
            return tax.Date <= search.Date && tax.Date.AddMonths(1) >= search.Date
                   && tax.TaxType == TaxType.Monthly;

        }

        private bool CheckTaxesYearly (TaxDto tax, SearchTaskDto search)
        {
            return tax.Date <= search.Date && tax.Date.AddYears(1) >= search.Date
                   && tax.TaxType == TaxType.Yearly;
        }
    }
}
