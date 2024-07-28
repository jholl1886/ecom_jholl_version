using Amazon.Library.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace jholl_eCommerce.API.DataBase
{
    public class MSSQLContext
    {
        public Product AddProduct(Product p)
        {//DMA for c++ basically
            using(SqlConnection conn = new SqlConnection("Server=JACOBLAPTOP;Database=AMAZON;Trusted_Connection=yes;TrustServerCertificate=True") ) //connectionsstrings.com
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    var sql = $"Product.InsertProduct";
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", $"'{p.Name}'"));
                    cmd.Parameters.Add(new SqlParameter("@Description", ""));
                    cmd.Parameters.Add(new SqlParameter("@Quantity", p.Quantity));
                    cmd.Parameters.Add(new SqlParameter("@Price", p.Price));
                    cmd.Parameters.Add(new SqlParameter("@MarkDown", p.MarkDown));
                    cmd.Parameters.Add(new SqlParameter("@IsBogo", p.IsBogo));

                    var id = new SqlParameter("Id", p.Id);
                    id.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(id);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close(); //not great practice to not close even if inside a using statement

                        p.Id = (int)id.Value;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return p;
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            using (SqlConnection conn = new SqlConnection("Server=JACOBLAPTOP;Database=AMAZON;Trusted_Connection=yes;TrustServerCertificate=True"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    var sql = @"
                SELECT 
                    Id, 
                    REPLACE(Name, '''','') AS Name, 
                    Description, 
                    Quantity, 
                    Price, 
                    MarkDown, 
                    IsBogo
                FROM 
                    PRODUCT";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Quantity = (int)reader["Quantity"],
                                Price = (decimal)reader["Price"],
                                MarkDown = (decimal)reader["MarkDown"],
                                IsBogo = (bool)reader["IsBogo"]
                            });

                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            return products;
        }

        public Product Delete(int id)
        {
            Product deletedProduct = null;

            using (SqlConnection conn = new SqlConnection("Server=JACOBLAPTOP;Database=AMAZON;Trusted_Connection=yes;TrustServerCertificate=True"))
            {
                conn.Open();

                // Fetch product details before deletion
                using (SqlCommand fetchCmd = conn.CreateCommand())
                {
                    fetchCmd.CommandText = "SELECT Id, Name, Description, Price, Quantity, MarkDown, IsBogo FROM PRODUCT WHERE Id = @Id";
                    fetchCmd.Parameters.Add(new SqlParameter("@Id", id));

                    using (var reader = fetchCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            deletedProduct = new Product
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = (decimal)reader["Price"],
                                Quantity = (int)reader["Quantity"],
                                MarkDown = (decimal)reader["MarkDown"],
                                IsBogo = (bool)reader["IsBogo"]
                            };
                        }
                    }
                }

                
                using (SqlCommand deleteCmd = conn.CreateCommand())
                {
                    deleteCmd.CommandText = "Product.DeleteProduct";
                    deleteCmd.CommandType = CommandType.StoredProcedure;
                    deleteCmd.Parameters.Add(new SqlParameter("@Id", id));
                    deleteCmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            return deletedProduct;
        }


    }
}
