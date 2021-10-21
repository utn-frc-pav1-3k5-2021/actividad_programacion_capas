using BugTracker.DataAccessLayer;
using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.BusinessLayer
{
    class ProductoService
    {
        private ProductoDao oProductoDao;

        public ProductoService()
        {
            oProductoDao = new ProductoDao();
        }

        public IList<Producto> ConsultarProductosConFiltro(Dictionary<string, object> parametros) {
          return  oProductoDao.ConsultarProductosConFiltro(parametros);
        }
    }
}
