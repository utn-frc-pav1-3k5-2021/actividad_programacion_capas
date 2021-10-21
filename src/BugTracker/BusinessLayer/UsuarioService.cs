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
        private UsuarioDao oUsuarioDao;
        public UsuarioService()
        {
            oUsuarioDao = new UsuarioDao();
        }

        public IList<Usuario> ConsultarUsuariosConFiltro(Dictionary<string,object> param)
        {
            return oUsuarioDao.ConsultarUsuariosConFiltro(param);
        }

        public bool CrearUsuario(Usuario U)
        {
            return oUsuarioDao.Alta(U);
        }

        public bool ExisteUsuario(string usuario)
        {

            return oUsuarioDao.ExisteUsuario(usuario);
        }

        public bool ActualizarUsuario(Usuario usuario) {
            return oUsuarioDao.Modificar(usuario);
        }

        public bool EliminarUsuario(Usuario usuario)
        {
            return oUsuarioDao.Baja(usuario);
        }
    }
}
