using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DataAccessLayer;
using BugTracker.Entities;

namespace BugTracker.BusinessLayer
{
    class EstadoService
    {
        private EstadoDao oEstado;

        public EstadoService()
        {
            oEstado = new EstadoDao();
        }

        public IList<Estado> ConsultarEstadoPorFiltros(Dictionary<string, object> parametros)
        {
            return oEstado.GetEstadoByFilters(parametros);
        }

        public IList<Estado> ObtenerEstadoActivos()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("borrado", 0);

            return oEstado.GetEstadoByFilters(parametros);
        }

        public Estado ConsultarEstadoPorId(int id)
        {
            return oEstado.GetEstadoById(id);
        }
    }
}
