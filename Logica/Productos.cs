using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Logica
{
    public class Productos
    {
        public static List<ListadoProductos> Obtener()
        {

            using (VentasEntities1 datos = new VentasEntities1())
            {

                return (from d in datos.Productos
                               join u in datos.usuarios on d.fk_usuarios equals u.identificacion
                               select new ListadoProductos()
                               {
                                   id = d.id,
                                   Nombre_Producto = d.Nombre_Producto,
                                   Stock = d.Stock,
                                   Valor = d.Valor,
                                   fk_usuarios = d.fk_usuarios
                               }).ToList();
            }
        }
    }
}
