using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Commons.Repositories.Common;
using Movies.Core.Contexts;
using System.Linq.Expressions;

namespace Movies.Core.Repositories.Common;

public abstract class Repository<TDbSet, TPKey> : IRepository<TDbSet, TPKey> where TDbSet : class
{
    private readonly IMapper _mapper;
    protected readonly MoviesContext Context;
    protected virtual DbSet<TDbSet> DbSet { get; }

    public Repository(MoviesContext context, IMapper mapper)
    {
        Context = context;
        DbSet = context.Set<TDbSet>();
        _mapper = mapper;
    }

    public Task<List<TReturn>> GetAsync<TReturn>(Expression<Func<TDbSet, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync<TRequestToMap>(TRequestToMap requestEntity)
    {
        var entity = _mapper.Map<TDbSet>(requestEntity);
        Context.Set<TDbSet>().AddAsync(entity).AsTask();
        return Context.SaveChangesAsync();
    }

    public void Update(TDbSet entity)
    {
        Context.Set<TDbSet>().Update(entity);
    }

    public void RemoveAsync(TDbSet entity)
    {
        Context.Set<TDbSet>().Remove(entity);
    }
}
