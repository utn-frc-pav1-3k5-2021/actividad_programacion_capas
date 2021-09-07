using BugTracker.DataAccessLayer;
using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.BusinessLayer
{
    class EstadoService
    {
        private EstadoDao estadoDao;

        public EstadoService()
        {
            estadoDao = new EstadoDao();
        }

        public Estado GetEstadoById(int idEstado)
        {
            return estadoDao.GetEstadoById(idEstado);
        }

        public IList<Estado> GetListEstados(Boolean incluyeBorrados)
        {
            return estadoDao.GetListEstados(incluyeBorrados);
        }
    }
}
