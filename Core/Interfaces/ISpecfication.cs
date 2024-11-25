using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecfication<T>
{   
    Expression<Func<T,bool>> Criteria {get;}
}
