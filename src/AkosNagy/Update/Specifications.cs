//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace RepositoryPoC.AkosNagy.Update
//{
//    /* https://dotnetfalcon.com/using-the-specification-pattern-with-repository-and-unit-of-work/
//     * 
//     */
//    public class CheapProductSpecification : FilterSpecification<Product>
//    {
//        protected override Expression<Func<Product, bool>> SpecificationExpression => p => p.UnitPrice < 30;
//    }

//    public class DiscontinuedProductSpecification : FilterSpecification<Product>
//    {
//        protected override Expression<Func<Product, bool>> SpecificationExpression => p => p.Discontinued;
//    }
//}
