using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MunicipalitiesTaxes.Contract.Dto;
using MunicipalitiesTaxes.Contract.Services;

namespace MunicipalitiesTaxes.Controllers
{
    [RoutePrefix("api/taxes")]
    public class TaxesController : ApiController
    {
        private readonly ITaxesService _taxesService;

        public TaxesController(ITaxesService taxesService)
        {
            _taxesService = taxesService;
        }

        [Route("get")]
        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri]SearchTaskDto searchTaskDto)
        {
            var tax = await _taxesService.GetTax(searchTaskDto);

            if (tax == null)
            {
                return BadRequest("No taxes for this date have been found");
            }

            return Ok(tax);
        }

        [Route("GetAllTaxes")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllTaxes()
        {
            var result = await _taxesService.GetAllTaxesAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("post")]
        [HttpPost]
        public async Task<IHttpActionResult> AddTax([FromBody]TaxDto taxDto)
        {
            var addTax = await _taxesService.AddTax(taxDto);

            if (addTax == null)
            {
                return BadRequest("Something wrong data to allow it to add");
            }

            return Ok(addTax);
        }

        [Route("update")]
        [HttpPut]
        public async Task<IHttpActionResult> UpadateTaxAsync([FromBody]TaxDto taxDto)
        {
           var updateTax = await _taxesService.UpdateTax(taxDto);

            if (updateTax == null)
            {
                return BadRequest("Something wrong data to allow it to update");
            }

            return Ok(updateTax);
        }
    }
}
