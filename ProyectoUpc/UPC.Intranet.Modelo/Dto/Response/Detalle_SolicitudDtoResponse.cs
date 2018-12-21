using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UPC.Intranet.Modelo.Dto.Response
{
    [DataContract]
    public class Detalle_SolicitudDtoResponse
    {
        [DataMember]
        public int COD_UNICO { get; set; }

        [DataMember]
        public string COD_LINEA_NEGOCIO { get; set; }

        [DataMember]
        public string COD_MODAL_EST { get; set; }

        [DataMember]
        public string COD_PERIODO { get; set; }

        [DataMember]
        public int? COD_TRAMITE { get; set; }

        [DataMember]
        public string ESTADO { get; set; }

        [DataMember]
        public string COD_ALUMNO { get; set; }

        [DataMember]
        public int COD_DETALLE { get; set; }
        
        [DataMember]
        public string COD_CURSO { get; set; }

        [DataMember]
        public string COD_TIPO_PRUEBA { get; set; }

        [DataMember]
        public int NUM_PRUEBA { get; set; }

        [DataMember]
        public string SECCION { get; set; }

        [DataMember]
        public string GRUPO { get; set; }

        [DataMember]
        public string ESTADO_CURSO { get; set; }

        [DataMember]
        public List<Detalle_SolicitudDtoResponse> ListDetalle_SolicitudDtoResponse { get; set; }
    }
}
