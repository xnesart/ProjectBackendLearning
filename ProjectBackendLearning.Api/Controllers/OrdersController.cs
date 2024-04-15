using Microsoft.AspNetCore.Mvc;

namespace ProjectBackendLearning.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : Controller
{
    private List<string> Orders = new List<string>()
    {
        new string("Order1"),
        new string("Order2"),
    };

    public OrdersController()
    {
    }

    [HttpGet]
    public List<string> GetOrders()
    {
        return Orders;
    }
}