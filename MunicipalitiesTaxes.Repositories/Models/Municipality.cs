using System;
using System.Collections.ObjectModel;
using MunicipalitiesTaxes.Contract.Dto;

namespace MunicipalitiesTaxes.Repositories.Models
{
    public class Municipality
    {
        public Guid Id  { get; set; }

        public string Name { get; set; }

        public Collection<Tax> Taxes { get; set; }
    }
}