using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using UPC.Intranet.Negocio;
using UPC.Intranet.Negocio.Interfaz;
using System.Reflection;
using System.Web.Mvc;
using UPC.Intranet.Negocio.Implementacion;
using UPC.Intranet.Datos.Implementacion;
using UPC.Intranet.Datos.Interfaz;
using UPC.Intranet.Datos;

namespace ProyectoUpc
{
    public static class InitializeContainer
    {
        public static Container Container = new Container();
        public static void Start()
        {
            Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            Container.Register<UpcContext, UpcContext>(Lifestyle.Scoped);
            Container.Register<IDetalleSolicitudDal, DetalleSolicitudDal>(Lifestyle.Scoped);
            Container.Register<IDetalleSolicitudBl, DetalleSolicitudBl>(Lifestyle.Scoped);
            Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            Container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(Container));
        }
    }
}