using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalitiesTaxes.Contract.Repositories;
using MunicipalitiesTaxes.Contract.Services;
using MunicipalitiesTaxes.Repositories.TaxesRepository;
using MunicipalitiesTaxes.Service.TaxesService;
using Ninject;

namespace MunicipalitiesTaxes.SelfHost.App_Start
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Bind<ITaxesService>().To<TaxesService>();
                kernel.Bind<ITaxesRepository>().To<TaxesRepository>();
                return kernel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
