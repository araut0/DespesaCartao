using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DespesaCartao.Startup))]
namespace DespesaCartao
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
