using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.Intranet.Datos.Interfaz;
using UPC.Intranet.Modelo.Dto.Request;
using UPC.Intranet.Modelo.Dto.Response;
using UPC.Intranet.Modelo.Modelo;

namespace UPC.Intranet.Datos.Implementacion
{
    public class DetalleSolicitudDal : Repository<Detalle_Solicitud>, IDetalleSolicitudDal
    {
        readonly DbContext context;

        public DetalleSolicitudDal(UpcContext unitOfWork) : base(unitOfWork)
        {
            context = UnitOfWork as UpcContext;
        }

        public Detalle_SolicitudDtoResponse ListarDetalleSolicitud(Detalle_SolicitudDtoRequest dto)
        {
            var objet = new UpcContext();
            return objet.ListarDetalleSolicitud(dto); 
        }
    }
}
