using System;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if(spec.Criteria != null)
        {
            query = query.Where(spec.Criteria); // x => x.Brand == brand
        }

        if (spec.OrdetBy != null)
        {
            query = query.OrderBy(spec.OrdetBy);
        }
        if (spec.OrdetByDescending != null)
        {
            query = query.OrderByDescending(spec.OrdetByDescending);
        }
        
        if (spec.IsDistinct)
        {
            query = query.Distinct();
        }

        return query;
    }
    // Overloading the GetQuery for another purpose and to return different results
        public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query,
             ISpecification<T, TResult> spec)
    {
        if(spec.Criteria != null)
        {
            query = query.Where(spec.Criteria); // x => x.Brand == brand
        }

        if (spec.OrdetBy != null)
        {
            query = query.OrderBy(spec.OrdetBy);
        }
        if (spec.OrdetByDescending != null)
        {
            query = query.OrderByDescending(spec.OrdetByDescending);
        }
        
        var selectQuery = query as IQueryable<TResult>;

        if(spec.Select != null)
        {
            selectQuery = query.Select(spec.Select);

        }
        if (spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }

        return selectQuery ?? query.Cast<TResult>();
    }

}
