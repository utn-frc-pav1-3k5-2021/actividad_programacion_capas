using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.DataAccessLayer
{
    class PrioridadDao
    {
        public Prioridad GetPrioridadById(int idPrioridad)
        {
            var strSql = String.Concat(" SELECT p.id_prioridad, p.nombre ",
                                        " FROM Prioridades p ",
                                        " WHERE p.id_prioridad = " + idPrioridad.ToString()
                                            );
            return MappingPrioridad(DataManager.GetInstance().ConsultaSQL(strSql).Rows[0]);
        }

        public IList<Prioridad> GetListPrioridades(Boolean incluyeBorrados)
        {
            List<Prioridad> prioridades = new List<Prioridad>();
            var strSql = String.Concat(" SELECT p.id_prioridad, p.nombre ",
                                        " FROM prioridades p ");
            if (!incluyeBorrados)
            {
                strSql += " WHERE p.borrado = 'false' ";
            }

            var resultado = (DataRowCollection)DataManager.GetInstance().ConsultaSQL(strSql).Rows;

            foreach (DataRow fila in resultado)
            {
                prioridades.Add(MappingPrioridad(fila));
            }

            return prioridades;
        }

        private Prioridad MappingPrioridad(DataRow row)
        {
            Prioridad prioridad = new Prioridad();

            prioridad.IdPrioridad = Convert.ToInt32(row["id_prioridad"].ToString());
            prioridad.Nombre = row["nombre"].ToString();

            return prioridad;
        }
    }
}
