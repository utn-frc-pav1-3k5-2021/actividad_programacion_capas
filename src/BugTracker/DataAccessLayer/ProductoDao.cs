using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Entities;

namespace BugTracker.DataAccessLayer
{
    class ProductoDao
    {
        public Producto GetProductoById(int idProducto)
        {
            string sqlString = String.Concat("SELECT P.id_producto, ", 
                                             "P.nombre FROM Productos P ", 
                                             "WHERE P.id_producto = " + idProducto.ToString());

            return this.MappingProducto(DataManager.GetInstance().ConsultaSQL(sqlString).Rows[0]);
        }

        public IList<Producto> GetProductoByFilters(Dictionary<string, object> parametros)
        {
            List<Producto> listadoProductos = new List<Producto>();

            string sqlString = String.Concat("SELECT P.id_producto, P.nombre",
                                             " FROM Productos P",
                                             " WHERE 1 = 1");
            if (parametros != null)
            {
                if (parametros.ContainsKey("id_producto"))
                    sqlString += " AND P.id_producto = @idProducto";
                if (parametros.ContainsKey("borrado"))
                    sqlString += " AND P.borrado = @borrado";
                if (parametros.ContainsKey("nombre"))
                    sqlString += " AND NOMBRE LIKE '%' + @nombre + '%'";
            }
            sqlString += " ORDER BY P.id_producto DESC";

            var resultado = (DataRowCollection) DataManager.GetInstance().ConsultaSQL(sqlString, parametros).Rows;

            foreach (DataRow producto in resultado)
                listadoProductos.Add(this.MappingProducto(producto));
            
            return listadoProductos;
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
