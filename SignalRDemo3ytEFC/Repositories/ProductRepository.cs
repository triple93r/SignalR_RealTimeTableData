using SignalRDemo3ytEFC.Data;
using SignalRDemo3ytEFC.Models;
using System.Data;
using System.Data.SqlClient;

namespace SignalRDemo3ytEFC.Repositories
{
    public class ProductRepository
    {
        string connectionString;
        private readonly ApplicationDbContext dbContext;


        public ProductRepository(string connectionString, ApplicationDbContext _dbContext)
        {
            this.connectionString = connectionString;
            this.dbContext = _dbContext;
        }

        public List<Product> GetProducts()
        {
            var prodList = dbContext.Product.ToList();
            foreach (var emp in prodList)
            {
                dbContext.Entry(emp).Reload();
            }
            //var f = dbContext.Product.ToList();
            return prodList;
        }

        public List<ProductForGraph> GetProductsForGraph()
        {
            List<ProductForGraph> productsForGraph = new List<ProductForGraph>();
            ProductForGraph productForGraph;

            var data = GetProductsForGraphFromDb();

            foreach (DataRow row in data.Rows)
            {
                productForGraph = new ProductForGraph
                {
                    Category = row["Category"].ToString(),
                    Products = Convert.ToInt32(row["Products"])
                };
                productsForGraph.Add(productForGraph);
            }

            return productsForGraph;
        }

        private DataTable GetProductsForGraphFromDb()
        {
            var query = "SELECT Category, COUNT(Id) Products FROM Product GROUP BY Category";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }


    }
}
