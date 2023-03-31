using AutoMapper;
using Movies.Application.Responses.MovieRate;
using Movies.Core.Entities;

namespace Movies.Application.Profiles;

public class MovieRateProfile : Profile
{
	public MovieRateProfile()
	{
		CreateProjection<MovieRate, MovieRateResponse>();
	}
}
