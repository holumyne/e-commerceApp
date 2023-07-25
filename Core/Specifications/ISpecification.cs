using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }

        //for sorting
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        //for pagination
        int Take { get; }  //this will enable us to take a certain amount of product
        int Skip { get; }  // this will enable us to skip some product
        bool IsPagingEnabled { get; }
    }

}
