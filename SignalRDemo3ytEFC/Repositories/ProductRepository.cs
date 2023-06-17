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
    }
}
