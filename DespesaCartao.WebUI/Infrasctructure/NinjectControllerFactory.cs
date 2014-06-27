using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Concrete;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Infrasctructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IClienteRepository>().To<EFClienteRepository>();
            ninjectKernel.Bind<ILojaRepository>().To<EFLojaRepository>();
            ninjectKernel.Bind<ISegmentoRepository>().To<EFSegmentoRepository>();
            ninjectKernel.Bind<ICartaoRepository>().To<EFCartaoRepository>();
            ninjectKernel.Bind<IDespesaRepository>().To<EFDespesaRepository>().WithConstructorArgument<IGerenciadorParcelamento>(new GerenciadorParcelamento(new EFParcelaRepository(), new EFCartaoRepository()));
        }
    }
}