using Microsoft.AspNetCore.SignalR;
using SignalRDemo3ytEFC.Data;
using SignalRDemo3ytEFC.Repositories;

namespace SignalRDemo3ytEFC.Hubs
{
    public class DashboardHub : Hub
    {
        ProductRepository productRepository;
        private readonly ApplicationDbContext dbContext;

        public DashboardHub(IConfiguration configuration, ApplicationDbContext _dbContext)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            dbContext = _dbContext;
            productRepository = new ProductRepository(connectionString, dbContext);
        }

        public async Task SendProducts()
        {
            var products = productRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts", products);
        }
    }
}
