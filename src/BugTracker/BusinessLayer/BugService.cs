using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Entities;
using BugTracker.DataAccessLayer;
using System.Data;

namespace BugTracker.BusinessLayer
{
    public class BugService
    {
        private BugDao oBugDao;
        public BugService()
        {
            oBugDao = new BugDao();
        }
        public IList<Bug> ConsultarBugsConFiltros(Dictionary<string, object> parametros)
        {
            return oBugDao.GetBugByFilters(parametros);
        }

        public Bug ConsultarBugsPorId(int id)
        {
            return oBugDao.GetBugById(id);
        }

        public DataTable ConsultarPrioridades()
        {
            return oBugDao.GetPrioridades();
        }

        public DataTable ConsultarCriticidades()
        {
            return oBugDao.GetCriticidades();
        }

        public DataTable ConsultarUsuarios()
        {
            return oBugDao.GetUsuarios();
        }

        public DataTable ConsultarProductos()
        {
            return oBugDao.GetProductos();
        }

        public DataTable ConsultarEstados()
        {
            return oBugDao.GetEstados();
        }

    }
}
