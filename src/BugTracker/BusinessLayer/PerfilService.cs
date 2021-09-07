using BugTracker.DataAccessLayer;
using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.BusinessLayer
{
    class PerfilService
    {
        private PerfilDao prioridadDao;

        public PerfilService()
        {
            prioridadDao = new PerfilDao();
        }

        public Perfil GetPerfilById(int idPerfil)
        {
            return prioridadDao.GetPerfilById(idPerfil);
        }

        public IList<Perfil> GetListPerfiles(Boolean incluyeBorrados)
        {
            return prioridadDao.GetListPerfiles(incluyeBorrados);
        }
    }
}
