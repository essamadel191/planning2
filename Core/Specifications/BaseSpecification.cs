using System;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>> criteria) : ISpecification<T>
{
    //A Null Ctor
    protected BaseSpecification() : this(null!){}

    // This is mean when we creating a new instance of this 
    public Expression<Func<T, bool>> Criteria => criteria;

    public Expression<Func<T, object>>? OrdetBy { get; private set; }

    public Expression<Func<T, object>>? OrdetByDescending { get; private set; }

    public bool IsDistinct { get; private set; }

    protected void AddOrderBy(Expression<Func<T, Object>> orderByExpression)
    {
        OrdetBy = orderByExpression;
    }

    protected void AddOrdetByDescending(Expression<Func<T, Object>> orderByDescExpression)
    {
        OrdetByDescending = orderByDescExpression;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }
}

// Implementing another base specification that takes two parameters
public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria)
    : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null!){}
    public Expression<Func<T, TResult>>? Select { get; private set;}

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}



