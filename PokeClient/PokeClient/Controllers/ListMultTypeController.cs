using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListMultType.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListMultTypeController : Controller
    {
        [HttpGet]
        public async Task<string> ListMultType()
        {
            Rpc rpc = new Rpc();

            List<string> args = new List<string>{};

            return await rpc.CallApi("ListMultType", args);
        }
        
    }
}
