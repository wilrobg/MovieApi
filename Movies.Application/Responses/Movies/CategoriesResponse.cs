using Movies.Core.Enums;

namespace Movies.Application.Responses.Movies
{
    public record CategoriesResponse(MovieCategory Id, string Name);
}
