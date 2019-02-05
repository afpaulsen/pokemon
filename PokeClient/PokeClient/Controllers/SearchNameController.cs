using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchTypeController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchNameController : Controller
    {
        [HttpGet]
        public string SearchName()
        {
            //This could also list all pokemon
            return "Please provide a name (case sensitive)";
        }

        [HttpGet("{name}")]
        public async Task<string> SearchName(string name)
        {
            Rpc rpc = new Rpc();

            List<string> args = new List<string>
            {
                name
            };

            return await rpc.CallApi("SearchName",args);
        }
    }
}
