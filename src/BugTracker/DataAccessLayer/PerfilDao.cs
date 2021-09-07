using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.DataAccessLayer
{
    class PerfilDao
    {
        public Perfil GetPerfilById(int idPerfil)
        {
            var strSql = String.Concat(" SELECT p.id_perfil, p.nombre ",
                                        " FROM Perfiles p ",
                                        " WHERE p.id_Perfil = " + idPerfil.ToString()
                                            );
            return MappingPerfil(DataManager.GetInstance().ConsultaSQL(strSql).Rows[0]);
        }

        public IList<Perfil> GetListPerfiles(Boolean incluyeBorrados)
        {
            List<Perfil> perfiles = new List<Perfil>();
            var strSql = String.Concat(" SELECT p.id_perfil, p.nombre ",
                                        " FROM Perfiles p ");
            if (!incluyeBorrados)
            {
                strSql += " WHERE p.borrado = 'false' ";
            }

            var resultado = (DataRowCollection)DataManager.GetInstance().ConsultaSQL(strSql).Rows;

            foreach (DataRow fila in resultado)
            {
                perfiles.Add(MappingPerfil(fila));
            }

            return perfiles;
        }

        private Perfil MappingPerfil(DataRow row)
        {
            Perfil perfil = new Perfil();

            perfil.IdPerfil = Convert.ToInt32(row["id_perfil"].ToString());
            perfil.Nombre = row["nombre"].ToString();

            return perfil;
        }
    }
}
