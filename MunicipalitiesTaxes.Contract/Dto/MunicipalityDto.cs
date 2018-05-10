using System;
using System.Collections.ObjectModel;

namespace MunicipalitiesTaxes.Contract.Dto
{
    public class MunicipalityDto
    {
        public Guid Id  { get; set; }

        public string Name { get; set; }

        public Collection<TaxDto> Taxes { get; set; }
    }
}