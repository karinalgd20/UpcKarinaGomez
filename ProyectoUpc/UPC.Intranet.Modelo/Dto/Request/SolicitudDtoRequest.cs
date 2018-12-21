using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UPC.Intranet.Modelo.Dto.Request
{
    [DataContract]
    public class SolicitudDtoRequest
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
    }
}
