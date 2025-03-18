using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Runtime.Remoting.Messaging;

namespace CapaDatos
{
    public class CD_Categoria
    {
        public List<Categoria> Listar() {

            List<Categoria> lista = new List<Categoria>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT c.IdCategoria, c.Descripcion,c.Estado FROM CATEGORIA c");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) {
                            lista.Add(new Categoria() {
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Categoria>();

                }
            return lista;
        }

        public int Registrar(Categoria objcategoria, out string mensaje) {
            int idCategoriagenerada = 0;
            mensaje = string.Empty;

            try {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_CATEGORIA", oconexion);
                    //Se definen parametros de entrada
                    cmd.Parameters.AddWithValue("Descripcion", objcategoria.Descripcion);
                    //cmd.Parameters.AddWithValue("Estado", objcategoria.Estado);
                    //Se defienen parametros de salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idCategoriagenerada = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex) {
                idCategoriagenerada= 0;
                mensaje = ex.Message;
            }
            return 0;
        }
        public bool Editar(Categoria objcategoria, out string mensaje) {
            int resultado = 0;
            mensaje = string.Empty;

            try {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena)) {

                    SqlCommand cmd = new SqlCommand("SP_EDITAR_CATEGORIA", oconexion);

                    //Se definen los parametros de entrada
                    cmd.Parameters.AddWithValue("Descripcion", objcategoria.Descripcion);
                    //cmd.Parameters.AddWithValue("Estado", objcategoria.Estado);
                    //se definen los  parametros de salida
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                mensaje = ex.Message;
            }
            return false;
        }
        public bool Eliminar(Categoria objcategoria, out string mensaje){
            int resultado = 0;
            mensaje = string.Empty;

            try {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_CATEGORIA", oconexion);
                    //Se definen parametros de entrada
                    cmd.Parameters.AddWithValue("Descripcion", objcategoria.Descripcion);
                    //cmd.Parameters.AddWithValue("Estado", objcategoria.Estado);
                    //Se definen parametros de salida
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                mensaje = ex.Message;
            }
            return false;
        }
    }
}
