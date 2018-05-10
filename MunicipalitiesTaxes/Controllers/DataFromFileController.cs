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
    [RoutePrefix("api/taxesAdd")]
    public class DataFromFileController : ApiController
    {
        private readonly ITaxesService _taxesService;

        public DataFromFileController(ITaxesService taxesService)
        {
            _taxesService = taxesService;
        }

        [Route("range")]
        [HttpPost]
        public async Task<IHttpActionResult> AddMunicipalitiesData()
        {
            using (var r = new StreamReader("file.json"))
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
