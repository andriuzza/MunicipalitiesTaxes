using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using MunicipalitiesTaxes.Contract.Dto;
using MunicipalitiesTaxes.Contract.Services;
using Newtonsoft.Json;

namespace MunicipalitiesTaxes.Controllers
{
    public class DataFromFileController : ApiController
    {
        private readonly ITaxesService _taxesService;

        public DataFromFileController(ITaxesService taxesService)
        {
            _taxesService = taxesService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "MunicipalitiesDataJson", "MunicipalietiesTaxes.json");

            using (var r = new StreamReader(path ?? throw new InvalidOperationException()))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<TaxDto>>(json);

                if (await _taxesService.AddRangeTaxes(items))
                {
                    return Ok("Successfuly added");
                }

                return BadRequest("Something get wrong");
            }
        }
    }
}
