using SimpleInjector;
using SimpleInjector.Integration.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.Intranet.Datos;
using UPC.Intranet.Datos.Implementacion;
using UPC.Intranet.Datos.Interfaz;
using UPC.Intranet.Negocio.Implementacion;
using UPC.Intranet.Negocio.Interfaz;

namespace UPC.Intranet.Negocio
{
    public static class InjectorConfiguration
    {
        public static void ConfigurationContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();
            container.Register<UpcContext, UpcContext>(Lifestyle.Scoped);
            container.Register<IDetalleSolicitudDal, DetalleSolicitudDal>(Lifestyle.Scoped);

            container.Register<IDetalleSolicitudBl, DetalleSolicitudBl>(Lifestyle.Scoped);
            container.Verify();

        }
    }
}
