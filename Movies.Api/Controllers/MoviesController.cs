using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Requests.Movies;
using Movies.Application.Requests.Rates;
using Movies.Application.Responses.Movies;
using Movies.Application.Services.MovieRates;
using Movies.Application.Services.Movies;
using Movies.Core.Common;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesServices _services;
        private readonly IMovieRateServices _movieRateServices;
        public MoviesController(
            IMoviesServices services, 
            IMovieRateServices movieRateServices)
        {
            _services = services;
            _movieRateServices = movieRateServices;
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(UpdateMovieRequest request)
        {
            await _services.UpdateMovie(request);
            return Ok();
        }

        [HttpPut("Rate")]
        [Authorize]
        public async Task<ActionResult> RateMovie(RateMovieRequest request)
        {
            await _movieRateServices.RateMovie(request);
            return Ok();
        }

        [HttpPut("Image")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateMovieImage([FromForm] UpdateMovieImageRequest request)
        {
            await _services.UpdateMovieImage(request);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
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
