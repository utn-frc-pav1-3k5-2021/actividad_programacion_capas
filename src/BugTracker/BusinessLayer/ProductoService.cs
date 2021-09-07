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
        private ProductoDao productoDao;

        public ProductoService()
        {
            productoDao = new ProductoDao();
        }

        public Producto GetProductoById(int idProducto)
        {
            return productoDao.GetProductoById(idProducto);
        }

        public IList<Producto> GetListProductos(Boolean incluyeBorrados)
        {
            return productoDao.GetListProductos(incluyeBorrados);
        }
    }
}
