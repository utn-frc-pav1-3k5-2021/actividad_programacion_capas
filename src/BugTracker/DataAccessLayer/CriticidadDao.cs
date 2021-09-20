using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Entities;
using System.Data;

namespace BugTracker.DataAccessLayer
{
    class CriticidadDao
    {

        public Criticidad GetCriticidadById(int idCriticidad)
        {
            string sqlString = String.Concat("SELECT C.id_criticidad, C.nombre ",
                                             "FROM Criticidades C ",
                                             "WHERE C.id_criticidad = " + idCriticidad);

            return this.MappingCriticidad(DataManager.GetInstance().ConsultaSQL(sqlString).Rows[0]);
        }

        public IList<Criticidad> GetCriticidadByFilters(Dictionary<string, object> parametros)
        {
            List<Criticidad> listadoCriticidad = new List<Criticidad>();

            string sqlString = String.Concat("SELECT C.id_criticidad, C.nombre",
                                             " FROM Criticidades C",
                                             " WHERE 1 = 1");

            if (parametros != null)
            {
                if (parametros.ContainsKey("id_criticidad"))
                    sqlString += " AND C.id_criticidad = @idCriticidad";
                if (parametros.ContainsKey("borrado"))
                    sqlString += " AND C.borrado = @borrado";
                if (parametros.ContainsKey("nombre"))
                    sqlString += " AND C.nombre LIKE '%' + @nombre + '%'";
            }
            sqlString += " ORDER BY C.id_criticidad DESC";

            var resultado = (DataRowCollection)DataManager.GetInstance().ConsultaSQL(sqlString, parametros).Rows;

            foreach (DataRow criticidad in resultado)
                listadoCriticidad.Add(this.MappingCriticidad(criticidad));

            return listadoCriticidad;
        }

        private Criticidad MappingCriticidad(DataRow data)
        {
            Criticidad oCriticidad = new Criticidad();

            oCriticidad.IdCriticidad = Convert.ToInt32(data["id_criticidad"].ToString());
            oCriticidad.Nombre = data["nombre"].ToString();

            return oCriticidad;
        }
    }
}
