using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPC.Intranet.Modelo.Dto.Request;
using UPC.Intranet.Negocio;
using UPC.Intranet.Negocio.Interfaz;

namespace ProyectoUpc.Controllers
{
    public class BandejaDetalleSolicitudController : Controller
    {
        public readonly IDetalleSolicitudBl _IDetalleSolicitudBl;
        public BandejaDetalleSolicitudController(IDetalleSolicitudBl IDetalleSolicitudBl)
        {
           this._IDetalleSolicitudBl = IDetalleSolicitudBl;
        }
        
        // GET: BandejaDetalleSolicitud
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Resultado(Detalle_SolicitudDtoRequest dto)
        {
            var lista = _IDetalleSolicitudBl.ListarDetalleSolicitud(dto);

            ViewBag.Lista = lista.ListDetalle_SolicitudDtoResponse;
            return View();
        }

      
    }
}