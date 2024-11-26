using System;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specfications;

public class BaseSpecfication<T>(Expression<Func<T, bool>> criteria) : ISpecification<T>
{
    //A Null Ctor
    protected BaseSpecfication() : this(null){}

    // This is mean when we creating a new instance of this 
    public Expression<Func<T, bool>> Criteria => criteria;

    public Expression<Func<T, object>>? OrdetBy { get; private set; }

    public Expression<Func<T, object>>? OrdetByDescending { get; private set; }

    protected void AddOrderBy(Expression<Func<T, Object>> orderByExpression)
    {
        OrdetBy = orderByExpression;
    }

    protected void AddOrdetByDescending(Expression<Func<T, Object>> orderByDescExpression)
    {
        OrdetByDescending = orderByDescExpression;
    }

}

