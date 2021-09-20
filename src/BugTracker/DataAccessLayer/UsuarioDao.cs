using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Entities;
using System.Data;

namespace BugTracker.DataAccessLayer
{
    class UsuarioDao
    {
        public Usuario GetUsuarioById(int idUsuario)
        {
            string sqlString = String.Concat("SELECT U.id_usuario, U.id_perfil, ",
                                             "U.usuario AS nombreUsuario, U.borrado, ",
                                             "U.email, P.nombre AS nombrePerfil ",
                                             "FROM Usuarios U ",
                                             "JOIN Perfiles P ON P.id_perfil = U.id_perfil ",
                                             "WHERE U.id_usuario = " + idUsuario);

            return this.MappingUsuario(DataManager.GetInstance().ConsultaSQL(sqlString).Rows[0]);
        }

        public IList<Usuario> GetUsuarioByFilters(Dictionary<string, object> parametros)
        {
            List<Usuario> listadoUsuarios = new List<Usuario>();

            string sqlString = String.Concat("SELECT U.id_usuario, U.id_perfil,",
                                             " U.usuario AS nombreUsuario, U.borrado,",
                                             " U.email, P.nombre AS nombrePerfil",
                                             " FROM Usuarios U",
                                             " JOIN Perfiles P ON P.id_perfil = U.id_perfil", 
                                             " WHERE 1 = 1");

            if (parametros != null)
            {
                if (parametros.ContainsKey("id_usuario"))
                    sqlString += " AND U.id_usuario = @id_usuario";
                if (parametros.ContainsKey("borrado"))
                    sqlString += " AND U.borrado = @borrado";
                if (parametros.ContainsKey("usuario"))
                    sqlString += " AND U.usuario LIKE '%' + @usuario + '%'";
                if (parametros.ContainsKey("email"))
                    sqlString += " AND U.email LIKE '%' + @email + '%'";
            }
            sqlString += " ORDER BY U.id_usuario DESC";

            var resultado = (DataRowCollection)DataManager.GetInstance().ConsultaSQL(sqlString, parametros).Rows;

            foreach (DataRow usuario in resultado)
                listadoUsuarios.Add(this.MappingUsuario(usuario));

            return listadoUsuarios;
        }

        private Usuario MappingUsuario(DataRow data)
        {
            Usuario oUsuario = new Usuario();

            oUsuario.IdUsuario = Convert.ToInt32(data["id_usuario"].ToString());
            oUsuario.NombreUsuario = data["nombreUsuario"].ToString();
            oUsuario.Email = data["email"].ToString();
            oUsuario.Estado = data["borrado"].ToString();

            oUsuario.Perfil = new Perfil();
            oUsuario.Perfil.IdPerfil = Convert.ToInt32(data["id_perfil"].ToString());
            oUsuario.Perfil.Nombre = data["nombrePerfil"].ToString();

            return oUsuario;
        }
    }
}
