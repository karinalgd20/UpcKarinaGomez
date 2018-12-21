using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.Intranet.Modelo.Modelo
{
    public class Detalle_Solicitud
    {
        [Key]
        public int COD_DETALLE { get; set; }
        public int COD_UNICO { get; set; }
        public string COD_CURSO { get; set; }
        public string COD_TIPO_PRUEBA { get; set; }
        public int NUM_PRUEBA { get; set; }
        public string SECCION { get; set; }
        public string GRUPO { get; set; }
        public string ESTADO_CURSO { get; set; }
    }
}
