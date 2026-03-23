using System;
using System.Data.SqlClient;
using System.Configuration;

namespace library
{
    public class CADProduct
    {
        private string constring;

        public CADProduct()
        {
            // Inicializa la cadena de conexión leyendo del Web.config 
            constring = ConfigurationManager.ConnectionStrings["miconexion"].ToString();
        }

        public bool Create(ENProduct en)
        {
            bool success = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "INSERT INTO Products (name, code, amount, price, category, creationDate) VALUES (@name, @code, @amount, @price, @category, @creationDate)";
                SqlCommand com = new SqlCommand(query, c);

                com.Parameters.AddWithValue("@name", en.Name);
                com.Parameters.AddWithValue("@code", en.Code);
                com.Parameters.AddWithValue("@amount", en.Amount);
                com.Parameters.AddWithValue("@price", en.Price);
                com.Parameters.AddWithValue("@category", en.Category);
                com.Parameters.AddWithValue("@creationDate", en.CreationDate);

                if (com.ExecuteNonQuery() > 0)
                    success = true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
            finally
            {
                c.Close(); // Aseguramos el cierre de la conexión
            }
            return success;
        }

        public bool Update(ENProduct en)
        {
            bool success = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "UPDATE Products SET name = @name, amount = @amount, price = @price, category = @category, creationDate = @creationDate WHERE code = @code";
                SqlCommand com = new SqlCommand(query, c);

                com.Parameters.AddWithValue("@name", en.Name);
                com.Parameters.AddWithValue("@code", en.Code);
                com.Parameters.AddWithValue("@amount", en.Amount);
                com.Parameters.AddWithValue("@price", en.Price);
                com.Parameters.AddWithValue("@category", en.Category);
                com.Parameters.AddWithValue("@creationDate", en.CreationDate);

                if (com.ExecuteNonQuery() > 0)
                    success = true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
            finally
            {
                c.Close(); 
            }
            return success;
        }

        public bool Delete(ENProduct en)
        {
            bool success = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "DELETE FROM Products WHERE code = @code";
                SqlCommand com = new SqlCommand(query, c);

                com.Parameters.AddWithValue("@code", en.Code);

                if (com.ExecuteNonQuery() > 0)
                    success = true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
            finally
            {
                c.Close();
            }
            return success;
        }

        public bool Read(ENProduct en)
        {
            bool success = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "SELECT * FROM Products WHERE code = @code";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@code", en.Code);

                SqlDataReader dr = com.ExecuteReader(); 

                if (dr.Read())
                {
                    en.Name = dr["name"].ToString();
                    en.Amount = Convert.ToInt32(dr["amount"]);
                    en.Price = Convert.ToSingle(dr["price"]);
                    en.Category = Convert.ToInt32(dr["category"]);
                    en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                    success = true;
                }
                dr.Close(); // Cerramos el reader al terminar de leer 
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
            finally
            {
                c.Close(); 
            }
            return success;
        }

        public bool ReadFirst(ENProduct en)
        {
            bool success = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "SELECT TOP 1 * FROM Products ORDER BY code ASC";
                SqlCommand com = new SqlCommand(query, c);

                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    en.Code = dr["code"].ToString();
                    en.Name = dr["name"].ToString();
                    en.Amount = Convert.ToInt32(dr["amount"]);
                    en.Price = Convert.ToSingle(dr["price"]);
                    en.Category = Convert.ToInt32(dr["category"]);
                    en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                    success = true;
                }
                dr.Close(); 
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
            finally
            {
                c.Close();
            }
            return success;
        }

        public bool ReadNext(ENProduct en)
        {
            bool success = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "SELECT TOP 1 * FROM Products WHERE code > @code ORDER BY code ASC";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@code", en.Code);

                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    en.Code = dr["code"].ToString();
                    en.Name = dr["name"].ToString();
                    en.Amount = Convert.ToInt32(dr["amount"]);
                    en.Price = Convert.ToSingle(dr["price"]);
                    en.Category = Convert.ToInt32(dr["category"]);
                    en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                    success = true;
                }
                dr.Close(); 
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
            finally
            {
                c.Close(); 
            }
            return success;
        }

        public bool ReadPrev(ENProduct en)
        {
            bool success = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "SELECT TOP 1 * FROM Products WHERE code < @code ORDER BY code DESC";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@code", en.Code);

                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    en.Code = dr["code"].ToString();
                    en.Name = dr["name"].ToString();
                    en.Amount = Convert.ToInt32(dr["amount"]);
                    en.Price = Convert.ToSingle(dr["price"]);
                    en.Category = Convert.ToInt32(dr["category"]);
                    en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                    success = true;
                }
                dr.Close(); 
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
            finally
            {
                c.Close(); 
            }
            return success;
        }
    }
}