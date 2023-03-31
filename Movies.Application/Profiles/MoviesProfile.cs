using AutoMapper;
using Movies.Application.Requests.Movies;
using Movies.Application.Responses.Movies;
using Movies.Core.Models;

namespace Movies.Application.Profiles;

public class MoviesProfile : Profile
{
	public MoviesProfile()
	{
        CreateProjection<Movie, MoviesResponse>()
            .ForMember(m => m.Rating, map => map.MapFrom(s => s.MovieRates.Sum(p => p.Rate)/ (double) s.MovieRates.Count()));

        CreateMap<AddMovieRequest, Movie>();

        CreateMap<UpdateMovieRequest, Movie>();
    }
}
