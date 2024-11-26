using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{   
    Expression<Func<T,bool>>? Criteria {get;}
    Expression<Func<T, object>>? OrdetBy {get;}
    Expression<Func<T, object>>? OrdetByDescending {get;}
    
}
