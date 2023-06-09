﻿namespace Movies.Infrastructure.HttpContextAccessor;

public interface IUserHttpContextAccesor
{
    string Id { get; }
    string Email { get; }
    List<string> Roles { get; }
}
