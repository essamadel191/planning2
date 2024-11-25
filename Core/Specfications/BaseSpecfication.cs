using System;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specfications;

public class BaseSpecfication<T>(Expression<Func<T, bool>> criteria) : ISpecfication<T>
{
    //A Null Ctor
    protected BaseSpecfication() : this(null){}

    // This is mean when we creating a new instance of this 
    public Expression<Func<T, bool>> Criteria => criteria;  
}

