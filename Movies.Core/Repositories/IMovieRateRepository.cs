using Movies.Application.Commons.Repositories.Common;
using Movies.Core.Entities;
using Movies.Core.Models;

namespace Movies.Core.Repositories;

public interface IMovieRateRepository : IRepository<MovieRate, int>
{
}
