using BugTracker.DataAccessLayer;
using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.BusinessLayer
{
    class UsuarioService
    {
        private UsuarioDao productoDao;

        public UsuarioService()
        {
            productoDao = new UsuarioDao();
        }

        public Usuario GetUsuarioById(int idUsuario)
        {
            return productoDao.GetUsuarioById(idUsuario);
        }

        public IList<Usuario> GetListUsuarios(Boolean incluyeBorrados)
        {
            return productoDao.GetListUsuarios(incluyeBorrados);
        }
    }
}
