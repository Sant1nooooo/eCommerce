using FluentValidation;
using MediatR;
using Server.Application.Interfaces;
using static Server.Core.ResponseModels;
using Server.Application.Models;

namespace Server.Application.Command_Operations.Products
{
    public class CreateProduct_Command : IRequest<CreateProduct_Result>
    {
        public string? ProductName { get; set; }
        public string? ProductDetails { get; set; }
        public string? Type { get; set; }
        public string? ImageURL { get; set; }
        public int Price { get; set; }
    }

    public class CreateProduct_CommandHandler : IRequestHandler<CreateProduct_Command, CreateProduct_Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProduct_Command> _validator;

        public CreateProduct_CommandHandler(IProductRepository productRepository, IValidator<CreateProduct_Command> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<CreateProduct_Result> Handle(CreateProduct_Command request, CancellationToken ct)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                return new CreateProduct_Result()
                {
                    IsExisting = true,
                    Message = string.Join(", ", result.Errors.Select(e => e.ErrorMessage))
                };
            }

            Product product = new Product(request.ProductName!, request.ProductDetails!, request.Type!, request.ImageURL!, request.Price);
            await _productRepository.CreateProductAsync(product);
            return new CreateProduct_Result() { IsExisting = false, Message = $"Product({product.ProductName}) created successfully!" };
        }
    }
}
