using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ListadoProductos
    {
        public int id { get; set; }
        public string Nombre_Producto { get; set; }
        public int? Stock { get; set; }
        public int? Valor { get; set; }
        public int fk_usuarios { get; set; }

    }
}
