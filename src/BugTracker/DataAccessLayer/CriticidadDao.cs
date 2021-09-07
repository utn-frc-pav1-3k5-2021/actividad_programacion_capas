using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.DataAccessLayer
{
    class CriticidadDao
    {
        public Criticidad GetCriticidadById(int idCriticidad)
        {
            string strSql = String.Concat("SELECT c.id_criticidad, c.nombre",
                                           "FROM Criticidades c",
                                           "WHERE c.id_criticidad = " + idCriticidad.ToString()
                                            );
            return this.MappingCriticidad(DataManager.GetInstance().ConsultaSQL(strSql).Rows[0]);
        }

        public IList<Criticidad> GetListCriticidades(Boolean incluyeBorrados)
        {
            List<Criticidad> criticidades = new List<Criticidad>();

            var strSql = String.Concat("SELECT c.id_criticidad, c.nombre ",
                                        " FROM Criticidades c ");
            if (!incluyeBorrados)
            {
                strSql += " WHERE c.borrado = 'false' ";
            }

            var resultadoConsulta = (DataRowCollection) DataManager.GetInstance().ConsultaSQL(strSql).Rows;

            foreach(DataRow fila in resultadoConsulta)
            {
                
                criticidades.Add(MappingCriticidad(fila));
            }

            return criticidades;
        }
        private Criticidad MappingCriticidad(DataRow row)
        {
            Criticidad criticidad = new Criticidad();
            criticidad.IdCriticidad = Convert.ToInt32(row["id_criticidad"].ToString());
            criticidad.Nombre = row["nombre"].ToString();

            return criticidad;
        }
    }
}
