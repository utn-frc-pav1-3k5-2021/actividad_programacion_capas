using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Entities;
using System.Data;

namespace BugTracker.DataAccessLayer
{
    class EstadoDao
    {
        public Estado GetEstadoById(int idEstado)
        {
            string sqlString = String.Concat("SELECT E.id_estado, E.nombre ",
                                             "FROM Estados E ",
                                             "WHERE E.id_estado = " + idEstado);

            return this.MappingEstado(DataManager.GetInstance().ConsultaSQL(sqlString).Rows[0]);
        }

        public IList<Estado> GetEstadoByFilters(Dictionary<string, object> parametros)
        {
            List<Estado> listadoEstados = new List<Estado>();

            string sqlString = String.Concat("SELECT E.id_estado, E.nombre",
                                             " FROM Estados E",
                                             " WHERE 1 = 1");

            if (parametros != null)
            {
                if (parametros.ContainsKey("id_estado"))
                    sqlString += " AND E.id_estado = @idEstado";
                if (parametros.ContainsKey("borrado"))
                    sqlString += " AND E.borrado = @borrado";
                if (parametros.ContainsKey("nombre"))
                    sqlString += " AND E.nombre LIKE '%' + @nombre + '%'";
            }
            sqlString += " ORDER BY E.id_estado DESC";

            var resultado = (DataRowCollection)DataManager.GetInstance().ConsultaSQL(sqlString, parametros).Rows;

            foreach (DataRow estado in resultado)
                listadoEstados.Add(this.MappingEstado(estado));

            return listadoEstados;
        }

        private Estado MappingEstado(DataRow data)
        {
            Estado oEstado = new Estado();

            oEstado.IdEstado = Convert.ToInt32(data["id_estado"].ToString());
            oEstado.Nombre = data["nombre"].ToString();

            return oEstado;
        }
    }
}
