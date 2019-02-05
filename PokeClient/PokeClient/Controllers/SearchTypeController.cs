using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchTypeController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchTypeController : Controller
    {
        [HttpGet]
        public string SearchType()
        {
            //This could also list all pokemon
            return "Please provide a type";
        }

        [HttpGet("{type}")]
        public async Task<string> SearchType(string type)
        {
            Rpc rpc = new Rpc();

            List<string> args = new List<string>
            {
                type
            };

            return await rpc.CallApi("SearchType",args);
        }
    }
}
