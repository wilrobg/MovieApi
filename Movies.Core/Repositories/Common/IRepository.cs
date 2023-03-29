﻿using System.Linq.Expressions;

namespace Movies.Application.Commons.Repositories.Common;

public interface IRepository<TDbSet, TPKey> where TDbSet : class
{
    ValueTask<TDbSet> GetByIdAsync(TPKey id);
    IQueryable<TReturn> GetAsync<TReturn>(Expression<Func<TDbSet, bool>> predicate);
    Task AddAsync<TRequestToMap>(TRequestToMap requestEntity);
    Task Update<TRequestToMap>(TDbSet entity, TRequestToMap requestEntity);
    Task RemoveAsync(TDbSet entity);
}