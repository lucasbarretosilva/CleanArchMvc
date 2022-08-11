using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Products.Handlers
{
    internal class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        IProductRepository _productRepository;

        public ProductRemoveCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if(product is null)
                throw new ApplicationException($"Error creating entity");


            return await _productRepository.RemoveAsync(product);

        }
    }
}
