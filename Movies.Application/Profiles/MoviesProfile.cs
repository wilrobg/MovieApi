using AutoMapper;
using Movies.Application.Requests;
using Movies.Core.Models;

namespace Movies.Application.Profiles;

public class MoviesProfile : Profile
{
	public MoviesProfile()
	{
		CreateMap<AddMovieRequest, Movie>()
			.ForMember(m => m.Category, map => map.MapFrom(s => s.CategoryId));
	}
}
