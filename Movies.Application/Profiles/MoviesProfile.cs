using AutoMapper;
using Movies.Application.Requests.Movies;
using Movies.Application.Responses.Movies;
using Movies.Core.Models;
using System.Linq;

namespace Movies.Application.Profiles;

public class MoviesProfile : Profile
{
	public MoviesProfile()
	{
        CreateProjection<Movie, MoviesResponse>()
            .ForMember(m => m.MovieRateSum, map => map.MapFrom(x => (short) x.MovieRates.Sum(x => x.Rate)))
            .ForMember(m => m.MovieRateCount, map => map.MapFrom(x => x.MovieRates.Count()))
            ;

        CreateMap<AddMovieRequest, Movie>();

        CreateMap<UpdateMovieRequest, Movie>();
    }
}
