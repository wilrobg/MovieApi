using AutoMapper;
using Movies.Application.Requests.Movies;
using Movies.Application.Responses.Movies;
using Movies.Core.Models;

namespace Movies.Application.Profiles;

public class MoviesProfile : Profile
{
	public MoviesProfile()
	{
        CreateProjection<Movie, MoviesResponse>();

        CreateMap<AddMovieRequest, Movie>();

        CreateMap<UpdateMovieRequest, Movie>();
    }
}
