using System;
using System.ComponentModel.DataAnnotations.Schema;
using MunicipalitiesTaxes.Contract.Dto;

namespace MunicipalitiesTaxes.Repositories.Models
{
    public class Tax
    {
        public Guid Id { get; set; }

        public TaxType TaxType { get; set; }

        public decimal  TaxDecimal { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public Guid MunicipalityId { get; set; }

        public Municipality Municipality { get; set; }
    }
}