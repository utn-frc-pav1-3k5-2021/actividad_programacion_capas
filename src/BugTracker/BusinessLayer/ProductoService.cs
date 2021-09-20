using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DataAccessLayer;
using BugTracker.Entities;

namespace BugTracker.BusinessLayer
{
    class ProductoService
    {
        private ProductoDao oProducto;

        public ProductoService()
        {
            oProducto = new ProductoDao();
        }

        public IList<Producto> ConsultarProductoPorFiltros(Dictionary<string, object> parametros)
        {
            return oProducto.GetProductoByFilters(parametros);
        }

        public IList<Producto> ObtenerProductosActivos()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("borrado", 0);

            return oProducto.GetProductoByFilters(parametros);
        }

        public Producto ConsultarProductoPorId(int id)
        {
            return oProducto.GetProductoById(id);
        }
    }
}
