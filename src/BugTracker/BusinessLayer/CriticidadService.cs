using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DataAccessLayer;
using BugTracker.Entities;

namespace BugTracker.BusinessLayer
{
    class CriticidadService
    {
        private CriticidadDao oCriticidad;

        public CriticidadService()
        {
            oCriticidad = new CriticidadDao();
        }

        public IList<Criticidad> ConsultarCriticidadPorFiltros(Dictionary<string, object> parametros)
        {
            return oCriticidad.GetCriticidadByFilters(parametros);
        }

        public IList<Criticidad> ObtenerCriticidadesActivas()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("borrado", 0);

            return oCriticidad.GetCriticidadByFilters(parametros);
        }

        public Criticidad ConsultarProductoPorId(int id)
        {
            return oCriticidad.GetCriticidadById(id);
        }
    }
}
