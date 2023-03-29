using Movies.Core.Enums;

namespace Movies.Application.Responses
{
    public record CategoriesResponse(MovieCategory Id, string Name);
}
