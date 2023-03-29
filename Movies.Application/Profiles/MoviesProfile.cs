using AutoMapper;
using Movies.Application.Requests;
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
