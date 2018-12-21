using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UPC.Intranet.Modelo.Dto.Response
{
    [DataContract]
    public class ProcesoResponse
    {
        public int Id { get; set; }
        public int TipoProceso { get; set; }
        public int Mensaje { get; set; }

    }
}
