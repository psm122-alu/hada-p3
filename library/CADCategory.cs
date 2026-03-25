using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace library
{
    public class CADCategory
    {
        private string conexion;

        public CADCategory()
        {
            conexion = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        public bool read(ENCategory en)
        {
            bool leer = false;
            SqlConnection conn = null;

            String comando = "Select * from Categories where id = " + en.Id;

            try
            {
                conn = new SqlConnection(conexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand(comando, conn);
                SqlDataReader datos_leer = cmd.ExecuteReader();

                if (datos_leer.Read())
                {
                    en.Name = datos_leer["name"].ToString();
                    leer = true;
                }

                datos_leer.Close();
            }
            catch (SqlException sqlex)
            {
                throw new Exception("FALLO AL LEER: " + en.Id, sqlex);
            }

            finally
            {
                if (conn != null) conn.Close();
            }
            return leer;
        }

        public List<ENCategory> readAll()
        {
            List<ENCategory> lista = new List<ENCategory>();
            SqlConnection conn = null;

            string comando = "Select * from Categories";

            try
            {
                conn = new SqlConnection(conexion);
                conn.Open();

                SqlCommand cmd = new SqlCommand(comando, conn);
                SqlDataReader datos_leer = cmd.ExecuteReader();

                while (datos_leer.Read())
                {
                    ENCategory categoria = new ENCategory();
                    categoria.Id = int.Parse(datos_leer["id"].ToString());
                    categoria.Name = datos_leer["name"].ToString();
                    lista.Add(categoria);
                }
                datos_leer.Close();
            }
            catch (SqlException sqlex) { throw new Exception("FALLO EN CONSULTA", sqlex); }
            catch (Exception ex) { throw ex; }

            finally
            {
                if (conn != null) conn.Close();
            }
            return lista;

        }
    }
}
