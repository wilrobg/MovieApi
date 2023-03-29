using Microsoft.AspNetCore.Mvc;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return Enumerable.Empty<object>();
        }
    }
}
