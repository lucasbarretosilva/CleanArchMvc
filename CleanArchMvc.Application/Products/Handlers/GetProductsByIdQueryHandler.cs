using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Products.Handlers
{
    internal class GetProductsByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        IProductRepository _productRepository;

        public GetProductsByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if(product is null)
                throw new ApplicationException($"Error creating entity");

            return product;

        }
    }
}
