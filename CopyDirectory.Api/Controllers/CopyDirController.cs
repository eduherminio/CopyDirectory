using CopyDirectoryLib.Model;
using CopyDirectoryLib.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CopyDirectory.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/copydir")]
    public class CopyDirController : Controller
    {
        private readonly ICopyService _copyService;

        public CopyDirController(ICopyService copyService)
        {
            _copyService = copyService;
        }

        [HttpGet]
        public async Task Copy([FromBody]Item item)
        {
            await _copyService.CopyDirectory(item);
        }
    }
}
