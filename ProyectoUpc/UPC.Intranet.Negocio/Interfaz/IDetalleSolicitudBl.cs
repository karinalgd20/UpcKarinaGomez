﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.Intranet.Modelo.Dto.Request;
using UPC.Intranet.Modelo.Dto.Response;

namespace UPC.Intranet.Negocio.Interfaz
{
    public interface IDetalleSolicitudBl
    {
        Detalle_SolicitudDtoResponse ListarDetalleSolicitud(Detalle_SolicitudDtoRequest dto);
    }
}
