
using MediatR;
using Ecommerce.Domain;
using Ecommerce.Application.Persistence;
using System.Linq.Expressions;

namespace Ecommerce.Application.Features.Products.Queries.GetProductList
{

    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductListQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>
            {
                p => p.Images!,
                p => p.Reviews!
            };

            Expression<Func<Product, bool>>? whereCondition = null;
            bool disableTracking = true;
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderByCondition = x => x.OrderBy(p => p.Name);

            var productList = await _unitOfWork.Repository<Product>().GetAsync(
                whereCondition,
                orderByCondition,
                includes,
                disableTracking
            );

            return (List<Product>)productList;
        }
    }

}
