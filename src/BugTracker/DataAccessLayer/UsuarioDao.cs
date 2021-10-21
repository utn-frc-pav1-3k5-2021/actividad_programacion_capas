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
        public IList<Usuario> ConsultarUsuariosConFiltro(Dictionary<string,object> param)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            string consultaSQL = String.Concat("select u.id_usuario, u.usuario,u.id_perfil, p.nombre as perfil, u.password, u.email, u.borrado " +
                "from Usuarios as u inner join Perfiles as p on u.id_perfil = p.id_perfil where u.borrado = 0 ");

            if (param.ContainsKey("NombreUsuario"))
            {
                consultaSQL += " and u.usuario = @NombreUsuario";
            }

            if (param.ContainsKey("Perfil"))
            {
                consultaSQL += " and u.id_perfil = @Perfil";
            }

            if (param.ContainsKey("Borrado")) {
                consultaSQL += " or u.borrado = 1";
            }

            consultaSQL += " order by u.id_usuario asc";

            var resultadoConsulta = (DataRowCollection) DataManager.GetInstance().ConsultaSQL(consultaSQL, param).Rows;

            foreach (DataRow row in resultadoConsulta)
            {
                listaUsuarios.Add(MappingUsuario(row));
            }

            return listaUsuarios;
        }

        private Usuario MappingUsuario(DataRow row)
        {
            Usuario oUsuario = new Usuario();
            oUsuario.IdUsuario = Convert.ToInt32(row["id_usuario"].ToString());
            oUsuario.NombreUsuario = row["usuario"].ToString();
            oUsuario.Email = row["email"].ToString();
            oUsuario.Contrasena = row["password"].ToString();
            oUsuario.Borrado = row["borrado"].ToString();

            oUsuario.Perfil = new Perfil();
            oUsuario.Perfil.IdPerfil = Convert.ToInt32(row["id_perfil"].ToString());
            oUsuario.Perfil.Nombre = row["perfil"].ToString();


            return oUsuario;
        }

        public bool ExisteUsuario(string usuario)
        {

            //Construimos la consulta sql para buscar el usuario en la base de datos.
            String strSql = string.Concat(" SELECT id_usuario, ",
                                          "        usuario, ",
                                          "        email, ",
                                          "        password, ",
                                          "        p.id_perfil, ",
                                          "        p.nombre as perfil ",
                                          "   FROM Usuarios u",
                                          "  INNER JOIN Perfiles p ON u.id_perfil= p.id_perfil ",
                                          "  WHERE usuario = @usuario");

            var parametros = new Dictionary<string, object>();
            parametros.Add("usuario", usuario);
            //Usando el método GetDBHelper obtenemos la instancia unica de DBHelper (Patrón Singleton) y ejecutamos el método ConsultaSQL()
            var resultado = DataManager.GetInstance().ConsultaSQL(strSql, parametros);

            // Validamos que el resultado tenga al menos una fila.
            if (resultado.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public bool Alta(Usuario usuario)
        {
            string str_sql = "     INSERT INTO Usuarios (usuario, password, email, id_perfil, borrado)" +
                              "     VALUES (@usuario, @password, @email, @id_perfil, 0)";

            var parametros = new Dictionary<string, object>();

            parametros.Add("usuario", usuario.NombreUsuario);
            parametros.Add("password", usuario.Contrasena);
            parametros.Add("email", usuario.Email);
            parametros.Add("id_perfil", usuario.Perfil.IdPerfil);

            // Si una fila es afectada por la inserción retorna TRUE. Caso contrario FALSE
            return (DataManager.GetInstance().EjecutarSQL(str_sql, parametros) == 1);
        }

        public bool Modificar(Usuario usuario)
        {
            string str_sql = "UPDATE Usuarios SET usuario = @usuario, password = @password, email = @email, id_perfil = @id_perfil " +
                " WHERE id_usuario = @id_usuario ";

            var parametros = new Dictionary<string, object>();

            parametros.Add("usuario", usuario.NombreUsuario);
            parametros.Add("password", usuario.Contrasena);
            parametros.Add("email", usuario.Email);
            parametros.Add("id_perfil", usuario.Perfil.IdPerfil);
            parametros.Add("id_usuario", usuario.IdUsuario);

            return (DataManager.GetInstance().EjecutarSQL(str_sql, parametros)) == 1;
        }

        public bool Baja(Usuario usuario)
        {
            string str_sql = "UPDATE Usuarios SET borrado = 1" +
                " WHERE id_usuario = @id_usuario ";

            var parametros = new Dictionary<string, object>();

            parametros.Add("id_usuario", usuario.IdUsuario);

            return (DataManager.GetInstance().EjecutarSQL(str_sql, parametros)) == 1;
        }

    }
}
