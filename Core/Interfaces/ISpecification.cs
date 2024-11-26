using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{   
    Expression<Func<T,bool>>? Criteria {get;}
    Expression<Func<T, object>>? OrdetBy {get;}
    Expression<Func<T, object>>? OrdetByDescending {get;}
    
    bool IsDistinct { get; }
}

// Another Vesion on Specifiation to return a different results
public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T,TResult>>? Select { get; }
    
}
