using Microsoft.AspNetCore.Mvc;
using Movies.Application.Requests.Movies;
using Movies.Application.Responses.Movies;
using Movies.Application.Services.Movies;
using Movies.Core.Common;

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

        [HttpPut]
        public async Task<ActionResult> Put(UpdateMovieRequest request)
        {
            await _services.UpdateMovie(request);
            return Ok();
        }

        [HttpPut("Image")]
        public async Task<ActionResult> UpdateMovieImage([FromForm] UpdateMovieImageRequest request)
        {
            await _services.UpdateMovieImage(request);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _services.DeleteMovie(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<PaginationResult<MoviesResponse>> GetMovies([FromQuery] MoviesRequest request)
        {
            return await _services.GetMovies(request);
        }
    }
}
