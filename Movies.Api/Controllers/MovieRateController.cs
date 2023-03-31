using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Requests.Rates;
using Movies.Application.Responses.MovieRate;
using Movies.Application.Services.MovieRates;

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

    [HttpGet]
    [Authorize]
    public async Task<List<MovieRateResponse>> GetMoviesRate()
    {
        return await _movieRateServices.GetMoviesRate();
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> RateMovie(RateMovieRequest request)
    {
        await _movieRateServices.RateMovie(request);
        return Ok();
    }

    [HttpDelete("{movieId:int}")]
    [Authorize]
    public async Task<ActionResult> RemoveRateMovie(int movieId)
    {
        await _movieRateServices.RemoveRateMovie(movieId);
        return NoContent();
    }
}
