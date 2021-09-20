using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DataAccessLayer;
using BugTracker.Entities;

namespace BugTracker.BusinessLayer
{
    class UsuarioService
    {
        private UsuarioDao oUsuario;

        public UsuarioService()
        {
            oUsuario = new UsuarioDao();
        }

        public IList<Usuario> ConsultarUsuarioPorFiltros(Dictionary<string, object> parametros)
        {
            return oUsuario.GetUsuarioByFilters(parametros);
        }

        public IList<Usuario> ObtenerUsuariosActivos()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("borrado", 0);

            return oUsuario.GetUsuarioByFilters(parametros);
        }

        public Usuario ConsultarUsuarioPorId(int id)
        {
            return oUsuario.GetUsuarioById(id);
        }
    }
}
