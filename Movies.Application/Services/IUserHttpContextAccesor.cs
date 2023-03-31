namespace Movies.Api.HttpContextAccessor;

public interface IUserHttpContextAccesor
{
    string Id { get; }
    List<string> Roles { get; }
}
