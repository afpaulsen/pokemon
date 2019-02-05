using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchTypeController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHeaderController : Controller
    {
        [HttpGet]
        public string SearchHeader()
        {
            //This could also list all headers
            return "Please provide both a header and a value";
        }

        [HttpGet("{header}/{value}")]
        public async Task<string> SearchHeader(string header, string value)
        {
            Rpc rpc = new Rpc();

            List<string> args = new List<string>
            {
                header,
                value
            };

            return await rpc.CallApi("SearchHeader", args);
        }
    }
}
