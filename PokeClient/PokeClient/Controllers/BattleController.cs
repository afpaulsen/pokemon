using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchTypeController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : Controller
    {
        [HttpGet]
        public string Battle()
        {
            //This could also list all headers
            return "Please provide both names for pokemon figther A and B, case sensitive";
        }

        [HttpGet("{a}/{b}")]
        public async Task<string> Battle(string a, string b)
        {
            Rpc rpc = new Rpc();

            List<string> args = new List<string>
            {
                a,
                b
            };

            return await rpc.CallApi("Battle", args);
        }
    }
}
