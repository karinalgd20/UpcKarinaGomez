using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.Intranet.Datos.Interfaz;
using UPC.Intranet.Modelo.Dto.Request;
using UPC.Intranet.Modelo.Dto.Response;
using UPC.Intranet.Negocio.Interfaz;

namespace UPC.Intranet.Negocio.Implementacion
{
    public class DetalleSolicitudBl : IDetalleSolicitudBl
    {
        readonly IDetalleSolicitudDal iDetalleSolicitudDal;

        public DetalleSolicitudBl(IDetalleSolicitudDal IDetalleSolicitudDal)
        {
            iDetalleSolicitudDal = IDetalleSolicitudDal;
        }
        public Detalle_SolicitudDtoResponse ListarDetalleSolicitud(Detalle_SolicitudDtoRequest dto)
        {
            return iDetalleSolicitudDal.ListarDetalleSolicitud(dto);
        }
    }
}
