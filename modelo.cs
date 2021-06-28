using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablasCRUD
{
    class modelo
    {
        private string _cnnString;
        public modelo()
        {
            _cnnString = ConfigurationManager.ConnectionStrings["InstitutoConnection"].ConnectionString;
        }
        


        public List<estadoAlumno> ConsultarLst()
        {
            List<estadoAlumno> lstEstados = new List<estadoAlumno>();
            string query = $"select id,nombre,Clave from EstatusAlumnos";
            try
            {
                using (SqlConnection con = new SqlConnection(_cnnString))
                {
                    DataTable dt = new DataTable();
                    SqlCommand comando = new SqlCommand(query, con);
                    comando.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        estadoAlumno estado = new estadoAlumno();
                        estado.id = Convert.ToInt32(reader["id"]);
                        estado.nombre = Convert.ToString(reader["nombre"]);
                        estado.clave = Convert.ToString(reader["Clave"]);
                        lstEstados.Add(estado);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Consultar ", ex);
            }

            return lstEstados;
        }


        public void EliminarEstatus(int id)
        {
            string f = Convert.ToString(id);
          
            string query = ($"delete EstatusAlumnos where id = {f}");

            try
            {
                using (SqlConnection con = new SqlConnection(_cnnString))
                {
                    SqlCommand comando = new SqlCommand(query, con);
                    comando.CommandType = CommandType.Text;
                    con.Open();
                    comando.ExecuteNonQuery();
                    comando.Parameters.Clear();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar", ex);
            }
        }

        public int AgregarEstatus(string clave,string nombre)
        {


            int IdRegreso = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(_cnnString))
                {
                    SqlCommand comando = new SqlCommand("sp_AddEstatus", con);
                    comando.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlParameter paramID = new SqlParameter("id", SqlDbType.Int);
                    paramID.Direction = ParameterDirection.Output;
                    comando.Parameters.Add(paramID);

                    comando.Parameters.AddWithValue("@clave", clave);
                    comando.Parameters.AddWithValue("@nombre", nombre);

                    int numfilaAfec = comando.ExecuteNonQuery();

                    if (numfilaAfec > 0)
                    {
                        IdRegreso = Convert.ToInt32(comando.Parameters["id"].Value);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar", ex);
            }
            return IdRegreso;
        }

        public void ModificarEstatus(int id,string nombre,string clave)
        {
    
            List<estadoAlumno> list = ConsultarLst();

            estadoAlumno estatuAlumno = list.Find(alum => alum.id == id);

            try
            {
                using (SqlConnection con = new SqlConnection(_cnnString))
                {
                    SqlCommand comando = new SqlCommand("sp_ActualizarEstatus", con);
                    comando.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@clave", clave);
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    comando.ExecuteNonQuery();
                    comando.Parameters.Clear();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Actualizar", ex);
            }


        }

    }
}
