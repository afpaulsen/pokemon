using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListHeaders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListHeadersController : Controller
    {
        [HttpGet]
        public async Task<string> ListHeaders()
        {
            Rpc rpc = new Rpc();

            List<string> args = new List<string>{};

            return await rpc.CallApi("ListHeaders", args);
        }
        
    }
}
