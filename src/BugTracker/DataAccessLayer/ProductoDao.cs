using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.DataAccessLayer
{
    class ProductoDao
    {

        public IList<Producto> ConsultarProductosConFiltro(Dictionary<string,object> prm)
        {
            List<Producto> productos = new List<Producto>();

            string consultaSQL = "select id_producto, nombre from Productos where 1=1";

            if (prm.ContainsKey("idProducto"))
            {
                consultaSQL += "and id_producto = @idProducto";
            }

            if (prm.ContainsKey("nombreProducto"))
            {
                consultaSQL += "and nombre = @nombreProducto";
            }

            consultaSQL += " ORDER BY id_producto asc";

            var resultadoConsulta = (DataRowCollection) DataManager.GetInstance().ConsultaSQL(consultaSQL, prm).Rows;

            foreach (DataRow row in resultadoConsulta)
            {
                productos.Add(MappingProducto(row));
            }

            return productos;
        }

        private Producto MappingProducto(DataRow row)
        {
            Producto oProducto = new Producto();

            oProducto.IdProducto = Convert.ToInt32(row["id_producto"].ToString());
            oProducto.Nombre = row["nombre"].ToString();

            return oProducto;
        }
    }
}
