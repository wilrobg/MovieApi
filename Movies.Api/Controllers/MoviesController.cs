using Microsoft.AspNetCore.Mvc;
using Movies.Application.Requests;
using Movies.Application.Responses;
using Movies.Application.Services;
using Movies.Core.Enums;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesServices _services;
        public MoviesController(IMoviesServices services)
        {
            _services = services;
        }

        [HttpGet("Categories")]
        public IEnumerable<CategoriesResponse> Get()
        {
            return _services.GetMovieCategories();

        }

        [HttpPost]
        public async Task<ActionResult> Post(AddMovieRequest request)
        {
            await _services.AddMovie(request);
            return Ok();
        }
    }
}
