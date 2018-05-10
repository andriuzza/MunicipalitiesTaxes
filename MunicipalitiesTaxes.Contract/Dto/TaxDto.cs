using System;

namespace MunicipalitiesTaxes.Contract.Dto
{
    public class TaxDto
    {
        public Guid Id { get; set; }

        public TaxType TaxType { get; set; }

        public decimal  TaxDecimal { get; set; }

        public DateTime Date { get; set; }

        public Guid MunicipalityId { get; set; }
    }
}