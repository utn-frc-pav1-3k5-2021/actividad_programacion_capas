using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.DataAccessLayer
{
    class UsuarioDao
    {
        public Usuario GetUsuarioById(int idUsuario)
        {
            var strSql = String.Concat(" SELECT u.id_usuario, u.usuario, u.password, u.email, u.id_perfil, u.borrado ",
                                        " FROM Usuarios as u ",
                                        " WHERE u.id_usuario = " + idUsuario.ToString()
                                            );
            return MappingUsuario(DataManager.GetInstance().ConsultaSQL(strSql).Rows[0]);
        }

        public IList<Usuario> GetListUsuarios(Boolean incluyeBorrados)
        {
            List<Usuario> Usuarios = new List<Usuario>();
            var strSql = String.Concat(" SELECT u.id_usuario, u.usuario, u.email, u.id_perfil, u.borrado ",
                                        " FROM Usuarios as u ",
                                        " LEFT JOIN Perfiles as p ON ",
                                        " u.id_perfil = p.id_perfil ");
            if (!incluyeBorrados)
            {
                strSql += " WHERE u.borrado = 'false' ";
            }
            

            var resultado = (DataRowCollection)DataManager.GetInstance().ConsultaSQL(strSql).Rows;

            foreach (DataRow fila in resultado)
            {
                Usuarios.Add(MappingUsuario(fila));
            }

            return Usuarios;
        }

        private Usuario MappingUsuario(DataRow row)
        {
            Usuario usuario = new Usuario();
            Perfil perfil = null;

            if (row["id_perfil"].ToString() != "")
            {
                perfil = new PerfilDao().GetPerfilById(Convert.ToInt32(row["id_perfil"]));
            }

            

            usuario.IdUsuario = Convert.ToInt32(row["id_usuario"].ToString());
            usuario.NombreUsuario = row["usuario"].ToString();
            usuario.Email = row["email"].ToString();
            usuario.Perfil = perfil;
            usuario.Estado = row["borrado"].ToString();

            return usuario;
        }
    }
}
