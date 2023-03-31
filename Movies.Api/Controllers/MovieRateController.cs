using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Requests.Rates;
using Movies.Application.Responses.MovieRate;
using Movies.Application.Services.MovieRates;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieRateController : ControllerBase
{
    private readonly IMovieRateServices _movieRateServices;
    public MovieRateController(IMovieRateServices movieRateServices)
    {
        _movieRateServices = movieRateServices;
    }

    [SwaggerOperation(Summary = "Get rated movies by the user")]
    [HttpGet]
    [Authorize]
    public async Task<List<MovieRateResponse>> GetMoviesRate()
    {
        return await _movieRateServices.GetMoviesRate();
    }

    [SwaggerOperation(Summary = "Rate movie", Description = "This endpoint works for update or add a movie rate by user. Rate must be from 1 to 10.")]
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> RateMovie(RateMovieRequest request)
    {
        await _movieRateServices.RateMovie(request);
        return Ok();
    }


    [SwaggerOperation(Summary = "Delete movie rate", Description = "This endpoint delete user movie rate.")]
    [HttpDelete("{movieId:int}")]
    [Authorize]
    public async Task<ActionResult> RemoveRateMovie(int movieId)
    {
        await _movieRateServices.RemoveRateMovie(movieId);
        return NoContent();
    }
}
