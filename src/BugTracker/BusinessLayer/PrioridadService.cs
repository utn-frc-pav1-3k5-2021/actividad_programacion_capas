using BugTracker.DataAccessLayer;
using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.BusinessLayer
{
    class PrioridadService
    {
        private PrioridadDao prioridadDao;

        public PrioridadService()
        {
            prioridadDao = new PrioridadDao();
        }

        public Prioridad GetPrioridadById(int idPrioridad)
        {
            return prioridadDao.GetPrioridadById(idPrioridad);
        }

        public IList<Prioridad> GetListPrioridades(Boolean incluyeBorrados)
        {
            return prioridadDao.GetListPrioridades(incluyeBorrados);
        }
    }
}
