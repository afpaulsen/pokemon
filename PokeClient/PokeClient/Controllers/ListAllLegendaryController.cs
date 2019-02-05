using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListAllLegendary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListAllLegendaryController : Controller
    {
        [HttpGet]
        public async Task<string> ListAllLegendary()
        {
            Rpc rpc = new Rpc();

            List<string> args = new List<string>{};

            return await rpc.CallApi("ListAllLegendary", args);
        }
        
    }
}
