using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Common;
using Movies.Core.Contexts;
using Movies.Core.Enums;
using Movies.Core.Models;
using Movies.Core.Repositories.Common;
using System.Linq.Expressions;

namespace Movies.Core.Repositories;

public class MoviesRepository : Repository<Movie, int>, IMoviesRepository
{
    private readonly MoviesContext _context;
    private readonly IMapper _mapper;
    public MoviesRepository(MoviesContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginationResult<TReturn>> GetAsync<TReturn>(
        PaginationRequest paginationRequest,
        string name,
        string synopsis,
        MovieCategory? categoryId,
        int? releaseYear,
        short? rating,
        string createdBy,
        string orderBy,
        string orderByDesc)
    {
        var predicate = PredicateBuilder.New<Movie>(true);

        if (!string.IsNullOrEmpty(name))
            predicate.And(x => EF.Functions.ILike(x.Name, $"%{name}%"));
        
        if (!string.IsNullOrEmpty(synopsis))
            predicate.And(x => EF.Functions.ILike(x.Synopsis, $"%{synopsis}%"));

        if (categoryId is not null)
            predicate.And(x => x.CategoryId == categoryId);

        if (releaseYear is not null)
            predicate.And(x => x.ReleaseYear == releaseYear);

        if (rating is not null)
            predicate.And(x =>
                Math.Floor(x.MovieRates.Average(x => x.Rate)) == rating);

        if (!string.IsNullOrEmpty(createdBy))
            predicate.And(x => x.CreatedBy == createdBy);

        var query = _context.Movies.Where(predicate);

        var list = await query.ToListAsync();

        string[] orderByArray = orderBy?.Split(',') ?? Array.Empty<string>();
        orderByArray.ForEach(o =>
        {
            query = o switch
            {
                "year" => query.OrderBy(x => x.ReleaseYear),
                "name" => query.OrderBy(x => x.Name),
                "createdDate" => query.OrderBy(x => x.CreatedDate),
                "rating" => query.OrderBy(x => x.MovieRates.Average(x => x.Rate)),
                _ => query
            };
        });


        string[] orderByDescArray = orderByDesc?.Split(',') ?? Array.Empty<string>();
        orderByDescArray.ForEach(o =>
        {
            query = o switch
            {
                "year" => query.OrderByDescending(x => x.ReleaseYear),
                "name" => query.OrderByDescending(x => x.Name),
                "createdDate" => query.OrderByDescending(x => x.CreatedDate),
                "rating" => query.OrderByDescending(x => x.MovieRates.Average(x => x.Rate)),
                _ => query
            };
        });

        var data = await query
        .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
        .Take(paginationRequest.PageSize)
        .ProjectTo<TReturn>(_mapper.ConfigurationProvider)
        .ToListAsync();

        var totalRecords = await query.CountAsync();

        return PaginationResult<TReturn>.GetPagination(data, paginationRequest.PageNumber, paginationRequest.PageSize, totalRecords);
    }
}
