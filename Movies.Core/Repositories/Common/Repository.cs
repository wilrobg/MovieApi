using AutoMapper;
using AutoMapper.QueryableExtensions;
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

    public Task AddAsync<TRequestToMap>(TRequestToMap requestEntity)
    {
        var entity = _mapper.Map<TDbSet>(requestEntity);
        Context.Set<TDbSet>().AddAsync(entity).AsTask();
        return Context.SaveChangesAsync();
    }

    public Task AddAsync(TDbSet entity)
    {
        Context.Set<TDbSet>().AddAsync(entity).AsTask();
        return Context.SaveChangesAsync();
    }

    public Task Update<TRequestToMap>(TDbSet entity, TRequestToMap requestEntity)
    {
        entity = _mapper.Map(requestEntity, entity);
        Context.Set<TDbSet>().Update(entity);
        return Context.SaveChangesAsync();
    }

    public Task Update(TDbSet entity)
    {
        Context.Set<TDbSet>().Update(entity);
        return Context.SaveChangesAsync();
    }

    public Task RemoveAsync(TDbSet entity)
    {
        Context.Set<TDbSet>().Remove(entity);
        return Context.SaveChangesAsync();
    }

    public ValueTask<TDbSet> GetByIdAsync(TPKey id)
    {
        return Context.Set<TDbSet>().FindAsync(id);
    }

    public virtual IQueryable<TReturn> GetAsync<TReturn>(Expression<Func<TDbSet, bool>> predicate)
    {
        return DbSet.Where(predicate).ProjectTo<TReturn>(_mapper.ConfigurationProvider);
    }

    public Task<TDbSet> FirstOrDefault(Expression<Func<TDbSet, bool>> predicate)
    {
        return DbSet.FirstOrDefaultAsync(predicate);
    }
}
