using BugTracker.DataAccessLayer;
using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.BusinessLayer
{
    class CriticidadService
    {
        CriticidadDao criticidadDao;

        public CriticidadService()
        {
            criticidadDao = new CriticidadDao();
        }

        public Criticidad GetCriticidadById(int idCriticidad)
        {
            return criticidadDao.GetCriticidadById(idCriticidad);
        }

        public IList<Criticidad> GetListCriticidades(Boolean incluyeBorrados)
        {
            return criticidadDao.GetListCriticidades(incluyeBorrados);
        }
    }
}
