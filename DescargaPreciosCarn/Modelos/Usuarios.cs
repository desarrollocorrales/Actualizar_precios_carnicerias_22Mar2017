using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DescargaPreciosCarn.Modelos
{
    public class Usuarios
    {
        public int idUsuario { get; set; }
        public string nombreCompleto { get; set; }
        public string correo { get; set; }
        public string fechaCreacion { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string status { get; set; }
    }
}
