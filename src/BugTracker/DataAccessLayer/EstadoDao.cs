using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.DataAccessLayer
{
    class EstadoDao
    {
        public Estado GetEstadoById(int idEstado)
        {
            string strSql = String.Concat("SELECT e.id_Estado, e.nombre",
                                           "FROM Estados e",
                                           "WHERE e.id_estado = " + idEstado.ToString()
                                            );
            return this.MappingEstado(DataManager.GetInstance().ConsultaSQL(strSql).Rows[0]);
        }

        public IList<Estado> GetListEstados(Boolean incluyeBorrados)
        {
            List<Estado> estados = new List<Estado>();

            var strSql = String.Concat("SELECT e.id_estado, e.nombre ",
                                        " FROM Estados e ");
            if (!incluyeBorrados)
            {
                strSql += " WHERE e.borrado = 'false' ";
            }

            var resultadoConsulta = (DataRowCollection)DataManager.GetInstance().ConsultaSQL(strSql).Rows;

            foreach (DataRow fila in resultadoConsulta)
            {

                estados.Add(MappingEstado(fila));
            }

            return estados;
        }
        private Estado MappingEstado(DataRow row)
        {
            Estado estado = new Estado();
            estado.IdEstado = Convert.ToInt32(row["id_estado"].ToString());
            estado.Nombre = row["nombre"].ToString();

            return estado;
        }
    }
}

