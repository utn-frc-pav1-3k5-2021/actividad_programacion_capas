using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BugTracker.DataAccessLayer
{
    class ProductoDao
    {
        public Producto GetProductoById(int idProducto)
        {
            var strSql = String.Concat(" SELECT p.id_producto, p.nombre ",
                                        " FROM Productos p ",
                                        " WHERE p.id_producto = " + idProducto.ToString()
                                            );
            return MappingProducto(DataManager.GetInstance().ConsultaSQL(strSql).Rows[0]);
        }

        public IList<Producto> GetListProductos(Boolean incluyeBorrados)
        {
            List<Producto> productos = new List<Producto>();
            var strSql = String.Concat(" SELECT p.id_producto, p.nombre ",
                                        " FROM Productos p ");
            if (!incluyeBorrados)
            {
                strSql += " WHERE p.borrado = 'false' ";
            }

            var resultado =  (DataRowCollection) DataManager.GetInstance().ConsultaSQL(strSql).Rows;

            foreach(DataRow fila in resultado)
            {
                productos.Add(MappingProducto(fila));
            }

            return productos;
        }

        private Producto MappingProducto(DataRow row) {
            Producto producto = new Producto();

            producto.IdProducto = Convert.ToInt32(row["id_producto"].ToString());
            producto.Nombre = row["nombre"].ToString();

            return producto;
        }
    }
}
