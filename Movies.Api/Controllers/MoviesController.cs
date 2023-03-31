using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Requests.Movies;
using Movies.Application.Responses.Movies;
using Movies.Application.Services.Movies;
using Movies.Core.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesServices _services;
        public MoviesController(
            IMoviesServices services)
        {
            _services = services;
        }

        [SwaggerOperation(Summary = "Get List of categories", Description = "Only for admin")]
        [HttpGet("Categories")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<CategoriesResponse> Get()
        {
            return _services.GetMovieCategories();

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add Movie endpoint", Description = "Only for admin, get categories id from Movies/Categories")]
        public async Task<ActionResult> Post(AddMovieRequest request)
        {
            await _services.AddMovie(request);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update Movie endpoint", Description = "Only for admin, get categories id from Movies/Categories")]
        public async Task<ActionResult> Put(UpdateMovieRequest request)
        {
            await _services.UpdateMovie(request);
            return Ok();
        }

        [HttpPut("Image")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update Movie Image endpoint", Description = "Only for admin")]
        public async Task<ActionResult> UpdateMovieImage([FromForm] UpdateMovieImageRequest request)
        {
            await _services.UpdateMovieImage(request);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete Movie endpoint", Description = "Only for admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _services.DeleteMovie(id);
            return NoContent();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get movies endpoint", Description = "Only for admin, get categories id from Movies/Categories, paginations options are setup by default. Movie rating is the averages of all users rates for movie.")]
        public async Task<PaginationResult<MoviesResponse>> GetMovies([FromQuery] MoviesRequest request)
        {
            return await _services.GetMovies(request);
        }
    }
}
